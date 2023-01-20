using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Klasa obsługująca przejścia pomiędzy scenami
/// </summary>

public class TransScript : MonoBehaviour
{
    public Animator transition;
    public GameObject canv;
    public Rigidbody2D balll;


    /// <summary>
    /// Funkcja do załadowania sceny o zadanym numerze
    /// </summary>
    /// <param name="level"></param>
    public void Loadlevell(int level) //this is function to load scene(name level is misleading XD) at specific number
    {
        StartCoroutine(LoadTransition(level));
    }
    
    /// <summary>
    /// funkcja zmieniająca scenę z przejściem
    /// </summary>
    /// <param name="scene_nr"></param>
    /// <returns></returns>
    public IEnumerator LoadTransition(int scene_nr) 
    {
        transition.SetTrigger("Start");//turning animation on

        yield return new WaitForSeconds(1); // waiting 1s

        SceneManager.LoadScene(scene_nr);//loading scene
    }
    /// <summary>
    /// funkcja do przeładowania poziomu 
    /// </summary>
    /// <param name="lv"></param>
    public void ReLoadlevell(int lv) // this reloads the level? idk tbh XD
    {
        StartCoroutine(LoadTransition(lv));
        balll.velocity = Vector2.zero;
        Time.timeScale = 1;
        canv.SetActive(false);
       // EscMenu.isPaused = false;

    }
    /// <summary>
    /// funkcja do otworzenia poziomu 
    /// </summary>
    /// <param name="level"></param>
    public void PlayLevel(int level) //this plays level we past in the variable
    {
        MenuVars.level = level;
        Loadlevell(2);
    }
}
