using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockControllerFour : MonoBehaviour
{
    public Transform hourHandTransform; // Reference to the hour hand game object
    public Transform minuteHandTransform; // Reference to the minute hand game object
    public Transform secondHandTransform; // Reference to the second hand game object

    // Update is called once per frame
    void Update()
    {
        // Get the current system time
        System.DateTime currentTime = System.DateTime.Now;

        // Rotate the hour hand
        float hourAngle = 360f * ((float)currentTime.Hour / 12f); // 12 hours for a full rotation
        hourHandTransform.localRotation = Quaternion.Euler(0f, 0f, -hourAngle);

        // Rotate the minute hand
        float minuteAngle = 360f * ((float)currentTime.Minute / 60f); // 60 minutes for a full rotation
        minuteHandTransform.localRotation = Quaternion.Euler(0f, 0f, -minuteAngle);

        // Rotate the second hand
        float secondAngle = 360f * ((float)currentTime.Second / 60f); // 60 seconds for a full rotation
        secondHandTransform.localRotation = Quaternion.Euler(0f, 0f, -secondAngle);
    }
}