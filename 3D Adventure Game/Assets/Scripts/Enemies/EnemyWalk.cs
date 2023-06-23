using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enemy
{
    public class EnemyWalk : EnemyBase
    {
        [Header("Waypoints")]
        public Transform[] waypoints;
        public float minDistance = 1f;
        public float speed;

        public int index;

        private Vector3 _lastPosition;

        // Update is called once per frame
        void Update()
        {
            if (isAlive)
            {
                if (Vector3.Distance(transform.position, waypoints[index].position) < minDistance)
                {
                    index++;
                    if (index >= waypoints.Length)
                    {
                        index = 0;
                    }
                }

                transform.position = Vector3.MoveTowards(transform.position, waypoints[index].position, Time.deltaTime * speed);

                _lastPosition = transform.position;

                transform.LookAt(waypoints[index].position);
            }
            else
            {
                transform.position = _lastPosition;
            }
        }
    }
}

