using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AMMO/Character Motor Data", fileName = "Default Motor Data")]
public class CharacterMotorData : ScriptableObject
{
    public float m_MoveSpeed = 8.0f;
    public float m_Gravity = 20.0f;
    public float m_JumpSpeed = 10.0f;
    public float m_Acceleration = 8.0f;
    public AnimationCurve m_Friction = AnimationCurve.EaseInOut(0.0f, 0.1f, 1.0f, 1.0f);
    public float m_StopFriction = 2.0f;
}
