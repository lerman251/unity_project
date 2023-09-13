using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellData : MonoBehaviour
{
    [Header("Shooting")]
    public float damage;
    public float maxDistance;
   
    [Header("Reloading")]
    public int currentMana;
    public int totalMana;
    public float castRate;
    public float manaRecharge;
    public bool recharging;
}
