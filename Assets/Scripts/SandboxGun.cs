﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandboxGun : MonoBehaviour
{
    public Text m_TextDisplay;

    public GunComponentData m_Data;

    public Transform m_FireOrigin;
    public GameObject m_ProjectilePrefab;

    public float m_ReloadLength;
    private float m_ReloadTimer = 0.0f;
    private bool m_IsReloading = false;

    public float m_FireRate = 1.0f;
    private float m_FireTimer = 0.0f;

    public int m_MaxClip = 16;
    public int m_Clip = 0;

    public int m_MaxPool = 48;
    public int m_Pool = 0;

    public int m_AmmoRegen = 1;
    public float m_AmmoRegenLength = 1.0f;
    private float m_AmmoRegenTimer = 0.0f;

    private void Awake()
    {
        m_Clip = m_MaxClip;
        m_Pool = m_MaxPool;
    }

    public void AddAmmo(int _ammo)
    {
        m_Pool += _ammo;
        m_Pool = Mathf.Min(m_Pool, m_MaxPool);
    }

    private void FireProjectile()
    {
        Instantiate(m_ProjectilePrefab, m_FireOrigin.position, m_FireOrigin.rotation);
    }

    private void ReloadGun()
    {
        int diff = m_MaxClip - m_Clip;
        m_Clip = m_MaxClip;
        m_Pool -= diff;
        if (m_Pool < 0)
        {
            m_Clip += m_Pool;
        }
    }

    private void Update()
    {
        m_FireTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (m_Clip > 0 && m_FireTimer <= 0.0f)
            {
                FireProjectile();
                m_Clip--;
                m_FireTimer = 1.0f / m_FireRate;

                m_IsReloading = false;
            }
        }

        if ((Input.GetKey(KeyCode.R) || m_Clip == 0) && !m_IsReloading && m_FireTimer <= 0.0f)
        {
            m_IsReloading = true;
            m_ReloadTimer = m_ReloadLength;
        }

        if (m_IsReloading)
        {
            m_ReloadTimer -= Time.deltaTime;
            if (m_ReloadTimer <= 0.0f)
            {
                ReloadGun();
                m_IsReloading = false;
            }
        }

        m_AmmoRegenTimer -= Time.deltaTime;
        if (m_AmmoRegenTimer <= 0.0f)
        {
            AddAmmo(m_AmmoRegen);
            m_AmmoRegenTimer = m_AmmoRegenLength;
        }

        m_TextDisplay.text = m_Clip + " / " + m_Pool;
    }
}
