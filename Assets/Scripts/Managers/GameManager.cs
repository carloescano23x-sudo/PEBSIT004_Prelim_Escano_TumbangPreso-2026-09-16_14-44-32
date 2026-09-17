using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // =========================================================
    // GAME STATES
    // =========================================================

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

    // =========================================================
    // GAME DATA
    // =========================================================

    [Header("Game Data")]
    public int score = 0;
    public int lives = 3;

    // =========================================================
    // HUD
    // =========================================================

    [Header("HUD")]
    public TMP_Text scoreText;
    public TMP_Text livesText;

    // =========================================================
    // PANELS
    // =========================================================

    [Header("Panels")]
    public GameObject readyPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject loadingPanel;

    // =========================================================
    // GAME OVER UI
    // =========================================================

    [Header("Game Over UI")]
    public TMP_Text finalScoreText;
    public TMP_Text bestScoreText;

    // =========================================================
    // AWAKE
    // =========================================================

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

    // =========================================================
    // START - PART 86
    // =========================================================

    void Start()
    {
        // Reset round data
        score = 0;
        lives = 3;

        UpdateUI();

        // Hide all panels first
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        if (readyPanel != null)
        {
            readyPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Check whether PLAY AGAIN caused this scene reload
        bool restartDirectly =
            PlayerPrefs.GetInt("RestartDirectly", 0) == 1;

        if (restartDirectly)
        {
            // Clear restart flag
            PlayerPrefs.SetInt(
                "RestartDirectly",
                0
            );

            PlayerPrefs.Save();

            // Immediately start a new round
            currentState = GameState.Playing;

            Time.timeScale = 1f;

            LockCursor();

            Debug.Log("NEW ROUND STARTED");
        }
        else
        {
            // First launch:
            // Loading -> Ready
            StartCoroutine(LoadingRoutine());
        }
    }

    // =========================================================
    // LOADING ROUTINE - PART 85
    // =========================================================

    private IEnumerator LoadingRoutine()
    {
        currentState = GameState.Loading;

        // Freeze normal gameplay
        Time.timeScale = 0f;

        // Show loading screen
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }

        // Hide other panels
        if (readyPanel != null)
        {
            readyPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Cursor should be available during loading
        UnlockCursor();

        Debug.Log("LOADING...");

        // IMPORTANT:
        // Realtime is used because Time.timeScale = 0
        yield return new WaitForSecondsRealtime(1.5f);

        // Hide loading screen
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        // Change to Ready state
        currentState = GameState.Ready;

        // Show Ready screen
        if (readyPanel != null)
        {
            readyPanel.SetActive(true);
        }

        UnlockCursor();

        Debug.Log("GAME READY");
    }

    // =========================================================
    // UPDATE
    // =========================================================

    void Update()
    {
        // ESC = Pause / Resume
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

    // =========================================================
    // START GAME
    // =========================================================

    public void StartGame()
    {
        if (currentState != GameState.Ready)
        {
            return;
        }

        currentState = GameState.Playing;

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        if (readyPanel != null)
        {
            readyPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        Time.timeScale = 1f;

        LockCursor();

        Debug.Log("GAME STARTED");
    }

    // =========================================================
    // ADD SCORE
    // =========================================================

    public void AddScore(int amount)
    {
        if (currentState != GameState.Playing)
        {
            return;
        }

        score += amount;

        UpdateUI();

        Debug.Log("Score: " + score);
    }

    // =========================================================
    // LOSE LIFE
    // =========================================================

    public void LoseLife()
    {
        if (currentState != GameState.Playing)
        {
            return;
        }

        if (lives <= 0)
        {
            return;
        }

        lives--;

        UpdateUI();

        Debug.Log("Lives: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    // =========================================================
    // PAUSE GAME
    // =========================================================

    public void PauseGame()
    {
        if (currentState != GameState.Playing)
        {
            return;
        }

        currentState = GameState.Paused;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        Time.timeScale = 0f;

        UnlockCursor();

        Debug.Log("GAME PAUSED");
    }

    // =========================================================
    // RESUME GAME
    // =========================================================

    public void ResumeGame()
    {
        if (currentState != GameState.Paused)
        {
            return;
        }

        currentState = GameState.Playing;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        Time.timeScale = 1f;

        LockCursor();

        Debug.Log("GAME RESUMED");
    }

    // =========================================================
    // GAME OVER
    // =========================================================

    void GameOver()
    {
        if (currentState == GameState.GameOver)
        {
            return;
        }

        currentState = GameState.GameOver;

        // Get saved best score
        int bestScore =
            PlayerPrefs.GetInt("BestScore", 0);

        // Check for new best score
        if (score > bestScore)
        {
            bestScore = score;

            PlayerPrefs.SetInt(
                "BestScore",
                bestScore
            );

            PlayerPrefs.Save();
        }

        // Update final score
        if (finalScoreText != null)
        {
            finalScoreText.text =
                "FINAL SCORE: " + score;
        }

        // Update best score
        if (bestScoreText != null)
        {
            bestScoreText.text =
                "BEST SCORE: " + bestScore;
        }

        // Hide other panels
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        if (readyPanel != null)
        {
            readyPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        // Show Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Freeze gameplay
        Time.timeScale = 0f;

        // Show cursor for PLAY AGAIN
        UnlockCursor();

        Debug.Log("GAME OVER");
    }

    // =========================================================
    // RESTART / PLAY AGAIN
    // =========================================================

    public void RestartGame()
    {
        Debug.Log("PLAY AGAIN CLICKED");

        // Tell the next scene load to skip
        // Loading and Ready
        PlayerPrefs.SetInt(
            "RestartDirectly",
            1
        );

        PlayerPrefs.Save();

        // Unfreeze before reloading
        Time.timeScale = 1f;

        // Reload current scene
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // =========================================================
    // UPDATE HUD
    // =========================================================

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "SCORE: " + score;
        }

        if (livesText != null)
        {
            livesText.text =
                "LIVES: " + lives;
        }
    }

    // =========================================================
    // LOCK CURSOR
    // =========================================================

    void LockCursor()
    {
        Cursor.lockState =
            CursorLockMode.Locked;

        Cursor.visible = false;
    }

    // =========================================================
    // UNLOCK CURSOR
    // =========================================================

    void UnlockCursor()
    {
        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;
    }
}