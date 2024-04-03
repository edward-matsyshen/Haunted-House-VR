using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBAppearance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Deactivate Object B when the game starts
        gameObject.SetActive(false);
    }

    // Method to activate Object B
    public void ActivateObjectB()
    {
        gameObject.SetActive(true);
    }
}
