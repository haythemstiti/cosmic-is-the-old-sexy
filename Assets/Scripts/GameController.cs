using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector2 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private bool gameOver;
    private bool restart;
    private int score;
    public Text scoreText;
    public Text highscoreText;
    public Button restartButton;

    // Use this for initialization
    void Start () {
        gameOver = false;
        restart = false;
        UpdateScore();
        PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("Highscore", 0));
        StartCoroutine(SpawnWaves());
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, 3)];
                Vector2 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                PlayerPrefs.SetInt("Highscore", score);
                Advertisement.Show();
                restartButton.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }

    public void GameOver()
    {     
        gameOver = true;
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
