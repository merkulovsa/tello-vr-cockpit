using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TelloLib;

public class BatteryHealthHandler : MonoBehaviour
{
    public TelloFlightTracking telloFlightTracking;

    Material healthBarMat;
    TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        healthBarMat = GetComponent<Renderer>().material;
        textMesh = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        var percentage = telloFlightTracking.GetTelloState().batteryPercentage;

        healthBarMat.SetFloat("_Value", (float)percentage / 100f);
        textMesh.text = "BTR" + "\n" + percentage.ToString();
    }
}
