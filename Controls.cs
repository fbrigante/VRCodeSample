using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    private XRIDefaultInputActions Controllers;

    //public TextMesh LeftController;
    //public TextMesh RightController;
    public GameObject droneCamera;

    public float droneYaw;
    public float droneVertY;
    public float droneFordX;
    public float droneHortX;

    
    void Start()
    {
        Controllers = new XRIDefaultInputActions();
        Controllers.Enable();
        //Controllers.XRILeftHand.TestAction.performed+=test=>Test();
    }

    void Update()
    {
        Vector2 joyStickDataL = Controllers.XRILeftHand.Move.ReadValue<Vector2>();
        //LeftController.text = joyStickDataL.ToString();
        //Controllers.XRILeftHand.TestAction.performed+=test=>Test();
        //Debug.Log(Controllers.XRILeftHand.TestAction.enabled);
        
        Vector2 joyStickDataR = Controllers.XRIRightHand.Move.ReadValue<Vector2>();
        //RightController.text = "Right" + joyStickDataR;

        droneHortX = joyStickDataR.x;
        droneFordX = joyStickDataR.y;
        droneYaw = joyStickDataL.x;
        droneVertY = joyStickDataL.y;

        float downCamera = Controllers.XRILeftHand.Select.ReadValue<float>();
        if(downCamera > 0)
        {
            droneCamera.transform.Rotate(10 * Time.deltaTime, 0, 0);
        }
        float upCamera = Controllers.XRIRightHand.Select.ReadValue<float>();
        if(upCamera > 0)
        {
            droneCamera.transform.Rotate(-10 * Time.deltaTime, 0, 0);
        }
        //Debug.Log(downCamera);
        //bool upCamera = Controllers.XRILeftHand.Select.ReadValue<bool>();


    }

    /*void Test()
    {
        Debug.Log("Button Pressed");
    }*/
}
