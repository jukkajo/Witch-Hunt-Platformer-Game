using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    public static UnityAction<GameObject, int> damageTaken;
    public static UnityAction<GameObject, int> damageHealed;
    public static UnityAction<GameObject, int> treasurePicked;
}
