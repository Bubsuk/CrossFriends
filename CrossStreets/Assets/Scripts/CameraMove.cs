using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool _isMove = false;
    [SerializeField]
    private GameObject _camTarget;
    private Vector3 _delta = new Vector3(0f, 15f, 10f);

    void Update()
    {
        transform.position = _camTarget.transform.position + _delta;
    }
}
