using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    public void Damage(int value);

    public void Damage(int value, Vector3 dir);
}
