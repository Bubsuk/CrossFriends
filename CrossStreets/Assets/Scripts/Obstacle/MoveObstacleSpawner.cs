using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacleSpawner : MonoBehaviour
{
    private TileLine _baseTile;

    private MapConstructor _mapConstructor;
    private bool _isLeft = false;
    private float _obstacleMoveSpeed = 0f;
    float _elapsedTime = 0f;
    float _respawnDurationTime;

    [SerializeField]
    private Transform _leftSpawnPos;
    [SerializeField]
    private Transform _rightSpawnPos;

    private ObstacleType _obstacleType;

    private System.Func<ObstacleType, MoveObstacle> _onGetObstacle;

    public TileLine BaseTile
    {
        set { _baseTile = value; }
    }

    public void Initialize(TileLine baseTile, System.Func<ObstacleType, MoveObstacle> onGetObstacle)
    {
        _baseTile = baseTile;
        _onGetObstacle = onGetObstacle;
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
                _obstacleType = ObstacleType.Dragon1;
                _respawnDurationTime = Random.Range(1.5f, 3f);
            }
            else
            {
                _obstacleType = ObstacleType.Dragon2;
                _respawnDurationTime = Random.Range(1.5f, 3f);
            }
        }
        else if (baseTile.Type == TileType.Water)
        {
            _obstacleType = ObstacleType.FloatingLog;
            _respawnDurationTime = Random.Range(1.5f, 3f);
        }
        else if (baseTile.Type == TileType.Rail)
        {
            _obstacleType = ObstacleType.Train;
            _respawnDurationTime = 4f;
        }

        _obstacleMoveSpeed = Random.Range(8, 20);
        
    }


    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if(_baseTile == null)
        {
            Debug.Log("BaseTile NULL");
        }

        if (_baseTile.gameObject.activeSelf == true && _elapsedTime > _respawnDurationTime)
        {
            SpawnObstacle(_baseTile.ObstacleType);
            _elapsedTime = 0f;
        }

    }

    private MoveObstacle SpawnObstacle(ObstacleType type)
    {
        MoveObstacle newObstacle = _onGetObstacle(_obstacleType);

        if(type != ObstacleType.FloatingLog)
        {
            newObstacle.transform.rotation = (_isLeft ? Quaternion.Euler(new Vector3(0, 90, 0)) : Quaternion.Euler(new Vector3(0, -90, 0)));
        }
        newObstacle.MoveSpeed = _obstacleMoveSpeed;
        newObstacle.MoveDir = (_isLeft ? true : false);
        newObstacle.gameObject.transform.position = (_isLeft ? _leftSpawnPos.position : _rightSpawnPos.position);
        newObstacle.gameObject.SetActive(true);

        return newObstacle;
    }

}