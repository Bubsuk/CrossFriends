using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState _state = PlayerState.Idle;
    private PlayerDir _dir;
    private PlayerDir _saveDir;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float _rotateTime = 1f;
    [SerializeField]
    private float _jumpTime = 1f;
    [SerializeField]
    private GameObject _destroyZone;

    [SerializeField]
    private GameObject _frontDetector;
    [SerializeField]
    private GameObject _rearDetector;
    [SerializeField]
    private GameObject _leftDetector;
    [SerializeField]
    private GameObject _rightDetector;

    private readonly int TILE_LAYER = 3;

    Coroutine _rotateCoroutine = null;
    private bool _isJump = false;

    public event Action OnInputKey = null;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    bool CanGoThrough(PlayerDir dir)
    {
        RaycastHit hit;
        float maxDistance = 20f;
        switch(dir)
        {
            case PlayerDir.Forward:
                if (Physics.Raycast(_frontDetector.transform.position, new Vector3(0, -1, 0), out hit, maxDistance))
                {
                    Debug.DrawRay(_frontDetector.transform.position, new Vector3(0, -1, 0) * 100f, Color.red, 1f);
                    return IsCanMove(hit.collider.gameObject.layer);
                }
                break;
            case PlayerDir.Back:
                if (Physics.Raycast(_rearDetector.transform.position, new Vector3(0, -1, 0), out hit, maxDistance))
                {
                    Debug.DrawRay(_rearDetector.transform.position, new Vector3(0, -1, 0) * 100f, Color.red, 1f);
                    return IsCanMove(hit.collider.gameObject.layer);
                }
                break;
            case PlayerDir.Right:
                if (Physics.Raycast(_rightDetector.transform.position, new Vector3(0, -1, 0), out hit, maxDistance))
                {
                    Debug.DrawRay(_rightDetector.transform.position, new Vector3(0, -1, 0) * 100f, Color.red, 1f);
                    return IsCanMove(hit.collider.gameObject.layer);
                }
                break;
            case PlayerDir.Left:
                if (Physics.Raycast(_leftDetector.transform.position, new Vector3(0, -1, 0), out hit, maxDistance))
                {
                    Debug.DrawRay(_leftDetector.transform.position, new Vector3(0, -1, 0) * 100f, Color.red, 1f);
                    return IsCanMove(hit.collider.gameObject.layer);
                }
                break;

        }

        return false;
    }

    bool IsCanMove(int layer)
    {
        switch (layer)
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

    void Update()
    {
        _destroyZone.transform.position = transform.position - (Vector3.forward * 20f);

        if (Input.GetKeyDown(KeyCode.W) && _isJump == false)
        {
            bool result = false;
            _dir = PlayerDir.Forward;
            if(_saveDir == PlayerDir.Back)
            {
                result = CanGoThrough(PlayerDir.Back);
            }
            else if(_saveDir == PlayerDir.Left)
            {
                result = CanGoThrough(PlayerDir.Right);
            }
            else if(_saveDir == PlayerDir.Right)
            {
                result = CanGoThrough(PlayerDir.Left);
            }
            else
            {
                result = CanGoThrough(_dir);
            }

            if (result)
            {
                _isJump = true;
                _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Forward, _rotateTime));
                StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(0, 3, 1), transform.position + new Vector3(0, 3, 3), transform.position + new Vector3(0, 0, 4)));
                OnInputKey.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && _isJump == false)
        {
            bool result = false;
            _dir = PlayerDir.Back;
            if (_saveDir == PlayerDir.Forward)
            {
                result = CanGoThrough(PlayerDir.Back);
            }
            else if (_saveDir == PlayerDir.Left)
            {
                result = CanGoThrough(PlayerDir.Left);
            }
            else if (_saveDir == PlayerDir.Right)
            {
                result = CanGoThrough(PlayerDir.Right);
            }
            else
            {
                result = CanGoThrough(PlayerDir.Forward);
            }

            if (result)
            {
                _isJump = true;
                _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Back, _rotateTime));
                StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(0, 3, -1), transform.position + new Vector3(0, 3, -3), transform.position + new Vector3(0, 0, -4)));
                OnInputKey.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && _isJump == false)
        {
            bool result = false;
            _dir = PlayerDir.Left;
            if (_saveDir == PlayerDir.Forward)
            {
                result = CanGoThrough(PlayerDir.Left);
            }
            else if (_saveDir == PlayerDir.Left)
            {
                result = CanGoThrough(PlayerDir.Forward);
            }
            else if (_saveDir == PlayerDir.Right)
            {
                result = CanGoThrough(PlayerDir.Back);
            }
            else
            {
                result = CanGoThrough(PlayerDir.Right);
            }
            if (result)
            {
                _isJump = true;
                _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Left, _rotateTime));
                StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(-1, 3, 0), transform.position + new Vector3(-3, 3, 0), transform.position + new Vector3(-5, 0, 0)));
                OnInputKey.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && _isJump == false)
        {
            _dir = PlayerDir.Right;
            bool result = false;
            if (_saveDir == PlayerDir.Forward)
            {
                result = CanGoThrough(PlayerDir.Right);
            }
            else if (_saveDir == PlayerDir.Left)
            {
                result = CanGoThrough(PlayerDir.Back);
            }
            else if (_saveDir == PlayerDir.Right)
            {
                result = CanGoThrough(PlayerDir.Forward);
            }
            else
            {
                result = CanGoThrough(PlayerDir.Left);
            }
            if (result)
            {
                _isJump = true;
                _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Right, _rotateTime));
                StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(1, 3, 0), transform.position + new Vector3(3, 3, 0), transform.position + new Vector3(5, 0, 0)));
                OnInputKey.Invoke();
            }
        }

        _saveDir = _dir;
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

    IEnumerator PlayerJump(float durationTime, Vector3 vertex1, Vector3 vertex2, Vector3 destination)
    {
        float _elapsedTime = 0f;
        bool _isJumpEnd = false;

        Vector3 _startPosA = transform.position;
        Vector3 _startPosB = vertex1;
        Vector3 _destPosA = vertex2;
        Vector3 _destPosB = destination;

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

    private void OnCollisionEnter(Collision collision)
    {
        // Á×´Â Ã³¸®
        if (collision.gameObject.layer == LayerName.MOVE_OBSTACLE)
        {
            Debug.Log("GameOver");
            GameManager.Instance.OnPlayerDead();
        }
    }

}
