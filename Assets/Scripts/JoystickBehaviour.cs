using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickBehaviour : MonoBehaviour
{
    public GameObject joystick;
    public GameObject target;
    public float radius;
    public Vector2 value;

    Vector3 targetMin;
    Vector3 targetMax;
    Vector3 targetMid;
    Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start()
    {
        targetMin.x = -radius;
        targetMin.y = target.transform.localPosition.y;
        targetMin.z = -radius;

        targetMax.x = radius;
        targetMax.y = target.transform.localPosition.y;
        targetMax.z = radius;

        targetMid = (targetMin + targetMax) * 0.5f;

        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            var position = target.transform.localPosition;
            position.x = Mathf.Clamp(position.x, targetMin.x, targetMax.x);
            position.y = Mathf.Clamp(position.y, targetMin.y, targetMax.y);
            position.z = Mathf.Clamp(position.z, targetMin.z, targetMax.z);
            target.transform.localPosition = position;

            var distance = position - targetMid;
            value.x = distance.x / radius;
            value.y = distance.z / radius;

            var rotation = Quaternion.LookRotation((target.transform.position - joystick.transform.position).normalized, Vector3.up);

            joystick.transform.rotation = rotation;
        } else {
            joystick.transform.rotation = defaultRotation;
        }
    }
}
