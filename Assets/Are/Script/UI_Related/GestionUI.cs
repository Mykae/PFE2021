using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GestionUI : MonoBehaviour
{
    
    [SerializeField]
    private GameObject tableauDeQuetes;
    private GameObject queteEnCours;

    void Start()
    {
        queteEnCours = tableauDeQuetes.transform.GetChild(1).gameObject;
    }

    //indiceQuete pour la qu�te 1 2 ou 3 du personnage
    //addition == true -> addition, la qu�te avance. si false la qu�te recule (eg. destruction de ch�teau)
    public void AvancerQuete(int indiceQuete, bool addition)
    {
        queteEnCours = tableauDeQuetes.transform.GetChild(indiceQuete).gameObject;

        //Si la qu�te est finie on quitte la m�thode
        if (queteEnCours.activeSelf == false)
            return;

        //Quetes -> Avancement (child 1) -> ValeurActuelle (child 0)
        var valeurActuelle = queteEnCours.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();

        //Quetes -> Avancement (child 1) -> Total (child 1)
        var total = queteEnCours.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();

        int increment;
        //On converti le texte de valeurActuelle en int pour l'incr�menter puis on le remet � sa place
        if (addition)
            increment = System.Convert.ToInt32(valeurActuelle.text) + 1;
        else
            increment = System.Convert.ToInt32(valeurActuelle.text) - 1;
        valeurActuelle.text = "" + increment;

        //Si on a atteint l'objectif on d�sactive l'avancement et active l'interface de qu�te r�ussie
        if (valeurActuelle.text == total.text)
        {
            queteEnCours.transform.GetChild(1).gameObject.SetActive(false);
            queteEnCours.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
