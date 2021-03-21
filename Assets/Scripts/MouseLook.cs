using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public bool m_CursorLocked = true;

    public float m_Sensitivity = 1.0f;
    public float m_Spin = 0.0f;
    public float m_Tilt = 0.0f;
    public Vector2 m_TiltExtents = new Vector2(-85.0f, 85.0f);
 
    private void Awake()
    {
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = m_CursorLocked ? CursorLockMode.Confined : CursorLockMode.None;
        Cursor.visible = !m_CursorLocked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_CursorLocked = !m_CursorLocked;
            LockCursor();     
        }

        if (!m_CursorLocked)
        {
            return;
        }

        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        m_Spin += x * m_Sensitivity;
        m_Tilt -= y * m_Sensitivity;

        m_Tilt = Mathf.Clamp(m_Tilt, m_TiltExtents.x, m_TiltExtents.y);

        transform.localEulerAngles = new Vector3(m_Tilt, m_Spin, 0.0f);
    }
}
