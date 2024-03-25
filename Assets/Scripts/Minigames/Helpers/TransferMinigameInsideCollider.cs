using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMinigameInsideCollider : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			// Add handling - it should reset Player's cube to the start position
			Debug.Log("Touched inside");
		}
	}
}
