using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{


    [SerializeField]
    private Transform castlePrefab;

    public PlayerMovement movement;
    private GameObject actionButon;
    private Text actionText;
    public DialogueParDefaut selectionMonologue;
    public bool showMonologue = true;
    private float tempsMonologue = 0;

    //Différents types de trigger box
    bool playerTriggerBox = false;
    bool pnjTriggerBox = false;

    private string lastEncounteredPNJ;
    public GameObject messageBox;
    public Text dialogNameBox;

    private GameObject LastEncounteredPlayer;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        actionButon = GameObject.Find("Bouton_Action");
        actionText = actionButon.GetComponentInChildren<Text>();
        selectionMonologue = GetComponent<DialogueParDefaut>();
    }

    private void OnEnable()
    {
        actionButon.SetActive(false);
        showMonologue = true;
        movement.enabled = true;
        dialogNameBox.text = name;
        messageBox.SetActive(true);
        GestionMonologue();
        FindObjectOfType<TeleportToInitialPosition>().needTP = true;
    }

    //Ce qu'il faut faire pour changer de joueur
    private void OnDisable()
    {
        if (LastEncounteredPlayer != null)
        {
            Debug.Log("S'il y a EXACTEMENT 3 ERREURS juste après ce message ne paniquez pas, c'est normal <3");
            movement.enabled = false;
            //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.Find("Main Camera").SetParent(LastEncounteredPlayer.transform);
            transform.Find("Steps+ItemPicker").SetParent(LastEncounteredPlayer.transform);
            LastEncounteredPlayer.transform.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);
            LastEncounteredPlayer.transform.Find("Steps+ItemPicker").transform.localPosition = new Vector3(0, 0, 0);
            //LastEncounteredPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            LastEncounteredPlayer.GetComponent<PlayerBehavior>().enabled = true;
            LastEncounteredPlayer.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        tempsMonologue += Time.deltaTime;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (showMonologue)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) || tempsMonologue >= 10)
            {
                GestionMonologue();
            }
            return;
        }

        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && movement.enabled)
            ActionButton();
        //si le mouvement est désactivé et que la boite de dialogue est active c'est qu'on est en train de parler.
        else if(movement.enabled == false && messageBox.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Parler(lastEncounteredPNJ);
        }

    }


    private void ActionButton()
    {
        actionButon.SetActive(false);
        //actions des mini jeux gérées sur le script de leur gameobject
        if (playerTriggerBox)
        {
            this.enabled = false;

        }
        else if (pnjTriggerBox)
            Parler(lastEncounteredPNJ);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActiveAndEnabled && movement.enabled == true && !messageBox.activeSelf && Time.timeScale != 0)
        {
           if (collision.tag == "Player" && collision.name != name)
            {
                playerTriggerBox = true;
                actionText.text = "Changer";
                actionButon.SetActive(true);
                LastEncounteredPlayer = collision.gameObject;
            }
            else if (collision.tag == "Peche")
            {
                if (collision.GetComponent<OpenGame>().canGameBeReplayed && collision.GetComponent<OpenGame>().player == gameObject)
                {
                    actionButon.SetActive(true);
                    actionText.text = "Pêcher";
                }
            }
            else if (collision.tag == "Door")
            {
                if (collision.GetComponent<OpenGame>().canGameBeReplayed && collision.GetComponent<OpenGame>().player == gameObject)
                {
                    actionButon.SetActive(true);
                    actionText.text = "Ouvrir";
                }                
            }
            else if (collision.tag == "PNJ")
            {
                actionText.text = "Parler";
                actionButon.SetActive(true);
                pnjTriggerBox = true;
                lastEncounteredPNJ = collision.name;
            }
            else if (collision.tag == "Fruit")
            {
                if(collision.GetComponent<OpenGame>().player == gameObject)
                {
                    Debug.Log(collision.GetComponent<OpenGame>().player + " = " + gameObject);
                    actionButon.SetActive(true);
                    actionText.text = "Cueillir";
                }                
            }
            else if (collision.tag == "Finish")
            {
                actionButon.SetActive(true);
                actionText.text = "Terminer le jeu";
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActiveAndEnabled)
        {
            actionButon.SetActive(false);
            if (collision.tag == "Player" && collision.name != name)
            {
                playerTriggerBox = false;
            }
            else if (collision.tag == "PNJ")
            {
                pnjTriggerBox = false;
            }
        }
    }

    void Parler(string nomPNJ)
    {
        dialogNameBox.text = nomPNJ;
        var dialogues = GameObject.Find(nomPNJ).GetComponent<DialogueParDefaut>();
        if (name == "Miranda")
        {
            dialogues = GameObject.Find(nomPNJ).GetComponent<DialoguesAvecMiranda>();
        }
        else if (name == "Timmy")
        {
            dialogues = GameObject.Find(nomPNJ).GetComponent<DialoguesAvecTimmy>();
        }
        

        if(dialogues.index == -1)
        {
            movement.enabled = false;
            messageBox.SetActive(true);
        }
        if(dialogues.DialogueSuivant())
        {
            messageBox.GetComponentInChildren<Text>().text = dialogues.dialogues[dialogues.index];
        }
        else
        {
            messageBox.SetActive(false);
            movement.enabled = true;
            //dialogues.index = -1;
        }
    }

    void PlacerChateauDeSable()
    {
        var _castlePlacement = transform.position + Vector3.down;

        var _castle = Instantiate(castlePrefab, _castlePlacement, Quaternion.identity);
    }

    public void  GestionMonologue()
    {
        if (showMonologue)
        {
            tempsMonologue = 0;
            if (selectionMonologue.DialogueSuivant())
            {
                messageBox.GetComponentInChildren<Text>().text = selectionMonologue.dialogues[selectionMonologue.index];
            }
            else
            {
                messageBox.SetActive(false);
                showMonologue = false;
            }
        }
    }

    public void restartMonologue(string[] phrases)
    {
        selectionMonologue.ChangerDialogues(phrases);
        showMonologue = true;
        dialogNameBox.text = name;
        messageBox.SetActive(true);
        GestionMonologue();        
    }
}
