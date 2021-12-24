using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber2 : MonoBehaviour
{
    private Animator animator;
    private GrabbableObject2 hoveredObject;
    private GrabbableObject2 grabbedObject;

    public string buttonName;

    private void OnTriggerEnter(Collider other)
    {
        GrabbableObject2 grabble = other.GetComponent<GrabbableObject2>();

        if (grabble != null)
        {
            if (hoveredObject != null)
            {
                hoveredObject.OnHoverEnd();
            }
            hoveredObject = grabble;
            hoveredObject.OnHoverStart();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GrabbableObject grabble = other.GetComponent<GrabbableObject>();

        if (grabble == hoveredObject && grabble != null)
        {
            hoveredObject.OnHoverEnd();
            hoveredObject = null;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //check if grip button pressed
        if (Input.GetButtonDown(buttonName))
        {
            animator.SetBool("Gripped", true);

            if (hoveredObject != null)
            {
                hoveredObject.OnGrabStart(this);
                grabbedObject = hoveredObject;
                hoveredObject = null;
            }
        }
        
        //check if grip button released
        if (Input.GetButtonUp(buttonName))
        {
            animator.SetBool("Gripped", false);

            if (grabbedObject != null)
            {
                grabbedObject.OnGrabEnd();
                grabbedObject = null;
            }

        }
    }
    
    
}
