using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDeadPoint : MonoBehaviour
{
    [SerializeField]
    MapConstructor MapCreator;
    [SerializeField]
    private float _moveSpeed = 5f;

    private const string TAG_Finish = "Tile";
    private readonly int TILE_LAYER = 3;

    private void Update()
    {
        transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == TILE_LAYER)
        {
            MapCreator.ReturnTile(other.gameObject.GetComponent<TileLine>());
        }
    }
}
