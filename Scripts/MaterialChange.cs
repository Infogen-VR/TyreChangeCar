using System.Collections;
using System.Collections.Generic;
using SVR.Workflow.TriangleFactory.Scripts.Mechanics;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
   
    public Material otherMaterial = null;
    private bool usingOther = false;
    private MeshRenderer meshRenderer = null;
    private Material originalMaterial = null;
    

    private void Awake()
    {

        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }

    public void SetOtherMaterial()
    {
        usingOther = true;
        meshRenderer.material = otherMaterial;
    }

    public void SetOriginalMaterial()
    {
        usingOther = false;
        meshRenderer.material = originalMaterial;
    }

   

}
