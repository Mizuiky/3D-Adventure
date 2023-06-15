using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    public float shootAngle = 15f;
    public int shootAmount = 4;

    protected override void Shoot()
    {
        int mult = 0;

        var projectil1 = Instantiate(_projectil, _positionToShoot);
        projectil1.transform.position = _positionToShoot.position;

        for (int i = 0; i < shootAmount; i++)
        {
            if(i%2 == 0)
            {
                mult++;
            }

            var projectil = Instantiate(_projectil, _positionToShoot);
            projectil.transform.position = _positionToShoot.position;

            projectil.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? shootAngle : -shootAngle) * mult;
            
            projectil.transform.parent = null;

            projectil.speed = speed;
        }
    }
}
