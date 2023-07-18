using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    public void Damage(float value);

    public void Damage(float value, Vector3 dir);
}
