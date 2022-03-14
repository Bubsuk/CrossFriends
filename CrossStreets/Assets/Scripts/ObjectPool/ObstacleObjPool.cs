using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjPool
{
    private Obstacle _obstacle = null;

    public Queue<Obstacle> _obstacleQueue = new Queue<Obstacle>();

    public void ObstacleInit(Obstacle prefab, ObstacleType type)
    {
        _obstacle = prefab;
        _obstacleQueue.Enqueue(CreateObstacle(type));
    }

    private Obstacle CreateObstacle(ObstacleType type)
    {
        Obstacle newObject = GameObject.Instantiate(_obstacle);
        newObject.gameObject.SetActive(false);
        newObject.Type = type;

        return newObject;
    }

    public Obstacle GetObstacle()
    {
        if(_obstacleQueue.Count == 0)
        {
            Debug.LogError("장애물 풀이 비어있음");
        }
        Obstacle newObj = _obstacleQueue.Dequeue();

        return newObj;
    }

    public void ReturnObstacle(Obstacle obj)
    {
        obj.gameObject.SetActive(false);
        _obstacleQueue.Enqueue(obj);
    }

}
