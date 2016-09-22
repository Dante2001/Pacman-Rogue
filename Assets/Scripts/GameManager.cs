﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ItemHelper;

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

	public static void TestLevel2()
	{
		level = new char[,]
		{
			{ BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER },
			{ BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
			{ BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER },
			{ BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
			{ BORDER, COIN,   BORDER, BORDER, COIN,   BORDER, BORDER, COIN,   BORDER },
			{ BORDER, COIN,   BORDER, COIN,   COIN,   COIN,   BORDER, COIN,   BORDER },
			{ BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER },
			{ BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
			{ BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER },
			{ BORDER, COIN,   BORDER, COIN,   COIN,   COIN,   BORDER, COIN,   BORDER },
			{ BORDER, COIN,   BORDER, BORDER, COIN,   BORDER, BORDER, COIN,   BORDER },
			{ BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
			{ BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER, COIN,   BORDER },
			{ BORDER, COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   COIN,   BORDER },
			{ BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER },
		};
	}

	public static int hp=3;
	public static int coins=0;

	public static List<ItemType> inventory = new List<ItemType>();

}
