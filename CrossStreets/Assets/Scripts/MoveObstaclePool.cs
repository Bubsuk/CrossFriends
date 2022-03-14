using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstaclePool
{
    private MoveObstacle _obstacle = null;

    public Queue<MoveObstacle> _obstacleQueue = new Queue<MoveObstacle>();

    public void MoveObstacleInit(MoveObstacle prefab, ObstacleType type)
    {
        _obstacle = prefab;
        _obstacleQueue.Enqueue(CreateMoveObstacle(type));
    }

    private MoveObstacle CreateMoveObstacle(ObstacleType type)
    {
        MoveObstacle newObject = GameObject.Instantiate(_obstacle);
        newObject.gameObject.SetActive(false);
        newObject.Type = type;

        return newObject;
    }

    public MoveObstacle GetMoveObstacle()
    {
        if (_obstacleQueue.Count == 0)
        {
            Debug.LogError("장애물 풀이 비어있음");
        }
        MoveObstacle newObj = _obstacleQueue.Dequeue();

        return newObj;
    }

    public void ReturnMoveObstacle(MoveObstacle obj)
    {
        obj.gameObject.SetActive(false);
        _obstacleQueue.Enqueue(obj);
    }
}
