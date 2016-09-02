/*
 * The script is based on
 * Script made by: Mathijs van Sambeek
 * https://www.youtube.com/watch?v=VzGSxOktF4k
 * 
 * 
 * For the purpose of optimizing the process, the script turns of after taking a screenshot.
*/

using UnityEngine;
using System.Collections;

public class TakeScreenshotScript : MonoBehaviour {

    static public bool shouldTakeShot = false;

    public int resolution = 3; // 1= default, 2= 2x default, etc.
    public string imageName = "Screenshot_";
    public string customPath = "C:/Users/default/Desktop/UnityScreenshots/"; // leave blank for project file location
    public bool resetIndex = false;

    private int index = 0;


    void Awake()
    {
        if (resetIndex) PlayerPrefs.SetInt("ScreenshotIndex", 0);
        if (customPath != "")
        {
            if (!System.IO.Directory.Exists(customPath))
            {
                System.IO.Directory.CreateDirectory(customPath);
            }
        }
        index = PlayerPrefs.GetInt("ScreenshotIndex") != 0 ? PlayerPrefs.GetInt("ScreenshotIndex") : 1;
        this.enabled = false;

    }

    void LateUpdate()
    {
        if (shouldTakeShot)
        {
            Application.CaptureScreenshot(customPath + imageName + index + ".png", resolution);
            index++;
            Debug.LogWarning("Screenshot saved: " + customPath + " --- " + imageName + index);
            shouldTakeShot = false;
            this.enabled = false;
        }
    }
}
