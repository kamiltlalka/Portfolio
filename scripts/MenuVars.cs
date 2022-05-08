using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVars : MonoBehaviour
{
    //this script holds most of global variables
    //ingame values:



    public static int level = 1; // curent level to load
    public int image; //idk i have to check this XD

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);//to keep this object

    }

    class levels
    { 
        public Texture2D lvl; //holds an array of levels
    }

}
