using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed;
    public float screenEdge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed); //it prevents paddle go go out the screen
        if (transform.position.x < (screenEdge * -1))
            transform.position = new Vector2((screenEdge * -1), transform.position.y);
        if (transform.position.x > screenEdge)
            transform.position = new Vector2(screenEdge, transform.position.y);




    }
}
