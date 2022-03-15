using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    private float _moveSpeed;
    private bool _isLeftToRight = false;
    private System.Action<MoveObstacle> _onReturnObstacle;

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
    private ObstacleType _type;

    public ObstacleType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public void Initialize(System.Action<MoveObstacle> returnObstacle)
    {
        _onReturnObstacle = returnObstacle;
    }

    void Update()
    {
        if (_isLeftToRight == true)
        {
            transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
            if(transform.position.x >= 85f)
            {
                _onReturnObstacle(this);
            }
        }
        else
        {
            transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
            if (transform.position.x <= -85f)
            {
                _onReturnObstacle(this);
            }
        }
    }
}
