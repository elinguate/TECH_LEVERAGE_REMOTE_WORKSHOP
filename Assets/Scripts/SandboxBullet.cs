using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxBullet : MonoBehaviour
{
    public struct BulletData
    {
        public enum BulletType
        {
            DEFAULT,
            BOMB
        }
        public BulletType m_Type;

        public float m_MoveSpeed;
        public float m_Radius;
        public int m_Damage;

        public bool m_AffectedByGravity;
        public float m_Gravity;

        public int m_Pierces;
    }
    public BulletData m_Data;

    public LayerMask m_HittableLayerMask;

    private Vector3 m_Velocity;
    public float m_PierceCooldown = 0.25f;
    private float m_PierceTimer = 0.0f;

    public GameObject m_DefaultHitEffectPrefab;
    public GameObject m_BombHitEffectPrefab;

    public void Initialize(BulletData _data)
    {
        m_Data = _data;
        m_Velocity = transform.forward * m_Data.m_MoveSpeed;
    }

    private void Update()
    {
        m_PierceTimer -= Time.deltaTime;
        if (m_Data.m_AffectedByGravity)
        {
            m_Velocity.y -= m_Data.m_Gravity * Time.deltaTime;
        }
        Vector3 move = m_Velocity * Time.deltaTime;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, m_Data.m_Radius, move.normalized, out hit, move.magnitude, m_HittableLayerMask))
        {
            GameObject effectPrefab = m_Data.m_Type == BulletData.BulletType.BOMB ? m_BombHitEffectPrefab : m_DefaultHitEffectPrefab;
            Instantiate(effectPrefab, hit.point, Quaternion.LookRotation(hit.normal, hit.normal));

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(m_Data.m_Damage);
            }
            m_Data.m_Pierces -= 1;
            if (m_Data.m_Pierces <= 0)
            {
                Destroy(gameObject);
            }
        }

        transform.position += move;
    }
}
