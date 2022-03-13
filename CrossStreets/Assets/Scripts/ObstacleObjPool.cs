using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjPool
{
    private TreeObstacle _obstacle = null;

    public Queue<TreeObstacle> _obstacleQueue = new Queue<TreeObstacle>();

    public void ObstacleInit(TreeObstacle prefab, ObstacleType type)
    {
        _obstacle = prefab;
        _obstacleQueue.Enqueue(CreateObstacle(type));
    }

    private TreeObstacle CreateObstacle(ObstacleType type)
    {
        TreeObstacle newObject = GameObject.Instantiate(_obstacle);
        newObject.gameObject.SetActive(false);
        newObject.Type = type;

        return newObject;
    }

    public TreeObstacle GetObstacle()
    {
        if(_obstacleQueue.Count == 0)
        {
            Debug.LogError("장애물 풀이 비어있음");
        }
        TreeObstacle newObj = _obstacleQueue.Dequeue();

        return newObj;
    }

    public void ReturnObstacle(TreeObstacle obj)
    {
        obj.gameObject.SetActive(false);
        _obstacleQueue.Enqueue(obj);
    }

}
