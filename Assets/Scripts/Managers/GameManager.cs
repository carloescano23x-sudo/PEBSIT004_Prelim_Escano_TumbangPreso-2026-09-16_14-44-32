using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Loading,
        Ready,
        Playing,
        Paused,
        GameOver
    }

    [Header("Game State")]
    public GameState currentState;

    [Header("Game Data")]
    public int score = 0;
    public int lives = 3;

    [Header("HUD")]
    public TMP_Text scoreText;
    public TMP_Text livesText;

    [Header("Panels")]
    public GameObject readyPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    [Header("Game Over UI")]
    public TMP_Text finalScoreText;
    public TMP_Text bestScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 0f;

        currentState = GameState.Ready;

        score = 0;
        lives = 3;

        readyPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        UpdateUI();

        UnlockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }

    public void StartGame()
    {
        currentState = GameState.Playing;

        readyPanel.SetActive(false);

        Time.timeScale = 1f;

        LockCursor();
    }

    public void AddScore(int amount)
    {
        if (currentState != GameState.Playing)
            return;

        score += amount;

        UpdateUI();

        Debug.Log("Score: " + score);
    }

    public void LoseLife()
    {
        if (currentState != GameState.Playing)
            return;

        if (lives <= 0)
            return;

        lives--;

        UpdateUI();

        Debug.Log("Lives: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void PauseGame()
    {
        if (currentState != GameState.Playing)
            return;

        currentState = GameState.Paused;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        UnlockCursor();
    }

    public void ResumeGame()
    {
        if (currentState != GameState.Paused)
            return;

        currentState = GameState.Playing;

        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        LockCursor();
    }

    void GameOver()
    {
        currentState = GameState.GameOver;

        Time.timeScale = 0f;

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score > bestScore)
        {
            bestScore = score;

            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        finalScoreText.text = "FINAL SCORE: " + score;
        bestScoreText.text = "BEST SCORE: " + bestScore;

        gameOverPanel.SetActive(true);

        UnlockCursor();

        Debug.Log("GAME OVER");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score;
        }

        if (livesText != null)
        {
            livesText.text = "LIVES: " + lives;
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}