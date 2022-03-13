using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : Obstacle
{
    private float _moveSpeed;
    private bool _isLeftToRight = false;

    private Vector3 _spawnPos;
    
    public Vector3 SpawnPos
    {
        set { _spawnPos = value; }
    }
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    public bool MoveDir
    {
        get { return _isLeftToRight; }
        set { _isLeftToRight = value; }
    }

    private void Awake()
    {
        if (_isLeftToRight == true)
        {
            transform.rotation = Quaternion.Euler(Vector3.right);
            transform.position = _spawnPos;
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.left);
            transform.position = _spawnPos;
        }
    }
    void Update()
    {
        if(_isLeftToRight == true)
        {
            transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
        }
    }
}
