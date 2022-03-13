using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour/*, IObstacle*/
{
    private float _moveSpeed;
    private ObstacleType _type;
    private bool _isLeftToRight = false; 
    public ObstacleType Type
    {
        get { return _type; }
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

    public void Initialize(ObstacleType type)
    {
        gameObject.SetActive(false);
        _type = type;
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
