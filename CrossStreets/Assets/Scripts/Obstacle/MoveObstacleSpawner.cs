using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacleSpawner : MonoBehaviour
{
    private TileLine _baseTile;

    private MapConstructor _mapConstructor;
    private bool _isLeft = false;
    private float _obstacleMoveSpeed = 0f;
    private float _logMoveSpeed = 0f;
    private float _trainMoveSpeed = 0f;
    float _elapsedTime = 0f;
    float _respawnDurationTime = 0f;

    [SerializeField]
    private Transform _leftSpawnPos;
    [SerializeField]
    private Transform _rightSpawnPos;

    private ObstacleType _spawnerObstacleType;

    private System.Func<ObstacleType, MoveObstacle> _onGetObstacle;
    private System.Action<MoveObstacle> _onReturnObstacle;

    public TileLine BaseTile
    {
        set { _baseTile = value; }
    }

    public void Initialize(TileLine baseTile, System.Func<ObstacleType, MoveObstacle> onGetObstacle, System.Action<MoveObstacle> onReturnObstacle)
    {
        _baseTile = baseTile;
        _onGetObstacle = onGetObstacle;
        _onReturnObstacle = onReturnObstacle;
        if (Random.Range(0, 2) == 0)
        {
            _isLeft = true;
        }
        else
        {
            _isLeft = false;
        }

        if (baseTile.Type == TileType.Road)
        {
            if (UnityEngine.Random.Range(0, 2) % 2 == 0)
            {
                _spawnerObstacleType = ObstacleType.Dragon1;
                _respawnDurationTime = Random.Range(1.5f, 3f);
            }
            else
            {
                _spawnerObstacleType = ObstacleType.Dragon2;
                _respawnDurationTime = Random.Range(1.5f, 3f);
            }
        }
        else if (baseTile.Type == TileType.Water)
        {
            _spawnerObstacleType = ObstacleType.FloatingLog;
            _respawnDurationTime = Random.Range(3f, 5f);
        }
        else if (baseTile.Type == TileType.Rail)
        {
            _spawnerObstacleType = ObstacleType.Train;
            _respawnDurationTime = 7f;
        }

        _obstacleMoveSpeed = Random.Range(8, 20);
        _logMoveSpeed = Random.Range(8, 12);
        _trainMoveSpeed = 70f;

    }


    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_baseTile.gameObject.activeSelf == true && _elapsedTime > _respawnDurationTime)
        {
            SpawnObstacle();

            _elapsedTime = 0f;
        }

    }

    private MoveObstacle SpawnObstacle()
    {
        MoveObstacle newObstacle = _onGetObstacle(_spawnerObstacleType);
        newObstacle.Initialize(_onReturnObstacle);

        if (_spawnerObstacleType == ObstacleType.FloatingLog)
        {
            newObstacle.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            newObstacle.MoveSpeed = _logMoveSpeed;
        }
        else if (_spawnerObstacleType == ObstacleType.Train)
        {
            newObstacle.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            newObstacle.MoveSpeed = _trainMoveSpeed;
        }
        else
        {
            newObstacle.transform.rotation = (_isLeft ? Quaternion.Euler(new Vector3(0f, 90f, 0f)) : Quaternion.Euler(new Vector3(0f, -90f, 0f)));
            newObstacle.MoveSpeed = _obstacleMoveSpeed;
        }

        newObstacle.MoveDir = (_isLeft ? true : false);
        newObstacle.gameObject.transform.position = (_isLeft ? _leftSpawnPos.position : _rightSpawnPos.position);
        newObstacle.gameObject.SetActive(true);

        return newObstacle;
    }

}