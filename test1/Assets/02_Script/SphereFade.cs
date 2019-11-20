using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFade : MonoBehaviour
{
    MeshRenderer mr;
    Material mat;


    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;

        StartCoroutine(ChangeAlpha());
    }

    IEnumerator ChangeAlpha()
    {
        float newAlpha = 1f;

        while (newAlpha > 0)
        {
            newAlpha -= 0.01f;

            Color newColor = mat.color;
            newColor.a = newAlpha;
            mat.color = newColor;
            
            yield return null;
        }      
    }
}
