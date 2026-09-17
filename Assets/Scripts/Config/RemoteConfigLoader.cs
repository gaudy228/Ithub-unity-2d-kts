using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class RemoteConfigLoader
{
    private const string Url =
        "https://docs.google.com/spreadsheets/d/e/2PACX-1vRCW-MCGdqiKDn2r-U6FKE2hdaxXRSaMT-2k4Tv3L46ijYTpa3OJrThqDMvAm_5Y7QNH9zkFEOyoJ16/pub?output=csv";

    private const string CacheFileName = "weapon_config_cache.csv";

    private static string CachePath =>
        Path.Combine(Application.persistentDataPath, CacheFileName);

    private async Task<string> DownloadAsync(string url)
    {
        using var req = UnityWebRequest.Get(url);
        req.timeout = 10;

        var op = req.SendWebRequest();
        while (!op.isDone) await Task.Yield();

        if (req.result != UnityWebRequest.Result.Success)
        {
            throw new Exception($"HTTP {req.responseCode}: {req.error}");
        }

        return req.downloadHandler.text;
    }

    private List<WeaponConfig> ParseCsv(string raw)
    {
        var result = new List<WeaponConfig>();

        raw = raw.TrimStart('\uFEFF');
        var lines = raw.Split(new[] { '\n', '\r' },
                              StringSplitOptions.RemoveEmptyEntries);

        char separator = lines[0].Contains(';') ? ';' : ',';

        for (int i = 1; i < lines.Length; i++)
        {
            var cells = lines[i].Split(separator);
            if (cells.Length < 3) continue;

            try
            {
                result.Add(new WeaponConfig
                {
                    id = cells[0].Trim(),
                    damage = float.Parse(cells[1].Trim(), CultureInfo.InvariantCulture),
                    cooldown = float.Parse(cells[2].Trim(), CultureInfo.InvariantCulture)
                });
            }
            catch (FormatException)
            {
                Debug.LogWarning($"Строка {i} не распарсилась: {lines[i]}");
            }
        }

        return result;
    }

    private List<WeaponConfig> Validate(List<WeaponConfig> raw, out List<string> errors)
    {
        errors = new List<string>();
        var valid = new List<WeaponConfig>();

        foreach (var cfg in raw)
        {
            if (cfg.IsValid(out var err))
            {
                valid.Add(cfg);
            }
            else
            {
                errors.Add($"{cfg.id}: {err}");
            }
        }
        return valid;
    }

    private void SaveToCache(string raw)
    {
        try
        {
            File.WriteAllText(CachePath, raw, Encoding.UTF8);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Не удалось сохранить кэш: {e.Message}");
        }
    }

    private string LoadFromCache()
    {
        if (!File.Exists(CachePath))
        {
            return null;
        }
        try
        {
            return File.ReadAllText(CachePath, Encoding.UTF8);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Кэш повреждён: {e.Message}");
            return null;
        }
    }

    public async Task<List<Weapon>> LoadAsync()
    {
        string raw = null;

        try
        {
            raw = await DownloadAsync(Url);
            SaveToCache(raw);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Сеть недоступна {e.Message}");
            raw = LoadFromCache();

            if (raw == null)
            {
                Debug.LogError("дефолтные значения");
                return BuildFromDefaults();
            }
        }

        List<WeaponConfig> parsed;
        try
        {
            parsed = ParseCsv(raw);
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка парсинга {e.Message}");
            return BuildFromDefaults();
        }

        var valid = Validate(parsed, out var errors);
        foreach (var err in errors)
        {
            Debug.LogWarning($"Отклонена запись {err}");
        }

        if (valid.Count == 0)
        {
            return BuildFromDefaults();
        }

        var weapons = valid.Select(c => new Weapon(c)).ToList();
        weapons.ForEach(w => w.DebugPrint());
        return weapons;
    }

    private List<Weapon> BuildFromDefaults()
    {
        var weapons = ConfigDefaults.Weapons.Select(c => new Weapon(c)).ToList();
        weapons.ForEach(w => w.DebugPrint());
        return weapons;
    }
}