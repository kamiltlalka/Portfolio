using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControll : MonoBehaviour
{

    public void Play()
    {

        SceneManager.LoadScene(1); //load level selection scene
    }


}
