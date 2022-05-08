using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PoseHandler : MonoBehaviour
{
    public SteamVR_Behaviour_Pose behaviour_Pose;
    public SteamVR_Action_Boolean grabAction;

    SteamVR_Input_Sources handType;
    JoystickComponent joystick;
    Collider collidingObject;

    // Start is called before the first frame update
    void Start()
    {
        handType = behaviour_Pose.inputSource;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAction.GetStateDown(handType) && collidingObject) {
            joystick = collidingObject.GetComponent<JoystickComponent>();
        }

        if (grabAction.GetStateUp(handType)) {
            joystick.Release();
            joystick = null;
        }

        if (grabAction.GetState(handType) && joystick) {
            joystick.Hold(transform.position);
        }
        
    }

    private void SetCollidingObject(Collider collider) {
        collidingObject = collider;
    }

    private void ClearCollidingObject() {
        collidingObject = null;
    }

    private void OnTriggerEnter(Collider other) {
        SetCollidingObject(other);
    }

    private void OnTriggerStay(Collider other) {
        SetCollidingObject(other);
    }

    private void OnTriggerExit(Collider other) {
        ClearCollidingObject();
    }
}
