using UnityEngine;
using System.Collections;

public class CameraMount : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = new Vector3 (target.transform.position.x, target.transform.position.y + 2, target.transform.position.z);
        transform.position = targetPos;
	}
}
