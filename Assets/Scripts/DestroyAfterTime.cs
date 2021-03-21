using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float m_Time = 5.0f;

    private void Update()
    {
        m_Time -= Time.deltaTime;
        if (m_Time < 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
