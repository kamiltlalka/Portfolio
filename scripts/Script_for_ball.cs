using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // checking if the ball is in game or not and depend on that it is fixed to paddle or not
        if (!inPlay)
        {
            transform.position = paddle.position;
        }
        if (Input.GetButtonDown("Jump") & !inPlay)
        {
            rbody.AddForce(Vector2.up * speed);
            inPlay = true;
        }

    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("bottom")) //checking if ball colided with bottom edge
        {
            rbody.velocity = Vector2.zero;
            inPlay = false;
            lives = lives - 1; //managing lives
            GameObject overlay = GameObject.Find("Game_overlay");
            Text text = overlay.GetComponentInChildren<Text>();
            text.text = "Lives: " + lives;

            if (lives <= 0)
            {
                Gameover();
            }

        }

    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        // if statments to determine what blocks was hited by the ball. Every block will have seperate compare tag and separate instruction
        if (obj.transform.CompareTag("Brick"))// default statment for brick WIP
        {

          //  Debug.Log("Position - x" + (obj.transform.position.x + 9));
           // Debug.Log("Position - y" + (obj.transform.position.y * 2));
            int typ = obj.gameObject.GetComponent<Brick_vars>().type;

            if (typ == 0) // default block
            {
                obj.gameObject.GetComponent<Brick_vars>().hitpoints -= damage;
                if (obj.gameObject.GetComponent<Brick_vars>().hitpoints <= 0)
                {
                    Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
                    Destroy(toDestroy.gameObject, 2.5f);
                    Destroy(obj.gameObject);
                }
            } else if (typ == 1) // black block
            {
                obj.gameObject.GetComponent<Brick_vars>().hitpoints -= damage;
                if (obj.gameObject.GetComponent<Brick_vars>().hitpoints <= 0)
                {
                    Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
                    Destroy(toDestroy.gameObject, 2.5f);
                    Destroy(obj.gameObject);
                }
                else
                {
                    obj.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                }
            } else if (typ == 2) // bllue block
            {
                Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
                Destroy(toDestroy.gameObject, 2.5f);
                Destroy(obj.gameObject);
                damage = 1;
                if (speed == def_speed)
                {
                    rbody.velocity = new Vector2(rbody.velocity.x * (float)0.5,rbody.velocity.y* (float)0.5);
                    speed = (int)((float)speed * 0.5);
                    Invoke("delay_blue", 10);
                }
                else if(speed>def_speed) // todo slowing when fire is on
                {
                    rbody.velocity = new Vector2(rbody.velocity.x * (float)0.5, rbody.velocity.y * (float)0.5);
                    speed = def_speed;
                }
                


            }
        }
        else if (obj.transform.CompareTag("Hearth"))// instruction for main brick. ending game todo...
        {
            Transform toDestroy = Instantiate(exp, obj.transform.position, obj.transform.rotation);
            Destroy(toDestroy.gameObject, 2.5f);

            Debug.Log("Position - x" + (obj.transform.position.x + 9));
            Debug.Log("Position - y" + (obj.transform.position.y * 2));

            Destroy(obj.gameObject);

            Victory();
            
        }


    }
    void delay_blue() // returning speed after 10 s
    {
        Debug.Log("wykonalo sie invoke");
        rbody.velocity = new Vector2(rbody.velocity.x * (float)2, rbody.velocity.y * (float)2);
        speed = def_speed;
    }


    void Gameover() //handling loosing
    {
        fail.SetActive(true);
        Time.timeScale = 0;
        rbody.velocity = Vector2.zero;
    }

    void Victory() //handling victory
    {
        //todo set level as passed
        //victory screenn
        victory.SetActive(true);
      // Time.timeScale = 0;
        rbody.velocity = Vector2.zero;

    }

}
