using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public KeyCode pauseGame;
    public KeyCode exitGame;
    public GameObject pauseScreen;

    public GameObject endGameScreen;

    public Text breatheCounterText;
    public Text scoreText;
    public Text fuelText;
    public Text endGameText;
    public Text endGameScoreText;

    public float gameSpeed = 1f;
    public float gameTime = 0f;

    private float breatheCounter = 101;
    private float score = 0;
    private float fuel = 100;

    private bool gameOver = false;

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    void Start()
    {
        PauseGame();
        if (!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetFloat("HighScore", 0);
    }

    private void OnGUI()
    {
        breatheCounterText.text = "Breathe: " + (int)breatheCounter;
        scoreText.text = "Score: " + (int)score;
        fuelText.text = "Fuel: " + (int)fuel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(exitGame))
            ExitGame();
        if (Input.GetKeyDown(pauseGame))
            PauseGame();
        if (breatheCounter <= 0)
        {
            EndGameBreathe();
        }
        gameTime += Time.deltaTime;
        breatheCounter -= 5f * Time.deltaTime * gameSpeed;
        score += 4f * Time.deltaTime * gameSpeed;
        gameSpeed = gameTime / 10 + 1;
    }

    public void ActivateTrusters()
    {
        fuel -= 10 * Time.deltaTime * gameSpeed;
    }

    public bool CheckFuel()
    {
        if (fuel <= 0) return false;
        return true;
    }

    public void AddFuel(int _count)
    {
        fuel += _count;
    }

    public void AddOxygen(int _count)
    {
         breatheCounter += _count;
    }

    public void PauseGame()
    {
        if (!gameOver)
        {
            if (Time.timeScale == 1)
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
        }
        else RestartGame();
    }

    public void EndGameBreathe()
    {
        if (Time.timeScale == 1)
        {
            endGameScreen.SetActive(true);
            Time.timeScale = 0;
        }
        UpdateHighScore();
        endGameText.text = "You have run out of oxygen!";
        endGameScoreText.text = "Score: " + (int)score + "\nHighScore: " + (int)PlayerPrefs.GetFloat("HighScore");
        gameOver = true;
    }

    public void EndGameDestroy()
    {
        if (Time.timeScale == 1)
        {
            endGameScreen.SetActive(true);
            Time.timeScale = 0;
        }
        UpdateHighScore();
        endGameText.text = "You were crushed by a meteor!";
        endGameScoreText.text = "Score: " + (int)score + "\nHighScore: " + (int)PlayerPrefs.GetFloat("HighScore");
        gameOver = true;
    }

    void UpdateHighScore()
    {
        if (PlayerPrefs.GetFloat("HighScore") <= score)
            PlayerPrefs.SetFloat("HighScore", score);
        PlayerPrefs.Save();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
