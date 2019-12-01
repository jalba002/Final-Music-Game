using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour, ITakeItems
{

    public void TakeItem()
    {
        GameController.Instance.m_PlayerComponents.m_PlayerController.m_GravityDirection *= -1;
        GameController.Instance.m_PlayerComponents.m_PlayerController.SetOnGround(false);
        GameController.Instance.m_HasGravity = true;
    }
}