using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
    GameObject[] bodies = new GameObject[1000];
    GameManager gameManager;
    public GameObject body, ground;

    public int score = 0;

    //Checks if the snake gets out of the ground boundaries
    //If yes, moves it to the other side of the ground
    void Update()
    {
        int xBoundary = (int)ground.transform.localScale.x / 2;
        int yBoundary = (int)ground.transform.localScale.y / 2;

        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, 0);
        }
        else if (transform.position.x > xBoundary)
        {   
            transform.position = new Vector3(-xBoundary, transform.position.y, 0);
        }

        if (transform.position.y < -yBoundary)
        {
            transform.position = new Vector3(transform.position.x, yBoundary, 0);
        }
        else if (transform.position.y > yBoundary)
        {
            transform.position = new Vector3(transform.position.x, -yBoundary, 0);
        }
            
        //Updates the score on screen
        GameObject textScore = GameObject.Find("Score");
        textScore.GetComponent<Text>().text = "Score: " + score.ToString();
    }

    //Checks triggers
    void OnTriggerEnter2D(Collider2D col)
    {
        //If an apple triggers it, it eats it
        if (col.gameObject.tag == "Apple")
        {
            //Plays sound effect
            AudioSource bite = FindObjectOfType<AudioSource>();
            bite.Play();

            //Raises the score by 1
            score++;

            //Increases the speed of the snake
            if (FindObjectOfType<Snake>().intervalMovement > 0.115f)
            {
                FindObjectOfType<Snake>().intervalMovement -= 0.001f;
            }

            //The apple gets destroyed
            Destroy(col.gameObject);

            //Snake becomes longer by 1
            bodies = GameObject.FindGameObjectsWithTag("Body");
            Instantiate(body, bodies[bodies.Length - 1].transform.position, new Quaternion(0, 0, 0, 0));
        }
        //If a body part triggers it, you lose
        else if(col.gameObject.tag == "Body")
        {
            //Stops the snake
            FindObjectOfType<Snake>().StopMoving();

            //Game over screen pops up
            gameManager = FindObjectOfType<GameManager>();
            gameManager.GameOver();

            //Updates the score on the game over screen
            GameObject endScore = GameObject.Find("EndScore");
            endScore.GetComponent<Text>().text = "Score: " + score.ToString();
        }
    }
}
