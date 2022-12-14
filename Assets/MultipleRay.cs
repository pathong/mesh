using UnityEngine;

public class MultipleRay : MonoBehaviour
{

	void Update(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit h;

		if(Physics.Raycast(ray, out h)){
			//Debug.Log(h.point);	
			RaycastHit[] sphereHits = Physics.SphereCastAll(h.point, 1, transform.forward);
			

			








		}
		
	}

}
