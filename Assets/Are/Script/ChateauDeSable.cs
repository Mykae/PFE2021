using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChateauDeSable : MonoBehaviour
{
    private GestionUI UIQuetes;

    [SerializeField]
    private SpriteRenderer castleColor;

    float tiersTempsDestruction = 1f;

    // Start is called before the first frame update
    void Start()
    {
        UIQuetes = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GestionUI>();
        UIQuetes.AvancerQuete(1, true);
        StartCoroutine(destructionChateau());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destructionChateau()
    {
        yield return new WaitForSeconds(tiersTempsDestruction);
        castleColor.color = Color.red;
        yield return new WaitForSeconds(tiersTempsDestruction);
        castleColor.color = Color.black;
        yield return new WaitForSeconds(tiersTempsDestruction);
        UIQuetes.AvancerQuete(1, false);
        Destroy(gameObject);
    }
}
