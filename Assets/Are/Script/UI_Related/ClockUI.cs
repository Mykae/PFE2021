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
    public bool shouldRing;

    //[SerializeField] float deltaTimeYo = 1;
    bool beforeFive = true;

    private void Start()
    {
        day = startingTime;
    }



    // Update is called once per frame
    void Update()
    {
        //Time.timeScale = deltaTimeYo;

        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

        float hourNormalized = day % 1f;
        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.L))
        {
            day = .77f;
            
        }
        if(hourNormalized >= 0.7082f && beforeFive)
        {
            beforeFive = false;
            if(shouldRing)
                GetComponent<PlaySound>().Play(0);
        }
        if(day >= .75f)
        {
            FindObjectOfType<Fade>().TriggerFadeIn();
            FindObjectOfType<MenuPause>().GoToEndingScreen();
        }



        hourClockHand.eulerAngles = new Vector3(0, 0, -hourNormalized * rotationDegreesPerHour * 2);

        
        minutesClockHand.eulerAngles = new Vector3(0, 0, -hourNormalized * rotationDegreesPerHour * hoursPerDay);
    }
}
