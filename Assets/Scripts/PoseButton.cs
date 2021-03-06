using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseButton : MonoBehaviour
{

    Renderer renderer;
    Tween tween;

    bool isPressed = false;    bool _isPressed = false;
    bool stateDown = false;
    bool stateUp = false;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        tween = GetComponent<Tween>();
        tween.onUpdate += OnTweenUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        stateDown = false;
        stateUp = false;

        if (isPressed && !_isPressed) {
            stateDown = true;

            tween.reversed = false;
            tween.Play();
        }

        if (!isPressed && _isPressed) {
            stateUp = true;

            tween.reversed = true;
            tween.Play();
        }

        _isPressed = isPressed;
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

    public void Hold() {
        isPressed = true;
    }

    public void Release() {
        isPressed = false;
    }

    void OnTweenUpdate(float[] values) {
        var color = renderer.material.color;
        color.a = values[0];
        renderer.material.color = color;
    }
}
