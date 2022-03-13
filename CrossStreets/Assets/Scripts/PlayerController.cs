using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState _state = PlayerState.Idle;
    private PlayerDir _dir = PlayerDir.Forward;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float _rotateTime = 1f;
    [SerializeField]
    private float _jumpTime = 1f;
    [SerializeField]
    GameObject _destroyZone;
    [SerializeField]
    GameObject _rightDestroyZone;
    [SerializeField]
    GameObject _leftDestroyZone;

    Coroutine _rotateCoroutine = null;
    private bool _isJump = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _destroyZone.transform.position = transform.position - (Vector3.forward * 20f);
        _rightDestroyZone.transform.position = transform.position + new Vector3(50f, 0f, 38f);
        _leftDestroyZone.transform.position = transform.position + new Vector3(-50f, 0f, 38f);

        if (Input.GetKeyDown(KeyCode.W) && _isJump == false)
        {
            _dir = PlayerDir.Forward;
            _isJump = true;
            _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Forward, _rotateTime));
            StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(0, 3, 1), transform.position + new Vector3(0, 3, 3), transform.position + new Vector3(0, 0, 4)));
        }
        if (Input.GetKeyDown(KeyCode.S) && _isJump == false)
        {
            _dir = PlayerDir.Back;
            _isJump = true;
            _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Back, _rotateTime));
            StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(0, 3, -1), transform.position + new Vector3(0, 3, -3), transform.position + new Vector3(0, 0, -4)));
        }
        if (Input.GetKeyDown(KeyCode.A) && _isJump == false)
        {
            _dir = PlayerDir.Left;
            _isJump = true;
            _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Left, _rotateTime));
            StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(-1, 3, 0), transform.position + new Vector3(-3, 3, 0), transform.position + new Vector3(-5, 0, 0)));
        }
        if (Input.GetKeyDown(KeyCode.D) && _isJump == false)
        {
            _dir = PlayerDir.Right;
            _isJump = true;
            _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Right, _rotateTime));
            StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(1, 3, 0), transform.position + new Vector3(3, 3, 0), transform.position + new Vector3(5, 0, 0)));
        }
        //if (_isJump == false)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        _isJump = true;
        //        switch (_dir)
        //        {
        //            case PlayerDir.Forward:
        //                StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(0, 3, 1), transform.position + new Vector3(0, 3, 3), transform.position + new Vector3(0, 0, 4)));
        //                break;
        //            case PlayerDir.Back:
        //                StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(0, 3, -1), transform.position + new Vector3(0, 3, -3), transform.position + new Vector3(0, 0, -4)));
        //                break;
        //            case PlayerDir.Right:
        //                StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(1, 3, 0), transform.position + new Vector3(3, 3, 0), transform.position + new Vector3(5, 0, 0)));
        //                break;
        //            case PlayerDir.Left:
        //                StartCoroutine(PlayerJump(_jumpTime, transform.position + new Vector3(-1, 3, 0), transform.position + new Vector3(-3, 3, 0), transform.position + new Vector3(-5, 0, 0)));
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //}
        
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

   
}
