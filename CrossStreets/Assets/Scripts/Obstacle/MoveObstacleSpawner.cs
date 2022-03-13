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

    private System.Func<ObstacleType, Obstacle> _onGetObstacle;

    public TileLine BaseTile
    {
        set { _baseTile = value; }
    }

    public void Initialize(TileLine baseTile, System.Func<ObstacleType, Obstacle> onGetObstacle)
    {
        _baseTile = baseTile;
        _onGetObstacle = onGetObstacle;
        if (Random.Range(0, 2) % 2 == 0)
        {
            transform.position = new Vector3(-75, 1, _baseTile.gameObject.transform.position.z);
            _isLeft = true;
        }
        else
        {
            _isLeft = false;
            transform.position = new Vector3(75, 1, _baseTile.gameObject.transform.position.z);
        }
    }

    private void Awake()
    {
        _obstacleMoveSpeed = Random.Range(1, 7);
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime > Random.Range(2.5f, 4f))
        {
            
            SpawnObstacle(_baseTile.ObstacleType);
            _elapsedTime = 0f;
        }

    }

    private MoveObstacle SpawnObstacle(ObstacleType type)
    {
        MoveObstacle newObstacle = (MoveObstacle)_onGetObstacle(_baseTile.ObstacleType);
        if (_isLeft == true)
        {
            newObstacle.MoveDir = true;
        }
        else
        {
            newObstacle.MoveDir = false;
        }
        newObstacle.MoveSpeed = _obstacleMoveSpeed;
        newObstacle.SpawnPos = transform.position;
        newObstacle.gameObject.SetActive(true);


        return newObstacle;
    }


}
