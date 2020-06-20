using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

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

    private void Awake()
    {
        Instance = this;
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

        levelPrefabs = new GameObject[3];
        levelPrefabs[0] = Resources.Load<GameObject>("Prefabs/LevelGround");
        levelPrefabs[1] = Resources.Load<GameObject>("Prefabs/LevelMid");
        levelPrefabs[2] = Resources.Load<GameObject>("Prefabs/LevelTop");
    }

    private void CreateLevel()
    {
        
        foreach (Transform trans in EnvironmentContainer.GetComponentsInChildren<Transform>())
        {
            if (trans.name == EnvironmentContainer.name)
                continue;
            Destroy(trans.gameObject);
        }

        Instantiate(levelPrefabs[0], EnvironmentContainer.transform);
        for (int i = 0; i < Level; i++)
        {
            GameObject midPanel = Instantiate(levelPrefabs[1], EnvironmentContainer.transform);
            midPanel.transform.localPosition = new Vector3(0, i * 12, 0);
        }
        GameObject endPanel = Instantiate(levelPrefabs[2], EnvironmentContainer.transform);
        endPanel.transform.localPosition = new Vector3(0, Level * 12, 0);
    }

    #region Public methods
    public void LevelComplete()
    {
        AcornSpawner.Instance.Stop();
        LevelCompletePanel.SetActive(true);
    }

    public void GameOver()
    {
        EndGamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("EnemyScene");
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
