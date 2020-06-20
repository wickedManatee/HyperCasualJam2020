using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public enum GameState { play, paused, gameOver}
    public GameState gameState;

    private int _level;
    private float _gameTime;
    private int _score;
    public int Level { get { return _level; } set { _level = value; } }
    public float GameTime { get { return _gameTime; } set { _gameTime = value; } }
    public int Score { get { return _score; } set { _score = value; } }

    public Text txtScore;
    public Text txtGameTime;
    public GameObject EndGamePanel;
    public GameObject LevelCompletePanel;

    public GameObject EnvironmentContainer;

    private GameObject[] levelPrefabs;
    private PlayerController player;

    private void Awake()
    {
        Instance = this;
        gameState = GameState.play;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        if (!txtScore || !txtGameTime || !EndGamePanel)
            Debug.LogError("Set your canvas game objects");
        EndGamePanel.SetActive(false);
        LevelCompletePanel.SetActive(false);

        InstantiateWorld();
        CreateLevel();        
    }

    void Update()
    {
        _gameTime += Time.deltaTime;
        txtGameTime.text = Mathf.FloorToInt(_gameTime).ToString();
    }

    private void InstantiateWorld()
    {
        _level = 1;
        _gameTime = 0;
        _score = 0;

        levelPrefabs = new GameObject[4];
        levelPrefabs[0] = Resources.Load<GameObject>("Prefabs/LevelGround");
        levelPrefabs[1] = Resources.Load<GameObject>("Prefabs/LevelMid");
        levelPrefabs[2] = Resources.Load<GameObject>("Prefabs/LevelTop");
        levelPrefabs[3] = Resources.Load<GameObject>("Prefabs/PopupPanel");
    }

    private void CreateLevel()
    {
        //Destroy what's in it
        foreach (Transform trans in EnvironmentContainer.GetComponentsInChildren<Transform>())
        {
            if (trans.name == EnvironmentContainer.name)
                continue;
            Destroy(trans.gameObject);
        }

        //Create the ground/repeated middle/top
        Instantiate(levelPrefabs[0], EnvironmentContainer.transform);
        for (int i = 0; i < Level; i++)
        {
            GameObject midPanel = Instantiate(levelPrefabs[1], EnvironmentContainer.transform);
            midPanel.transform.localPosition = new Vector3(0, i+1 * 6, 0);
        }
        GameObject endPanel = Instantiate(levelPrefabs[2], EnvironmentContainer.transform);
        endPanel.transform.localPosition = new Vector3(0, Level+1 * 6, 0);

        //Popup for level start
        Instantiate(levelPrefabs[3], txtGameTime.transform.parent);
    }

    #region Public methods
    public void LevelComplete()
    {
        gameState = GameState.gameOver;
        LevelCompletePanel.SetActive(true);
    }

    public void GameOver()
    {
        gameState = GameState.gameOver;
        EndGamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("EnemyScene");
    }

    public void Pause()
    {
        gameState = GameState.paused;
        player.Paused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        gameState = GameState.play;
        player.Paused = false;
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        _level++;
        CreateLevel();
    }

    public void AddScore(int val)
    {
        _score+= val;
        txtScore.text = _score.ToString();
    }
    #endregion
}
