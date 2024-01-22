using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointsElement : MonoBehaviour
{   [SerializeField]
    private int _playerCash = 0;
    public int PlayerCash { get { return _playerCash; }
        set { _playerCash = value; }
    
    }
    public void AddToPoints(int increaseBy)
    {
        PlayerCash += increaseBy;
        Events.treasurePicked(gameObject, increaseBy);
    }

}
