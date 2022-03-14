using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDeadPoint : MonoBehaviour
{
    [SerializeField]
    private MapConstructor _mapCreator;
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private GameObject _player;

    private const string TAG_Finish = "Tile";
    private readonly int TILE_LAYER = 3;

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerName.TILE_LINE)
        {
            _mapCreator.ReturnTile(other.gameObject.GetComponent<TileLine>());
        }
    }
}
