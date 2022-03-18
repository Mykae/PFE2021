using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToInitialPosition : MonoBehaviour
{

    [SerializeField] private Transform timmy, miranda;
    [SerializeField] private Vector2 initialPositionTimmy, initialPositionMiranda;
    private PlayerBehavior isTimmyActive, isMirandaActive;
    private float teleportDistance = 12;
    public bool needTP = false;

    private void Awake()
    {
        initialPositionTimmy = timmy.position;
        initialPositionMiranda = miranda.position;
        isTimmyActive = timmy.gameObject.GetComponent<PlayerBehavior>();
        isMirandaActive = miranda.gameObject.GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector2.Distance(timmy.position, miranda.position);
        if (needTP)
        {
            if (isTimmyActive.isActiveAndEnabled && distance >= teleportDistance
                && teleportDistance <= Vector2.Distance(timmy.position, initialPositionMiranda))
            {
                needTP = false;
                miranda.position = initialPositionMiranda;
            }
            else if (isMirandaActive.isActiveAndEnabled && distance >= teleportDistance
                && teleportDistance <= Vector2.Distance(miranda.position, initialPositionTimmy))
            {
                needTP = false;
                timmy.position = initialPositionTimmy;
            }
        }                    
    }
}
