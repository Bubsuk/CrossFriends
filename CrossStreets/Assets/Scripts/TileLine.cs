using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLine : MonoBehaviour
{
    public int[] _tileObstacle = new int[20];
    public Obstacle[] _obstacleOwnThis = new Obstacle[20];

    public int _obstacleCode;
    public TileLine _prevTileLine;

    private float _moveObstacleSpeed = 0f;

    private TileType _type;
    private ObstacleType _obstacleType = ObstacleType.Tree;

    public TileType Type
    {
        get { return _type; }
        set { _type = value; }
    }
    public float ObstacleMoveSpeed
    {
        get { return _moveObstacleSpeed; }
    }
    public ObstacleType ObstacleType
    {
        get { return _obstacleType; }
    }

    public void Initialize(TileType type)
    {
        gameObject.SetActive(false);
        if((type != TileType.Grass) && (type != TileType.DarkGrass))
        {
            _moveObstacleSpeed = UnityEngine.Random.Range(1f, 7f);
        }
        
        _type = type;

        if(_type == TileType.Road)
        {
            if(UnityEngine.Random.Range(0, 2) % 2 == 0)
            {
                _obstacleType = ObstacleType.Dragon1;
            }
            else
            {
                _obstacleType = ObstacleType.Dragon2;
            }
        }
        else if(_type == TileType.Rail)
        {
            // 기차 넣기
        }
    }

}
