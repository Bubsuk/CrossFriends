using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5;

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
}
