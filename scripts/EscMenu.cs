using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{

    public static bool isPaused;
    public GameObject canv;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)// pause or unpause game and toggle the ui on or off
            {
                Time.timeScale = 0;
                canv.SetActive(true);
                isPaused = true;

            }
            else 
            {
                Time.timeScale = 1;
                canv.SetActive(false);
                isPaused = false;
            }
        
        }
    }

    public void restrume()// additional function to activate game(mainly for buttons)
    {

        Time.timeScale = 1;
        canv.SetActive(false);
        isPaused = false;

    }



}
