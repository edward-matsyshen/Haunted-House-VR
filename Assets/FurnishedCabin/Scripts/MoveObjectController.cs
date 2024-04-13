using UnityEngine;
using System.Collections;
using UnityEngine.XR;

public class MoveObjectController : MonoBehaviour 
{
    private Animator anim;
    private bool playerEntered;
    private bool showInteractMsg;
    private GUIStyle guiStyle;
    private string msg;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        setupGui();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player GameObject has a "Player" tag
        {
            playerEntered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = false;
            showInteractMsg = false;
        }
    }

    void Update()
    {
        if (playerEntered)
        {
            var rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool aButtonPressed);

            if (aButtonPressed)
            {
                showInteractMsg = true;
                bool isOpen = anim.GetBool("isOpen_Obj_");
                msg = getGuiMsg(isOpen);

                anim.enabled = true;
                anim.SetBool("isOpen_Obj_", !isOpen);
                msg = getGuiMsg(!isOpen);
            }
            else
            {
                showInteractMsg = false;
            }
        }
    }

    private void setupGui()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 16;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;
        msg = "Press A to Toggle";
    }

    private string getGuiMsg(bool isOpen)
    {
        return isOpen ? "Press A to Close" : "Press A to Open";
    }

    void OnGUI()
    {
        if (showInteractMsg)  // Show on-screen prompts to user for guide.
        {
            GUI.Label(new Rect(50, Screen.height - 50, 200, 50), msg, guiStyle);
        }
    }
}