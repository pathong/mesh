using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class Paintable : MonoBehaviour
{
	private Material painterableMat;
    private Mesh _mesh;

    private void Awake()
    {
		painterableMat = Resources.Load("Material/PaintableMaterial", typeof(Material)) as Material;
		this.GetComponent<MeshRenderer>().material = painterableMat;


		MeshFilter meshFilter = GetComponent<MeshFilter>();
		DisableAllColliderExceptMeshCollider();

		_mesh = meshFilter.mesh;

		InitialColor();
    }

	 void DisableAllColliderExceptMeshCollider() {
		 foreach(Collider c in GetComponents<Collider> ()) {
			 if(c.GetType() == typeof(MeshCollider)){
				 continue;
			 }
			 c.enabled = false; 
		}

     }


	 void InitialColor(){
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
