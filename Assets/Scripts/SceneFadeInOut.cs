using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

    public float fadeSpeed = 1.5f;
    public int count;

    private bool sceneStarting = false;
    private int _count;
    public GUITexture guiTexture;
    public int flag = 0;
    void Awake()
    {
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        _count = count;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (_count != 0)
        {
            if (sceneStarting)
                OpenEyes();
            else
                CloseEyes();

        }

        else if(_count == 0) //눈을 전부 다 깜박인 후
        {
            OpenEyes();
           
           
        }
        else
        {
            if (sceneStarting)
                OpenEyes();
            else
                CloseEyes();
        }

            
     //   print(_count);
        
	}



    public void FadeToClear()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    public void FadeToBlack()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
        
    }

    void OpenEyes()
    {
        FadeToClear();

        if(guiTexture.color.a <= 0.05f)
        {
            guiTexture.color = Color.clear;
            guiTexture.enabled = false;
            sceneStarting = false;
            
        }
    }

    public void CloseEyes()
    {
        guiTexture.enabled = true;
        FadeToBlack();
        if(guiTexture.color.a > 0.5f)
        {
            guiTexture.color = Color.black;
            sceneStarting = true;
            _count--;
        }
    }
}
