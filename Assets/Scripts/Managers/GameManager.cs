using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // =========================================================
    // INSTANCE
    // =========================================================

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

    // =========================================================
    // DIFFICULTY
    // =========================================================

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
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
    // DIFFICULTY SETTINGS
    // =========================================================

    [Header("Difficulty Settings")]
    public int mediumScoreRequirement = 30;
    public int hardScoreRequirement = 60;

    // =========================================================
    // ROUND TIMER SETTINGS
    // =========================================================

    [Header("Round Timer")]
    public float roundDuration = 60f;

    private float timeRemaining;
    private bool timerExpired = false;

    // =========================================================
    // HUD
    // =========================================================

    [Header("HUD")]
    public GameObject hud;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text difficultyText;
    public TMP_Text timerText;
    public TMP_Text feedbackText;

    // =========================================================
    // FEEDBACK SETTINGS
    // =========================================================

    [Header("Feedback Settings")]
    public float feedbackDuration = 1f;

    private Coroutine feedbackCoroutine;

    // =========================================================
    // PANELS
    // =========================================================

    [Header("Panels")]
    public GameObject loadingPanel;
    public GameObject readyPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    // =========================================================
    // GAME OVER UI
    // =========================================================

    [Header("Game Over UI")]
    public TMP_Text finalScoreText;
    public TMP_Text bestScoreText;
    public TMP_Text gameOverReasonText;

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
    // START
    // =========================================================

    void Start()
    {
        // Reset score and lives.
        score = 0;
        lives = 3;

        // Reset timer.
        timeRemaining = roundDuration;
        timerExpired = false;

        // Update HUD.
        UpdateUI();

        // Hide feedback at startup.
        HideFeedback();

        // Hide everything first.
        if (hud != null)
        {
            hud.SetActive(false);
        }

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

        // Check if PLAY AGAIN caused this reload.
        bool restartDirectly =
            PlayerPrefs.GetInt(
                "RestartDirectly",
                0
            ) == 1;

        if (restartDirectly)
        {
            // Clear restart flag.
            PlayerPrefs.SetInt(
                "RestartDirectly",
                0
            );

            PlayerPrefs.Save();

            // Start immediately.
            currentState =
                GameState.Playing;

            if (hud != null)
            {
                hud.SetActive(true);
            }

            UpdateUI();

            HideFeedback();

            Time.timeScale = 1f;

            LockCursor();

            Debug.Log(
                "NEW ROUND STARTED"
            );
        }
        else
        {
            StartCoroutine(
                LoadingRoutine()
            );
        }
    }

    // =========================================================
    // LOADING
    // =========================================================

    private IEnumerator LoadingRoutine()
    {
        currentState =
            GameState.Loading;

        Time.timeScale = 0f;

        if (hud != null)
        {
            hud.SetActive(false);
        }

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
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

        HideFeedback();

        UnlockCursor();

        Debug.Log(
            "LOADING..."
        );

        // Realtime is required because timeScale = 0.
        yield return new WaitForSecondsRealtime(
            1.5f
        );

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        currentState =
            GameState.Ready;

        if (hud != null)
        {
            hud.SetActive(false);
        }

        if (readyPanel != null)
        {
            readyPanel.SetActive(true);
        }

        HideFeedback();

        UnlockCursor();

        Debug.Log(
            "GAME READY"
        );
    }

    // =========================================================
    // UPDATE
    // =========================================================

    void Update()
    {
        // -----------------------------------------------------
        // ROUND TIMER
        // -----------------------------------------------------

        if (currentState ==
            GameState.Playing)
        {
            UpdateTimer();
        }

        // -----------------------------------------------------
        // PC PAUSE / RESUME
        // -----------------------------------------------------

#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState ==
                GameState.Playing)
            {
                PauseGame();
            }
            else if (currentState ==
                     GameState.Paused)
            {
                ResumeGame();
            }
        }

#endif
    }

    // =========================================================
    // UPDATE ROUND TIMER
    // =========================================================

    private void UpdateTimer()
    {
        if (timerExpired)
        {
            return;
        }

        timeRemaining -=
            Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;

            UpdateTimerUI();

            timerExpired = true;

Debug.Log(
    "TIME IS UP!"
);

GameOver(
    "TIME IS UP!"
);

            return;
        }

        UpdateTimerUI();
    }

    // =========================================================
    // UPDATE TIMER UI
    // =========================================================

    private void UpdateTimerUI()
    {
        if (timerText == null)
        {
            return;
        }

        int totalSeconds =
            Mathf.CeilToInt(
                timeRemaining
            );

        int minutes =
            totalSeconds / 60;

        int seconds =
            totalSeconds % 60;

        timerText.text =
            "TIME: " +
            minutes.ToString("00") +
            ":" +
            seconds.ToString("00");
    }

    // =========================================================
    // SHOW GAMEPLAY FEEDBACK
    // =========================================================

    public void ShowFeedback(string message)
    {
        // Only show feedback during gameplay.
        if (currentState !=
            GameState.Playing)
        {
            return;
        }

        if (feedbackText == null)
        {
            return;
        }

        // Stop old feedback if one is still running.
        if (feedbackCoroutine != null)
        {
            StopCoroutine(
                feedbackCoroutine
            );

            feedbackCoroutine = null;
        }

        feedbackCoroutine =
            StartCoroutine(
                FeedbackRoutine(message)
            );
    }

    // =========================================================
    // FEEDBACK ROUTINE
    // =========================================================

    private IEnumerator FeedbackRoutine(
        string message
    )
    {
        if (feedbackText == null)
        {
            yield break;
        }

        // Set message.
        feedbackText.text =
            message;

        // Show message.
        feedbackText.gameObject.SetActive(
            true
        );

        // Display briefly.
        yield return new WaitForSeconds(
            feedbackDuration
        );

        // Hide after duration.
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(
                false
            );
        }

        feedbackCoroutine = null;
    }

    // =========================================================
    // HIDE FEEDBACK
    // =========================================================

    private void HideFeedback()
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(
                feedbackCoroutine
            );

            feedbackCoroutine = null;
        }

        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(
                false
            );
        }
    }

    // =========================================================
    // START GAME
    // =========================================================

    public void StartGame()
    {
        Debug.Log(
            "START BUTTON CLICKED"
        );

        if (currentState !=
            GameState.Ready)
        {
            Debug.LogWarning(
                "Cannot start. Current state: " +
                currentState
            );

            return;
        }

        currentState =
            GameState.Playing;

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

        if (hud != null)
        {
            hud.SetActive(true);
        }

        // Make sure old feedback is hidden.
        HideFeedback();

        // Refresh HUD.
        UpdateUI();

        // Unfreeze gameplay.
        Time.timeScale = 1f;

        LockCursor();

        Debug.Log(
            "GAME STARTED"
        );
    }

    // =========================================================
    // ADD SCORE
    // =========================================================

    public void AddScore(int amount)
    {
        if (currentState !=
            GameState.Playing)
        {
            return;
        }

        score += amount;

        UpdateUI();

        // Show HIT feedback.
        ShowFeedback(
            "+" + amount + " HIT!"
        );

        Debug.Log(
            "Score: " + score
        );

        Debug.Log(
            "Difficulty: " +
            GetDifficulty()
        );
    }

    // =========================================================
    // LOSE LIFE
    // =========================================================

    public void LoseLife()
    {
        if (currentState !=
            GameState.Playing)
        {
            return;
        }

        if (lives <= 0)
        {
            return;
        }

        lives--;

        UpdateUI();

        Debug.Log(
            "Lives: " + lives
        );

        // If this was the last life,
        // go directly to Game Over.
if (lives <= 0)
{
    GameOver(
        "OUT OF LIVES!"
    );

    return;
}

        // Only show MISS feedback if
        // the player still has lives remaining.
        ShowFeedback(
            "MISS! -1 LIFE"
        );
    }

    // =========================================================
    // GET CURRENT DIFFICULTY
    // =========================================================

    public Difficulty GetDifficulty()
    {
        if (score >=
            hardScoreRequirement)
        {
            return Difficulty.Hard;
        }

        if (score >=
            mediumScoreRequirement)
        {
            return Difficulty.Medium;
        }

        return Difficulty.Easy;
    }

    // =========================================================
    // PAUSE
    // =========================================================

    public void PauseGame()
    {
        if (currentState !=
            GameState.Playing)
        {
            return;
        }

        currentState =
            GameState.Paused;

        if (hud != null)
        {
            hud.SetActive(true);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        Time.timeScale = 0f;

        UnlockCursor();

        Debug.Log(
            "GAME PAUSED"
        );
    }

    // =========================================================
    // RESUME
    // =========================================================

    public void ResumeGame()
    {
        if (currentState !=
            GameState.Paused)
        {
            return;
        }

        currentState =
            GameState.Playing;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (hud != null)
        {
            hud.SetActive(true);
        }

        Time.timeScale = 1f;

        LockCursor();

        Debug.Log(
            "GAME RESUMED"
        );
    }

    // =========================================================
    // GAME OVER
    // =========================================================

    void GameOver(string reason)
    {
        // Prevent GameOver from running twice.
        if (currentState ==
            GameState.GameOver)
        {
            return;
        }

        currentState =
            GameState.GameOver;

        // Remove gameplay feedback.
        HideFeedback();

        // -----------------------------------------------------
        // BEST SCORE
        // -----------------------------------------------------
// Display why the round ended.
if (gameOverReasonText != null)
{
    gameOverReasonText.text =
        reason;
}

        int bestScore =
            PlayerPrefs.GetInt(
                "BestScore",
                0
            );

        if (score > bestScore)
        {
            bestScore = score;

            PlayerPrefs.SetInt(
                "BestScore",
                bestScore
            );

            PlayerPrefs.Save();
        }

        // -----------------------------------------------------
        // GAME OVER TEXT
        // -----------------------------------------------------

        if (finalScoreText != null)
        {
            finalScoreText.text =
                "FINAL SCORE: " +
                score;
        }

        if (bestScoreText != null)
        {
            bestScoreText.text =
                "BEST SCORE: " +
                bestScore;
        }

        // -----------------------------------------------------
        // HIDE GAMEPLAY UI
        // -----------------------------------------------------

        if (hud != null)
        {
            hud.SetActive(false);
        }

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

        // -----------------------------------------------------
        // SHOW GAME OVER
        // -----------------------------------------------------

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Freeze gameplay.
        Time.timeScale = 0f;

        UnlockCursor();

        // Play Game Over SFX.
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameOverSound();
        }

        Debug.Log(
            "GAME OVER"
        );
    }

    // =========================================================
    // PLAY AGAIN
    // =========================================================

    public void RestartGame()
    {
        Debug.Log(
            "PLAY AGAIN CLICKED"
        );

        PlayerPrefs.SetInt(
            "RestartDirectly",
            1
        );

        PlayerPrefs.Save();

        // Scene must be unfrozen before reload.
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager
                .GetActiveScene()
                .buildIndex
        );
    }

    // =========================================================
    // UPDATE HUD
    // =========================================================

    void UpdateUI()
    {
        // -----------------------------------------------------
        // SCORE
        // -----------------------------------------------------

        if (scoreText != null)
        {
            scoreText.text =
                "SCORE: " +
                score;
        }

        // -----------------------------------------------------
        // LIVES
        // -----------------------------------------------------

        if (livesText != null)
        {
            livesText.text =
                "LIVES: " +
                lives;
        }

        // -----------------------------------------------------
        // DIFFICULTY
        // -----------------------------------------------------

        if (difficultyText != null)
        {
            Difficulty currentDifficulty =
                GetDifficulty();

            switch (currentDifficulty)
            {
                case Difficulty.Easy:

                    difficultyText.text =
                        "DIFFICULTY: EASY";

                    break;

                case Difficulty.Medium:

                    difficultyText.text =
                        "DIFFICULTY: MEDIUM";

                    break;

                case Difficulty.Hard:

                    difficultyText.text =
                        "DIFFICULTY: HARD";

                    break;
            }
        }

        // -----------------------------------------------------
        // TIMER
        // -----------------------------------------------------

        UpdateTimerUI();
    }

    // =========================================================
    // LOCK CURSOR
    // =========================================================

    void LockCursor()
    {
#if UNITY_EDITOR || UNITY_STANDALONE

        Cursor.lockState =
            CursorLockMode.Locked;

        Cursor.visible =
            false;

#endif
    }

    // =========================================================
    // UNLOCK CURSOR
    // =========================================================

    void UnlockCursor()
    {
#if UNITY_EDITOR || UNITY_STANDALONE

        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible =
            true;

#endif
    }
}