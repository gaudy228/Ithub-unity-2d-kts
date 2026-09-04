using UnityEngine.SceneManagement;

public class PlayerDeath : CreatureDeath
{
    public override void Death()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
