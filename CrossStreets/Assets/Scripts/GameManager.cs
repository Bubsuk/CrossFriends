using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;
    public static GameManager Instance { get { Init(); return s_Instance; } }

    private bool _isGameOver = false;
    public bool _isGameStart = false;
    //public int _score = 0;
    private int _bestScore;

    public bool IsGameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }


    void Start()
    {
        Init();
    }


    static void Init()
    {
        if (s_Instance == null)
        {
            GameObject _gameObject = GameObject.Find("@GameManager");
            if (_gameObject == null)
            {
                _gameObject = new GameObject { name = "@GameManager" };
                _gameObject.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(_gameObject);
            s_Instance = _gameObject.GetComponent<GameManager>();
        }
    }



    public void AddScore()
    {
        if (_isGameOver == false)
        {
            //++_score;
        }

    }

    public void OnPlayerDead()
    {
        _isGameOver = true;
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.IsGameOver = false;
        GameManager.Instance._isGameStart = false;
    }
}
