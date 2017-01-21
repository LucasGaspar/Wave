using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
	public GameObject hexagonPrefab;

	public int rows;
	public int columns;
	public float sizeSquare;

	public int radius;
	public float size;

    private float y;


	void Start ()
	{
        y = transform.position.y;
		StartCoroutine(GenerateHexagonalMap());
		//StartCoroutine(GenerateSquareMap());
	}

	IEnumerator GenerateSquareMap ()
	{
		Vector3 position = new Vector3();
		for (int x = 0; x <= columns; x++)
		{
			for (int z = 0; z <= rows; z++)
			{
				position.x = x*(sizeSquare);
				if(x%2 == 0)
					position.z = z*(sizeSquare);
				else
					position.z = z*(sizeSquare)+(sizeSquare/2);
				GameObject hexagon = (GameObject) Instantiate(hexagonPrefab, position, Quaternion.identity);
				hexagon.transform.SetParent(transform);
				yield return new WaitForEndOfFrame();
			}
		}
	}

	IEnumerator GenerateHexagonalMap ()
	{
		Vector2 position = new Vector2();

		for (int r = 0; r < radius; r++)
		{
			if(r == 0)
			{
				Hexagon hexagon = ((GameObject) Instantiate(hexagonPrefab, new Vector3(0,y,0), Quaternion.Euler(new Vector3(0,30,0)))).GetComponent<Hexagon>();
				hexagon.transform.SetParent(transform);
				hexagon.SetCoordinates(0,0);
				continue;
			}

			float degrees = 60;
			position = (DegreeToVector2(degrees) * (size/2) * r);
			degrees = -60;

			int coordinatesX = r;
			int coordinatesY = -r;
			int coordinatesXDisplacement = 0;
			int coordinatesYDisplacement = 0;

			for (int l = 0; l < 6; l++)
			{
				switch(l)
				{
				case 0: coordinatesXDisplacement = 0; coordinatesYDisplacement = 1;
					break;
				case 1: coordinatesXDisplacement = -1; coordinatesYDisplacement = 1;
					break;
				case 2: coordinatesXDisplacement = -1; coordinatesYDisplacement = 0;
					break;
				case 3: coordinatesXDisplacement = 0; coordinatesYDisplacement = -1;
					break;
				case 4: coordinatesXDisplacement = 1; coordinatesYDisplacement = -1;
					break;
				case 5: coordinatesXDisplacement = 1; coordinatesYDisplacement = 0;
					break;
				}

				for (int p = 0; p < r; p++)
				{
					Hexagon hexagon = ((GameObject) Instantiate(hexagonPrefab, Vector2ToVector3(position), Quaternion.Euler(new Vector3(0,30,0)))).GetComponent<Hexagon>();;
					hexagon.transform.SetParent(transform);
					yield return new WaitForEndOfFrame();
					hexagon.SetCoordinates(coordinatesX, coordinatesY);

					position += (DegreeToVector2(degrees) * (size/2));
					coordinatesX += coordinatesXDisplacement;
					coordinatesY += coordinatesYDisplacement;
				}
				degrees -= 60;
			}
		}
	}

	Vector2 RadianToVector2(float radian)
	{
		return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
	}

	Vector2 DegreeToVector2(float degree)
	{
		return RadianToVector2(degree * Mathf.Deg2Rad);
	}

	Vector3 Vector2ToVector3(Vector2 vector2)
	{
		return new Vector3 (vector2.x, y, vector2.y);
	}
}
