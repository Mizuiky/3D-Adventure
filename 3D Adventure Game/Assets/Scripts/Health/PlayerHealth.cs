using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{
    public void ChangeLife(int life)
    {
        if(life > 0)
        {
            _currentLife = (float)life / (float)_startLife;
        }
        else
        {
            _currentLife = _startLife;
        }

        UpdateUi(_currentLife);       
    }
}
