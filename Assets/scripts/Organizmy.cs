using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Organizmy : ScriptableObject 
{
    public GameObject Instancja;
    public GameObject matka;
    public System.Random rand = new System.Random();
    public enum Gatunek{
        Wilk,
        Jez,
        Kret,
        Zmija,
        Owca,
        Guarana,
        Roza
    }
    public Gatunek gatunek;
    public GameObject model;
    public int wiek=0;
    public int sila;
    public int inicjatywa;
    public Vector3 polozenie;
    public Swiat swiat;
    public GameObject rysunek;

    public abstract void akcja();
        //wykonaj akcje w turze

    public abstract void kolizja(Organizmy org);
        //cos robi kiedy zderza się z organizmem

    public void rysowanie()
    {
        //rysuje organizm na planszy w odpowiedniej pozycji i losowej rotacji
    }
}
