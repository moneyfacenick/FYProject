using UnityEngine;
using UnityEngine.VR;
using System.Collections;

// not actual FPS camera script
public class PlayerMouseLookDebug : MonoBehaviour {
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;

	void Start()
	{
		if(VRDevice.isPresent)
			this.enabled = false;
		else
			transform.parent.parent.GetComponent<OVRCameraRig>().enabled = false;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.LeftBracket))
		{
			sensitivityX = Mathf.Max(sensitivityX-1, 0);
			sensitivityY = Mathf.Max(sensitivityY-1, 0);
		}
		if(Input.GetKeyDown(KeyCode.RightBracket))
		{
			sensitivityX = Mathf.Min(sensitivityX+1, 30);
			sensitivityY = Mathf.Min(sensitivityY+1, 30);
		}

		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}
