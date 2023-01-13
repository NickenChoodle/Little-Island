using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroll : MonoBehaviour
{
    // Scroll main texture based on time

    float scrollSpeed = 0.04f;
    Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, -0.8f*offset));
    }
}
