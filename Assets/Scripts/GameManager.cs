using UnityEngine;
using System.Collections;

public static class GameManager {

    public static char ITEM = 'i';
    public static char COIN = 'c';
    public static char BORDER = 'b';

    public static char[,] level;

    public static float tileHeight;
    public static float tileWidth;
    public static Vector2 tileZeroZeroPosition;

    public static void TestLevel1()
    {
        level = new char[,]
        {
            { BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER },
            { BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
            { BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER },
            { BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER },
            { BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
            { BORDER, COIN,   BORDER, BORDER, BORDER, COIN,   BORDER },
            { BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
            { BORDER, COIN,   BORDER, BORDER, BORDER, COIN,   BORDER },
            { BORDER, COIN,   BORDER, BORDER, BORDER, COIN,   BORDER },
            { BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
            { BORDER, BORDER, BORDER, COIN,   BORDER, BORDER, BORDER },
            { BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
            { BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER }
        };
    }

}
