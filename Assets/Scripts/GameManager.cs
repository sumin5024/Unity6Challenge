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

    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public Player PlayerScript;


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

    void Update()
    {
        if(state == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            state = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
        }
        if(state == GameState.Playing && Lives == 0)
        {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            state = GameState.Dead;
        }
        if(state == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
