using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zwierze", menuName = "VirtualWorld/Zwierze", order = 0)]
public class Zwierze : Organizmy
{
    float x {get;set;}
    float z {get;set;}
    public override void akcja()
    {
        if(gatunek != Gatunek.Kret)
        {
            if(wiek==0)
            {
                rysunek = Instantiate(model, polozenie, Quaternion.identity);
                wiek=1;
            }
            else
            { 
                Destroy(rysunek);
                wiek++;
                x = 200;
                z = 200;

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

                polozenie = new Vector3(x+polozenie.x,0,z+polozenie.z);

                rysunek = Instantiate(model,polozenie,Quaternion.identity);
            }
        }
        else
        {
            if(wiek==0)
            {
                polozenie = new Vector3(rand.Next(-10,10),0f,rand.Next(-10,10));
                rysunek = Instantiate(model, polozenie, Quaternion.identity);
                wiek++;
            }
            else
            { 
                Destroy(rysunek);
                wiek++;
                polozenie = new Vector3(rand.Next(-10,10),0f,rand.Next(-10,10));
                rysunek = Instantiate(model, polozenie, Quaternion.identity);
            }
        }
        //rusz się o losowe pole sąsiednie
    }

    public override void kolizja(Organizmy org)
    {
        if(org.gatunek == Gatunek.Guarana)
        {
            sila += 3;
            Destroy(org.rysunek);
            Destroy(org.matka.gameObject);
        }
        else if(org.gatunek == Gatunek.Roza)
        {
            Destroy(org.rysunek);
            Destroy(org.matka.gameObject);
        }
        else if(org.gatunek == Gatunek.Zmija && org.gatunek != gatunek)
        {
            Destroy(org.rysunek);
            Destroy(org.matka);
            Destroy(rysunek);
            Destroy(matka);
        }
        else if(org.gatunek == gatunek)
        {
            // x = 200;
            // z = 200;
            // while((x+polozenie.x)>10 || (x+polozenie.x)<-10)
            // {
            //     if(rand.Next(0,2)%2 ==1)
            //     x = 1;
            //     else
            //     x=-1;
            // }
            // while((z+polozenie.z)>10 || (z+polozenie.z)<-10)
            // {
            //     if(rand.Next(0,2)%2 ==1)
            //     z = 1;
            //     else
            //     z=-1;
            // }
            // Debug.Log(Instancja);
            // GameObject ins = Instantiate(Instancja, new Vector3(0,0,0),Quaternion.identity);
            // Zwierze a = ScriptableObject.CreateInstance("Zwierze") as Zwierze;
            // KopiujZ(a);
            // a.matka = ins;
            // a.polozenie = new Vector3(x+ polozenie.x,0,z+polozenie.z);
            // ins.GetComponent<Instancja>().organizm = a;
            // swiat.organizmy.Add(ins);
        }
        else if(org.sila > sila)
        {
            Destroy(rysunek);
            Destroy(matka);
        }
        else if(org.sila <sila) 
        {
            Destroy(org.rysunek);
            Destroy(org.matka);
        }
        else if(org.sila == sila && org.wiek > wiek)
        {
            Destroy(rysunek);
            Destroy(matka);
        }
        else if(org.sila == sila && org.wiek < wiek)
        {
            Destroy(org.rysunek);
            Destroy(org.matka);
        }
        //kiedy są tego samego gatunku zostają w miejscu i tworzą kolejnego zwierzaka obok ich
        //kiedy są innego gatunku walczą
    }

    public void KopiujZ(Zwierze o)
    {
        o.wiek = wiek;
        o.gatunek = gatunek;
        o.rand = rand;
        o.model = model;
        o.sila = sila;
        o.inicjatywa = inicjatywa;
        o.polozenie = polozenie;
        o.swiat = swiat;
        o.Instancja = Instancja;
        o.matka = matka;
    }
}
