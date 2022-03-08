using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLine : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    void Update()
    {
        transform.Translate(0, 0, -_moveSpeed * Time.deltaTime);
    }
}
