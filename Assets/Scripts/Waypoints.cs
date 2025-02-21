using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//add System.Collections & Systems.Collections.Generic
public class Waypoints : MonoBehaviour
{
    // OnDrawGizmos adds selectable elements to objects (or 'gizmos') in scene that aren't otherwise selectable (ie empty Game Objects)

    [Range (0f, 2f)] //
    [SerializeField] private float waypointSize = 1f;
    
    private void OnDrawGizmos()
    {
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, 1f);
        }

        Gizmos.color = Color.red;
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }
    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }

        else
        {
            return transform.GetChild(0);
        }
    }
}
