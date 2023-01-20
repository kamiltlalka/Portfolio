using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Klasa odpowiedzialna za generowanie poziomów
/// </summary>

public class LevelGen : MonoBehaviour
{

    public Texture2D[] map;
    public ListBrick[] colorMap;
    public GameObject[,] bricks;

    public GameObject[] ListPaddle;
    public Sprite[] ListBG;
    public GameObject[] ListBall;

    public GameObject fail;
    public GameObject victory;

    public GameObject trans;

    public GameObject BGChange;


    /// <summary>
    /// Funkcja wywoływana przy tworzeniu obiektu klasy. Generuje paletkę, piłkę oraz zmienia tło.
    /// </summary>
    void Start()
    {
        spawner();



        bricks = new GameObject[19, 10]; //array of bricks

        if (MenuVars.level == 20)
            RNG();
        else
            Generete();
        Time.timeScale = 1;
        GameObject overlay = GameObject.Find("Game_overlay");
        Transform tr = overlay.transform.Find("Gold");
        Text text = tr.GetComponent<Text>();
        text.text = "Gold: " + MenuVars.gold;
    }

    /// <summary>
    /// Funkcja do generowania cegieł na ekranie
    /// </summary>

    void Generete() 
    {
        for (int x = 0; x < map[MenuVars.level].width; x++)
        {
            for(int y = 0; y < map[MenuVars.level].width; y++)
            {
                GenerateBlock(x,y);

            }

        }
    }

    
    /// <summary>
    /// Generowanie bloku o podanych koordynatach
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>

    void GenerateBlock(int x, int y) 
    {
        double y2;
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

    /// <summary>
    /// Funkcja generuje paletkę, piłkę i zmienia tło
    /// </summary>

    void spawner() { 
        
        Vector2 PaddlePosition = new Vector2(0, (float)-4.5);
        GameObject padd =  (GameObject)Instantiate(ListPaddle[MenuVars.Padle-1], PaddlePosition, Quaternion.identity, transform);


        Vector2 BallPosition = new Vector2((float)0.02680944, (float)-4.17557);
        GameObject ball = (GameObject) Instantiate(ListBall[MenuVars.Ball-1], BallPosition, Quaternion.identity, transform);

        ball.GetComponent<Script_for_ball>().paddle = padd.transform.GetChild(0);
        ball.GetComponent<Script_for_ball>().victory = victory;
        ball.GetComponent<Script_for_ball>().fail = fail;

        trans.GetComponent<TransScript>().balll = ball.GetComponent<Rigidbody2D>();

        BGChange.GetComponent<Image>().sprite = ListBG[MenuVars.BG-1];
    }

    /// <summary>
    /// Funkcja do losowego generowania bloków
    /// </summary>
    void RNG()
    {

        for (int x = 0; x < 17; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                int r = Random.Range(0, 100);


                if (r < 70)
                {

                }
                else if (r >= 70 && r < 80)
                {
                    bricks[x, y] = Instantiate(colorMap[6].pref, new Vector2(x - 8, (float)(y * 0.5)), Quaternion.identity, transform);
                }
                else if (r >= 80 && r < 85)
                { 
                    bricks[x, y] = Instantiate(colorMap[0].pref, new Vector2(x - 8, (float)(y * 0.5)), Quaternion.identity, transform);
                }
                else if (r >= 85 && r < 87)
                {
                    bricks[x, y] = Instantiate(colorMap[2].pref, new Vector2(x - 8, (float)(y * 0.5)), Quaternion.identity, transform);
                }
                else if (r >= 87 && r < 90)
                {
                    bricks[x, y] = Instantiate(colorMap[3].pref, new Vector2(x - 8, (float)(y * 0.5)), Quaternion.identity, transform);
                }
                else if (r >= 90 && r < 93)
                {
                    bricks[x, y] = Instantiate(colorMap[5].pref, new Vector2(x - 8, (float)(y * 0.5)), Quaternion.identity, transform);
                }
                else if (r >= 93 && r < 98)
                {
                    bricks[x, y] = Instantiate(colorMap[4].pref, new Vector2(x - 8, (float)(y * 0.5)), Quaternion.identity, transform);
                }
                else if (r >= 98)
                {
                    bricks[x, y] = Instantiate(colorMap[7].pref, new Vector2(x - 8, (float)(y * 0.5)), Quaternion.identity, transform);
                }
                


            }

        }
        bool insert = false;
        bool inserted = false;
        for (int x = 0; x < 17; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                int r = Random.Range(0, 100);
                
                if (r >= 80)
                {
                    insert = true;
                }

                if (bricks[x,y]==null)
                {
                    if (insert == true)
                    {
                        bricks[x, y] = Instantiate(colorMap[1].pref, new Vector2(x - 8, (float)(y * 0.5)), Quaternion.identity, transform);
                        inserted = true;
                        break;
                    }

                }

                
            }
            if (inserted == true) break;
        }

    }
}
