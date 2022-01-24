using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject tableauDeQuetes;
    private GameObject queteEnCours;

    //Qu�tes Timmy



    // Start is called before the first frame update
    void Start()
    {
        queteEnCours = tableauDeQuetes.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AvancerQuete(1);
    }

    public void AvancerQuete(int indiceQuete)
    {
        queteEnCours = tableauDeQuetes.transform.GetChild(indiceQuete).gameObject;

        //Si la qu�te est finie on quitte la m�thode
        if (queteEnCours.activeSelf == false)
            return;

        //Quetes -> Avancement (child 1) -> ValeurActuelle (child 0)
        var valeurActuelle = queteEnCours.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();

        //Quetes -> Avancement (child 1) -> Total (child 1)
        var total = queteEnCours.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();

        //On converti le texte de valeurActuelle en int pour l'incr�menter puis on le remet � sa place
        int increment = System.Convert.ToInt32(valeurActuelle.text) + 1;
        valeurActuelle.text = "" + increment;

        //Si on a atteint l'objectif on d�sactive l'avancement et active l'interface de qu�te r�ussie
        if (valeurActuelle.text == total.text)
        {
            queteEnCours.transform.GetChild(1).gameObject.SetActive(false);
            queteEnCours.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
