using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstructor : MonoBehaviour
{
    [System.Serializable]
    public class TileLine
    {
        [SerializeField] private GameObject _tilePrefab;
        private TileType _tileType;
        private int _creationWeightMin;
        private int _creationWeightMax;

        public GameObject tile
        {
            get { return _tilePrefab; }
            set { _tilePrefab = value; }
        }
        public TileType type
        {
            get { return _tileType; }
            set { _tileType = value; }
        }
        public  int maxWeight
        {
            get { return _creationWeightMax; }
            set { _creationWeightMax = value; }
        }
        public int minWeight
        {
            get { return _creationWeightMin; }
            set { _creationWeightMin = value; }
        }

    }


    [SerializeField]
    TileLine _grassPrefab;
    [SerializeField]
    TileLine _darkGrassPrefab;
    [SerializeField]
    TileLine _roadPrefab;
    [SerializeField]
    TileLine _waterPrefab;
    [SerializeField]
    TileLine _railPrefab;

    Queue<TileLine> _grassQueue = new Queue<TileLine>();
    Queue<TileLine> _roadQueue = new Queue<TileLine>();
    Queue<TileLine> _waterQueue = new Queue<TileLine>();
    Queue<TileLine> _railQueue = new Queue<TileLine>();

    private Vector3 _poolPos = new Vector3(0, 0, 160);
    private Vector3 _endPos = new Vector3(0, 0, -40);

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
                newTile = Instantiate(_grassPrefab.tile, _poolPos, Quaternion.identity).GetComponent<TileLine>();
                return newTile;
            case TileType.DarkGrass:
                newTile = Instantiate(_darkGrassPrefab.tile, _poolPos, Quaternion.identity).GetComponent<TileLine>();
                return newTile;
            case TileType.Rail:
                newTile = Instantiate(_railPrefab.tile, _poolPos, Quaternion.identity).GetComponent<TileLine>();
                return newTile;
            case TileType.Road:
                newTile = Instantiate(_roadPrefab.tile, _poolPos, Quaternion.identity).GetComponent<TileLine>();
                return newTile;
            case TileType.Water:
                newTile = Instantiate(_waterPrefab.tile, _poolPos, Quaternion.identity).GetComponent<TileLine>();
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
        if(Tile.type == TileType.Grass && Tile.type == TileType.DarkGrass)
        {
            _grassQueue.Enqueue(Tile);
        }
        else if (Tile.type == TileType.Rail)
        {
            _railQueue.Enqueue(Tile);
        }
        else if (Tile.type == TileType.Road)
        {
            _roadQueue.Enqueue(Tile);
        }
        else if (Tile.type == TileType.Water)
        {
            _waterQueue.Enqueue(Tile);
        }
    }

    private void Awake()
    {
        _grassPrefab.type = TileType.Grass;
        _grassPrefab.maxWeight = 5;
        _grassPrefab.minWeight = 1;

        _darkGrassPrefab.type = TileType.Grass;
        _darkGrassPrefab.maxWeight = 5;
        _darkGrassPrefab.minWeight = 1;

        _roadPrefab.type = TileType.Road;
        _roadPrefab.maxWeight = 3;
        _roadPrefab.minWeight = 1;

        _waterPrefab.type = TileType.Water;
        _waterPrefab.maxWeight = 2;
        _waterPrefab.minWeight = 1;

        _railPrefab.type = TileType.Rail;
        _railPrefab.maxWeight = 2;
        _railPrefab.minWeight = 1;

        Initialize();
    }

    void Start()
    {

    }
    void Update()
    {
        int cnt = 0;
        while(true)
        {
            if(cnt > 4)
            {
                cnt = 0;
            }
            if(cnt == 0)
            {

            }

            ++cnt;
        }
    }
}
