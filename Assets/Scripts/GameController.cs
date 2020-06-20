using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _level = 1;
        _gameTime = 0;
        _score = 0;

        if (!txtScore || !txtGameTime || !EndGamePanel)
            Debug.LogError("Set your canvas game objects");
        EndGamePanel.SetActive(false);
        LevelCompletePanel.SetActive(false);
    }

    void Update()
    {
        _gameTime += Time.deltaTime;
        txtGameTime.text = Mathf.FloorToInt(_gameTime).ToString();
    }

    public void LevelComplete()
    {
        _level++;
        AcornSpawner.Instance.Stop();
        LevelCompletePanel.SetActive(true);
    }

    public void GameOver()
    {
        EndGamePanel.SetActive(true);
    }

    public void RestartGame()
    {
        _level = 0;
        _score = 0;
        _gameTime = 0;
    }

    public void AddScore(int val)
    {
        _score+= val;
        txtScore.text = _score.ToString();
    }
}
