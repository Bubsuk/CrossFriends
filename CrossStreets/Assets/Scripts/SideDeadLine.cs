using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDeadLine : MonoBehaviour
{
    [SerializeField]
    private MapConstructor _mapCreator;
    [SerializeField]
    private TileDeadPoint _mainDeadPoint;
    [SerializeField]
    private float _moveSpeed = 1f;
    [SerializeField]
    private GameObject _player;

    private const string TAG_Finish = "Obstacle";
    private readonly int MOVE_OBSTACLE_LAYER = 7;

    private void Update()
    {
        if (_player.transform.position.z - _mainDeadPoint.gameObject.transform.position.z > 10f)
        {
            transform.position += Vector3.forward * _moveSpeed * Time.deltaTime * 10f;
        }
        else
        {
            transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == MOVE_OBSTACLE_LAYER)
        {
           // _mapCreator.ReturnObstacle(other.gameObject.GetComponent<MoveObstacle>());
        }
    }
}
