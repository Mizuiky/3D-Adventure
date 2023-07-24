using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{
    public void LoadLife(int life, int startLife)
    {
        _startLife = startLife;

        if(life <=0)
        {
            _currentLife = startLife;
        }
        else
        {
            _currentLife = life;
        }
         
        UpdateUi(_currentLife / _startLife);       
    }
}
