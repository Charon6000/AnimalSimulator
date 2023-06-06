using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Swiat", menuName = "VirtualWorld/Swiat", order = 0)]
public class Swiat : ScriptableObject 
{
    public List<GameObject> organizmy {get;set;}

    public void WykonajTure()
    {
        int a = organizmy.Count;
        //wykonaj ture
        for(int item = 0; item< a; item++)
        {
            if(organizmy[item] != null && Organizmy.Gatunek.Roza != organizmy[item].GetComponent<Instancja>().organizm.gatunek && Organizmy.Gatunek.Guarana != organizmy[item].GetComponent<Instancja>().organizm.gatunek)
            {
            organizmy[item].GetComponent<Instancja>().organizm.akcja();
            }
        }

            for(int item = 0; item< a; item++)
            {
                if(organizmy[item] != null && Organizmy.Gatunek.Roza == organizmy[item].GetComponent<Instancja>().organizm.gatunek)
                {
                organizmy[item].GetComponent<Instancja>().organizm.akcja();
                break;
                }
            }

            for(int item = 0; item< a; item++)
            {
                if(organizmy[item] != null && Organizmy.Gatunek.Guarana == organizmy[item].GetComponent<Instancja>().organizm.gatunek)
                {
                organizmy[item].GetComponent<Instancja>().organizm.akcja();
                break;
                }
            }
    }
}
