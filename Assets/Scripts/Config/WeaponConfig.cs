
[System.Serializable]
public class WeaponConfig
{
    public string id;
    public float damage;
    public float cooldown;

    public bool IsValid(out string error)
    {
        if (string.IsNullOrWhiteSpace(id)) 
        {
            error = "id пустой"; return false;
        }

        if (damage < 0) 
        { 
            error = $"damage={damage} < 0";
            return false;
        }

        if (cooldown <= 0)
        { error = $"cooldown={cooldown} <= 0";
            return false;
        }

        error = null;
        return true;
    }
}
