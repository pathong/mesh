using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastTriangle : MonoBehaviour
{
	[SerializeField] private Camera cam;


	void Update(){
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100f)){
			Debug.Log(hit.triangleIndex);
		}


		

	}
}
