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
    private Queue<TileLine> _darkGrassQueue = new Queue<TileLine>();
    private Queue<TileLine> _roadQueue = new Queue<TileLine>();
    private Queue<TileLine> _waterQueue = new Queue<TileLine>();
    private Queue<TileLine> _railQueue = new Queue<TileLine>();

    private Queue<TileType> _sequenceTileType = new Queue<TileType>();

    private Vector3 _spawnPos = new Vector3(0, 0, 160);

    private TileLine _lastTileLine;

    [SerializeField]
    private int _tileSize = 4;
    [SerializeField]
    private int _startTileAmount = 30;
    private TileLine[] _startTile = new TileLine[30];

    

    private void Initialize()
    {
        while(_sequenceTileType.Count < _startTileAmount)
        {
            TileSequenceMaker();
        }

        for (int i = 0; i < 15; ++i)
        {
            _grassQueue.Enqueue(CreateTileLine(TileType.Grass));
            _darkGrassQueue.Enqueue(CreateTileLine(TileType.DarkGrass));
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
                newTile = Instantiate(_grassPrefab, _spawnPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            case TileType.DarkGrass:
                newTile = Instantiate(_darkGrassPrefab, _spawnPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            case TileType.Rail:
                newTile = Instantiate(_railPrefab, _spawnPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            case TileType.Road:
                newTile = Instantiate(_roadPrefab, _spawnPos, Quaternion.identity);
                newTile.Initialize(type);
                return newTile;
            case TileType.Water:
                newTile = Instantiate(_waterPrefab, _spawnPos, Quaternion.identity);
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
            case TileType.DarkGrass:
                if (_grassQueue.Count > 0)
                {
                    newTile = _darkGrassQueue.Dequeue();
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

    int tempWeight = 0;
    private void TileSequenceMaker()
    {
        // _tilesInfo[0] = grass
        // _tilesInfo[1] = road
        // _tilesInfo[2] = water
        // _tilesInfo[3] = rail

        int _weightRand = Random.Range(0, 4);
        
        while(_weightRand == tempWeight)
        {
            _weightRand = Random.Range(0, 4);
        }

        int rand;
        switch (_weightRand)
        {
            case (int)TileType.Grass:
                rand = Random.Range(_tilesInfo[0].MinWeight, _tilesInfo[0].MaxWeight);
                for (int i = 0; i < rand; ++i)
                {
                    _sequenceTileType.Enqueue(TileType.Grass);
                    _sequenceTileType.Enqueue(TileType.DarkGrass);
                }
                break;
            case (int)TileType.Road:
                rand = Random.Range(_tilesInfo[1].MinWeight, _tilesInfo[1].MaxWeight);
                for (int i = 0; i < rand; ++i)
                {
                    _sequenceTileType.Enqueue(TileType.Road);
                }
                break;
            case (int)TileType.Water:
                rand = Random.Range(_tilesInfo[2].MinWeight, _tilesInfo[2].MaxWeight);
                for (int i = 0; i < rand; ++i)
                {
                    _sequenceTileType.Enqueue(TileType.Water);
                }
                break;
            case (int)TileType.Rail:
                rand = Random.Range(_tilesInfo[3].MinWeight, _tilesInfo[3].MaxWeight);
                for (int i = 0; i < rand; ++i)
                {
                    _sequenceTileType.Enqueue(TileType.Rail);
                }
                break;
            default:
                break;
        }
     
        
        tempWeight = _weightRand;

    }




    public void ReturnTile(TileLine Tile)
    {
        Tile.gameObject.SetActive(false);
        if (Tile.Type == TileType.Grass)
        {
            _grassQueue.Enqueue(Tile);
        }
        else if (Tile.Type == TileType.DarkGrass)
        {
            _darkGrassQueue.Enqueue(Tile);
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

        SpawnTile();
    }


    void SpawnTile()
    {
        TileLine SpawnTile;
        if(_sequenceTileType.Count <= 0)
        {
            TileSequenceMaker();
        }
        SpawnTile = GetTile(_sequenceTileType.Dequeue());
        SpawnTile.transform.position = _lastTileLine.gameObject.transform.position + Vector3.forward * _tileSize;
        SpawnTile.gameObject.SetActive(true);
        _lastTileLine = SpawnTile;
        SpawnTile._prevTile = _lastTileLine;
        _lastTileLine._nextTile = SpawnTile;
    }

    private void Awake()
    {
        Initialize();
    }

    void Start()
    {
        for (int i = 0; i < _startTileAmount; ++i)
        {
            if (0 <= i && i < 7)
            {
                if(i % 2 == 0)
                {
                    _startTile[i] = GetTile(TileType.Grass);
                    _startTile[i].transform.position = new Vector3(0, 0, -10 + (i * _tileSize));
                    _startTile[i].gameObject.SetActive(true);
                }
                else
                {
                    _startTile[i] = GetTile(TileType.DarkGrass);
                    _startTile[i].transform.position = new Vector3(0, 0, -10 + (i * _tileSize));
                    _startTile[i].gameObject.SetActive(true);
                }
                
            }
            else
            {
                _startTile[i] = GetTile(_sequenceTileType.Dequeue());
                _startTile[i].transform.position = new Vector3(0, 0, -10 + (i * _tileSize));
                _startTile[i].gameObject.SetActive(true);
            }
            if(i > 0)
            {
                _startTile[i]._prevTile = _startTile[i - 1];
                _startTile[i - 1]._nextTile = _startTile[i];
            }
        }
        _lastTileLine = _startTile[_startTileAmount - 1];
    }
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < _startTileAmount; ++i)
            {

            }
        }
  
    }

}