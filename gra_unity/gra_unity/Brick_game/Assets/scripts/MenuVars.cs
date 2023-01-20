using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Klasa przechowująca zmienne globalne w programie
/// </summary>

public class MenuVars : MonoBehaviour
{
    //this script holds most of global variables
    //ingame values:



    public static int level = 1; // curent level to load
    public AudioSource ambient;
    public static int gold; //gold we have
    public static int BG = 1;
    public static int Padle = 1;
    public static int Ball = 1;
    public static bool isplaying;

    /// <summary>
    /// funkcja inicjowana przy załadowaniu obiektu klasy. Ustawia wartości domyślne
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);//to keep this object
        gold = PlayerPrefs.GetInt("Gold", 0);

        BG = PlayerPrefs.GetInt("Background", 1);
        Padle = PlayerPrefs.GetInt("Paddle", 1);
        Ball = PlayerPrefs.GetInt("Ball", 1);

        if (isplaying)
        {

        }
        else
        {
            isplaying = true;
            ambient.Play();
        }

    }
    /// <summary>
    /// klasa przechowująca textury poziomów
    /// </summary>
    class levels
    { 
        public Texture2D lvl; //holds an array of levels
    }

}
