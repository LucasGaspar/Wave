using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	static Hexagon[,] map;

	public static void InitializeMap(int maxSize)
	{
		map = new Hexagon[maxSize,maxSize];
	}

	public static void AddHexagon(Hexagon hexagon, int x, int y)
	{
		map[x,y] = hexagon;
	}

	public static List<Hexagon> GetListOfHexagon (Hexagon center, int radius)
	{
		List<Hexagon> list = new List<Hexagon>();

		if(radius == 0)
		{
			TryAddToList(list, center.coordinates.x, center.coordinates.y);
			return list;
		}

		int x;
		int y;

		x = -radius+center.coordinates.x;
		for(y = 0+center.coordinates.y; y <= radius+center.coordinates.y; y++)
		{
			TryAddToList(list, x, y);
		}

		x = radius+center.coordinates.x;
		for(y = 0+center.coordinates.y; y >= -radius+center.coordinates.y; y--)
		{
			TryAddToList(list, x, y);
		}

		y = -radius+center.coordinates.y;
		for(x = 0+center.coordinates.x; x < radius+center.coordinates.x; x++)
		{
			TryAddToList(list, x, y);
		}

		y = radius+center.coordinates.y;
		for(x = 0+center.coordinates.x; x > -radius+center.coordinates.x; x--)
		{
			TryAddToList(list, x, y);
		}

		x = -radius+1+center.coordinates.x;
		y = -1+center.coordinates.y;
		for(; x < center.coordinates.x; x++ , y--)
		{
			TryAddToList(list, x, y);
		}

		x = radius-1+center.coordinates.x;
		y = 1+center.coordinates.y;
		for(; x > center.coordinates.x; x-- , y++)
		{
			TryAddToList(list, x, y);
		}

		//////////////////////////////////////////

		/*string s = "";
		for(int i = 0; i< list.Count; i++)
		{
			s = s+list[i].coordinates+"  ";
		}
		print(s);*/

		////////////////////////////////////////

		return list;
	}

	static void TryAddToList(List<Hexagon> list, int x, int y)
	{
		if(x < 0 || y < 0)
			return;
		if(x >= map.GetLength(0) || y >= map.GetLength(0))
			return;
		if(map[x,y] == null)
			return;
		list.Add(map[x,y]);
	}
}
