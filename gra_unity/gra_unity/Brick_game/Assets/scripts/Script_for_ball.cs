using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Klasa odpowiedzialna za poruszanie piłki, obsługę kolizji oraz warunek przegranej oraz wygranej.
/// </summary>

public class Script_for_ball : MonoBehaviour
{

    Rigidbody2D rbody;
    public Transform paddle;
    public bool inPlay;
    public int speed;
    public int def_speed;
    public int lives;
    public Transform exp;
    public GameObject fail;
    public GameObject victory;
    public int damage;
    public Sprite alt1Blackprite;
    public Sprite alt2Blackprite;
    public AudioSource hit;
    GameObject l1;
    GameObject l2;
    GameObject l3;


    /// <summary>
    /// Funkcja ustawiająca wartości domyślne
    /// </summary>
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        lives = 3;

        l1 = GameObject.Find("live1");
        l2 = GameObject.Find("live2");
        l3 = GameObject.Find("live3");
    }

    /// <summary>
    /// Funkcja sprawdzająca stan gry
    /// </summary>
    void Update()
    {
        // checking if the ball is in game or not and depend on that it is fixed to paddle or not
        if (!inPlay)
        {

            if(paddle)
            transform.position = paddle.position;
        }
        if (Input.touchCount>0 & !inPlay)
        {
            rbody.AddForce(Vector2.up * speed);
            inPlay = true;
        }

    }
    /// <summary>
    /// Wykrywanie kolizji z dołem ekranu
    /// </summary>
    /// <param name="obj"></param>
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("bottom")) //checking if ball colided with bottom edge
        {
            rbody.velocity = Vector2.zero;
            inPlay = false;
            lives = lives - 1; //managing lives
            updateLives();


        }

    }

    /// <summary>
    /// Wykrywanie kolizji z resztą otoczenia
    /// </summary>
    /// <param name="obj"></param>

    void OnCollisionEnter2D(Collision2D obj)
    {

        hit.Play();
        // if statments to determine what blocks was hited by the ball. Every block will have seperate compare tag and separate instruction
        if (obj.transform.CompareTag("Brick"))// default statment for brick WIP
        {

            //  Debug.Log("Position - x" + (obj.transform.position.x + 9));
            // Debug.Log("Position - y" + (obj.transform.position.y * 2));
            int typ = obj.gameObject.GetComponent<Brick_vars>().type;

            if (typ == 0) // default block
            {
                hitDefaultBlock(obj);
            }
            else if (typ == 1) // black block
            {
                hitBlackBlock(obj);
            }
            else if (typ == 2) // bllue block
            {
                hitBlueBlock(obj);

            }
            else if (typ == 3)  // Yellow block
            {
                hitYellowBlock(obj);
                
            }
            else if (typ == 4)  // Green block
            {
                hitGreenBlock(obj);
                
            }
            else if (typ == 5)  // magenta block
            {
                hitMagentaBlock(obj);
               
            }

            else if (typ == 6)  // white block
            {
                hitWhiteBlock(obj);
            }
        }
        else if (obj.transform.CompareTag("Hearth"))// instruction for heart brick
        {
            hitHearthBlock(obj);

        }

        
    }
    /// <summary>
    /// Funkcja przywracająca przędkość po zwolnieniu piłki
    /// </summary>
    void delay_blue() // returning speed after 10 s
    {
        Debug.Log("wykonalo sie invoke");
        rbody.velocity = new Vector2(rbody.velocity.x * (float)2, rbody.velocity.y * (float)2);
        speed = def_speed;
    }
    /// <summary>
    /// Funkcja przywracająca przędkość po przyśpieszeniu piłki
    /// </summary>
    void speed_magenta() // returning speed after 10 s
    {
        Debug.Log("wykonalo sie invoke");
        rbody.velocity = new Vector2(rbody.velocity.x * (float)0.5, rbody.velocity.y * (float)0.5);
        speed = def_speed;
    }
    /// <summary>
    /// Funkcja odpowiedzialna za przegraną
    /// </summary>

    void Gameover() //handling loosing
    {
        fail.SetActive(true);
        Time.timeScale = 0;
        rbody.velocity = new Vector2(rbody.velocity.x * 0, rbody.velocity.y * 0);
        //Destroy(rbody);
    }
    /// <summary>
    /// Funkcja odpowiedzialna za wygraną
    /// </summary>
    void Victory() //handling victory
    {
        Debug.Log(MenuVars.level);
        if(PlayerPrefs.GetInt("LevelReached", 1)<= MenuVars.level + 1)
        PlayerPrefs.SetInt("LevelReached", MenuVars.level+1);

        //victory screenn
        victory.SetActive(true);
      // Time.timeScale = 0;
        rbody.velocity = Vector2.zero;

    }
    /// <summary>
    /// Funkcja odpowiedzialna za uderzenie w podstawowy blok
    /// </summary>
    /// <param name="obj"></param>
    void hitDefaultBlock(Collision2D obj)
    {
        exp.gameObject.GetComponent<ParticleSystem>().startColor = Color.cyan;

        obj.gameObject.GetComponent<Brick_vars>().hitpoints -= damage;
        if (obj.gameObject.GetComponent<Brick_vars>().hitpoints <= 0)
        {
            Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
            Destroy(toDestroy.gameObject, 2.5f);
            Destroy(obj.gameObject);
        }
    }
    /// <summary>
    /// Funkcja odpowiedzialna za uderzenie w czarny blok
    /// </summary>
    void hitBlackBlock(Collision2D obj)
    {
        exp.gameObject.GetComponent<ParticleSystem>().startColor = Color.black;

        obj.gameObject.GetComponent<Brick_vars>().hitpoints -= damage;
        if (obj.gameObject.GetComponent<Brick_vars>().hitpoints <= 0)
        {
            Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
            Destroy(toDestroy.gameObject, 2.5f);
            Destroy(obj.gameObject);
        }
        else if (obj.gameObject.GetComponent<Brick_vars>().hitpoints == 1)
        {
            obj.gameObject.GetComponent<SpriteRenderer>().sprite = alt2Blackprite;
        }
        else if (obj.gameObject.GetComponent<Brick_vars>().hitpoints == 2)
        {
            obj.gameObject.GetComponent<SpriteRenderer>().sprite = alt1Blackprite;
        }
    }
    /// <summary>
    /// Funkcja odpowiedzialna za uderzenie w niebieski blok
    /// </summary>
    void hitBlueBlock(Collision2D obj)
    {
        exp.gameObject.GetComponent<ParticleSystem>().startColor = Color.blue;
        Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);

        Destroy(toDestroy.gameObject, 2.5f);
        Destroy(obj.gameObject);
        damage = 1;
        if (speed == def_speed)
        {
            rbody.velocity = new Vector2(rbody.velocity.x * (float)0.5, rbody.velocity.y * (float)0.5);
            speed = (int)((float)speed * 0.5);
            Invoke("delay_blue", 10);
        }
        else if (speed > def_speed) // todo slowing when fire is on
        {
            rbody.velocity = new Vector2(rbody.velocity.x * (float)0.5, rbody.velocity.y * (float)0.5);
            speed = def_speed;
        }


    }
    /// <summary>
    /// Funkcja odpowiedzialna za uderzenie w Żółty blok
    /// </summary>
    void hitYellowBlock(Collision2D obj)
    {
        exp.gameObject.GetComponent<ParticleSystem>().startColor = Color.yellow;

        obj.gameObject.GetComponent<Brick_vars>().hitpoints -= damage;
        if (obj.gameObject.GetComponent<Brick_vars>().hitpoints <= 0)
        {
            Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
            Destroy(toDestroy.gameObject, 2.5f);
            Destroy(obj.gameObject);


            GameObject overlay = GameObject.Find("Game_overlay");
            Transform tr = overlay.transform.Find("Gold");
            Text text = tr.GetComponent<Text>();
            MenuVars.gold = PlayerPrefs.GetInt("Gold");
            MenuVars.gold = MenuVars.gold + 5;
            text.text = "Gold: " + MenuVars.gold;
            PlayerPrefs.SetInt("Gold", MenuVars.gold);

        }
    }
    /// <summary>
    /// Funkcja odpowiedzialna za uderzenie w Zielony blok
    /// </summary>
    void hitGreenBlock(Collision2D obj)
    {
        exp.gameObject.GetComponent<ParticleSystem>().startColor = Color.green;

        obj.gameObject.GetComponent<Brick_vars>().hitpoints -= damage;
        if (obj.gameObject.GetComponent<Brick_vars>().hitpoints <= 0)
        {
            Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
            Destroy(toDestroy.gameObject, 2.5f);
            Destroy(obj.gameObject);


            lives = lives - 1; //managing lives
            updateLives();


        }
    }
    /// <summary>
    /// Funkcja odpowiedzialna za uderzenie w Fuksjowy blok
    /// </summary>
    void hitMagentaBlock(Collision2D obj)
    {
        exp.gameObject.GetComponent<ParticleSystem>().startColor = Color.magenta;
        Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);

        Destroy(toDestroy.gameObject, 2.5f);
        Destroy(obj.gameObject);
        damage = 3;
        if (speed == def_speed)
        {
            rbody.velocity = new Vector2(rbody.velocity.x * (float)2, rbody.velocity.y * (float)2);
            speed = (int)((float)speed * 2);
            Invoke("speed_magenta", 10);
        }
        else if (speed < def_speed) // todo slowing when fire is on
        {
            rbody.velocity = new Vector2(rbody.velocity.x * (float)2, rbody.velocity.y * (float)2);
            speed = def_speed;
        }


    }
    /// <summary>
    /// Funkcja odpowiedzialna za uderzenie w Biały blok
    /// </summary>
    void hitWhiteBlock(Collision2D obj)
    {

        exp.gameObject.GetComponent<ParticleSystem>().startColor = Color.white;

        obj.gameObject.GetComponent<Brick_vars>().hitpoints -= damage;
        if (obj.gameObject.GetComponent<Brick_vars>().hitpoints <= 0)
        {
            Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
            Destroy(toDestroy.gameObject, 2.5f);
            Destroy(obj.gameObject);


            lives = 0; //managing lives
            updateLives();


        }
    }
    /// <summary>
    /// Funkcja odpowiedzialna za uderzenie w Czerwony blok
    /// </summary>
    void hitHearthBlock(Collision2D obj)
    {
        exp.gameObject.GetComponent<ParticleSystem>().startColor = Color.red;

        Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
        Destroy(toDestroy.gameObject, 2.5f);

        Debug.Log("Position - x" + (obj.transform.position.x + 9));
        Debug.Log("Position - y" + (obj.transform.position.y * 2));

        Destroy(obj.gameObject);

        Victory();
    }
   

    /// <summary>
    /// Funkcja zarządzająca wyświetlaniem żyć
    /// </summary>
    void updateLives()
    {
        GameObject overlay = GameObject.Find("Game_overlay");
        // Text text = overlay.GetComponentInChildren<Text>();
        // text.text = "Lives: " + lives;




        if (lives == 0)
        {
            l1.active = false;
            l2.active = false;
            l3.active = false;

            Gameover();
        }

        if (lives == 1)
        {
            l1.active = true;
            l2.active = false;
            l3.active = false;
        }

        if (lives == 2)
        {
            l1.active = true;
            l2.active = true;
            l3.active = false;
        }

        if (lives == 3)
        {
            l1.active = true;
            l2.active = true;
            l3.active = true;
        }

    }
}
