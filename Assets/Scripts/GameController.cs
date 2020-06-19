using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _level;
    private float _gameTime;
    private int _score;
    public int Level { get { return _level; } set { _level = value; } }
    public float GameTime { get { return _gameTime; } set { _gameTime = value; } }
    public int Score { get { return _score; } set { _score = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _level = 0;
        _gameTime = 0;
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _gameTime += Time.deltaTime;
    }

    public void LevelComplete()
    {
        _level++;
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
