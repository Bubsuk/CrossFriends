using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjPool
{
    private TileLine _tile = null;
    public Queue<TileLine> _tileQueue = new Queue<TileLine>();


    public void TileInit(TileLine prefab, TileType type, System.Func<ObstacleType, MoveObstacle> onGetObstacle, System.Action<MoveObstacle> onReturnObstacle)
    {
        _tile = prefab;
        _tileQueue.Enqueue(CreateTile(type, onGetObstacle, onReturnObstacle));
    }

    private TileLine CreateTile(TileType type, System.Func<ObstacleType, MoveObstacle> onGetObstacle, System.Action<MoveObstacle> onReturnObstacle)
    {
        TileLine newObject = GameObject.Instantiate(_tile);
        newObject.Initialize(type);
        if ((type != TileType.DarkGrass) && (type != TileType.Grass))
        {
            newObject.GetComponent<MoveObstacleSpawner>()?.Initialize(newObject, onGetObstacle, onReturnObstacle);
        }

        return newObject;
    }

    public TileLine GetTile()
    {
        TileLine newObj = _tileQueue.Dequeue();

        return newObj;
    }

    public void ReturnTile(TileLine obj)
    {
        obj.gameObject.SetActive(false);
        _tileQueue.Enqueue(obj);
    }
}
