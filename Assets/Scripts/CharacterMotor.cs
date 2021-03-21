using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    [Header("Internal Components")]
    public CharacterController m_AttachedController;
    public MouseLook m_Look;

    [Header("Motion")]
    public CharacterMotorData m_Data;

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
            m_Velocity.y = m_Data.m_JumpSpeed;
            m_IsGrounded = false;
        }

        Vector3 inputVelocity = new Vector3(x, 0.0f, z);
        inputVelocity = Quaternion.Euler(0.0f, m_Look.m_Spin, 0.0f) * inputVelocity;
        if (inputVelocity.magnitude > 1.0f)
        {
            inputVelocity.Normalize();
        }

        float cacheY = m_Velocity.y;
        m_Velocity.y = 0.0f;

        m_Velocity += inputVelocity * m_Data.m_Acceleration * Time.deltaTime;
        Vector3 cacheVelocity = m_Velocity;
        m_Velocity -= m_Velocity.normalized * m_Data.m_Friction.Evaluate(m_Velocity.magnitude) * m_Data.m_Acceleration * Time.deltaTime;
        if (Vector3.Dot(cacheVelocity.normalized, m_Velocity.normalized) < -0.5f)
        {
            m_Velocity.x = 0.0f;
            m_Velocity.z = 0.0f;
        }

        m_Velocity.y = cacheY;
        m_Velocity.y -= m_Data.m_Gravity * Time.deltaTime;

        Vector3 trueVelocity = m_Velocity;
        trueVelocity.x *= m_Data.m_MoveSpeed;
        trueVelocity.z *= m_Data.m_MoveSpeed;

        m_AttachedController.Move(trueVelocity * Time.deltaTime);

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
