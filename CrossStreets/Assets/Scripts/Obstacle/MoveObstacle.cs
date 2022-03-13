using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
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
        set { _isLeftToRight = value; }
    }
    private ObstacleType _type;

    public ObstacleType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public void Initialize(ObstacleType type)
    {
        gameObject.SetActive(false);
        _type = type;
    }

    void Update()
    {
        if (_isLeftToRight == true)
        {
            transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
        }
    }
}
