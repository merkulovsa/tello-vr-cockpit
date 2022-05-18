using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseButton : MonoBehaviour
{
    public bool isPressed = false;

    Renderer renderer;
    Tween tween;
    bool _isPressed = false;

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
        if (GetStateDown()) {
            tween.reversed = false;
            tween.Play();
        }

        if (GetStateUp()) {
            tween.reversed = true;
            tween.Play();
        }

        this._isPressed = this.isPressed;
    }

    public void Hold() {
        this.isPressed = true;
    }

    public void Release() {
        this.isPressed = false;
    }

    public bool GetStateDown() {
        return this.isPressed && !this._isPressed;
    }

    public bool GetStateUp() {
        return !this.isPressed && this._isPressed;
    }

    void OnTweenUpdate(float[] values) {
        var color = renderer.material.color;
        color.a = values[0];
        renderer.material.color = color;
    }
}
