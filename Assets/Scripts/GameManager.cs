using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
    { 
        Intro,
        Playing,
        Dead
    }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state = GameState.Intro;

    public float PlayStartTime;

    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public Player PlayerScript;

    public TMP_Text scoreText;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
    }

    void Start()
    {
        IntroUI.SetActive(true);
    }

    float CalculateScore()
    {
        return Time.time - PlayStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed()
    {
        if(state != GameState.Playing)
        {
            return 5f;
        }
        float speed = 6f + (0.5f * Mathf.FloorToInt(CalculateScore() / 10f));
        return Mathf.Min(speed, 25f);
    }

    void Update()
    {
        if(state == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        }
        else if(state == GameState.Dead)
        {
            scoreText.text = "High Score: " + GetHighScore(); 
        }

        if(state == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            state = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            PlayStartTime = Time.time;
        }
        if(state == GameState.Playing && Lives == 0)
        {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            DeadUI.SetActive(true);
            state = GameState.Dead;
        }
        if(state == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
