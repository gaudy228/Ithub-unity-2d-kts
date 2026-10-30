using UnityEngine;

public class AdTeste : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 300));
        if (GUILayout.Button("Load Rewarded")) AdService.Instance.LoadRewarded();
        if (GUILayout.Button("Show Rewarded")) AdService.Instance.ShowRewarded();
        GUILayout.EndArea();
    }
}