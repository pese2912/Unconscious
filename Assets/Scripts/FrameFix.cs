using UnityEngine;
using System.Collections;

public class FrameFix : MonoBehaviour {
    float deltaTime = 0.0f;
    public int frame=60;
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }
    void Awake()
    {
       // Time.captureFramerate = frame;
    }



    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(20, 20, 200, 200);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.fontSize = 30;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
