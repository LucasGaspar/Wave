using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
	public GameObject hexagonPrefab;

	public int radius;
	public float pieceSize;

	void Start ()
	{
		StartCoroutine(GenerateHexagonalMap());
	}

	IEnumerator GenerateHexagonalMap ()
	{
		int maxSize = radius-1;
		Map.InitializeMap((maxSize*2)+1);

		Vector2 position = new Vector2();

		for (int r = 0; r < radius; r++)
		{
			if(r == 0)
			{
				CreateHexagon(position, maxSize, maxSize);
				continue;
			}

			float degrees = 60;
			position = (DegreeToVector2(degrees) * (pieceSize/2) * r);
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
					CreateHexagon(position, coordinatesX+maxSize, coordinatesY+maxSize);
					yield return new WaitForEndOfFrame();

					position += (DegreeToVector2(degrees) * (pieceSize/2));
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
		return new Vector3 (vector2.x, 0, vector2.y);
	}

	void CreateHexagon(Vector2 position, int coordinatesX, int coordinatesY)
	{
		Hexagon hexagon = ((GameObject) Instantiate(hexagonPrefab, Vector2ToVector3(position), Quaternion.Euler(new Vector3(0,30,0)))).GetComponent<Hexagon>();;
		hexagon.transform.SetParent(transform);
		hexagon.SetCoordinates(coordinatesX, coordinatesY);
		Map.AddHexagon(hexagon, coordinatesX, coordinatesY);
	}
}
