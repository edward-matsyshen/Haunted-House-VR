using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Key : MonoBehaviour
{
    [Tooltip("The name of the key. This corresponds with the key on the door")] public string keyName;

    public GameObject HoverIcon;

    [Header("(Optional)")]
    [Tooltip("(Optional.)")] public AudioClip CollectAudio;

    public void Update()
    {
        if (PlayerCasting.DistanceFromTarget <= 5) // Is the player in range?
        {
            var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

            if (triggerPressed)
            {
                if (CollectAudio != null) FindObjectOfType<KeyManager>().GetComponent<AudioSource>().PlayOneShot(CollectAudio); // (Optional.)

                FindObjectOfType<KeyManager>().keysInInventory.Add(keyName);

                Destroy(this.gameObject);
            }

            
        }
        
        
    }


}
