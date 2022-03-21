using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState _state = PlayerState.Idle;
    private PlayerDir _dir;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float _rotateTime = 1f;
    [SerializeField]
    private float _jumpTime = 1f;
    [SerializeField]
    private GameObject _destroyZone;

    Coroutine _rotateCoroutine = null;
    private bool _isJump = false;
    private bool _isOnLog = false;
    private bool _isLogLeftToRight = false;
    private float _floatingLogSpeed = 0f;


    public int _score = 0;
    public int _highScore = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _destroyZone.transform.position = transform.position - (Vector3.forward * 20f);

        if (_isOnLog == true)
        {
            if (_isLogLeftToRight == true)
            {
                transform.position += Vector3.right * _floatingLogSpeed * Time.deltaTime;
            }
            else
            {
                transform.position -= Vector3.right * _floatingLogSpeed * Time.deltaTime;
            }
        }

        if(-25f > transform.position.x || transform.position.x > 25f)
        {
            GameManager.Instance.IsGameOver = true;
        }

        if(GameManager.Instance.IsGameOver == false && (-25f < transform.position.x && transform.position.x < 25f))
        {
            if (Input.GetKeyDown(KeyCode.W) && _isJump == false )
            {
                bool result = false;
                bool logJump = false;
                GameObject rayedTile;
                MoveObstacle floatingLog = null;
                GameManager.Instance._isGameStart = true;

                _dir = PlayerDir.Forward;

                rayedTile = DetectTile(transform.position + new Vector3(0, 0, 4));

                if (rayedTile.GetComponent<TileLine>() != null)
                {
                    if (rayedTile.GetComponent<TileLine>().Type == TileType.Water)
                    {
                        GetComponent<Collider>().isTrigger = true;
                        GameManager.Instance.OnPlayerDead();
                    }
                    _isOnLog = false;
                    result = IsCanMove(rayedTile.GetComponent<TileLine>());
                    if (rayedTile.GetComponent<TileLine>().Type != TileType.Water)
                    {
                        ++_score;
                    }
                }
                if (rayedTile.layer == LayerName.FLOATING_LOG)
                {
                    _isOnLog = true;
                    floatingLog = rayedTile.GetComponent<MoveObstacle>();
                    logJump = true;
                    ++_score;
                }

                if (result)
                {
                    _isJump = true;
                    _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Forward, _rotateTime));
                    StartCoroutine(PlayerJump(rayedTile.GetComponent<TileLine>(), _jumpTime, transform.position + new Vector3(0, 3, 1), transform.position + new Vector3(0, 3, 3), transform.position + new Vector3(0, 0, 4)));
                }
                if (logJump)
                {
                    _isJump = true;
                    _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Forward, _rotateTime));
                    StartCoroutine(FloatingLogJump(floatingLog, _jumpTime, transform.position + new Vector3(0, 3, 1), transform.position + new Vector3(0, 3, 3), transform.position + new Vector3(0, 0, 4)));
                }

            }

            if (Input.GetKeyDown(KeyCode.S) && _isJump == false)
            {
                bool result = false;
                bool logJump = false;
                GameObject rayedTile;
                MoveObstacle floatingLog = null;
                GameManager.Instance._isGameStart = true;

                _dir = PlayerDir.Back;
                rayedTile = DetectTile(transform.position + new Vector3(0, 0, -4));

                if (rayedTile.GetComponent<TileLine>() != null)
                {
                    if (rayedTile.GetComponent<TileLine>().Type == TileType.Water)
                    {
                        GetComponent<Collider>().enabled = false;
                        GameManager.Instance.OnPlayerDead();
                    }
                    _isOnLog = false;
                    result = IsCanMove(rayedTile.GetComponent<TileLine>());
                    if (rayedTile.GetComponent<TileLine>().Type != TileType.Water)
                    {
                        --_score;
                    }
                }
                if (rayedTile.layer == LayerName.FLOATING_LOG)
                {
                    _isOnLog = true;
                    floatingLog = rayedTile.GetComponent<MoveObstacle>();
                    logJump = true;
                    --_score;
                }

                if (result)
                {
                    _isJump = true;
                    _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Back, _rotateTime));
                    StartCoroutine(PlayerJump(rayedTile.GetComponent<TileLine>(), _jumpTime, transform.position + new Vector3(0, 3, -1), transform.position + new Vector3(0, 3, -3), transform.position + new Vector3(0, 0, -4)));
                }
                if (logJump)
                {
                    _isJump = true;
                    _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Back, _rotateTime));
                    StartCoroutine(FloatingLogJump(floatingLog, _jumpTime, transform.position + new Vector3(0, 3, -1), transform.position + new Vector3(0, 3, -3), transform.position + new Vector3(0, 0, -4)));
                }
            }

            if (Input.GetKeyDown(KeyCode.A) && _isJump == false)
            {
                bool result = false;
                bool logJump = false;
                GameObject rayedTile;
                MoveObstacle floatingLog = null;
                GameManager.Instance._isGameStart = true;

                _dir = PlayerDir.Left;
                rayedTile = DetectTile(transform.position + new Vector3(-5, 0, 0));

                if (rayedTile.GetComponent<TileLine>() != null)
                {
                    if (rayedTile.GetComponent<TileLine>().Type == TileType.Water)
                    {
                        GetComponent<Collider>().enabled = false;
                        GameManager.Instance.OnPlayerDead();
                    }
                    _isOnLog = false;
                    result = IsCanMove(rayedTile.GetComponent<TileLine>());
                }
                if (rayedTile.layer == LayerName.FLOATING_LOG)
                {
                    _isOnLog = true;
                    floatingLog = rayedTile.GetComponent<MoveObstacle>();
                    logJump = true;
                }

                if (result && (transform.position.x + -5f > -25f))
                {
                    _isJump = true;
                    _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Left, _rotateTime));
                    StartCoroutine(PlayerJump(rayedTile.GetComponent<TileLine>(), _jumpTime, transform.position + new Vector3(-1, 3, 0), transform.position + new Vector3(-3, 3, 0), transform.position + new Vector3(-5, 0, 0)));
                }

                if (logJump)
                {
                    _isJump = true;
                    _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Left, _rotateTime));
                    StartCoroutine(FloatingLogJump(floatingLog, _jumpTime, transform.position + new Vector3(-1, 3, 0), transform.position + new Vector3(-3, 3, 0), transform.position + new Vector3(-5, 0, 0)));
                }
            }
            if (Input.GetKeyDown(KeyCode.D) && _isJump == false)
            {
                bool result = false;
                bool logJump = false;
                GameObject rayedTile;
                MoveObstacle floatingLog = null;
                GameManager.Instance._isGameStart = true;

                _dir = PlayerDir.Right;
                rayedTile = DetectTile(transform.position + new Vector3(5, 0, 0));

                if (rayedTile.GetComponent<TileLine>() != null)
                {
                    if (rayedTile.GetComponent<TileLine>().Type == TileType.Water)
                    {
                        GetComponent<Collider>().enabled = false;
                        GameManager.Instance.OnPlayerDead();
                    }
                    _isOnLog = false;
                    result = IsCanMove(rayedTile.GetComponent<TileLine>());
                }
                if (rayedTile.layer == LayerName.FLOATING_LOG)
                {
                    _isOnLog = true;
                    floatingLog = rayedTile.GetComponent<MoveObstacle>();
                    logJump = true;
                }

                if (result && (transform.position.x + 5f < 25f))
                {
                    _isJump = true;
                    _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Right, _rotateTime));
                    StartCoroutine(PlayerJump(rayedTile.GetComponent<TileLine>(), _jumpTime, transform.position + new Vector3(1, 3, 0), transform.position + new Vector3(3, 3, 0), transform.position + new Vector3(5, 0, 0)));
                }

                if (logJump)
                {
                    _isJump = true;
                    _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Right, _rotateTime));
                    StartCoroutine(FloatingLogJump(floatingLog, _jumpTime, transform.position + new Vector3(1, 3, 0), transform.position + new Vector3(3, 3, 0), transform.position + new Vector3(5, 0, 0)));
                }
            }
        }
       
        if(_score >= _highScore)
        {
            _highScore = _score;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerName.MOVE_OBSTACLE)
        {
            GameManager.Instance.OnPlayerDead();
            transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
        }
    }

    IEnumerator PlayerTurn(PlayerDir dir, float durationTime)
    {
        if(_rotateCoroutine != null)
        {
            StopCoroutine(_rotateCoroutine);
        }

        float _elapsedTime = 0f;
        bool _isRotateEnd = false;

        while(_isRotateEnd == false)
        {
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime >= durationTime)
            {
                _elapsedTime = durationTime;
                _isRotateEnd = true;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, (int)dir, 0), _elapsedTime / durationTime);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator PlayerJump(TileLine tile, float durationTime, Vector3 vertex1, Vector3 vertex2, Vector3 destination)
    {
        float _elapsedTime = 0f;
        bool _isJumpEnd = false;
        int posX = (int)Math.Abs(tile.transform.position.x - 50f - destination.x) / 5 + 1;

        Vector3 _startPosA = transform.position;
        Vector3 _startPosB = vertex1;
        Vector3 _destPosA = vertex2;
        Vector3 _destPosB = new Vector3(tile.transform.position.x - 50f + 5 * posX - 2.5f, tile.transform.position.y, tile.transform.position.z); ;

        while (_isJumpEnd == false)
        {
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime >= durationTime)
            {
                _elapsedTime = durationTime;
                _isJumpEnd = true;
            }

            transform.position = BezierCurve.Bezier(_startPosA, _startPosB, _destPosA, _destPosB, _elapsedTime, durationTime);

            yield return null;
        }

        _isJump = false;
    }

    IEnumerator FloatingLogJump(MoveObstacle log, float durationTime, Vector3 vertex1, Vector3 vertex2, Vector3 destination)
    {
        float _elapsedTime = 0f;
        bool _isJumpEnd = false;
        int posX = (int)Math.Abs(log.transform.position.x - 7.5f - destination.x) / 5 + 1;

        Vector3 _startPosA = transform.position;
        Vector3 _startPosB = vertex1;
        Vector3 _destPosA = vertex2;
        Vector3 _destPosB;
        if (log.MoveDir == true) // xÃà + ¹æÇâ
        {
            _destPosB = new Vector3(log.transform.position.x - 7.5f + 5 * posX  + durationTime * log.MoveSpeed - 2.5f, log.transform.position.y, log.transform.position.z);
        }
        else
        {
            _destPosB = new Vector3(log.transform.position.x - 7.5f + 5 * posX - durationTime * log.MoveSpeed - 2.5f, log.transform.position.y, log.transform.position.z);
        }

        while (_isJumpEnd == false)
        {
            _elapsedTime += Time.deltaTime;
            
            if (_elapsedTime >= durationTime)
            {
                _elapsedTime = durationTime;
                _isJumpEnd = true;
            }

            transform.position = BezierCurve.Bezier(_startPosA, _startPosB, _destPosA, _destPosB, _elapsedTime, durationTime);

            yield return null;
        }
        _isLogLeftToRight = log.MoveDir;
        _floatingLogSpeed = log.MoveSpeed;
        _isJump = false;
    }

    bool IsCanMove(TileLine tile)
    {
        switch (tile.gameObject.layer)
        {
            case LayerName.OBSTACLE:
                return false;
            case LayerName.MOVE_OBSTACLE:
                return true;
            case LayerName.FLOATING_LOG:
                return true;
            case LayerName.TILE_LINE:
                return true;
            default:
                return false;
        }
    }
    GameObject DetectTile(Vector3 rayDest)
    {
        RaycastHit hit;
        GameObject tempTileLine;
        float maxDistance = 20f;

        if (Physics.Raycast(rayDest + new Vector3(0, 10, 0), new Vector3(0, -1, 0), out hit, maxDistance))
        {
            Debug.DrawRay(rayDest + new Vector3(0, 10, 0), new Vector3(0, -1, 0) * 100f, Color.red, 1f);
            tempTileLine = hit.transform.gameObject;
            return tempTileLine;
        }
        return null;
    }
}
