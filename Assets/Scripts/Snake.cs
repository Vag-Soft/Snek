using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    GameObject head;
    GameObject[] bodies = new GameObject[1000];

    public float intervalMovement = 0.25f;

    Queue<string> inputQueue = new Queue<string>();
    string facing = "Right";

    int myScore;

    void Start()
    {
        inputQueue.Enqueue("Right");

        //Finds the head
        head = GameObject.FindWithTag("Head");

        InvokeRepeating("MoveForward", 0.5f, intervalMovement);
        myScore = FindObjectOfType<Head>().score;
    }

    //Checks input
    void Update()
    {
        //Moves the snake with the speed getting higher and higher
        if (FindObjectOfType<Head>().score != myScore && intervalMovement > 0.115f)
        {
            myScore = FindObjectOfType<Head>().score;
            ChangingSpeed();
        }

        if (Input.GetKeyDown(KeyCode.W) && facing != "Down" && facing != "Up")
        {
            inputQueue.Enqueue("Up");
            facing = "Up";
        }
        if (Input.GetKeyDown(KeyCode.S) && facing != "Up" && facing != "Down")
        {
            inputQueue.Enqueue("Down");
            facing = "Down";
        }
        if (Input.GetKeyDown(KeyCode.A) && facing != "Right" && facing != "Left")
        {
            inputQueue.Enqueue("Left");
            facing = "Left";
        }
        if (Input.GetKeyDown(KeyCode.D) && facing != "Left" && facing != "Right")
        {
            inputQueue.Enqueue("Right");
            facing = "Right";
        }
    }

    //Each body part follows the one ahead and the head moves to the direction we choose
    void MoveForward()
    {
        bodies = GameObject.FindGameObjectsWithTag("Body");

        for (int i = bodies.Length - 1; i > 0; i--)
        {
            bodies[i].transform.position = bodies[i - 1].transform.position;
        }

        bodies[0].transform.position = head.transform.position;

        if (inputQueue.Peek() == "Right")
        {
            head.transform.position += new Vector3(1f, 0f, 0f);
            head.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else if (inputQueue.Peek() == "Left")
        {
            head.transform.position += new Vector3(-1f, 0f, 0f);
            head.transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
        }
        else if (inputQueue.Peek() == "Up")
        {
            head.transform.position += new Vector3(0f, 1f, 0f);
            head.transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
        }
        else if (inputQueue.Peek() == "Down")
        {
            head.transform.position += new Vector3(0f, -1f, 0f);
            head.transform.rotation = new Quaternion(90f, 0f, 0f, 0f);
        }

        if (inputQueue.Count > 1)
        {
            inputQueue.Dequeue();
        }
    }

    void ChangingSpeed()
    {
        CancelInvoke("MoveForward");

        InvokeRepeating("MoveForward", 0.1f, intervalMovement);
    }

    public void StopMoving()
    {
        CancelInvoke("MoveForward");
    }
}
