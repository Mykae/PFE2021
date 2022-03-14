using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public Transform topPivot;
    public Transform botPivot;
    public Transform fish;
    float fishPosition;
    float fishDestination;
    float fishTimer; //temps entre chaque déplacement
    public float timerMultiply = 3f; //modificateur du temps entre chaque déplacement
    float fishSpeed;
    public float smoothMotion = 1f;


    public Transform hook;
    public float hookPosition;
    public float hookSize = 0.1f;
    public float hookPower = 0.5f;
    float hookProgress; //progression de la capture
    public float hookPullVelocity; //Vitesse du "jump" de l'appat
    public float hookPullPower = 0.01f; //Force du "jump" de l'appat
    public float hookGravityPower = 0.005f; //force de la gravité sur l'appat
    public float hookProgressDegradePower = 0.1f; //vitesse de retombée de la barre de progression
    public SpriteRenderer hookSpriteRenderer;

    public Transform progressBarContainer;
    public GameObject player;

    private void Start()
    {

    }

    private void OnEnable()
    {
        Resize();
    }

    private void Update()
    {
        Fish();
        Hook();
        ProgressCheck();
    }

    public void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = Random.value * timerMultiply;
            fishDestination = Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(botPivot.position, topPivot.position, fishPosition);
    }

    public void Hook()
    {
        if (Input.GetButton("Fire1"))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;
        }
        hookPullVelocity -= hookGravityPower * Time.deltaTime;
        
        hookPosition += hookPullVelocity;
        if (hookPosition <= 0.05f)
        {
            hookPullVelocity = 0;
        }
        else if (hookPosition >= 0.95f)
        {
            hookPullVelocity = 0;
        }
        hookPosition = Mathf.Clamp(hookPosition, hookSize/2, 1-hookSize/2);

        hook.position = Vector3.Lerp(botPivot.position, topPivot.position, hookPosition);
    }

    public void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;
        if(min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookProgressDegradePower * Time.deltaTime;
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
        if(hookProgress == 1f)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    public void Resize()
    {
        Bounds b = hookSpriteRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, botPivot.position);
        ls.y = (distance / ySize * hookSize);
        hook.localScale = -ls;
    }
}
