using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Duration : MonoBehaviour
{
    public float LifeTime;

    public void OnEnable()
    {
        Invoke(nameof(TurnOff), LifeTime);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }
}
