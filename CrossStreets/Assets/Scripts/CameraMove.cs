using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool _isMove = false;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _moveSpeed = 3f;
    [SerializeField]
    private PlayerController _playerCon;

    Coroutine _horizontalCoroutine;
    Coroutine _verticalCoroutine;

    private void Awake()
    {
        _playerCon.OnInputKey -= CamHorizontalMove;
        _playerCon.OnInputKey += CamHorizontalMove;
    }

    void CamHorizontalMove()
    {
        if (Mathf.Abs(transform.position.x - _player.transform.position.x) > 0.5f)
        {
            _horizontalCoroutine = StartCoroutine(ReviseHorizonCamPos());
        }
    }

    void Update()
    {
        if (_player.transform.position.z > transform.position.z + 8f)
        {
            _verticalCoroutine = StartCoroutine(ReviseVerticalCamPos());
        }
        else
        {
            transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;
        }

        if(_player.transform.position.z < transform.position.z - 1f)
        {
            GameManager.Instance.OnPlayerDead();
        }
    }

    IEnumerator ReviseHorizonCamPos()
    {
        if(_horizontalCoroutine != null)
        {
            StopCoroutine(_horizontalCoroutine);
        }
        float _elapsedTime = 0f;
        float _durationTime = 2f;

        bool _isRotaed = false;

        Vector3 _startA = transform.position + new Vector3(0, 1, 0);
        Vector3 _startCam = transform.position;

        while (_isRotaed == false)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _durationTime)
            {
                _elapsedTime = _durationTime;
                _isRotaed = true;
            }

            transform.position = new Vector3(BezierCurve.ReviseCam(_startCam, 
                _player.transform.position, _player.transform.position + new Vector3(0, 1, 0), _elapsedTime, _durationTime).x,
                transform.position.y, transform.position.z);

            yield return null;
        }

        transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
        _horizontalCoroutine = null;
    }

    IEnumerator ReviseVerticalCamPos()
    {
        if (_verticalCoroutine != null)
        {
            StopCoroutine(_verticalCoroutine);
        }
        float _elapsedTime = 0f;
        float _durationTime = 2f;

        bool _isRotaed = false;

        Vector3 _startA = transform.position + new Vector3(0, 1, 0);
        Vector3 _startCam = transform.position;

        while (_isRotaed == false)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _durationTime)
            {
                _elapsedTime = _durationTime;
                _isRotaed = true;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, BezierCurve.ReviseCam(_startCam,
                _player.transform.position, _player.transform.position + new Vector3(0, 0.5f, 0), _elapsedTime, _durationTime).z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z);
        _horizontalCoroutine = null;
    }
}
