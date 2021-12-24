using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LocomotionTeleport : MonoBehaviour
{
    private enum Hand
    {
        Left,
        Right
    };

    [SerializeField] private Hand hand;
    [SerializeField] private Transform xrRig;
    [SerializeField] private Transform teleportMarker;
    [SerializeField] private int lineResolution = 20;
    [SerializeField] private MeshRenderer blackScreen;
    
    private string buttonName;
    private string verticalAxisName;
    
    private LineRenderer line;
    private Vector3 hitpoint;
    private bool canTeleport = false;
    private bool teleportLock;
    
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        line.positionCount = lineResolution;

        buttonName = "XRI_" + hand + "_PrimaryButton";
    }

    void Update()
    {
        if (Input.GetButton(buttonName) && teleportLock == false)
        {
            line.SetPosition(0, transform.position);
            line.enabled = true;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                hitpoint = hit.point;
                //line.SetPosition(1, hitpoint);
                SetLinePosition(transform.position, hitpoint, lineResolution);
                canTeleport = true;
                teleportMarker.gameObject.SetActive(true);
                teleportMarker.position = hitpoint;
            }
            else
            {
                SetLinePosition(transform.position, transform.position + transform.forward * 15f, 0f);
                //line.SetPosition(1, transform.position + transform.forward * 15f);
                canTeleport = false;
                teleportMarker.gameObject.SetActive(false);
            }
        }
        
        else if (Input.GetButtonUp(buttonName))
        {
            line.enabled = false;
            teleportMarker.gameObject.SetActive(false);
            //xrRig.position = hitpoint;
            if (canTeleport == true)
            {
                StartCoroutine(FadedTeleport());
                //xrRig.position = hitpoint;
            }
        }
    }

    void SetLinePosition(Vector3 start, Vector3 end, float curve)
    {
        Vector3 startToEnd = end - start;
        Vector3 midpoint = (start + startToEnd / 2) + Vector3.up * 2;

        for (int i = 0; i < lineResolution; i++)
        {
            float t = i / (float)lineResolution;
            Vector3 startToMid = Vector3.Lerp(start, midpoint, t);
            Vector3 midToEnd = Vector3.Lerp(midpoint, end, t);
            Vector3 curvePoint = Vector3.Lerp(startToMid, midToEnd, t);
            
            line.SetPosition(i, curvePoint);
        }
    }

    IEnumerator FadedTeleport()
    {
        teleportLock = true;
        float currentTime = 0;

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
            // fade
            blackScreen.material.color = Color.Lerp(Color.clear, Color.black, currentTime);
        }

        xrRig.position = hitpoint;
        yield return new WaitForSeconds(0.5f);

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
            //fade in
            blackScreen.material.color = Color.Lerp(Color.clear, Color.black, currentTime);
        }

        teleportLock = false;
    }
}
