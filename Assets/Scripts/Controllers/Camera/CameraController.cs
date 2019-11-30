using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform m_Target;
    [Range(0.01f, 0.1f)] public float smoothTime;
    [Range(0.01f, 5f)] public float m_VerticalOffset;

    //CameraLight
    [HideInInspector] public Light cameraLight;

    //CameraShake
    private float shakeAmount = 0;


    private void Start()
    {
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        cameraLight = GetComponentInChildren<Light>();
        cameraLight.range = 20.0f;
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

    public void SetVerticalOffset(float time)
    {
        Invoke("SetVerticalOffsetDelay", time);
    }

    public void SetVerticalOffsetDelay()
    {
        m_VerticalOffset *= -1;
    }

    public void LightInterepolation(bool ReduceLight)
    {
        StartCoroutine(LightInterpolationPlayer(!ReduceLight));
        StartCoroutine(LightInterpolation(ReduceLight));
    }

    IEnumerator LightInterpolation(bool ReduceLight)
    {
        yield return new WaitForSeconds(Time.deltaTime * 0.2f);

        if (!ReduceLight)
        {

            if (cameraLight.range >= 20.0f)
                yield return 0;
            else
            {
                cameraLight.range += 0.4f;
                StartCoroutine(LightInterpolation(ReduceLight));
            }
        }
        else
        {
            if (cameraLight.range <= 0.0f)
                yield return 0;
            else
            {
                cameraLight.range -= 0.4f;
                StartCoroutine(LightInterpolation(ReduceLight));
            }
        }
    }

    IEnumerator LightInterpolationPlayer(bool ReduceLight)
    {
     
        yield return new WaitForSeconds(Time.deltaTime * 0.1f);

        if (!ReduceLight)
        {

            if (GameController.Instance.m_PlayerComponents.m_PlayerController.cameraLight.range >= 8.0f)
                yield return 0;
            else
            {
                GameController.Instance.m_PlayerComponents.m_PlayerController.cameraLight.range += 0.2f;
                StartCoroutine(LightInterpolationPlayer(ReduceLight));
            }
        }
        else
        {
            if (GameController.Instance.m_PlayerComponents.m_PlayerController.cameraLight.range <= 0.0f)
                yield return 0;
            else
            {
                GameController.Instance.m_PlayerComponents.m_PlayerController.cameraLight.range -= 0.2f;
                StartCoroutine(LightInterpolationPlayer(ReduceLight));
            }
        }
    }
}
