using UnityEngine;

namespace Tafang.Pathing
{
    public class WaypointPath : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;

        public int Count
        {
            get { return waypoints == null ? 0 : waypoints.Length; }
        }

        public Transform GetWaypoint(int index)
        {
            if (waypoints == null || index < 0 || index >= waypoints.Length)
            {
                return null;
            }

            return waypoints[index];
        }

        private void OnValidate()
        {
            if (waypoints != null && waypoints.Length > 0)
            {
                return;
            }

            int childCount = transform.childCount;
            if (childCount == 0)
            {
                return;
            }

            waypoints = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                waypoints[i] = transform.GetChild(i);
            }
        }

        private void OnDrawGizmos()
        {
            if (waypoints == null || waypoints.Length == 0)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            for (int i = 0; i < waypoints.Length; i++)
            {
                Transform point = waypoints[i];
                if (point == null)
                {
                    continue;
                }

                Gizmos.DrawSphere(point.position, 0.12f);

                if (i + 1 < waypoints.Length && waypoints[i + 1] != null)
                {
                    Gizmos.DrawLine(point.position, waypoints[i + 1].position);
                }
            }
        }
    }
}

