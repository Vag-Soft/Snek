using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject curApple;
    public GameObject apple, ground, gameOverPanel, pauseBut, resumeBut, pausedHomeBut, pausedPanel, quitBut;

    //Spawns an apple at the start
    void Start()
    {
        Time.timeScale = 1f;

        SpawnApple();
        
        GameObject.Find("HighScore").GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    //Spawns an apple every time the current apple gets eaten
    void Update()
    {
        curApple = GameObject.FindWithTag("Apple");
        if(curApple == null)
        {
            SpawnApple();
        }

        UpdateHighScore();
    }

    //Spawns an apple
    void SpawnApple()
    {
        //Finds a random position
        int xBoundary = (int)ground.transform.localScale.x / 2;
        int yBoundary = (int)ground.transform.localScale.y / 2;

        Vector3 randomPos = new Vector3(Random.Range(-xBoundary, xBoundary + 1), Random.Range(-yBoundary, yBoundary + 1), 0f);

        //Checks if the snake exists on that position (kinda)
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        for(int i = 0; (allObjects[i].tag == "Body" || allObjects[i].tag == "Head") && i < allObjects.Length; i++)
        {
            if (allObjects[i].transform.position == randomPos)
            {
                return;
            }
        }

        //If not, it spawns an apple there
        Instantiate(apple, randomPos, new Quaternion(0, 0, 0, 0));
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        GameObject.Find("HighScore1").GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        pausedPanel.SetActive(true);
        pausedHomeBut.SetActive(true);
        quitBut.SetActive(true);
        pauseBut.SetActive(false);
        resumeBut.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        pausedPanel.SetActive(false);
        pausedHomeBut.SetActive(false);
        quitBut.SetActive(false);
        resumeBut.SetActive(false);
        pauseBut.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void UpdateHighScore()
    {
        if (FindObjectOfType<Head>().score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", FindObjectOfType<Head>().score);
            GameObject.Find("HighScore").GetComponent<Text>().text = "High Score: " + FindObjectOfType<Head>().score.ToString();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
