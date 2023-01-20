using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Klasa do załadowania poziomu gry
/// </summary>
public class MenuControll : MonoBehaviour
{
    /// <summary>
    /// funkcja do załadowania sceny gry
    /// </summary>
    public void Play()
    {

        SceneManager.LoadScene(1); //load level selection scene
    }


}
