using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enemy 
{
    public class EnemyFollow : EnemyBase
    {
        private Vector3 _player;

        public float speed;

        public override void Update()
        {
            _player = WorldManager.Instance.Player.transform.position;

            transform.position = Vector3.MoveTowards(transform.position, _player, speed * Time.deltaTime);

            transform.LookAt(_player);
        }
    }
}

