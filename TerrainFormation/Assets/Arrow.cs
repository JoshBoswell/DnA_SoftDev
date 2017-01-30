using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
	public TerrainMesh terrain;

	void Update () {
		transform.Translate (new Vector3 (0, -5 * Time.deltaTime, 0));

		if (transform.position.y < terrain.getHeightAtPosition (transform.position))
		{
			terrain.addHeightToPosition(transform.position);
			Destroy (gameObject);
		}
	}
}
