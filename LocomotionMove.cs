using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionMove : MonoBehaviour
{
   private enum Hand
    {
        Left,
        Right
    };

    [SerializeField] private Hand hand;
    
    private string horizontalAxisName;
    private string verticalAxisName;
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform xrRig;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        horizontalAxisName = "XRI_" + hand + "_Primary2DAxis_Horizontal";
        verticalAxisName = "XRI_" + hand + "_Primary2DAxis_Vertical";
    }

    void Update()
    {
        float x = Input.GetAxis(horizontalAxisName);
        xrRig.RotateAround(playerHead.position, Vector3.up, turnSpeed * x * Time.deltaTime);

        float y = Input.GetAxis(verticalAxisName);
        Vector3 direction = playerHead.forward;
        direction.y = 0;
        //direction.Normalize();
        xrRig.position += moveSpeed * -y * Time.deltaTime * direction;
    }
}
