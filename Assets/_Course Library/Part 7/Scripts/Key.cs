using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Key : MonoBehaviour
{
    [Tooltip("The name of the key. This corresponds with the key on the door")] public string keyName;

    public GameObject HoverIcon;
    private bool isPlayerClose = false;
    [SerializeField] private GameObject[] HoverObjects;

    [Header("(Optional)")]
    [Tooltip("(Optional.)")] public AudioClip CollectAudio;

    public void Update()
    {
        UpdateHoverObjectsState();
        var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (triggerPressed && isPlayerClose)
        {
            if (CollectAudio != null) FindObjectOfType<KeyManager>().GetComponent<AudioSource>().PlayOneShot(CollectAudio); // (Optional.)

            FindObjectOfType<KeyManager>().keysInInventory.Add(keyName);

            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player GameObject has a "Player" tag
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }

    private void UpdateHoverObjectsState()
    {
        foreach (GameObject hoverObject in HoverObjects)
        {
            hoverObject.SetActive(isPlayerClose);
        }
    }

}
