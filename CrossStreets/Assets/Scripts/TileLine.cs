using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLine : MonoBehaviour
{
    [SerializeField]
    private TileType _tileType;

    private  int[] _tileIndex = new int[20];

    // ���� ����Ʈó��
    public TileLine _prevTile = null;
    public TileLine _nextTile = null;

    public TileType Type
    {
        get { return _tileType; }
        set { _tileType = value; }
    }

    public void Initialize(TileType type)
    {
        _tileType = type;
        gameObject.SetActive(false);
        for(int  i = 0; i < 20; ++i)
        {
            if ((0 <= i && i < 5) || (15 <= i && i < 20))
            {
                _tileIndex[i] = 1;
            }
            // ���� �����鼭 ��ֹ��� �����ϱ�
        }
    }

}
