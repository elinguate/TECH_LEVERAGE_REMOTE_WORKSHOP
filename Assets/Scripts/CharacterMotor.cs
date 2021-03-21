using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    [Header("Internal Components")]
    public CharacterController m_AttachedController;
    public MouseLook m_Look;

    [Header("Motion")]
    public float m_MoveSpeed = 8.0f;
    public float m_Gravity = 20.0f;
    public float m_JumpSpeed = 10.0f;
    public float m_Acceleration = 8.0f;
    public AnimationCurve m_Friction = AnimationCurve.EaseInOut(0.0f, 0.1f, 1.0f, 1.0f);
    public float m_StopFriction = 2.0f;

    private Vector3 m_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    private bool m_IsGrounded = false;

    private void Update()
    {
        float x = 0.0f;
        float z = 0.0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            x += 1.0f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            z -= 1.0f;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            z += 1.0f;
        }

        if ((m_AttachedController.collisionFlags & CollisionFlags.Below) != 0)
        {
            m_IsGrounded = true;
        }

        if (m_IsGrounded && Input.GetKey(KeyCode.Space))
        {
            m_Velocity.y = m_JumpSpeed;
            m_IsGrounded = false;
        }

        Vector3 inputVelocity = new Vector3(x, 0.0f, z);
        inputVelocity = Quaternion.Euler(0.0f, m_Look.m_Spin, 0.0f) * inputVelocity;

        m_Velocity.y -= m_Gravity * Time.deltaTime;
        m_Velocity.x = inputVelocity.x * m_MoveSpeed;
        m_Velocity.z = inputVelocity.z * m_MoveSpeed;

        m_AttachedController.Move(m_Velocity * Time.deltaTime);

        if ((m_AttachedController.collisionFlags & CollisionFlags.Below) != 0)
        {
            m_Velocity.y = -0.1f;
        }
        if ((m_AttachedController.collisionFlags & CollisionFlags.Above) != 0)
        {
            m_Velocity.y = -0.1f;
        }
    }
}
