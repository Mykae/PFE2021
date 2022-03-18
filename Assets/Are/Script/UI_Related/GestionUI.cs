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

    //indiceQuete pour la quête 1 2 ou 3 du personnage
    //addition == true -> addition, la quête avance. si false la quête recule (eg. destruction de château)
    public void AvancerQuete(int indiceQuete, bool addition)
    {
        queteEnCours = tableauDeQuetes.transform.GetChild(indiceQuete).gameObject;

        //Si la quête est finie on quitte la méthode
        if (queteEnCours.activeSelf == false)
            return;

        //Quetes -> Avancement (child 1) -> ValeurActuelle (child 0)
        var valeurActuelle = queteEnCours.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();

        //Quetes -> Avancement (child 1) -> Total (child 1)
        var total = queteEnCours.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();

        int increment;
        //On converti le texte de valeurActuelle en int pour l'incrémenter puis on le remet à sa place
        if (addition)
            increment = System.Convert.ToInt32(valeurActuelle.text) + 1;
        else
            increment = System.Convert.ToInt32(valeurActuelle.text) - 1;
        valeurActuelle.text = "" + increment;

        //Si on a atteint l'objectif on désactive l'avancement et active l'interface de quête réussie
        if (valeurActuelle.text == total.text)
        {
            queteEnCours.transform.GetChild(1).gameObject.SetActive(false);
            queteEnCours.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
