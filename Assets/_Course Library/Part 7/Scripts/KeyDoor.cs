using UnityEngine;
using UnityEngine.XR;

public class KeyDoor : MonoBehaviour // This script should be on the Locked Door Trigger
{
    [Header("Attributes")]
   
    [Tooltip("The name of the key that is required.")] public string keyName = "";

    [Header("References")]

    public Animation Door;

    public AudioSource DoorOpenSound;

    private KeyManager km;

    public AudioSource LockedDoorSound;

    private bool isUnlocked;

    private void Start()
    {
        km = FindObjectOfType<KeyManager>(); // Assign
    }

    private void OnMouseOver() // Activates when the player looks at the door
    {

        if ( PlayerCasting.DistanceFromTarget <= 4 ) // If the player IS close enough to the door..
        {


            // Check for A button press on the right-hand controller
            if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
            {

                foreach (string key in km.keysInInventory) // Check to see if the player has key
                {
                    if (key.Trim().ToLower() == keyName.Trim().ToLower())
                    { 
                        GetComponent<BoxCollider>().enabled = false; // Turns off the player's ability to open the door again even though it's already open

                        Door.Play(); // Play the door open animation

                        DoorOpenSound.Play(); // Play the door open sound

                        km.keysInInventory.Remove(key); // Removes the key from the inventory

                        isUnlocked = true;
                    }
                }

                if (isUnlocked == false)
                {
                    LockedDoorSound.Play();
                }
            }

        }
    }

}
