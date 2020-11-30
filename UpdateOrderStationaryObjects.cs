using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOrderStationaryObjects : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Transform objectTransform;

	public int sortingOrderFactor = 100;

	// sorting order messes up at levels more negative than -32769;
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		objectTransform = GetComponent<Transform>();

		if (spriteRenderer != null)
		{
			spriteRenderer.sortingOrder = (int)(objectTransform.position.y * -sortingOrderFactor);
		}

	}
}
