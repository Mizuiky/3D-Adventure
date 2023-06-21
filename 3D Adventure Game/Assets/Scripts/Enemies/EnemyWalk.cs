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

        // Update is called once per frame
        void Update()
        {
            if(Vector3.Distance(transform.position, waypoints[index].position) < minDistance)
            {
                index++;
                if(index >= waypoints.Length)
                {
                    index = 0;
                }            
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[index].position, Time.deltaTime * speed);
            transform.LookAt(waypoints[index].position);
        }
    }
}

