using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParDefaut : MonoBehaviour
{
    public string[] dialogues;
    [HideInInspector]
    public int index = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // GameObject.Find("PNJ3").GetComponent<DialoguesAvecMiranda>().ChangerDialogues(new string[3] { "lsjg", "mojbpoudsif", "qsldbhqlsfb" });
    public void ChangerDialogues(string[] nouveauxDialogues)
    {
        index = -1;
        dialogues = new string[nouveauxDialogues.Length];
        dialogues = nouveauxDialogues;
    }

    public bool DialogueSuivant()
    {
        if(index +1 < dialogues.Length)
        {
            index++;
            return true;
        }
        index = -1;
        return false;
    }
}
