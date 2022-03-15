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
    PlayerController _player;


    private void Update()
    {
        _score.text = $"Score {_player._score}";
        _topScore.text = $"Top {_player._highScore}";
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }
}
