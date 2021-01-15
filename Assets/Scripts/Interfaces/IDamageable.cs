using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    event Action OnReceiveDamage;
    void DoDamage(float damage);
}
