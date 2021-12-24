using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterController : MonoBehaviour
{
    public float Xspeed = 10;
    private CharacterController controller;

    private Controls controlInput;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        controlInput = GetComponent<Controls>();
    }

    void Update()
    {
        
        //drone yaw, rotate Y axis
        controller.transform.Rotate(0, 0.05f * Xspeed * controlInput.droneYaw, 0);
        //drone vertical position, Y axis
        controller.Move(Xspeed * Time.deltaTime * controlInput.droneVertY * transform.up);
        //drone forward and back, local Z axis
        controller.Move(Xspeed * Time.deltaTime * controlInput.droneFordX * transform.forward);
        //drone left and right, local X axis
        controller.Move(Xspeed * Time.deltaTime * controlInput.droneHortX * transform.right);

    }

}
