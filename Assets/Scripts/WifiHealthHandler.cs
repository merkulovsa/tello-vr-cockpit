using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WifiHealthHandler : MonoBehaviour
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
        var percentage = telloFlightTracking.GetTelloState().wifiStrength;

        healthBarMat.SetFloat("_Value", (float)percentage / 100f);
        textMesh.text = "WIFI" + "\n" + percentage.ToString();
    }
}