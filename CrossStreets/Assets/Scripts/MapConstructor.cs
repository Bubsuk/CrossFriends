using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileInfo
{
    [SerializeField]
    private int _creationWeightMin;
    [SerializeField]
    private int _creationWeightMax;
   
    public int MaxWeight
    {
        get { return _creationWeightMax; }
        set { _creationWeightMax = value; }
    }

    public int MinWeight
    {
        get { return _creationWeightMin; }
        set { _creationWeightMin = value; }
    }
}

public class MapConstructor : MonoBehaviour
{
    [SerializeField]
    List<TileInfo> _tilesInfo;

    [SerializeField]
    private TileLine _grassPrefab;
    [SerializeField]
    private TileLine _darkGrassPrefab;
    [SerializeField]
    private TileLine _roadPrefab;
    [SerializeField]
    private TileLine _waterPrefab;
    [SerializeField]
    private TileLine _railPrefab;

    private Queue<TileLine> _grassQueue = new Queue<TileLine>();
    private Queue<TileLine> _roadQueue = new Queue<TileLine>();
    private Queue<TileLine> _waterQueue = new Queue<TileLine>();
    private Queue<TileLine> _railQueue = new Queue<TileLine>();

    private Vector3 _poolPos = new Vector3(0, 0, 160);
    private Vector3 _endPos = new Vector3(0, 0, -40);

    bool _isDestroy = false;

    private void Initialize()
    {
        for (int i = 0; i < 20; ++i)
        {
            if (i % 2 == 0)
            {
                _grassQueue.Enqueue(CreateTileLine(TileType.Grass));
            }
            else
            {
                _grassQueue.Enqueue(CreateTileLine(TileType.DarkGrass));
            }
            _roadQueue.Enqueue(CreateTileLine(TileType.Road));
            _waterQueue.Enqueue(CreateTileLine(TileType.Water));
            _railQueue.Enqueue(CreateTileLine(TileType.Rail));
        }
    }

    private TileLine CreateTileLine(TileType type)
    {
        TileLine newTile;
        switch (type)
        {
            case TileType.Grass:
                newTile = Instantiate(_grassPrefab, _poolPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            case TileType.DarkGrass:
                newTile = Instantiate(_darkGrassPrefab, _poolPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            case TileType.Rail:
                newTile = Instantiate(_railPrefab, _poolPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            case TileType.Road:
                newTile = Instantiate(_roadPrefab, _poolPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            case TileType.Water:
                newTile = Instantiate(_waterPrefab, _poolPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            default:
                break;
        }

        return null;
    }

    private TileLine GetTile(TileType type)
    {
        TileLine newTile;
        switch (type)
        {
            case TileType.Grass:
                if (_grassQueue.Count > 0)
                {
                    newTile = _grassQueue.Dequeue();
                }
                else
                {
                    newTile = CreateTileLine(type);
                }
                return newTile;
            case TileType.Rail:
                if (_railQueue.Count > 0)
                {
                    newTile = _railQueue.Dequeue();
                }
                else
                {
                    newTile = CreateTileLine(type);
                }
                return newTile;
            case TileType.Road:
                if (_roadQueue.Count > 0)
                {
                    newTile = _roadQueue.Dequeue();
                }
                else
                {
                    newTile = CreateTileLine(type);
                }
                return newTile;
            case TileType.Water:
                if(_waterQueue.Count > 0)
                {
                    newTile = _waterQueue.Dequeue();
                }
                else
                {
                    newTile = CreateTileLine(type);
                }
                return newTile;
            default:
                break;
        }

        return null;
    }

    void ReturnTile(TileLine Tile)
    {
        if(Tile.Type == TileType.Grass && Tile.Type == TileType.DarkGrass)
        {
            _grassQueue.Enqueue(Tile);
        }
        else if (Tile.Type == TileType.Rail)
        {
            _railQueue.Enqueue(Tile);
        }
        else if (Tile.Type == TileType.Road)
        {
            _roadQueue.Enqueue(Tile);
        }
        else if (Tile.Type == TileType.Water)
        {
            _waterQueue.Enqueue(Tile);
        }
    }

    private void Awake()
    {
        //_grassPrefab. = TileType.Grass;
        //_grassPrefab.maxWeight = 5;
        //_grassPrefab.minWeight = 1;

        //_darkGrassPrefab.type = TileType.Grass;
        //_darkGrassPrefab.maxWeight = 5;
        //_darkGrassPrefab.minWeight = 1;

        //_roadPrefab.type = TileType.Road;
        //_roadPrefab.maxWeight = 3;
        //_roadPrefab.minWeight = 1;

        //_waterPrefab.type = TileType.Water;
        //_waterPrefab.maxWeight = 2;
        //_waterPrefab.minWeight = 1;

        //_railPrefab.type = TileType.Rail;
        //_railPrefab.maxWeight = 2;
        //_railPrefab.minWeight = 1;

        Initialize();
    }

    void Start()
    {

    }
    void Update()
    {
        if(_isDestroy == true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    GetTile(TileType.Grass).gameObject.SetActive(true);
                    break;
                case 1:
                    GetTile(TileType.Road).gameObject.SetActive(true);
                    break;
                case 2:
                    GetTile(TileType.Water).gameObject.SetActive(true);
                    break;
                case 3:
                    GetTile(TileType.Rail).gameObject.SetActive(true);
                    break;
            }

            _isDestroy = true;
        }


    }
}
