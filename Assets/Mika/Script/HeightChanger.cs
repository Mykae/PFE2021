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

    private Vector3 getRelativePosition(Transform origin, Vector3 position)
    {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

        return relativePosition;
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        yPosEnter = Mathf.Sign(getRelativePosition(this.gameObject.transform, col.gameObject.transform.position).y);
        if(col.gameObject.tag == "Player")
        {
            if(isPassed == false)
            {
                isPassed = true;
            }

            /*if (col.GetComponent<PlayerMovement>().height == fromLayer && isPassed == false)
            {
                col.GetComponent<PlayerMovement>().SetHeight(toLayer);
                isPassed = true;
            }
            else if (col.GetComponent<PlayerMovement>().height == toLayer && isPassed == false)
            {
                col.GetComponent<PlayerMovement>().SetHeight(fromLayer);
                isPassed = true;
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            yPosExit = Mathf.Sign(getRelativePosition(this.gameObject.transform, col.gameObject.transform.position).y);
            if (yPosEnter != yPosExit)
            {
                if (col.GetComponent<PlayerMovement>().height == fromLayer)
                {
                    col.GetComponent<PlayerMovement>().SetHeight(toLayer);
                }
                else if (col.GetComponent<PlayerMovement>().height == toLayer)
                {
                    col.GetComponent<PlayerMovement>().SetHeight(fromLayer);
                }
            }
            isPassed = false;
        }
    }
}
