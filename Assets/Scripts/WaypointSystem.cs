using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaypointSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> waypoint = new List<Transform>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (waypoint.Count > 0)
        {
            foreach (Transform t in waypoint)
                Gizmos.DrawSphere(t.position, 1f);


            Gizmos.color = Color.red;

            for (int a = 0; a < waypoint.Count-1; a++)
            {
                Gizmos.DrawLine(waypoint[a].position,waypoint[a+1].position );
            }
        }
    }
}
