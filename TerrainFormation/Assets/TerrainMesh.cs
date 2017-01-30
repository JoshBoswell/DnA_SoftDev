using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshCollider))]
public class TerrainMesh : MonoBehaviour
{
	[SerializeField] private int xSize;
	[SerializeField] private int zSize;

	private MeshFilter meshFilter;
	private MeshCollider meshCollider;
	private float[,] heightMap;

	void Awake(){
		meshFilter = GetComponent<MeshFilter>();
		//meshCollider = GetComponent<MeshCollider>();
		heightMap = new float[xSize, zSize];
		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {
				heightMap[x, z] = 0;
			}
		}
	}

	void Start () {
		UpdateMesh();
	}

	private void UpdateMesh(){
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

		for (int x = 0; x < xSize; x++) {
			for (int z = 0; z < zSize; z++) {
				vertices.Add(new Vector3(x, heightMap[x, z], z));

				if (x == xSize - 1 || z == zSize - 1)
					continue;

				triangles.Add(z + x*zSize);
				triangles.Add(z+1 + x*zSize);
				triangles.Add(z+1 + (x+1)*zSize);

				triangles.Add(z+1 + (x+1)*zSize);
				triangles.Add(z + (x+1)*zSize);
				triangles.Add(z + x*zSize);
			}
		}

		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
		meshFilter.mesh = mesh;
		//meshCollider.sharedMesh = mesh;
	}

	public float getHeightAtPosition(Vector3 position){

		int x = (int)Mathf.Round(position.x);
		int z = (int)Mathf.Round(position.z);

		return this.heightMap[x, z];
	}

	public void addHeightToPosition(Vector3 position)
	{
		int x = (int)Mathf.Round(position.x);
		int z = (int)Mathf.Round(position.z);


		heightMap [x, z] += 1;
		UpdateMesh();
	}
}
