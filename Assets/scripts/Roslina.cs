using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Roslina", menuName = "VirtualWorld/Roslina", order = 0)]
public class Roslina : Organizmy
{
    public int prawdopodobienstwo {get;set;}
    float x {get;set;}
    float z {get;set;}
    

    public override void akcja()
    {
        if(wiek ==0)
        {
            wiek = 1;
            rysunek = Instantiate(model,polozenie,Quaternion.identity);
        }
        else if(rand.Next(0,100) <= prawdopodobienstwo || Gatunek.Roza == gatunek)
        {
            wiek++;
            x = 1;
            z = 1;

            while((x+polozenie.x)>10 || (x+polozenie.x)<-10)
            {
                if(rand.Next(0,2)%2 ==1)
                x = 1;
                else
                x=-1;
            }

            while((z+polozenie.z)>10 || (z+polozenie.z)<-10)
            {
                if(rand.Next(0,2)%2 ==1)
                z = 1;
                else
                z=-1;
            }
            GameObject ins = Instantiate(Instancja, new Vector3(0,0,0),Quaternion.identity);
            Roslina a = ScriptableObject.CreateInstance("Roslina") as Roslina;
            KopiujR(a);
            a.matka = ins;
            a.polozenie = new Vector3(x+ polozenie.x,0,z+polozenie.z);
            ins.GetComponent<Instancja>().organizm = a;
            swiat.organizmy.Add(ins);

        }
        else 
        wiek++;
        //z pewnym prawdopodobienstwem może zasiac się na drugim polu
    }

    public override void kolizja(Organizmy org)
    {
        if(org.gatunek != Gatunek.Roza && gatunek == Gatunek.Guarana || org.gatunek != Gatunek.Guarana && gatunek == Gatunek.Guarana)
        {
            Debug.Log("usuwam1");
            org.sila += 3;
            Destroy(rysunek);
            Destroy(matka);
        }
        else if(org.gatunek != Gatunek.Roza && gatunek == Gatunek.Roza || org.gatunek != Gatunek.Guarana && gatunek == Gatunek.Roza)
        {
            Debug.Log("usuwam2");
            Destroy(rysunek);
            Destroy(matka);
        }
        else
        {
            Debug.Log("usuwam3");
            Destroy(org.rysunek);
            Destroy(org.matka);
            Destroy(rysunek);
            Destroy(matka);
        }
    }

     public void KopiujR(Roslina o)
    {
        o.wiek = wiek;
        o.gatunek = gatunek;
        o.rand = rand;
        o.model = model;
        o.sila = sila;
        o.inicjatywa = inicjatywa;
        o.polozenie = polozenie;
        o.swiat = swiat;
        o.prawdopodobienstwo = prawdopodobienstwo;
        o.Instancja = Instancja;
        o.matka = matka;
    }
}
