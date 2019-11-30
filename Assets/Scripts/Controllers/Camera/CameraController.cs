using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform m_Target;
    [Range(0.01f, 0.1f)] public float smoothTime;
    [Range(0.01f, 5f)] public float m_VerticalOffset;

    //CameraShake
    private float shakeAmount = 0;


    private void Start()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 smoothposition = Vector3.Lerp(transform.position, new Vector3(m_Target.transform.position.x, m_Target.transform.position.y + m_VerticalOffset, -10), smoothTime);
        transform.position = smoothposition;
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

    public void SetVerticalOffset()
    {
        m_VerticalOffset *= -1;
    }
}
