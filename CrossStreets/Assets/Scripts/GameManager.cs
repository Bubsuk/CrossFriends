using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager s_Instance;
    static GameManager Instance { get { Init(); return s_Instance; } }

    public Text ScoreText;
    public GameObject GameOverUI;


    

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    static void Init()
    {
        if (s_Instance == null)
        {
            GameObject _gameObject = GameObject.Find("@Managers");
            if (_gameObject == null)
            {
                _gameObject = new GameObject { name = "@Managers" };
                _gameObject.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(_gameObject);
            s_Instance = _gameObject.GetComponent<GameManager>();
        }

    }
}
