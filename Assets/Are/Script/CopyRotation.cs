using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    public GameObject rotationToCopy;

    private void OnEnable()
    {
        transform.rotation = rotationToCopy.transform.rotation;
    }
}
