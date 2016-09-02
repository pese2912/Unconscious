/*
 * Script written by: Sara Magdalena Jujeczka
 * https://twitter.com/sara_jujeczka
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
    using UnityEditor;
#endif

/*
 * A class containing the swiching sprites for the Camera's UI.
*/
[System.Serializable]
public class PhoneUI {


    public Material cellphone;
    public Material cellphoneFlashlight;
    public Material cellphoneCamera;
    public Material cellphoneFLoff;
    public GameObject cameraUI;
    public Image cameraImg;
    public Image cameraClickedImg;
    public Image flashButtonOnImg;
    public Image flashButtonOffImg;
    public Image flashButtonAutoImg;
    public Image flashButtonOutImg;
    public Image FlButtonOnImg;
    public Image FlButtonOffImg;
    public Slider zoomSlider;
    public GameObject flashlightUI;
}


public class ArmScript : MonoBehaviour
{


    //Static variables

    //Determines if the flashlight is out
    static bool isFlashOut = false;

    //Determines if the camera is out
    static bool isCameraOut = false;

    //Block the input while the animation is still running
    static bool canPress = true;

    //Determines if a photo will be taken with or without the flash 
    static bool photoWithFL = true;

    /* These two variables determine which image should be displayed as "Flash Options"
     * sach as:
     * "Auto Flash" when flashOn = false and flashAuto = true
     * "Flash On" when flashOn = true and flashAuto = true
     * "Flash Off" when flashOn = true and flashAuto = false
     * "Disabled Flash" when flashOn = false and flashAuto = false
    */
    static bool flashOn = true;
    static bool flashAuto = true;

    //Public variables


    public PhoneUI instanceUI;
    //Variables used to store different values of the Flash light which can be changed via the inspector panel.
    public float flashlightRange = 180;
    public float flashlightSpotAngle = 180;
    public float flashlightIntensity = 1.6f;
    public GameObject smartPhone;

    //Private variables
    [SerializeField]
    private PlayerAnimState state; // 현재 플레이어 애니메이션 상태

    Camera playerCamera;
    GameObject smartPhoneFlashlight;
    Animator anim;
    float fov;

    //Variables used to store different values of the Flashlight light.
    float lightRange;
    float lightSpotAngle;
    float lightIntensity;

    void Awake()
    {
        if(!Checking())
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        playerCamera = Camera.main.GetComponent<Camera>();
        smartPhoneFlashlight = GameObject.FindGameObjectWithTag("CameraFlashlight");
        fov = playerCamera.fieldOfView;
        anim = GetComponent<Animator>();
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphone;
        smartPhoneFlashlight.SetActive(false);
        //The function which disables all the images of the UI (when the camera is not used)
        DisactiveImages();
        //The function which disables all the children of the Arm object (when the phone is not used)
        DisActivateKids(true);
        lightRange = instanceUI.flashlightUI.GetComponent<Light>().range;
        lightSpotAngle = instanceUI.flashlightUI.GetComponent<Light>().spotAngle;
        lightIntensity = instanceUI.flashlightUI.GetComponent<Light>().intensity;
    }


    void Update()
    {
        if (canPress) { 
            //Input "R1"
            if (Input.GetButtonDown("R1")) 
            {
                if (!isFlashOut && !isCameraOut)
                {
                   
                    state.TState = PlayerAnimState.ActionState.PhoneUpLightOn;
                    StartCoroutine("StartPlayingFlashlight");
                }
                else if (isFlashOut && !isCameraOut)
                {
                    state.TState = PlayerAnimState.ActionState.Idle;
                    StartCoroutine("StopPlayingFlashlight");
                }
                else if (isFlashOut && isCameraOut)
                {
                    state.TState = PlayerAnimState.ActionState.PhotoModeLightOff;
                    smartPhoneFlashlight.SetActive(false);
                    instanceUI.flashlightUI.SetActive(false);
                    instanceUI.FlButtonOffImg.enabled = true;
                    instanceUI.FlButtonOnImg.enabled = false;
                    flashAuto = false;
                    flashOn = true;
                    FlashFunction();
                    isFlashOut = false;
                }
                else if (!isFlashOut && isCameraOut)
                {
                    state.TState = PlayerAnimState.ActionState.PhotoModeLightOn;
                    smartPhoneFlashlight.SetActive(false);
                    instanceUI.flashlightUI.SetActive(true);
                    instanceUI.FlButtonOnImg.enabled = true;
                    instanceUI.FlButtonOffImg.enabled = false;
                    flashAuto = false;
                    flashOn = false;
                    FlashFunction();
                    isFlashOut = true;
                }
            }

            //Input "A"
            if (Input.GetButtonDown("A"))
            {
                if (!isCameraOut)
                {
                    if (isFlashOut)
                    {
                        state.TState = PlayerAnimState.ActionState.PhotoModeLightOn;
                        anim.SetBool("CameraOut", true);
                        DisActivateKids(false);
                        StartCoroutine("StartPlayingCameraAndFlash");
                        isCameraOut = true;
                    }
                    else
                    {
                        state.TState = PlayerAnimState.ActionState.PhotoModeLightOff;
                        DisActivateKids(false);
                        StartCoroutine("StartPlayingCamera");
                        anim.SetBool("CameraOut", true);
                        isCameraOut = true;
                    }
                }

                else if(isCameraOut && !isFlashOut)
                {
                
                    state.TState = PlayerAnimState.ActionState.Idle;
                    StartCoroutine("StopPlayingCamera");
                }

                else if (isCameraOut && isFlashOut)
                {
                    state.TState = PlayerAnimState.ActionState.PhoneUpLightOn;
                    StartCoroutine("StopPlayingCameraStartFlashlight");
                }
            }
            if (isCameraOut)
            {
                //Input Left Mouse Button
                if (Input.GetButtonDown("B"))
                {
                    state.TState = PlayerAnimState.ActionState.PhotoShot;
                    StartCoroutine("TakeScreenShot");
                }
                //Input Right Mouse Button
                if (Input.GetButtonDown("Fire2"))
                {
                    if (flashAuto && flashOn)
                    {
                        smartPhoneFlashlight.SetActive(false);
                        instanceUI.flashlightUI.SetActive(false);
                        instanceUI.FlButtonOffImg.enabled = true;
                        instanceUI.FlButtonOnImg.enabled = false;
                        isFlashOut = false;
                    }
                    FlashFunction(); 
                }

                if (Input.GetAxis("Oculus_GearVR_RThumbstickY") != 0)
                {
                    float inputScroll = Input.GetAxis("Oculus_GearVR_RThumbstickY");
                    instanceUI.zoomSlider.value -= inputScroll * 5;
                    playerCamera.fieldOfView = instanceUI.zoomSlider.value;
                }
            }
        }
    }

    /* 
     * The following function turns off the Camera UI while the screenshot is taken.
     * It turns on TakeScreenshotScript.
     * Turns on the flash during the process depends on the variable photoWithFL.
    */
    IEnumerator TakeScreenShot()
    {
        instanceUI.cameraClickedImg.enabled = true;
        if (photoWithFL)
        {
            instanceUI.flashlightUI.SetActive(true);
            instanceUI.flashlightUI.GetComponent<Light>().intensity = flashlightIntensity;
            instanceUI.flashlightUI.GetComponent<Light>().range = flashlightRange;
            instanceUI.flashlightUI.GetComponent<Light>().spotAngle = flashlightSpotAngle;
        }
        yield return null;
        instanceUI.cameraUI.SetActive(false);
        yield return null;
       // GetComponent<TakeScreenshotScript>().enabled = true;
      //  TakeScreenshotScript.shouldTakeShot = true;
        yield return null;
        instanceUI.cameraUI.SetActive(true);
        instanceUI.cameraImg.enabled = true;
        instanceUI.cameraClickedImg.enabled = false;
        instanceUI.cameraImg.enabled = true;
        if (photoWithFL)
        {
            instanceUI.flashlightUI.GetComponent<Light>().intensity = lightIntensity;
            instanceUI.flashlightUI.GetComponent<Light>().range = lightRange;
            instanceUI.flashlightUI.GetComponent<Light>().spotAngle = lightSpotAngle;
            instanceUI.flashlightUI.SetActive(false);
        }
        yield return null;
    }

    /* 
     * The following function is called when the player wants to turn on the camera 
     * (with the flashlight off). It activates the Camera UI.
    */
    IEnumerator StartPlayingCamera()
    {
        canPress = false;
        yield return new WaitForSeconds(0.5f);
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphoneCamera;
        yield return new WaitForSeconds(0.5f);
        DisActivateKids(true);
        instanceUI.cameraUI.SetActive(true);
        instanceUI.cameraImg.enabled = true;
        FlashFunction();
        instanceUI.FlButtonOffImg.enabled = true;
        smartPhoneFlashlight.SetActive(false);
        yield return null;
        canPress = true;
        yield return null;
    }

    /* 
     * The following function is called when the player wants to turn off the camera.
     * It disactivates the build in camera's flashlight.
    */
    IEnumerator StopPlayingCamera()
    {
        canPress = false;
        DisActivateKids(false);
        instanceUI.flashlightUI.SetActive(false);
        anim.SetBool("FlashOut", false);
        anim.SetBool("CameraOut", false);
        playerCamera.fieldOfView = fov;
        instanceUI.cameraUI.SetActive(false);
        isCameraOut = false;
        yield return new WaitForSeconds(0.867f);
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphone;
        yield return new WaitForSeconds(0.133f);
        DisActivateKids(true);
        yield return null;
        canPress = true;
        yield return null;
    }

    /*
     * The following function is called when the player wants to turn off the camera, but 
     * the flashlight is still on.
    */
    IEnumerator StopPlayingCameraStartFlashlight()
    {
        canPress = false;
        DisActivateKids(false);
        instanceUI.flashlightUI.SetActive(false);
        smartPhoneFlashlight.SetActive(true);
        anim.SetBool("FlashOut", true);
        anim.SetBool("CameraOut", false);
        playerCamera.fieldOfView = fov;
        instanceUI.cameraUI.SetActive(false);
        isCameraOut = false;
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphoneCamera;
        yield return new WaitForSeconds(1.634f);
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphoneFlashlight;
        yield return new WaitForSeconds(0.033f);
        canPress = true;
        yield return null;
    }

    /*
     * The following function is called when the player wants to turn on the camera, and 
     * the flashlight is still on.
    */
    IEnumerator StartPlayingCameraAndFlash()
    {
        canPress = false;
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphoneFlashlight;
        yield return new WaitForSeconds(0.4f);
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphone;
        yield return new WaitForSeconds(0.8f);
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphoneCamera;
        yield return new WaitForSeconds(0.467f);
        DisActivateKids(true);
        instanceUI.cameraUI.SetActive(true);
        instanceUI.cameraImg.enabled = true;
        flashAuto = false;
        flashOn = false;
        FlashFunction();
        instanceUI.flashlightUI.SetActive(true);
        instanceUI.FlButtonOnImg.enabled = true;
        yield return null;
        canPress = true;
        yield return null;
    }

    /*
     * The following function is called when the player wants to turn the flashlight.
    */
    IEnumerator StartPlayingFlashlight()
    {
        canPress = false;
        anim.SetBool("FlashOut", true);
        DisActivateKids(false);
        isFlashOut = true;
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphoneFLoff;
        yield return new WaitForSeconds(1.877f);
        smartPhoneFlashlight.SetActive(true);
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphoneFlashlight;
        yield return null;
        canPress = true;
        yield return null;
    }

    /*
     * The following function is called when the player wants to turn off the flashlight.
    */
    IEnumerator StopPlayingFlashlight()
    {
        canPress = false;
        anim.SetBool("FlashOut", false);
        isFlashOut = false;
        yield return new WaitForSeconds(0.807f);
        smartPhoneFlashlight.SetActive(false);
        smartPhone.GetComponent<Renderer>().material = instanceUI.cellphone;
        yield return new WaitForSeconds(0.87f);
        DisActivateKids(true);
        yield return null;
        canPress = true;
        yield return null;
    }

    /*
     * The following function is called when there is a need to turn off the camera's UI.
    */
    void DisactiveImages()
    {
        instanceUI.cameraImg.enabled = false;
        instanceUI.cameraClickedImg.enabled = false;
        instanceUI.flashButtonOnImg.enabled = false;
        instanceUI.flashButtonOffImg.enabled = false;
        instanceUI.flashButtonAutoImg.enabled = false;
        instanceUI.flashButtonOutImg.enabled = false;
        instanceUI.FlButtonOnImg.enabled = false;
        instanceUI.FlButtonOffImg.enabled = false;
    }

    /*
     * The following function is called to determine which image of the flash functionality is displayed,
     * and which one is next.
    */
    void FlashFunction()
    {
        if (flashAuto && flashOn)
        {
            photoWithFL = true;
            instanceUI.flashButtonOnImg.enabled = true;
            instanceUI.flashButtonOffImg.enabled = false;
            instanceUI.flashButtonAutoImg.enabled = false;
            instanceUI.flashButtonOutImg.enabled = false;
            flashAuto = false;
            flashOn = true;
        }
        else if (!flashAuto && flashOn)
        {
            photoWithFL = false;
            instanceUI.flashButtonOnImg.enabled = false;
            instanceUI.flashButtonOffImg.enabled = true;
            instanceUI.flashButtonAutoImg.enabled = false;
            instanceUI.flashButtonOutImg.enabled = false;
            flashAuto = true;
            flashOn = false;
        }
        else if (flashAuto && !flashOn)
        {
            photoWithFL = true;
            instanceUI.flashButtonOnImg.enabled = false;
            instanceUI.flashButtonOffImg.enabled = false;
            instanceUI.flashButtonAutoImg.enabled = true;
            instanceUI.flashButtonOutImg.enabled = false;
            flashAuto = true;
            flashOn = true;
        }
        else
        {
            photoWithFL = false;
            instanceUI.flashButtonOnImg.enabled = false;
            instanceUI.flashButtonOffImg.enabled = false;
            instanceUI.flashButtonAutoImg.enabled = false;
            instanceUI.flashButtonOutImg.enabled = true;
            flashAuto = true;
            flashOn = true;
        }
    }

    /*
     * The following function is called when the arm should be activated or disactivated.
    */
    void DisActivateKids(bool yesOrNo)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child != null)
            {
                child.SetActive(!yesOrNo);
            }
        }
    }

    /*
     * The following function is called for checking if every variable in the instance of PhoneUI class was filled.
    */
    bool Checking() {
        if (instanceUI.cellphone == null) { 
            Debug.Log("The cellphone material is empty");
            return false;
        }
        if (instanceUI.cellphoneFlashlight == null) { 
            Debug.Log("The cellphone's flashlight material is empty");
            return false;
        }
        if (instanceUI.cellphoneCamera == null) { 
            Debug.Log("The cellphone's camera material is empty");
            return false;
        }
        if (instanceUI.cellphoneFLoff == null) { 
            Debug.Log("The cellphone's flashlight off material is empty");
            return false;
        }
        if (instanceUI.cameraUI == null) { 
            Debug.Log("The cellphone's UI gameobject is empty");
            return false;
        }
        if (instanceUI.cameraImg == null) { 
            Debug.Log("The cellphone's unclicked image is empty");
            return false;
        }
        if (instanceUI.cameraClickedImg == null) { 
            Debug.Log("The cellphone's clicked image is empty");
            return false;
        }
        if (instanceUI.flashButtonOnImg == null) { 
            Debug.Log("The flash on option image is empty");
            return false;
        }
        if (instanceUI.flashButtonOffImg == null) { 
            Debug.Log("The flash off option image is empty");
            return false;
        }
        if (instanceUI.flashButtonAutoImg == null) { 
            Debug.Log("The flash auto option image is empty");
            return false;
        }
        if (instanceUI.flashButtonOutImg == null) { 
            Debug.Log("The flash out option image is empty");
            return false;
        }
        if (instanceUI.FlButtonOnImg == null) { 
            Debug.Log("The flashlight on image is empty");
            return false;
        }
        if (instanceUI.FlButtonOffImg == null) { 
            Debug.Log("The flashlight off image is empty");
            return false;
        }
        if (instanceUI.zoomSlider == null) { 
            Debug.Log("The Zoom slider is empty");
            return false;
        }
        if(instanceUI.flashlightUI == null) { 
            Debug.Log("The flashlight gameobject is empty");
            return false;
        }
        else
            return true;
    }
}



