using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ClockControllerFive : MonoBehaviour
{
    public Transform hourHandTransform; // Reference to the hour hand game object
    public Transform minuteHandTransform; // Reference to the minute hand game object
    public Transform secondHandTransform; // Reference to the second hand game object

    void Start()
    {
        UpdateClock(); // Update the clock initially
        InvokeRepeating("UpdateClock", 0, 1); // Update the clock every second
    }

    void UpdateClock()
    {
        // Rotate the hour hand
        float hourAngle = 360f * (3f / 12f); // 3:33 corresponds to 3 hours and 33 minutes
        hourHandTransform.localRotation = Quaternion.Euler(0f, 0f, -hourAngle);

        // Rotate the minute hand
        float minuteAngle = 360f * (33f / 60f);
        minuteHandTransform.localRotation = Quaternion.Euler(0f, 0f, -minuteAngle);

        // Rotate the second hand
        float secondAngle = 360f * (0f / 60f); // We're not considering seconds here, setting to 0
        secondHandTransform.localRotation = Quaternion.Euler(0f, 0f, -secondAngle);
    }
}