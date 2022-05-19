using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject target;
    public float radius;
    public bool X = true;
    public bool Y = true;
    public Vector2 value;

    Vector3 targetMin;
    Vector3 targetMax;
    Vector3 targetMid;
    Quaternion defaultRotation;
    Vector3 targetPos;
    bool isPressed = false;
    bool _isPressed = false;
    bool stateDown = false;
    bool stateUp = false;

    // Start is called before the first frame update
    void Start()
    {
        targetMin.x = X ? -radius : 0;
        targetMin.y = target.transform.localPosition.y;
        targetMin.z = Y ? -radius : 0;

        targetMax.x = X ? radius : 0;
        targetMax.y = target.transform.localPosition.y;
        targetMax.z = Y ? radius : 0;

        targetMid = (targetMin + targetMax) * 0.5f;

        targetPos = Vector3.zero;

        defaultRotation = joystick.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        stateDown = false;
        stateUp = false;

        if (isPressed && !_isPressed) {
            stateDown = true;
        }

        if (!isPressed && _isPressed) {
            stateUp = true;
        }

        _isPressed = isPressed;

        if (target) {
            var position = target.transform.localPosition;
            position.x = Mathf.Clamp(position.x, targetMin.x, targetMax.x);
            position.y = Mathf.Clamp(position.y, targetMin.y, targetMax.y);
            position.z = Mathf.Clamp(position.z, targetMin.z, targetMax.z);
            target.transform.localPosition = position;

            var distance = position - targetMid;
            value.x = distance.x / radius;
            value.y = distance.z / radius;

            var baseRotation = Quaternion.LookRotation((target.transform.position - joystick.transform.position).normalized, Vector3.up);
            baseRotation.eulerAngles = new Vector3(
                baseRotation.eulerAngles.x, 
                baseRotation.eulerAngles.y, 
                -baseRotation.eulerAngles.y
            );
            joystick.transform.rotation = baseRotation;
        } else {
            joystick.transform.rotation = defaultRotation;
        }
    }

    public bool GetState() {
        return isPressed;
    }

    public bool GetStateDown() {
        return stateDown;
    }

    public bool GetStateUp() {
        return stateUp;
    }

    public void Release() {
        isPressed = false;

        target.transform.localPosition = targetMid;
        targetPos = Vector3.zero;
    }

    public void Hold(Vector3 position) {
        isPressed = true;

        if (targetPos.magnitude > 0) {
            target.transform.position += position - targetPos;
        }
        
        targetPos = position;
    }
}
