using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject playerPrefab;
    public CinemachineVirtualCamera camFollow;
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

    [Header("Containers")] //to keep our editor clean
    public Transform acornContainer;
    public Transform seedContainer;
    public Transform branchContainer;
    public Transform fxContainer;
    public Transform vineContainer;
    public GameObject EnvironmentContainer;

    [Header("Sound")]
    public AudioSource WinSource;
    public AudioSource LoseSource;

    private GameObject[] levelPrefabs;
    private PlayerController player;
    private GameObject finish;

    private void Awake()
    {
        Instance = this;
        gameState = GameState.play;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        finish = GameObject.FindGameObjectWithTag("Finish");
    }

    void Start()
    {
        if (!txtScore || !txtGameTime || !EndGamePanel)
            Debug.LogError("Set your canvas game objects");
        EndGamePanel.SetActive(false);
        LevelCompletePanel.SetActive(false);

        InstantiateWorld();
        CreateLevelBackground();        
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

    private void CreateLevelBackground()
    {
        //Destroy background contents
        foreach (Transform trans in EnvironmentContainer.GetComponentsInChildren<Transform>())
        {
            if (trans.name == EnvironmentContainer.name)
                continue;
            Destroy(trans.gameObject);
        }

        //Create the ground/repeated middle/top
        Instantiate(levelPrefabs[0], EnvironmentContainer.transform);
        GameObject midPanel = Instantiate(levelPrefabs[1], EnvironmentContainer.transform);
        midPanel.transform.localPosition = new Vector3(0, 1.02f, 0);
        for (int i = 0; i < Level-1; i++)
        {
            GameObject midPanel2 = Instantiate(levelPrefabs[1], EnvironmentContainer.transform);
            midPanel2.transform.localPosition = new Vector3(0, 1.02f + ((i+1) * 5.6f), 0);
        }
        GameObject canopyPanel = Instantiate(levelPrefabs[2], EnvironmentContainer.transform);
        canopyPanel.transform.localPosition = new Vector3(0, 1.02f+ 2.47f + Level * 5.6f, 0);

        finish.transform.position = new Vector3(finish.transform.position.x, canopyPanel.transform.position.y+2, finish.transform.position.z);

        //Popup for level start
        GameObject levelStart = Instantiate(levelPrefabs[3], txtGameTime.transform.parent);
        levelStart.GetComponentInChildren<Text>().text = "Level " + Level;
    }

    public void ResetLevel()
    {
        //Refresh player
        Destroy(player.gameObject);
        GameObject newPlayer = Instantiate(playerPrefab, new Vector3(0, -4, 0), Quaternion.identity);
        player = newPlayer.GetComponent<PlayerController>();
        camFollow.Follow = newPlayer.transform;

        //Refresh vines
        foreach (Transform trans in vineContainer.GetComponentsInChildren<Transform>())
        {
            if (trans.name == vineContainer.name)
                continue;
            Destroy(trans.gameObject);
        }

        //Refresh branches
        foreach (Transform trans in branchContainer.GetComponentsInChildren<Transform>())
        {
            if (trans.name == branchContainer.name)
                continue;
            Destroy(trans.gameObject);
        }
        GameObject branchPrefab = Resources.Load<GameObject>("Prefabs/branch");
        GameObject branch = Instantiate(branchPrefab, branchContainer);
        branch.transform.position = new Vector3 (0,-5,0);


        //Refresh acorns
        foreach (Transform trans in acornContainer.GetComponentsInChildren<Transform>())
        {
            if (trans.name == acornContainer.name)
                continue;
            Destroy(trans.gameObject);
        }

        //Refresh seeds
        foreach (Transform trans in seedContainer.GetComponentsInChildren<Transform>())
        {
            if (trans.name == seedContainer.name)
                continue;
            Destroy(trans.gameObject);
        }
    }

    #region Public methods
    public void LevelComplete()
    {
        gameState = GameState.gameOver;
        LevelCompletePanel.SetActive(true);
        WinSource.Play();
    }

    public void GameOver()
    {
        if (gameState == GameController.GameState.play)
        {
            gameState = GameState.gameOver;
            EndGamePanel.SetActive(true);
            LoseSource.time = 0.3f;
            LoseSource.Play();
            Time.timeScale = 0;
        }        
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
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
        LevelCompletePanel.SetActive(false);
        gameState = GameState.play;
        _level++;        
        CreateLevelBackground();
        ResetLevel();
    }

    public void AddScore(int val)
    {
        _score+= val;
        txtScore.text = _score.ToString();
    }
    #endregion
}
