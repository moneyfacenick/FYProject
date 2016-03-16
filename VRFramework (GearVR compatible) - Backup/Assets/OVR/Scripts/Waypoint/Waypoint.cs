using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
    float accel = 50.0f; //This is the rate of accelleration after the function "Accell()" is called. Higher values will cause the object to reach the "speedLimit" in less time.

    float inertia = 0.9f; //This is the the amount of velocity retained after the function "Slow()" is called. Lower values cause quicker stops. A value of "1.0" will never stop. Values above "1.0" will speed up.

    float speedLimit = 1000.0f; //This is as fast the object is allowed to go.

    float minSpeed = 1.0f; //This is the speed that tells the functon "Slow()" when to stop moving the object.

    float stopTime = 1.0f; //This is how long to pause inside "Slow()" before activating the function "Accell()" to start the script again.

    //This variable "currentSpeed" is the major player for dealing with velocity.
    //The "currentSpeed" is mutiplied by the variable "accel" to speed up inside the function "accell()".
    //Again, The "currentSpeed" is multiplied by the variable "inertia" to slow things down inside the function "Slow()".
    private float currentSpeed = 0.0f;

    //The variable "functionState" controlls which function, "Accell()" or "Slow()", is active. "0" is function "Accell()" and "1" is function "Slow()".
    private float functionState = 0f;

    //The next two variables are used to make sure that while the function "Accell()" is running, the function "Slow()" can not run (as well as the reverse).
    private bool accelState = false;

    //This variable will store the "active" target object (the waypoint to move to).
    public Transform waypoint;

    //This is the speed the object will rotate to face the active Waypoint.
    float rotationDamping = 6.0f;

    //If this is false, the object will rotate instantly toward the Waypoint. If true, you get smoooooth rotation baby!
    bool smoothRotation = true;

    //This variable is an array. []< that is an array container if you didnt know. It holds all the Waypoint Objects that you assign in the inspector.
    private List<Transform> waypoints;

    //This variable keeps track of which Waypoint Object, in the previously mentioned array variable "waypoints", is currently active.
    private int WPindexPointer;

    public Vector3 Direction;

    public bool WaypointUpdated = false;

    //The function "Start()" is called just before anything else but only one time.
    void Start()
    {
        functionState = 0; //When the script starts set "0" or function Accell() to be active.
        GetWayPoint();
    }




    //The function "Update()" is called every frame. It can get slow if overused.
    void Update()
    {
        if (functionState == 0) //If functionState variable is currently "0" then run "Accell()". Withouth the "if", "Accell()" would run every frame.
        {
            Accell();
        }
        waypoint = waypoints[WPindexPointer]; //Keep the object pointed toward the current Waypoint object.

        Direction = waypoint.position - transform.position;
        Direction.Normalize();
    }




    //I declared "Accell()".
    void Accell()
    {
        if (accelState == false) //
        {                   //
            accelState = true;    //Make sure that if Accell() is running, Slow() can not run.
        }
        //
        //I grabbed this next part from the unity "SmoothLookAt" script but try to explain more.
        if (waypoint) //If there is a waypoint do the next "if".
        {
            if (smoothRotation) //If smoothRotation is set to "On", do the rotation over time with nice ease in and ease out motion.
            {
                //Look at the active waypoint.
                var rotation = Quaternion.LookRotation(waypoint.position - transform.position);
                //Make the rotation nice and smooth.
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            }
        }
        //Now do the accelleration toward the active waypoint untill the "speedLimit" is reached
        currentSpeed = currentSpeed + accel * accel;
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        //When the "speedlimit" is reached or exceeded ...
        if (currentSpeed >= speedLimit)
        {
            // ... turn off accelleration and set "currentSpeed" to be exactly the "speedLimit". Without this, the "currentSpeed will be slightly above "speedLimit"
            currentSpeed = speedLimit;
        }
    }




    //The function "OnTriggerEnter" is called when a collision happens.
    public void OnTriggerEnter()
    {
        WPindexPointer++;  //When the GameObject collides with the waypoint's collider, change the active waypoint to the next one in the array variable "waypoints".

        //When the array variable reaches the end of the list ...
        if (WPindexPointer >= waypoints.Count)
        {
            WPindexPointer = 0; // ... reset the active waypoint to the first object in the array variable "waypoints" and start from the beginning.
        }

        WaypointUpdated = true;
    }

    void GetWayPoint()
    {
        Transform getwaypoints = waypoint.GetComponentInChildren<Transform>();
        waypoints = new List<Transform>();

        foreach (Transform findwaypoints in getwaypoints)
        {
            waypoints.Add(findwaypoints);
        }
    }
}

