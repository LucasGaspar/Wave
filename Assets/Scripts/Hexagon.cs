using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hexagon : MonoBehaviour
{
	float rand;
	float rand2;

	int xPos;
	int yPos;
	public TextMesh textmesh;

	void Start()
	{
		rand = Random.value;
		rand2 = Random.value;
	}

	void Update ()
	{
		this.transform.position += new Vector3(0,Mathf.Sin((Time.time+rand)*5)*(Time.deltaTime*0.2f*rand2),0);
	}

	public void SetCoordinates(int x, int y)
	{
		this.xPos = x;
		this.yPos = y;
		textmesh.text = x+", "+y;

		if(x == -1 && y == 0)
		{
			GetRadius(2);
		}
	}

	void GetRadius(int radius)
	{
		List<Vector2> list = new List<Vector2>();
		int x;
		int y;

		x = -radius+xPos;
		for(y = 0+yPos; y <= radius+yPos; y++)
		{
			list.Add(new Vector2(x,y));
		}

		x = radius+xPos;
		for(y = 0+yPos; y >= -radius+yPos; y--)
		{
			list.Add(new Vector2(x,y));
		}

		y = -radius+yPos;
		for(x = 0+xPos; x < radius+xPos; x++)
		{
			list.Add(new Vector2(x,y));
		}

		y = radius+yPos;
		for(x = 0+xPos; x > -radius+xPos; x--)
		{
			list.Add(new Vector2(x,y));
		}

		x = -radius+1+xPos;
		y = -1+yPos;
		for(; x < xPos; x++ , y--)
		{
			list.Add(new Vector2(x,y));
		}

		x = radius-1+xPos;
		y = 1;
		for(; x > xPos; x-- , y++)
		{
			list.Add(new Vector2(x,y));
		}

		//////////////////////////////////////////

		string s = "";
		for(int i = 0; i< list.Count; i++)
		{
			s = s+list[i]+"  ";
		}
		print(s);
	}
}
