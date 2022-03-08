using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Die,
        ReadyJump,
    }

    enum PlayerDir
    {
        Forward = 0,
        Back = 180,
        Right = 90,
        Left = -90,
    }

    private PlayerState _state = PlayerState.Idle;
    private PlayerDir _dir = PlayerDir.Forward;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float _rotateTime = 1f;
    [SerializeField]
    private float _jumpTime = 1f;

    Coroutine _rotateCoroutine = null;
    Coroutine _jumpCoroutine = null;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Forward, _rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Back, _rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Left, _rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _rotateCoroutine = StartCoroutine(PlayerTurn(PlayerDir.Right, _rotateTime));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpCoroutine = StartCoroutine(PlayerJump(_jumpTime));
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
            //ypwlield return null;
        }
    }

    IEnumerator PlayerJump(float durationTime)
    {
        float _elapsedTime = 0f;
        bool _isJumpEnd = false;

        Vector3 _startPosA = transform.position;
        Vector3 _startPosB = transform.position + new Vector3(0 , 10, 5);
        Vector3 _destPosA = transform.position + new Vector3(0, 10, 20);
        Vector3 _destPosB = transform.position + new Vector3(0, 0, 20);

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
    }

   
}
