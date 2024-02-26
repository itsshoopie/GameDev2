using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "GunData", menuName = "GunData")]
public class GunData : ScriptableObject
{
    public float range = 1000f;
    public int ammo_per_clip = 12;
    public bool automatic = false;
    public float primary_fire_delay = 0.5f;
    [Range(0f, 90f)] public float spread = 0.0f;
}
