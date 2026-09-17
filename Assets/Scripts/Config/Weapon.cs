using UnityEngine;

public class Weapon
{
    public string Id;
    public float Damage;
    public float Cooldown;

    public Weapon(WeaponConfig cfg)
    {
        Id = cfg.id;
        Damage = cfg.damage;
        Cooldown = cfg.cooldown;
    }

    public void DebugPrint()
    {
        Debug.Log($"[Weapon] id={Id} damage={Damage} cooldown={Cooldown}");
    }
}