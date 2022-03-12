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

    private TileType _type;

    public TileType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public void Initialize(TileType type)
    {
        gameObject.SetActive(false);
        _type = type;
    }

}
