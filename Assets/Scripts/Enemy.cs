using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int m_MaxHealth = 100;
    public int m_Health = 0;

    private void Awake()
    {
        m_Health = m_MaxHealth;
    }

    public void TakeDamage(int _damage)
    {
        m_Health -= _damage;
        if (m_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
