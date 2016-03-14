using UnityEngine;
using System.Collections;

public class CameraDirection : MonoBehaviour {

    public float smooth = 2.5f;

    public GameObject CubeMount;

    private Quaternion previousLookAtRotation;

    private Quaternion lookAtRotation;

    private Quaternion finalRotation;

    private bool rotate = false;

    float angle = 0;

	// Use this for initialization
	void Start () {
        previousLookAtRotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {

        if(CubeMount.GetComponent<Waypoint>().WaypointUpdated)
        {
            UpdateCameraView();
        }

        Debug.Log("Transform" + transform.rotation);

        Debug.Log("Look At" + lookAtRotation);

        Debug.Log(angle);

        if(rotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, smooth * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, finalRotation) < 2.5f)
            {
                transform.rotation = finalRotation;
                rotate = false;
            }
        }
	}

    void UpdateCameraView()  {
        lookAtRotation = Quaternion.LookRotation(CubeMount.GetComponent<Waypoint>().Direction, Vector3.up);

        if (previousLookAtRotation == Quaternion.identity || Quaternion.Angle(lookAtRotation, previousLookAtRotation) > 2.5f)
        {
            var forwardB = lookAtRotation * Vector3.forward;
            var forwardA = previousLookAtRotation * Vector3.forward;

            var angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
            var angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;

            float angle = Mathf.DeltaAngle(angleA, angleB);

            //angle = Quaternion.Angle(lookAtRotation, previousLookAtRotation);
            previousLookAtRotation = lookAtRotation;
            finalRotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + angle, Vector3.up);
            rotate = true;
        }

        else
        {
            rotate = false;
        }

        CubeMount.GetComponent<Waypoint>().WaypointUpdated = false;
    }
}
