using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{

    private const float REAL_SECONDS_PER_INGAME_DAY = 1440f;

    [SerializeField]
    private Transform hourClockHand, minutesClockHand;

    private float startingTime = 0.333333333333333f;
    private float rotationDegreesPerHour = 360f;
    private float hoursPerDay = 24f;
    public float day;

    private void Start()
    {
        day = startingTime;
    }



    // Update is called once per frame
    void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

        float hourNormalized = day % 1f;
        

        hourClockHand.eulerAngles = new Vector3(0, 0, -hourNormalized * rotationDegreesPerHour * 2);

        
        minutesClockHand.eulerAngles = new Vector3(0, 0, -hourNormalized * rotationDegreesPerHour * hoursPerDay);
    }
}