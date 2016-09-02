using UnityEngine;
using System.Collections;

public class DemoBehaviour : MonoBehaviour {

    public OutlineSystem outlineSystem;
    public GameObject targetGameobject;
	
	void Update () {
        //Rotate object
        targetGameobject.transform.Rotate(0f, Time.deltaTime * 30f, 0f);
        //Bob object
        targetGameobject.transform.position = new Vector3(0f, Mathf.Sin(Time.time) * 0.15f);
    }

    public void ChangeOutlineColourR(float val)
    {
        outlineSystem.outlineColor.r = val;
    }

    public void ChangeOutlineColourG(float val)
    {
        outlineSystem.outlineColor.g = val;
    }

    public void ChangeOutlineColourB(float val)
    {
        outlineSystem.outlineColor.b = val;
    }

    public void ChangeOutlineSize(float val)
    {
        outlineSystem.outlineSize = val;
    }

    public void ToggleMode(bool solid)
    {
        outlineSystem.solidOutline = solid;
    }
}
