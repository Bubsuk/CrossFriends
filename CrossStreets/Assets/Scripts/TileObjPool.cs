using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjPool
{
    private TileLine _tile = null;
    public Queue<TileLine> _tileQueue = new Queue<TileLine>();

    public void TileInit(TileLine prefab, TileType type)
    {
        _tile = prefab;
        _tileQueue.Enqueue(CreateTile(type));
    }

    private TileLine CreateTile(TileType type)
    {
        TileLine newObject = GameObject.Instantiate(_tile);
        newObject.gameObject.SetActive(false);
        newObject.Type = type;

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
