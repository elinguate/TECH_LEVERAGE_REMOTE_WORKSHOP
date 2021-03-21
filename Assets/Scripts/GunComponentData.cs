using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AMMO/Gun Component Data", fileName = "Default Component")]
public class GunComponentData : ScriptableObject
{
    public float m_FireRate;
    public int m_Clip;
    public int m_Pool;
}
