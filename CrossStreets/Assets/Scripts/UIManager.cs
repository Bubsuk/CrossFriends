using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text _score;
    [SerializeField]
    Text _topScore;
    [SerializeField]
    private GameObject GameOverUI;

    //[SerializeField]
    //PlayerController _player;
    [SerializeField]
    TouchInput _player;

    Button _button;

    private int _bestScore;

    private void Awake()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore");
    }

    private void Update()
    {
        if(_bestScore <= _player._highScore )
        {
            PlayerPrefs.SetInt("BestScore", _player._highScore);
            _bestScore = _player._highScore;
        }
        
        
        _score.text = $"Score {_player._highScore}";
        _topScore.text = $"Top {_bestScore}";

        if (GameManager.Instance.IsGameOver == true)
        {
            GameOverUI.gameObject.SetActive(true);
        }

 
    }

    public void GameRestart()
    {
        GameManager.Instance.GameRestart();
    }
}
