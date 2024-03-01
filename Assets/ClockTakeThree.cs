using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTakeThree : MonoBehaviour
{
    [SerializeField]
    private GameObject Clock_Analog_A_Second, Clock_Analog_A_Minute, Clock_Analog_A_Hour;
    private float secondsMultiplier = 1f;
    private int inGameSeconds;
    private int timeAtStart = 10000;

    void Update()
    {
        inGameSeconds = Mathf.RoundToInt(Time.time * secondsMultiplier) + timeAtStart;
        Clock_Analog_A_Second.transform.localRotation = Quaternion.Euler(0, inGameSeconds * 90 / 60, 0);
        Clock_Analog_A_Minute.transform.localRotation = Quaternion.Euler(0, inGameSeconds * 90 / 3600, 0);
        Clock_Analog_A_Hour.transform.localRotation = Quaternion.Euler(0, inGameSeconds * 90 / 43200, 0);
    }
}
