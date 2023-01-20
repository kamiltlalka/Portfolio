using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Klasa odpowiedzialna za aktywacje poziomów w menu poziomów
/// </summary>

public class LevelUnlocks : MonoBehaviour
{

    public Button[] buttons;


    /// <summary>
    /// Funkcja wywo³ywana przy za³adowaniu obiektu klasy. Odblokowywuje przyciski od poziomów.
    /// </summary>
    void Start()
    {
        int LevelReached = PlayerPrefs.GetInt("LevelReached", 0);

        for (int i = 0; i < buttons.Length; i++)
        {
            if(i>LevelReached)

            buttons[i].interactable = false;
        }
    }


}
