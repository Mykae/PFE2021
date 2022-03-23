using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    public int currentEnding = 0;
    //0 = Ending : Default
    //1 = Ending : party with 4 and cake cooked

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
