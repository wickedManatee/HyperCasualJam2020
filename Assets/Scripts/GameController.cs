using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int _level;
    private float _gameTime;
    private int _score;
    public int Level { get { return _level; } set { _level = value; } }
    public float GameTime { get { return _gameTime; } set { _gameTime = value; } }
    public int Score { get { return _score; } set { _score = value; } }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _level = 0;
        _gameTime = 0;
        _score = 0;
    }

    void Update()
    {
        _gameTime += Time.deltaTime;
    }

    public void LevelComplete()
    {
        _level++;
        AcornSpawner.Instance.Stop();
    }

    public void GameOver()
    {
        Debug.Log("You lose");
        //Show game over panel with score
        //Give option to restart
    }

    public void RestartGame()
    {
        _level = 0;
        _score = 0;
        _gameTime = 0;
    }

    public void AddScore()
    {
        _score++;
    }
}
