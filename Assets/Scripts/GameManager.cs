using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    
    public bool gameIsRunning;
    private float spawnRate = 1.0f;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
    }

    IEnumerator spawnTargets()
    {
        while (gameIsRunning) {
            yield return new WaitForSeconds(spawnRate);
            
            var index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score " + score;
    }

    public void StartGame(int difficulty)
    {
        gameIsRunning = true;
        score = 0;
        spawnRate /= difficulty;

        titleScreen.gameObject.SetActive(false);
        StartCoroutine(spawnTargets());
        UpdateScore(0);
    }

    public void GameOver()
    {
        gameIsRunning = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
