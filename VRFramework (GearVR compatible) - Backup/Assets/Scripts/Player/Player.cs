using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VR = UnityEngine.VR;

public class Player : MonoBehaviour {
	protected static Player _instance = null;
	public static Player Instance 
	{
		get 
		{
			if(_instance == null)
				_instance = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			
			return _instance;
		}
	}

	public bool allowMovement = false;				// if true, player can move - used in OVRPlayer

	public OVRPlayerController playerController { get; private set; }

	void Awake() 
	{
		if(_instance == null)
			_instance = this;
		else if(_instance != this)
			Destroy(gameObject);
		
		playerController = GetComponent<OVRPlayerController>();
	}
	
	void Update() 
	{
		if(Input.GetKeyDown(KeyCode.Space) || OVRGamepadController.GPC_GetButtonDown(OVRGamepadController.Button.Back))
			VR.InputTracking.Recenter();
	}
}