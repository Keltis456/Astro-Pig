using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public KeyCode pauseGame;
    public KeyCode exitGame;
    public KeyCode switchBG;

    public GameObject pauseScreen;
    public GameObject endGameScreen;
    public GameObject bg1;
    public GameObject bg2;

    public Text breatheCounterText;
    public Text scoreText;
    public Text fuelText;
    public Text livesText;
    public Text endGameText;
    public Text endGameScoreText;

    public float gameSpeed = 1f;
    public float gameTime = 0f;

    private float breatheCounter = 101;
    private float score = 0;
    private float fuel = 100;
    private int gameSpeedMod = 10;
    private int lives = 3;
    private int currActiveBG = 1;

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
        livesText.text = "Lives: " + (int)lives;
    }

    private void Update()
    {
        if (Input.GetKeyDown(exitGame))
            ExitGame();
        if (Input.GetKeyDown(pauseGame))
            PauseGame();
        if (Input.GetKeyDown(switchBG))
            SwitchBG();
        if (breatheCounter <= 0)
        {
            EndGameBreathe();
        }
        gameTime += Time.deltaTime;
        breatheCounter -= 5f * Time.deltaTime * gameSpeed;
        score += 4f * Time.deltaTime * gameSpeed;
        gameSpeed = gameTime / gameSpeedMod + 1;
    }

    public void DecreaseGameSpeed(int count)
    {
        gameSpeedMod += count;
    }

    public void SwitchBG()
    {
        if (currActiveBG <= 1)
        {
            currActiveBG = 2;
        }
        else
        {
            currActiveBG = 1;
        }
        UpdateBG();
    }

    void UpdateBG()
    {
        switch (currActiveBG)
        {
            case 1:
                bg1.SetActive(true);
                bg2.SetActive(false);
                break;
            case 2:
                bg2.SetActive(true);
                bg1.SetActive(false);
                break;
            default:
                break;
        }
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

    public void TakeDamage(int count)
    {
        lives -= count;
        if (lives <= 0) EndGameDestroy();
    }

    public void AddHP(int count)
    {
        lives += count;
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
