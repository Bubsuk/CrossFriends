using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDeadPoint : MonoBehaviour
{
    [SerializeField]
    private MapConstructor _mapCreator;

    private readonly int MOVE_OBSTACLE_LAYER = 7;


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == MOVE_OBSTACLE_LAYER)
        {
            _mapCreator.ReturnMoveObstacle(other.gameObject.GetComponent<MoveObstacle>());
        }
    }
}
