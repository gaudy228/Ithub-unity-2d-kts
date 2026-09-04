
public class EnemyDeath : CreatureDeath
{
    public override void Death()
    {
        Destroy(gameObject);
    }
}
