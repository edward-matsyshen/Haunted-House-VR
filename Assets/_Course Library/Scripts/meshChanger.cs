using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshChanger : MonoBehaviour
{
    [SerializeField] private MeshFilter modelYouWantToChange;
    [SerializeField] private Mesh modelYouWantToUse;
    private bool change = true; 
    void Update()
    {
        if (change == true)
        {
            modelYouWantToChange.mesh = modelYouWantToUse;
        } 
    }
    public void ChangeMeshAutomatically()
    {
        modelYouWantToChange.mesh = modelYouWantToUse;
    }
}
