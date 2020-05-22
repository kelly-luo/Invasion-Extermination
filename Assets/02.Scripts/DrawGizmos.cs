using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public enum Type { WAYPOINT, ENEMYSPAWN }
    private const string wayPointFile = "Enemy";
    public Type type = Type.WAYPOINT;



    public Color _color = Color.yellow;
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        if (type == Type.WAYPOINT)
        {
            Gizmos.color = _color;

            Gizmos.DrawSphere(transform.position, _radius);
        }
        else
        {
            Gizmos.color = _color;

            Gizmos.DrawIcon(transform.position + Vector3.up * 1.0f, wayPointFile, true);
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
