using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public int enemiesAlive;
    public int round;
    public GameObject[] spawnPoints;
    public GameObject[] enemyPrefab;

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI countZText;

    public GameObject gameOverPanel;
    public GameObject pausePanel;

    public Animator fadePanelAnimator;

    public TextMeshProUGUI roundSurvivedText;

    public bool isPaused;
    public bool isGameOver;

    public static GameManager sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isPaused = false;
        isGameOver = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        countZText.text = $"Zombies: {enemiesAlive}";

        if (enemiesAlive == 0)
        {
            round++;
            roundText.text = $"Round: {round}";
            NextWave(round);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void NextWave(int round)
    {
        for (int i = 0; i < round; i++)
        {
            int randomPos = Random.Range(0, spawnPoints.Length);
            GameObject spawnPoint = spawnPoints[randomPos];

            GameObject enemyInstance = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], spawnPoint.transform.position, Quaternion.identity);
            enemyInstance.GetComponent<EnemyManager>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        roundSurvivedText.text = round.ToString();

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        isGameOver = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        fadePanelAnimator.SetTrigger("FadeIn");
        Invoke("LoadMainMenuScene", 0.5f);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        AudioListener.volume = 0;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        AudioListener.volume = 1;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }
}
