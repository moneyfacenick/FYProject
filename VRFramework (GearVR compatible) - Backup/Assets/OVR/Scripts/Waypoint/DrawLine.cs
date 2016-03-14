using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrawLine : MonoBehaviour
{
    private List<Transform> DrawWayPoints;
    public Transform WayPoints;
    public Color rayColor = Color.red;

    void Start()
    {
    }
    void Update()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Transform getwaypoint = WayPoints.GetComponentInChildren<Transform>();
        DrawWayPoints = new List<Transform>();

        foreach (Transform findwaypoint in getwaypoint)
        {
            DrawWayPoints.Add(findwaypoint);
        }

        foreach (Transform findwaypoint in getwaypoint)
        {
            if (findwaypoint == WayPoints)
            DrawWayPoints.Add(findwaypoint);
        }

        for(int i = 0; i < DrawWayPoints.Count - 1; i ++)
        {
            Transform posObj = DrawWayPoints[i].transform;
            Vector3 pos = posObj.position;

            if( i > 0 )
            {
                Transform prevObj = DrawWayPoints[i - 1].transform;
                Vector3 prev = prevObj.position;

                Gizmos.DrawLine(prev, pos);
            }
        }
    }
}
