using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Klasa odpowiedzialna za zarz¹dzanie sklepem
/// </summary>
public class TradeHandling : MonoBehaviour
{

    public GameObject[] ShopList;
    public Text goldT;

    /// <summary>
    /// Ustawianie domyœlnych wartoœci sklepu na starcie
    /// </summary>
    void Start()
    {
        MenuVars.gold = PlayerPrefs.GetInt("Gold", 0);
        goldT.text = "Gold: " + MenuVars.gold;
        reload();
       



    }

    /// <summary>
    /// Prze³adowanie zawartoœci sklepu
    /// </summary>
    private void reload()
    {
        foreach (GameObject loopObj in ShopList)
        {

            if (PlayerPrefs.GetInt(loopObj.name, 0) == 1)
                loopObj.GetComponentInChildren<TMP_Text>().text = "Kupione";



            if ((string.Equals(loopObj.name, ("Paddle" + MenuVars.Padle))) || (string.Equals(loopObj.name, ("BG" + MenuVars.BG))) || (string.Equals(loopObj.name, ("Ball" + MenuVars.Ball))))
                loopObj.GetComponent<Image>().color = Color.yellow;
            else
                loopObj.GetComponent<Image>().color = Color.white;
        }

    }
    /// <summary>
    /// Zakup przedmiotu
    /// </summary>
    /// <param name="number"></param>
    public void BuyItem(int number)
    {
        string name = ShopList[number].name;
        int value = (int)ShopList[number].transform.position.z;

        if (PlayerPrefs.GetInt(name,0)==1)
        {
            //set active
            Debug.Log("already b");

            if(string.Compare(name, "padd", true) == 1) {
                Debug.Log(int.Parse(name.Remove(0,6)));
                MenuVars.Padle = int.Parse(name.Remove(0, 6));
                PlayerPrefs.SetInt("Paddle", MenuVars.Padle);
                 }
            else if(string.Compare(name, "bg", true) == 1) {
                Debug.Log(int.Parse(name.Remove(0, 2)));
                MenuVars.BG = int.Parse(name.Remove(0, 2));
                PlayerPrefs.SetInt("Background", MenuVars.BG);
            }
            else if(string.Compare(name, "ball", true) == 1) { Debug.Log(int.Parse(name.Remove(0, 4)));
                MenuVars.Ball = int.Parse(name.Remove(0, 4));
                PlayerPrefs.SetInt("Ball", MenuVars.Ball);
            }
            reload(); 
        }
        else
        {
            if (MenuVars.gold >= value)
            {
                PlayerPrefs.SetInt(name, 1);
                PlayerPrefs.SetInt("Gold", MenuVars.gold - value);
                MenuVars.gold = PlayerPrefs.GetInt("Gold", 0);
                Debug.Log("bought");
                goldT.text = "Gold: " + MenuVars.gold;

                reload();
            }


        }

    }

}
