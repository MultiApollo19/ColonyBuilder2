using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera m_Camera;
    public bool amActive = false;
    public bool autoInit = false;
    GameObject myContainer;

    void Awake()
    {
        if (autoInit == true)
        {
            m_Camera = Camera.main;
            amActive = true;
        }
    }
    private void Start()
    {
        myContainer = this.gameObject;
    }
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        if (amActive == true)
        {
            myContainer.transform.LookAt(myContainer.transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up);
        }
    }
}
