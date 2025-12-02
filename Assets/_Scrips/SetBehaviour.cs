using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBehaviour : MonoBehaviour
{
    public int value;
    private void Awake()
    {
        RCC_SceneManager.SetBehavior(value);
    }
}
