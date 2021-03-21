using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxBullet : MonoBehaviour
{
    public float m_MoveSpeed = 8.0f;
    public float m_Radius = 0.25f;

    public int m_Damage = 1;

    public LayerMask m_HittableLayerMask;

    private void Update()
    {
        float dist = m_MoveSpeed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, m_Radius, transform.forward, out hit, dist, m_HittableLayerMask))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(m_Damage);
            }
            Destroy(gameObject);
        }

        transform.position += transform.forward * dist;
    }
}
