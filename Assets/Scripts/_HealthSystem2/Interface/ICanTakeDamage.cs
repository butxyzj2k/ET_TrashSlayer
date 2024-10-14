using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanTakeDamage 
{
    public float Damage { get; set; }

    public void TakeHit(float damage, GameObject receiver);

}

