using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChangeOnEvent : MonoBehaviour
{
    [Header("MeshRenderer z którego bierzemy materia³")]
    public MeshRenderer meshRenderer;
    [Header("Kolor na który zmieni siê materia³ w reakcji na zdarzenie")]
    public Color color;

    //T¹ metodê musimy pod³¹czyæ do Eventu Ÿrod³owego
    public void ChangeColor()
    {
        meshRenderer.material.color = color;
    }


}
