using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PoseHandler : MonoBehaviour
{
    public SteamVR_Behaviour_Pose behaviour_Pose;
    public SteamVR_Action_Boolean grabGripAction;
    public SteamVR_Action_Boolean grabPinchAction;
    public SteamVR_Action_Boolean menuAction;
    public GameObject center;
    public GameObject cockpit;

    SteamVR_Input_Sources handType;
    PoseJoystick joystick;
    PoseButton button;
    Collider collidingObject;

    // Start is called before the first frame update
    void Start()
    {
        handType = behaviour_Pose.inputSource;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabPinchAction != null && grabPinchAction.GetStateDown(handType) && collidingObject) {
            // print("+");
            joystick = collidingObject.GetComponent<PoseJoystick>();
        }

        if (grabPinchAction != null && grabPinchAction.GetStateUp(handType) && joystick) {
            joystick.Release();
            joystick = null;
        }

        if (grabPinchAction != null && grabPinchAction.GetState(handType) && joystick) {
            joystick.Hold(transform.position);
        }

        if (grabGripAction != null && grabGripAction.GetState(handType) && collidingObject) {
            button = collidingObject.GetComponent<PoseButton>();

            if (button != null && !button.isPressed) {
                button.Hold();
            }
        }

        if (menuAction != null && menuAction.GetStateDown(handType)) {
            cockpit.transform.position = center.transform.position;
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

        if (button) {
            button.Release();
            button = null;
        }
    }
}
