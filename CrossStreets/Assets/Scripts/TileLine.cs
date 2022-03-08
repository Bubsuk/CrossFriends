using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLine : MonoBehaviour
{
    [SerializeField]
    private TileType _tileType;
    [SerializeField]
    private float _moveSpeed = 5f;
    public TileType Type
    {
        get { return _tileType; }
        set { _tileType = value; }
    }

    public void Initialize(TileType type)
    {
        _tileType = type;
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.Translate(0, 0, -_moveSpeed * Time.deltaTime);
    }
}
