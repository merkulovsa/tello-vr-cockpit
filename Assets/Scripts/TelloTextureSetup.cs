using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelloTextureSetup : MonoBehaviour
{
    public TelloVideoTexture telloVideoTexture;

    Renderer skyBoxRenderer;
    Renderer telloVideoRenderer;

    // Start is called before the first frame update
    void Start()
    {
        skyBoxRenderer = GetComponent<Renderer>();
        telloVideoRenderer = telloVideoTexture.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skyBoxRenderer.material.mainTexture != telloVideoRenderer.material.mainTexture) {
            skyBoxRenderer.material.mainTexture = telloVideoRenderer.material.mainTexture;
        }
    }
}
