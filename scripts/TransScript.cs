using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransScript : MonoBehaviour
{
    public Animator transition;
    public GameObject canv;
    public Rigidbody2D balll;

    void Update()
    {


    }

    public void Loadlevell(int level) //this is function to load scene(name level is misleading XD) at specific number
    {
        StartCoroutine(LoadTransition(level));
    }
    

    public IEnumerator LoadTransition(int scene_nr) 
    {
        transition.SetTrigger("Start");//turning animation on

        yield return new WaitForSeconds(1); // waiting 1s

        SceneManager.LoadScene(scene_nr);//loading scene
    }

    public void ReLoadlevell(int lv) // this reloads the level? idk tbh XD
    {
        StartCoroutine(LoadTransition(lv));
        balll.velocity = Vector2.zero;
        Time.timeScale = 1;
        canv.SetActive(false);
        EscMenu.isPaused = false;

    }

    public void PlayLevel(int level) //this plays level we past in the variable
    {
        MenuVars.level = level;
        Loadlevell(2);
    }
}
