using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
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

    public bool DialogueSuivant()
    {
        if(index +1 < dialogues.Length)
        {
            index++;
            return true;
        }
        return false;
    }
}
