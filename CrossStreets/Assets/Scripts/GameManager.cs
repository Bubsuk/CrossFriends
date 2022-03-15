using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;
    public static GameManager Instance { get { Init(); return s_Instance; } }

    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private GameObject GameOverUI;


    private bool _isGameOver = false;
    public bool _isGameStart = false;
    private int _score = 0;

    public bool IsGameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }


    void Start()
    {
        Init();
    }

    void Update()
    {
        if(_isGameOver == true)
        {
            //GameOverUI.GetComponent<>
        }
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
            ++_score;
            ScoreText.text = $"Score : {_score}";
        }

    }

    public void OnPlayerDead()
    {
        _isGameOver = true;
        GameOverUI.SetActive(true);
    }
}
