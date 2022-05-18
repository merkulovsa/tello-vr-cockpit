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
        if (grabPinchAction != null) {
            if (joystick != null) {
                if (grabPinchAction.GetState(handType)) {
                    joystick.Hold(transform.position);
                }

                if (grabPinchAction.GetStateUp(handType)) {
                    joystick.Release();
                    joystick = null;
                }
            }
        }

        if (grabGripAction != null) {
            if (button != null) {
                if (grabGripAction.GetState(handType)) {
                    button.Hold();
                } else {
                    button.Release();
                }
            }
        }

        if (menuAction != null) {
            if (menuAction.GetStateDown(handType)) {
                cockpit.transform.position = center.transform.position;
            }
        }
    }

    private void SetCollidingObject(Collider collider) {
        if (collider.TryGetComponent(out PoseButton _button)) {
            button = _button;
            collidingObject = collider;
        }
        
        if (collider.TryGetComponent(out PoseJoystick _joystick)) {
            joystick = _joystick;
            collidingObject = collider;
        }
    }

    private void ClearCollidingObject() {
        collidingObject = null;
    }

    private void OnTriggerEnter(Collider other) {
        if (collidingObject == null) {
            SetCollidingObject(other);
        }
    }

    // private void OnTriggerStay(Collider other) {
    //     SetCollidingObject(other);
    // }

    private void OnTriggerExit(Collider other) {
        if (collidingObject == other) {
            ClearCollidingObject();

            if (button != null) {
                button.Release();
                button = null;
            }
        }
    }
}
