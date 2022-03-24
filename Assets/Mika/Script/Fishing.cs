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

    public GameObject fishToInstantiate;

    private bool isEnding;
    private bool isExit;

    private Vector3 ls;

    public GameObject waitToFishAgain;

    public PlaySound soundManager;
    int soundToPlay;

    private void OnEnable()
    {
        //Resize();
    }

    private void OnDisable()
    {
        waitToFishAgain.GetComponent<OpenGame>().ExitSucessfullFishGame();
    }

    private void Start()
    {
        //Resize();
    }
    private void Update()
    {
        Fish();
        Hook();
        ProgressCheck();
        if (Input.GetKeyDown(KeyCode.Escape)) isExit = true;
        CheckEnding();
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
        if (Input.GetButton("Jump") || Input.GetMouseButtonDown(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;
        }
        hookPullVelocity -= hookGravityPower * Time.deltaTime;
        
        hookPosition += hookPullVelocity;
        if (hookPosition <= 0.125f)
        {
            hookPullVelocity = 0;
        }
        else if (hookPosition >= 0.875f)
        {
            hookPullVelocity = 0;
        }
        hookPosition = Mathf.Clamp(hookPosition, hookSize/2, 1-(hookSize/2));

        hook.position = Vector3.Lerp(botPivot.position, topPivot.position, hookPosition);
    }

    public void ProgressCheck()
    {
        ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;
        if(min < fishPosition && fishPosition < max)
        {
            if (soundToPlay != 0)
                soundManager.Stop();
            soundToPlay = 0;
            soundManager.Play(soundToPlay);
            hookProgress += hookPower * Time.deltaTime;
            
        }
        else
        {
            if (soundToPlay != 1)
                soundManager.Stop();
            soundToPlay = 1;
            soundManager.Play(soundToPlay);
            hookProgress -= hookProgressDegradePower * Time.deltaTime;
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
        if(hookProgress == 1f && !isEnding)
        {

            StartCoroutine(EndMinigame());
        }
    }

    public void Resize()
    {
        Bounds b = hookSpriteRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, botPivot.position);
        ls.y = distance / (ySize * hookSize);
        hook.localScale = ls;
    }

    public void CheckEnding()
    {
        if (isEnding == true || isExit == true)
        {
            if (isEnding) {
                Instantiate(fishToInstantiate, player.transform.position, player.transform.rotation);
                GameObject.Find("Ludo").GetComponent<DialoguesAvecTimmy>().dialogues = new string[1] { "Super ! C’est un beau spécimen, je suis sûr qu’il sera délicieux. J’apporterai tous ces plats ce soir grâce à toi !" };
                GameObject.Find("Ludo").GetComponent<DialoguesAvecMiranda>().dialogues = new string[1] { "Miranda ! N'oublie pas la fête ce soir, je ramenerai un plat avec ce beau poisson que Timmy m'a pêché !" };
                soundManager.Stop();
                var i = player.GetComponent<PlaySound>();
                i.Play(0);                
            }
                
            hookPosition = 0f;
            hookProgress = 0f;
            ls.y = hookProgress;
            progressBarContainer.localScale = ls;
            player.GetComponent<PlayerMovement>().enabled = true;
            isEnding = false;
            isExit = false;
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator EndMinigame()
    {
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        isEnding = true;
    }

    
}
