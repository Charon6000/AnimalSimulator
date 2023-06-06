using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GManager : MonoBehaviour
{
    public Swiat swiat;
    public int liczba_tur=0;
    public List<Zwierze> Zwierzeta;
    public List<Roslina> Rosliny;
    public GameObject Instancja;
    public int poczatkowaliczbaroslin = 1;
    public int poczatkowaliczbazwierzat = 2;
    public enum Gatunek{
        Wilk,
        Jez,
        Kret,
        Zmija,
        Owca,
        Guarana,
        Roza
    }
    public Gatunek gatunekmyszy = Gatunek.Wilk;
    public List<GameObject> myszy;
    public GameObject modelmyszy;
    Zwierze a;
    Roslina b;
    System.Random rand = new System.Random();

    private void Update() 
    {
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > -10 && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 10 && Camera.main.ScreenToWorldPoint(Input.mousePosition).z > -10 && Camera.main.ScreenToWorldPoint(Input.mousePosition).z < 10)
        {
        if(modelmyszy!=null)
        Destroy(modelmyszy.gameObject);
        if(gatunekmyszy == Gatunek.Wilk) { modelmyszy = Instantiate(myszy[0], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,0,Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);}
        else if(gatunekmyszy == Gatunek.Owca) { modelmyszy = Instantiate(myszy[1], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,0,Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);}
        else if(gatunekmyszy == Gatunek.Zmija) { modelmyszy = Instantiate(myszy[2], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,0,Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);}
        else if(gatunekmyszy == Gatunek.Jez) { modelmyszy = Instantiate(myszy[3], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,0,Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);}
        else if(gatunekmyszy == Gatunek.Kret) { modelmyszy = Instantiate(myszy[4], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,0,Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);}
        else if(gatunekmyszy == Gatunek.Guarana) { modelmyszy = Instantiate(myszy[5], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,0,Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);}
        else if(gatunekmyszy == Gatunek.Roza) { modelmyszy = Instantiate(myszy[6], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,0,Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);}
            if(Input.GetMouseButtonDown(0))
            {
                if(gatunekmyszy == Gatunek.Roza || gatunekmyszy == Gatunek.Guarana)
                {
                Roslina b = ScriptableObject.CreateInstance("Roslina") as Roslina;
                GameObject ins = Instantiate(Instancja, new Vector3(0,0,0), Quaternion.identity);
                if(gatunekmyszy == Gatunek.Guarana){Roslina a = ScriptableObject.CreateInstance("Roslina") as Roslina; Rosliny[0].KopiujR(b);}
                else if(gatunekmyszy == Gatunek.Roza){Roslina a = ScriptableObject.CreateInstance("Roslina") as Roslina; Rosliny[1].KopiujR(b);}
                b.matka = ins;
                b.Instancja = Instancja;
                b.polozenie = new Vector3(Convert.ToInt32(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),0, Convert.ToInt32(Camera.main.ScreenToWorldPoint(Input.mousePosition).z));
                ins.GetComponent<Instancja>().organizm = b;
                swiat.organizmy.Add(ins);
                }
                else
                {
                    Zwierze a = ScriptableObject.CreateInstance("Zwierze") as Zwierze;
                    GameObject ins = Instantiate(Instancja, new Vector3(0,0,0), Quaternion.identity);
                    if(gatunekmyszy == Gatunek.Wilk){Zwierzeta[3].KopiujZ(a);}
                    else if(gatunekmyszy == Gatunek.Owca){ Zwierzeta[2].KopiujZ(a);}
                    else if(gatunekmyszy == Gatunek.Zmija){ Zwierzeta[4].KopiujZ(a);}
                    else if(gatunekmyszy == Gatunek.Jez){ Zwierzeta[0].KopiujZ(a);}
                    else if(gatunekmyszy == Gatunek.Kret){Zwierzeta[1].KopiujZ(a);}
                    a.matka = ins;
                    a.Instancja = Instancja;
                    a.polozenie = new Vector3(Convert.ToInt32(Camera.main.ScreenToWorldPoint(Input.mousePosition).x),0, Convert.ToInt32(Camera.main.ScreenToWorldPoint(Input.mousePosition).z));
                    ins.GetComponent<Instancja>().organizm = a;
                    swiat.organizmy.Add(ins);
                }
            }
        }
        else { Destroy(modelmyszy); modelmyszy = Instantiate(new GameObject(), new Vector3(0,0,0), Quaternion.identity);}
        //Debug.Log(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z));
    }

    private void Awake() 
    {
        swiat.organizmy.Clear();
        TworzInstancje();
        Invoke("SprawdzKolizje",1f);
    }

    public void Zapisz()
    {
        //Nadpisz swiat w pliku
        PlayerPrefs.SetString("json", JsonUtility.ToJson(swiat));
        Debug.Log(PlayerPrefs.GetString("json"));
    }

    public void Wczytaj()
    {
        swiat.organizmy.Clear();
        //wczytaj swiat z pliku
        if(PlayerPrefs.GetString("json") != null)
        swiat = JsonUtility.FromJson<Swiat>(PlayerPrefs.GetString("json"));
        Invoke("SprawdzKolizje",1f);
    }

    void TworzInstancje()
    {
        foreach (var item in Zwierzeta)
            {
                for (int i = 0; i < poczatkowaliczbazwierzat; i++)
                {
                    GameObject ins = Instantiate(Instancja, new Vector3(0,0,0), Quaternion.identity);
                    Zwierze a = ScriptableObject.CreateInstance("Zwierze") as Zwierze;
                    item.KopiujZ(a);
                    a.matka = ins;
                    a.Instancja = Instancja;
                    a.polozenie = new Vector3(rand.Next(-10,10),0f,rand.Next(-10,10));
                    ins.GetComponent<Instancja>().organizm = a;
                    swiat.organizmy.Add(ins);
                }
            }
            foreach (var item in Rosliny)
            {
                for (int i = 0; i < poczatkowaliczbaroslin; i++)
                {
                    GameObject ins = Instantiate(Instancja, new Vector3(0,0,0), Quaternion.identity);
                    Roslina a = ScriptableObject.CreateInstance("Roslina") as Roslina;
                    item.KopiujR(a);
                    a.matka = ins;
                    a.Instancja = Instancja;
                    a.polozenie = new Vector3(rand.Next(-10,10),0f,rand.Next(-10,10));
                    ins.GetComponent<Instancja>().organizm = a;
                    swiat.organizmy.Add(ins);
                }
            }
    }

    public void WykonajTure()
    {
        swiat.WykonajTure();
        SprawdzKolizje();
    }

    public void WyczyscTure()
    {
        swiat.organizmy.Clear();
        SceneManager.LoadScene(0);
    }

    public void SprawdzKolizje()
    {
        //sprawdz czy kolidują ze sobą zwierzęta
        for (int item = 0; item < swiat.organizmy.Count; item++)
        {
            for (int second = 0; second < swiat.organizmy.Count; second++)
            {
                if(swiat.organizmy[item] != null && swiat.organizmy[second] != null && item != second && swiat.organizmy[item].GetComponent<Instancja>().organizm.polozenie == swiat.organizmy[second].GetComponent<Instancja>().organizm.polozenie)
                {
                    Debug.Log("kolizja " + swiat.organizmy[item].GetComponent<Instancja>().organizm.gatunek + " z " + swiat.organizmy[second].GetComponent<Instancja>().organizm.gatunek);
                    Debug.Log(swiat.organizmy[item].GetComponent<Instancja>().organizm.polozenie);
                    Debug.Log(swiat.organizmy[second].GetComponent<Instancja>().organizm.polozenie);
                    swiat.organizmy[item].GetComponent<Instancja>().organizm.kolizja(swiat.organizmy[second].GetComponent<Instancja>().organizm);
                }
            }
        }
    }
    public void Wilk(){gatunekmyszy = Gatunek.Wilk;}
    public void Owca(){gatunekmyszy = Gatunek.Owca;}
    public void Zmija(){gatunekmyszy = Gatunek.Zmija;}
    public void Jez(){gatunekmyszy = Gatunek.Jez;}
    public void Kret(){gatunekmyszy = Gatunek.Kret;}
    public void Guarana(){gatunekmyszy = Gatunek.Guarana;}
    public void Roza(){gatunekmyszy = Gatunek.Roza;}
}
