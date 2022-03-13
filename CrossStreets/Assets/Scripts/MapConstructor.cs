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

    // Tile
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
    [SerializeField]
    private TileLine _roadLinePrefab;


    // Obstacle
    [SerializeField]
    private TreeObstacle _dragon1Prefab;
    [SerializeField]
    private TreeObstacle _dragon2Prefab;
    [SerializeField]
    private TreeObstacle _treePrefab;
    [SerializeField]
    private TreeObstacle _shortTreePrefab;


    private Queue<TileType> _sequenceTileType = new Queue<TileType>();

   // private Vector3 _spawnPos = new Vector3(0, 0, 160);

    private TileLine _lastTileLine;

    [SerializeField]
    private int _tileSize = 4;
    [SerializeField]
    private int _startTileAmount = 30;
    [SerializeField]
    private int _obstacleMaxAmt = 5;

    private TileLine[] _startTile = new TileLine[30];

    private TileObjPool _grassPool = new TileObjPool();
    private TileObjPool _darkGrassPool = new TileObjPool();
    private TileObjPool _roadPool = new TileObjPool();
    private TileObjPool _waterPool = new TileObjPool();
    private TileObjPool _railPool = new TileObjPool();
    private TileObjPool _roadLinePool = new TileObjPool();

    private ObstacleObjPool _dragon1Pool = new ObstacleObjPool();
    private ObstacleObjPool _dragon2Pool = new ObstacleObjPool();
    private ObstacleObjPool _treePool = new ObstacleObjPool();
    private ObstacleObjPool _shortTreePool = new ObstacleObjPool();

    private Dictionary<TileType, TileObjPool> _tileObjQueue = new Dictionary<TileType, TileObjPool>();
    private Dictionary<ObstacleType, ObstacleObjPool> _obstacleQueue = new Dictionary<ObstacleType, ObstacleObjPool>();

    private void Initialize()
    {
        while(_sequenceTileType.Count < _startTileAmount)
        {
            TileSequenceMaker();
        }

        for(int i = 0; i < 15; ++i)
        {
            _grassPool.TileInit(_grassPrefab, TileType.Grass);
            _darkGrassPool.TileInit(_darkGrassPrefab, TileType.DarkGrass);
            _roadPool.TileInit(_roadPrefab, TileType.Road);
            _waterPool.TileInit(_waterPrefab, TileType.Water);
            _railPool.TileInit(_railPrefab, TileType.Rail);
            _roadLinePool.TileInit(_roadLinePrefab, TileType.RoadLine);
        }

        for (int i = 0; i < 400; ++i)
        {
            _dragon1Pool.ObstacleInit(_dragon1Prefab, ObstacleType.Dragon1);
            _dragon2Pool.ObstacleInit(_dragon2Prefab, ObstacleType.Dragon2);
            _treePool.ObstacleInit(_treePrefab, ObstacleType.Tree);
            _shortTreePool.ObstacleInit(_shortTreePrefab, ObstacleType.ShortTree);
        }

        _tileObjQueue.Add(TileType.Grass, _grassPool);
        _tileObjQueue.Add(TileType.DarkGrass, _darkGrassPool);
        _tileObjQueue.Add(TileType.Road, _roadPool);
        _tileObjQueue.Add(TileType.Water, _waterPool);
        _tileObjQueue.Add(TileType.Rail, _railPool);
        _tileObjQueue.Add(TileType.RoadLine, _roadLinePool);

        _obstacleQueue.Add(ObstacleType.Dragon1, _dragon1Pool);
        _obstacleQueue.Add(ObstacleType.Dragon2, _dragon2Pool);
        _obstacleQueue.Add(ObstacleType.Tree, _treePool);
        _obstacleQueue.Add(ObstacleType.ShortTree, _shortTreePool);
    }
    private void Awake()
    {
        Initialize();
    }


    private TileLine GetTile(TileType type)
    {
        return _tileObjQueue[type].GetTile();
    }

    private TreeObstacle GetObstacle(ObstacleType type)
    {
        return _obstacleQueue[type].GetObstacle();
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


    public void ReturnTile(TileLine tile)
    {
        if (!_tileObjQueue.ContainsKey(tile.Type))
        {
            Debug.Log($"{tile.Type} 타입의 키가 없습니다");
        }
        if (tile.Type != TileType.RoadLine)
        {
            for (int i = 0; i < 20; ++i)
            {
                if (tile._obstacleOwnThis[i] != null)
                {
                    ReturnObstacle(tile._obstacleOwnThis[i]);
                }
            }

            SpawnTile();
        }
        _tileObjQueue[tile.Type].ReturnTile(tile);

    }

    public void ReturnObstacle(TreeObstacle obstacle)
    {
        if (!_obstacleQueue.ContainsKey(obstacle.Type))
        {
            Debug.Log($"{obstacle.Type} 타입의 키가 없습니다");
        }
        _obstacleQueue[obstacle.Type].ReturnObstacle(obstacle); 
    }


    void SpawnTile()
    {
        TileLine spawnTile;

        if(_sequenceTileType.Count <= 0)
        {
            TileSequenceMaker();
        }

        spawnTile = GetTile(_sequenceTileType.Dequeue());
        spawnTile.transform.position = _lastTileLine.gameObject.transform.position + Vector3.forward * _tileSize;
        spawnTile.gameObject.SetActive(true);
        spawnTile._obstacleCode = ObstacleMaker.RandomTenBinaryDigitsGenerator(_obstacleMaxAmt);
        spawnTile._tileObstacle = ObstacleMaker.RandomObstacleArr(spawnTile.Type, _lastTileLine, spawnTile._obstacleCode);
        
        if(_lastTileLine.Type == TileType.Road && spawnTile.Type == TileType.Road)
        {
            TileLine roadLine = GetTile(TileType.RoadLine);
            roadLine.transform.position = _lastTileLine.gameObject.transform.position + Vector3.forward * 2f;
            roadLine._obstacleCode = 0;
            roadLine._tileObstacle = ObstacleMaker.RandomObstacleArr(roadLine.Type, _lastTileLine, roadLine._obstacleCode);
            roadLine.gameObject.SetActive(true);
            roadLine._prevTileLine = roadLine;
        }
        if (spawnTile.Type == TileType.Grass || spawnTile.Type == TileType.DarkGrass)
        {
            ArrangeTree(spawnTile);
        }
        _lastTileLine = spawnTile;

    }

   
    private void ArrangeTree(TileLine tile)
    {
        for (int i = 0; i < 20; ++i)
        {
            TreeObstacle obstacle;
            if (tile._tileObstacle[i] == 1)
            {
                if (Random.Range(0, 2) % 2 == 0)
                {
                    obstacle = GetObstacle(ObstacleType.Tree);
                    obstacle.gameObject.SetActive(true);
                    obstacle.gameObject.transform.position = new Vector3((i * 5) - 47.5f, 1, tile.gameObject.transform.position.z);
                    tile._obstacleOwnThis[i] = obstacle;
                }
                else
                {
                    obstacle = GetObstacle(ObstacleType.ShortTree);
                    obstacle.gameObject.SetActive(true);
                    obstacle.gameObject.transform.position = new Vector3((i * 5) - 47.5f, 1, tile.gameObject.transform.position.z);
                    tile._obstacleOwnThis[i] = obstacle;
                }

            }
            else
            {
                tile._obstacleOwnThis[i] = null;
            }
        }
    }
    void Start()
    {
        _startTile[0] = _tileObjQueue[TileType.Grass].GetTile();
        _startTile[0].transform.position = new Vector3(0, 0, -10);
        _startTile[0].gameObject.SetActive(true);
        _startTile[0]._prevTileLine = _startTile[0];
        _startTile[0]._tileObstacle = new int[20] { 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1 };
        _startTile[0]._obstacleCode = ObstacleMaker.RandomTenBinaryDigitsGenerator(_obstacleMaxAmt);
        ArrangeTree(_startTile[0]);

        for (int i = 1; i < _startTileAmount; ++i)
        {
            if (0 <= i && i < 7)
            {
                if(i != 0 && i % 2 == 0)
                {
                    _startTile[i] = _tileObjQueue[TileType.Grass].GetTile();
                    _startTile[i].transform.position = new Vector3(0, 0, -10 + (i * _tileSize));
                    _startTile[i].gameObject.SetActive(true);
                    _startTile[i]._prevTileLine = _startTile[i - 1];
                    _startTile[i]._obstacleCode = ObstacleMaker.RandomTenBinaryDigitsGenerator(_obstacleMaxAmt);
                    _startTile[i]._tileObstacle = ObstacleMaker.RandomObstacleArr(TileType.Grass, _startTile[i]._prevTileLine, _startTile[i]._obstacleCode);
                    ArrangeTree(_startTile[i]);
                }
                else if(i == 3)
                {
                    _startTile[i] = _tileObjQueue[TileType.DarkGrass].GetTile();
                    _startTile[i].transform.position = new Vector3(0, 0, -10 + (i * _tileSize));
                    _startTile[i].gameObject.SetActive(true);
                    _startTile[i]._prevTileLine = _startTile[i - 1];
                    _startTile[i]._obstacleCode = ObstacleMaker.RandomTenBinaryDigitsGenerator(_obstacleMaxAmt);
                    _startTile[i]._tileObstacle = ObstacleMaker.RandomObstacleArr(TileType.Grass, _startTile[i]._prevTileLine, _startTile[i]._obstacleCode);
                    ArrangeTree(_startTile[i]);
                    _startTile[i]._tileObstacle[10] = 0;
                }
                else
                {
                    _startTile[i] = _tileObjQueue[TileType.DarkGrass].GetTile();
                    _startTile[i].transform.position = new Vector3(0, 0, -10 + (i * _tileSize));
                    _startTile[i].gameObject.SetActive(true);
                    _startTile[i]._prevTileLine = _startTile[i - 1];
                    _startTile[i]._obstacleCode = ObstacleMaker.RandomTenBinaryDigitsGenerator(_obstacleMaxAmt);
                    _startTile[i]._tileObstacle = ObstacleMaker.RandomObstacleArr(TileType.Grass, _startTile[i]._prevTileLine, _startTile[i]._obstacleCode);
                    ArrangeTree(_startTile[i]);
                }
            }
            else
            {
                _startTile[i] = _tileObjQueue[_sequenceTileType.Dequeue()].GetTile();
                _startTile[i].transform.position = new Vector3(0, 0, -10 + (i * _tileSize));
                _startTile[i].gameObject.SetActive(true);
                _startTile[i]._prevTileLine = _startTile[i - 1];
                _startTile[i]._obstacleCode = ObstacleMaker.RandomTenBinaryDigitsGenerator(_obstacleMaxAmt);
                _startTile[i]._tileObstacle = ObstacleMaker.RandomObstacleArr(_startTile[i].Type, _startTile[i]._prevTileLine, _startTile[i]._obstacleCode);
                if (_startTile[i].Type == TileType.Road && _startTile[i - 1].Type == TileType.Road)
                {
                    TileLine roadLine = GetTile(TileType.RoadLine);
                    roadLine.transform.position = _startTile[i - 1].gameObject.transform.position + Vector3.forward * 2f;
                    roadLine._obstacleCode = 0;
                    roadLine._tileObstacle = ObstacleMaker.RandomObstacleArr(roadLine.Type, roadLine, roadLine._obstacleCode);
                    roadLine.gameObject.SetActive(true);
                    roadLine._prevTileLine = roadLine;
                }
                if (_startTile[i].Type == TileType.Grass || _startTile[i].Type == TileType.DarkGrass)
                {
                    ArrangeTree(_startTile[i]);
                }
            }
        }
        
        _lastTileLine = _startTile[_startTileAmount - 1];

    }

}