using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject2 : MonoBehaviour
{

    private Material material;
    private Color originalColor;
    public Vector3 parentLocation;
    public Quaternion parentRotation;


    private bool isGrabbed;
    [SerializeField]
    private List<Vector3> positions = new List<Vector3>();

    private float throwForce = 35f;

    public Color hoverColor = new Color(0f, 1f, 0f, 0f);

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
        parentLocation = GetComponent<Transform>().position;
        parentRotation = GetComponent<Transform>().rotation;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (isGrabbed == true)
        {
            if (positions.Count > 20)
            {
                positions.RemoveAt(0);
                positions.Add(transform.position);
            }
            else
            {
                positions.Add(transform.position);
            }
        }
    }


    public void OnHoverStart()
    {
        material.color = hoverColor;
    }

    public void OnHoverEnd()
    {
        material.color = originalColor;
    }

    public void OnGrabStart(Grabber2 hand)
    {
        isGrabbed = true;
        
        #region Kinematic grab
        //transform.SetParent(hand.transform);
        //GetComponent<Rigidbody>().useGravity = false;
        //GetComponent<Rigidbody>().isKinematic = true;
        #endregion

        // fixed joint grab
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.connectedBody = hand.GetComponent<Rigidbody>();
        fx.breakForce = 5000;
        fx.breakTorque = 5000;

    }

    public void OnGrabEnd()
    {
        isGrabbed = false;
        
        #region Kinematic release
        //transform.SetParent(null);
        //GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<Rigidbody>().isKinematic = false;
        #endregion

        // fixed joint release
        FixedJoint fx = GetComponent<FixedJoint>();
        if (fx != null)
        {
            Destroy(fx);
        }

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.velocity = (positions[positions.Count - 1] - positions[0]) * throwForce/rb.mass;
        
        positions.Clear();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Floor" | collision.collider.tag == "Target")
        {
            gameObject.SetActive(false);
            transform.position = parentLocation;
            transform.rotation = parentRotation;
            Invoke("respawnFood", 3f);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
        }

    }

    void respawnFood()
    {
        gameObject.SetActive(true);
    }

}
