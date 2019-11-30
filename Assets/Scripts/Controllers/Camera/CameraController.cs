using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform m_Target;
    public float smoothTime;
    public Vector3 m_Offset;

    //CameraShake
    private float shakeAmount = 0;


    private void Start()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(m_Target.transform.position.x + m_Offset.x, m_Offset.y, -10);
    }


    public void Shake(float amount, float leght)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", leght);
    }

    void BeginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPos.x += offsetX;
            camPos.y += offsetY;

            transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
    }
}
