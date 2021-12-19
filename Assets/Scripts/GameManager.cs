using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    Camera m_MainCamera;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public int livesAmount;
    public Slider musicVolume;
    public bool gameIsPaused = false;
    void Start()
    {
        m_MainCamera = Camera.main;
    }

    
    void Update()
    {
        liveText.text = "Lives: " + livesAmount;
        m_MainCamera.GetComponent<AudioSource>().volume = musicVolume.value;
        if (Input.GetKey(KeyCode.P)&& !gameIsPaused && isGameActive)
        {
            Time.timeScale = 0;
            gameIsPaused = true;
            pauseScreen.gameObject.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.U) && gameIsPaused )
        {
            Time.timeScale = 1;
            gameIsPaused = false;
            pauseScreen.gameObject.SetActive(false);
        }
        
        

    }


    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; 
        scoreText.text = "Score: " + score;
    }

    public void MinusLive()
    {
        if (livesAmount > 0)
        {
            livesAmount -= 1;
        }
        else
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        livesAmount = 3;
    }

    public void StartGame( int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        score = 0;
        spawnRate = spawnRate / difficulty;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
    }
}
