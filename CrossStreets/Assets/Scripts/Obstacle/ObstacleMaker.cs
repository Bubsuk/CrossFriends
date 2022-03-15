using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObstacleMaker
{
    private static readonly int CANT_PASS = 1023; // Decimal 1023 to Binary : 1111111111

    public static int[] RandomObstacleArr(TileType type, TileLine prevTile, int _randObstacleCode)
    {
        int[] _hasObstacle = new int[20];

        while(true)
        {
            if (CanGoThrough(prevTile, _randObstacleCode) == true)
            {
                int shiftCompare = 1;
                for (int i = 0; i < 20; ++i)
                {
                    if ((0 <= i && i < 5) || (15 <= i && i < 20))
                    {
                        _hasObstacle[i] = 1;
                    }
                    else
                    {
                        switch (type)
                        {
                            case TileType.Grass:
                                if ((_randObstacleCode & shiftCompare) != 0)
                                {
                                    _hasObstacle[i] = 1;
                                }
                                else
                                {
                                    _hasObstacle[i] = 0;
                                }
                                break;
                            case TileType.DarkGrass:
                                if ((_randObstacleCode & shiftCompare) != 0)
                                {
                                    _hasObstacle[i] = 1;
                                }
                                else
                                {
                                    _hasObstacle[i] = 0;
                                }
                                break;
                            default:
                                _hasObstacle[i] = 0;
                                break;
                        }
                        if (i > 4)
                        {
                            shiftCompare <<= 1;
                        }

                    }

                }
                break;
            }
            else
            {
                _randObstacleCode = RandomTenBinaryDigitsGenerator(4);
            }
        }
        
        
        return _hasObstacle;

    }

    public static int RandomTenBinaryDigitsGenerator(int howManyObstacle)
    {
        int randNum;
        while(true)
        {
            randNum = Random.Range(0, 1024);

            int tempComp = 1;
            int cnt = 0;
            for(int i = 0; i < 10; ++i)
            {
                if ((randNum & tempComp) != 0)
                {
                    ++cnt;
                }
                tempComp <<= 1;
            }

            if (cnt <= howManyObstacle)
            {
                break;
            }
        }

        return randNum;

    }


    static bool CanGoThrough(TileLine prevTile, int curObstacleCode)
    {
        if ((prevTile._obstacleCode & curObstacleCode) != CANT_PASS)
        {
            return true;
        }
        return false;
    }


}
