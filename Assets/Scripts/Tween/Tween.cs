using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    public delegate void OnPlayHandler();
    public delegate void OnStopHandler();
    public delegate void OnCompleteHandler();
    public delegate void OnUpdateHandler(float[] values);

    public static Easing[] easings = {
        new LinearNone(),
        new BounceIn(),
        new BounceOut(),
        new BounceInOut(),
    };

    public enum EasingType
    {
        LinearNone,
        BounceIn,
        BounceOut,
        BounceInOut,
    }

    public float[] valuesFrom;
    public float[] valuesTo;
    public float duration = 1;
    public EasingType easingType;
    public bool reversed = false;

    public event OnPlayHandler onPlay;
    public event OnStopHandler onStop;
    public event OnCompleteHandler onComplete;
    public event OnUpdateHandler onUpdate;

    float[] values;
    float length;
    float startTime = -1;

    public void Play()
    {
        startTime = Time.time;
        onPlay?.Invoke();
    }

    public void Stop()
    {
        startTime = -1;
        onStop?.Invoke();
    }

    void Start()
    {
        values = (float[])valuesFrom.Clone();
        length = Mathf.Min(valuesFrom.Length, valuesTo.Length);
    }

    void Update()
    {
        if (startTime < 0)
        {
            return;
        }

        var elapsed = easings[(int)easingType].ease(Mathf.Clamp01((Time.time - startTime) / duration));

        if (reversed) {
            elapsed = 1 - elapsed;
        }

        for (int i = 0; i < length; ++i)
        {
            values[i] = Mathf.Lerp(valuesFrom[i], valuesTo[i], elapsed);
        }

        onUpdate?.Invoke(values);

        if (elapsed == 1)
        {
            startTime = -1;
            onComplete?.Invoke();
        }
    }
}
