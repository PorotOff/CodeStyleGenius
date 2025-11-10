using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action Destroyed;

    public void TakeDamage()
    {
        Destroyed?.Invoke();
        Destroy(gameObject);
    }
}