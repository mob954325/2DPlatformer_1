using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface EnemyState
{
    void BeforeAction();
    void InAction();
    void AfterAction();
}
