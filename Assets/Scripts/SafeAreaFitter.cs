using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaFitter : MonoBehaviour
{
    private RectTransform panel;
    private Rect lastSafeArea;
    private Vector2Int lastScreenSize;
    private ScreenOrientation lastOrientation;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    private void Update()
    {
        bool changed = lastSafeArea != Screen.safeArea ||
                       lastScreenSize.x != Screen.width ||
                       lastScreenSize.y != Screen.height ||
                       lastOrientation != Screen.orientation;

        if (changed)
        {
            ApplySafeArea();
        }
    }

    private void ApplySafeArea()
    {
        Rect area = Screen.safeArea;

        Vector2 anchorMin = area.position;
        Vector2 anchorMax = area.position + area.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;

        lastSafeArea = area;
        lastScreenSize = new Vector2Int(Screen.width, Screen.height);
        lastOrientation = Screen.orientation;

        Debug.Log($"Safe Area updated: {Screen.safeArea}, Screen Size: {Screen.width}x{Screen.height}, Orientation: {Screen.orientation}");
    }
}
