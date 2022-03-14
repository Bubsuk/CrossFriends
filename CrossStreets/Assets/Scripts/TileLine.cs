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
    private ObstacleType _obstacleType;

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
        if((type != TileType.Grass) && (type != TileType.DarkGrass))
        {
            _moveObstacleSpeed = UnityEngine.Random.Range(1f, 7f);
        }
        _type = type;
       
        gameObject.SetActive(false);
    }

}
