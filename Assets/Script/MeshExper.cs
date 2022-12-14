using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MeshExper : MonoBehaviour
{

	private Mesh _mesh;
	public enum e_PaintColor{ Red, Blue, Green }
	private Color PaintColor;
	[Range(1f,10f)]
	[SerializeField] private float brushSize;

	private bool isOpen;
    
	private void Awake(){

		isOpen = false;
		PaintColor = Color.red;
	}




	private void Update()
	{

		if(!isOpen) return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(SystemInfo.deviceType == DeviceType.Desktop){
			if(!Input.GetMouseButton(0)) return;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}
		else if(SystemInfo.deviceType == DeviceType.Handheld){
			if(Input.touchCount == 0)return;
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		}




		if (Physics.Raycast(ray, out var hit))
		{
			if(!hit.collider.GetComponent<Paintable>()) return;
			if(!hit.collider.GetComponent<MeshFilter>()) return;


				
			_mesh = hit.collider.GetComponent<MeshFilter>().mesh;



			// Get current vertices, triangles and colors
			var vertices = _mesh.vertices;
			var triangles = _mesh.triangles;
			var colors = _mesh.colors;

			// Get the vert indices for this triangle
			var vert1Index = triangles[hit.triangleIndex * 3 + 0];
			var vert2Index = triangles[hit.triangleIndex * 3 + 1];
			var vert3Index = triangles[hit.triangleIndex * 3 + 2];

			// Get the positions for the vertices
			var vert1Pos = vertices[vert1Index];
			var vert2Pos = vertices[vert2Index];
			var vert3Pos = vertices[vert3Index];

			// Now for all three vertices we first check if any other triangle if using it
			// by simply count how often the indices are used in the triangles list
			var vert1Occurrences = 0;
			var vert2Occurrences = 0;
			var vert3Occurrences = 0;
			foreach (var index in triangles)
			{
				if (index == vert1Index) vert1Occurrences++;
				else if (index == vert2Index) vert2Occurrences++;
				else if (index == vert3Index) vert3Occurrences++;
			}

			// Create copied Lists so we can dynamically add entries
			var newVertices = vertices.ToList();
			var newColors = colors.ToList();

			// Now if a vertex is shared we need to add a new individual vertex
			// and also an according entry for the color array
			// and update the vertex index
			// otherwise we will simply use the vertex we already have
			if (vert1Occurrences > 1)
			{
				newVertices.Add(vert1Pos);
				newColors.Add(new Color());
				vert1Index = newVertices.Count - 1;
			}

			if (vert2Occurrences > 1)
			{
				newVertices.Add(vert2Pos);
				newColors.Add(new Color());
				vert2Index = newVertices.Count - 1;
			}

			if (vert3Occurrences > 1)
			{
				newVertices.Add(vert3Pos);
				newColors.Add(new Color());
				vert3Index = newVertices.Count - 1;
			}

			// Update the indices of the hit triangle to use the (eventually) new
			// vertices instead
			triangles[hit.triangleIndex * 3 + 0] = vert1Index;
			triangles[hit.triangleIndex * 3 + 1] = vert2Index;
			triangles[hit.triangleIndex * 3 + 2] = vert3Index;

			// color these vertices
			newColors[vert1Index] = PaintColor;
			newColors[vert2Index] = PaintColor;
			newColors[vert3Index] = PaintColor;

			// write everything back
			_mesh.vertices = newVertices.ToArray();
			_mesh.triangles = triangles;
			_mesh.colors = newColors.ToArray();

			_mesh.RecalculateNormals();

		}
		else
		{
		    Debug.Log("no hit");
		}
	}

	public void ColorPicker(int v){

		switch ((e_PaintColor)v)
		{
			case e_PaintColor.Red:
				PaintColor = Color.red;	
				break;
			case e_PaintColor.Blue:
				PaintColor = Color.blue;
				break;
			default:
				PaintColor = Color.red;
				break;
		}
	}




	public void TogglePaint(){
		isOpen = !isOpen;

	}


}
