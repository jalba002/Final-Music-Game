using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLight : MonoBehaviour, ITakeItems
{

    public void TakeItem()
    {
        GameController.Instance.m_ChangeLight = !GameController.Instance.m_ChangeLight;
        GameController.Instance.m_Camera.LightInterepolation(GameController.Instance.m_ChangeLight);
        GameController.Instance.m_HasDLight = true;
    }
}
