using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instancja : MonoBehaviour
{
    public Organizmy organizm;

    private void Start() 
    {
        organizm.wiek = 0;
        organizm.matka = this.gameObject;
        organizm.akcja();
    }
}
