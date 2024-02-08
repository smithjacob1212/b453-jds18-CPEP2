using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CharacterController2D targetPlayer;
    public GameObject xMinBoundObject;
    public GameObject xMaxBoundObject;
    public GameObject yMinBoundObject;
    public GameObject yMaxBoundObject;

    public float smoothSpeed = 10f;

    private float? xMinBound = null;
    private float? xMaxBound = null;
    private float? yMinBound = null;
    private float? yMaxBound = null;

    private float distXCenterToMin;
    public  float decalageX;  // decalage depuis position x player vers position x camera

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = FindObjectOfType<CharacterController2D>();
        if (targetPlayer != null)
        {
            Camera c = GetComponentInChildren<Camera>();

            float fovCameraRadian = Mathf.Deg2Rad * Camera.VerticalToHorizontalFieldOfView(c.fieldOfView, c.aspect)/2;

            //decalageX = transform.position.x - targetPlayer.transform.position.x;

            float zDistance = Mathf.Abs(transform.position.z - targetPlayer.transform.position.z);
            distXCenterToMin = Mathf.Tan(fovCameraRadian) * zDistance;
        }


        if(xMinBoundObject != null)
        {
            xMinBound = xMinBoundObject.transform.position.x;
        }
        if (xMaxBoundObject != null)
        {
            xMaxBound = xMaxBoundObject.transform.position.x;
        }
        if (yMinBoundObject != null)
        {
            yMinBound = yMinBoundObject.transform.position.y;
        }
        if (yMaxBoundObject != null)
        {
            yMaxBound = yMaxBoundObject.transform.position.y;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetPlayer == null) targetPlayer = FindObjectOfType<CharacterController2D>();
        if (targetPlayer != null)
        {
            float xDesiredPos = targetPlayer.transform.position.x + (decalageX * targetPlayer.transform.localScale.x);

            if (xMinBound != null && xDesiredPos - distXCenterToMin < xMinBound)
            {
                xDesiredPos = xMinBound.Value + distXCenterToMin;
            }
            if (xMaxBound != null && xDesiredPos + distXCenterToMin > xMaxBound)
            {
                xDesiredPos = xMaxBound.Value - distXCenterToMin;
            }

            Vector3 desiredPos = new Vector3(xDesiredPos, transform.position.y, transform.position.z);

            // if evite des secouses en cas de très petits mouvements
            if (Mathf.Abs(transform.position.x - desiredPos.x) > 0.05f) {
                Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

                transform.position = smoothPos;
            }
        }
    }
}
