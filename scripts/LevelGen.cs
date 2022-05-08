using UnityEngine;

public class LevelGen : MonoBehaviour
{

    public Texture2D[] map;
    public ListBrick[] colorMap;
    public GameObject[,] bricks;




    // Start is called before the first frame update
    void Start()
    {
        bricks = new GameObject[19, 10]; //array of bricks
        Generete();
        Time.timeScale = 1;
    }

    void Generete() //function to generate bricks on screan
    {
        for (int x = 0; x < map[MenuVars.level].width; x++)
        {
            for(int y = 0; y < map[MenuVars.level].width; y++)
            {
                GenerateBlock(x,y);

            }

        }
    }

    double x2, y2;


    void GenerateBlock(int x, int y) //generate block at specyfic coordinates
    {
        Color pixel = map[MenuVars.level].GetPixel(x, y); //get pixel from map that has number of our global variable

        if (pixel.a == 0) //for alpha i guess
        {



            return;
        }
        foreach (ListBrick colorMaping in colorMap) // loop trough our array of bricks
        {
            if (colorMaping.color.Equals(pixel)) //checking if color maches database
            {
                y2 = y;//some coordinates transformations to mach the grid we are working on
                y2 *=0.5;
                
                Vector2 position = new Vector2((x-9), (float)y2); // setting vector for position
                bricks[x,y] = (GameObject)Instantiate(colorMaping.pref, position, Quaternion.identity, transform); // creating brick and inserting it to the array
            }
        
        
        }
    }


}
