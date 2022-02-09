using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChanger : MonoBehaviour
{    
    public int fromLayer;
    public int toLayer;
    public bool isPassed;
    public float yPosEnter;
    public float yPosExit;

    private void OnTriggerEnter2D(Collider2D col)
    {
        yPosEnter = col.bounds.center.y;
        if(col.gameObject.tag == "Player")
        {
            if (col.GetComponent<PlayerMovement>().height == fromLayer && isPassed == false)
            {
                col.GetComponent<PlayerMovement>().SetHeight(toLayer);
                isPassed = true;
            }
            else if (col.GetComponent<PlayerMovement>().height == toLayer && isPassed == false)
            {
                col.GetComponent<PlayerMovement>().SetHeight(fromLayer);
                isPassed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        yPosExit = col.bounds.center.y;
        if (Mathf.Sign(yPosEnter) != Mathf.Sign(yPosExit))
        {
            isPassed = false;
            yPosEnter = 0f;
            yPosExit = 0f;
        }
    }
}
