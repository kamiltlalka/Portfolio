using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa obsługująca poruszanie paletką
/// </summary>
public class Paddle : MonoBehaviour
{

    public float speed;
    public float screenEdge;

    /// <summary>
    /// Funkcja odpowiedzialna za obliczanie prętkości paletki
    /// </summary>
    void Update()
    {


            transform.Translate(Vector2.right * Input.acceleration.x * Time.deltaTime * speed);


         //it prevents paddle go go out the screen
        if (transform.position.x < (screenEdge * -1))
            transform.position = new Vector2((screenEdge * -1), transform.position.y);
        if (transform.position.x > screenEdge)
            transform.position = new Vector2(screenEdge, transform.position.y);

    }




}
