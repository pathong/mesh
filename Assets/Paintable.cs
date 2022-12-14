using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{


    private Mesh _mesh;

    private void Awake()
    {
	MeshFilter meshFilter = GetComponent<MeshFilter>();

	_mesh = meshFilter.mesh;

	//create new colors array where the colors will be created
	var colors = new Color[_mesh.vertices.Length];
	for (var k = 0; k < colors.Length; k++)
	{
	    colors[k] = Color.white;
	}
	_mesh.colors = colors;
	_mesh.RecalculateNormals();
    }
}
