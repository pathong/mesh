using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float angleLerpTime;

    private Touch touch;

    public float zoomSpeed{get;set;}
	public float rotateSpeed{get;set;}
    [SerializeField] private Camera mainCam;

	[SerializeField] private Slider sliderZoom;
	[SerializeField] private Slider sliderRotate;





	private float normalFOV;


	private bool isOpen;
    
	private void Awake(){
		normalFOV = mainCam.fieldOfView;

		isOpen = true;
	}


	public void ToggleOpen() {isOpen = !isOpen;}


    void Update()
    {
		zoomSpeed = sliderZoom.value;
		rotateSpeed = sliderRotate.value;


		if(!isOpen) return;
		if(Input.touchCount == 1){
			touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Moved){


				pivot.Rotate(touch.deltaPosition.y * rotateSpeed , 0f,0f, Space.Self);
				pivot.Rotate(0f, touch.deltaPosition.x * rotateSpeed, 0f, Space.World);
			}
		}
		if(Input.touchCount == 2){
			Touch t0 = Input.GetTouch(0);
			Touch t1 = Input.GetTouch(1);
			
			Vector2 t0_prevPos = t0.position - t0.deltaPosition;
			Vector2 t1_prevPos = t1.position - t1.deltaPosition;

			float prevPosDiff = (t0_prevPos - t1_prevPos).magnitude;
			float curDiff = (t0.position - t1.position).magnitude;


			float zoomMod = (t0.deltaPosition - t1.deltaPosition).magnitude * zoomSpeed;


			if (prevPosDiff > curDiff) mainCam.fieldOfView += zoomMod;
			else if (prevPosDiff < curDiff) mainCam.fieldOfView -= zoomMod;

			
		}

		mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView, 5f, 100f);

	
    }
	

	public void ResetCamera(){
		mainCam.fieldOfView = normalFOV;
		transform.rotation = Quaternion.Euler(0,0,0);
	}


	public void SetZoomSpeed(float v) => zoomSpeed = v;
	public void SetRotateSpeed(float v) => rotateSpeed = v;
		

	


}
