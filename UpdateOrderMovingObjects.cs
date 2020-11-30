using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOrderMovingObjects : MonoBehaviour
{
	[SerializeField] private int internalSort = 0;

	private SpriteRenderer spriteRenderer;
	private Transform objectTransform;

	public int sortingOrderFactor = 100;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		objectTransform = GetComponent<Transform>();
		spriteRenderer.sortingOrder = (int)(objectTransform.position.y * -sortingOrderFactor) + internalSort;
	}

	void Update()
	{
		spriteRenderer.sortingOrder = (int)(objectTransform.position.y * -sortingOrderFactor) + internalSort;
	}
}
