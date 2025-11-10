using System;
using UnityEngine;

public class TargetCollisionChecker : MonoBehaviour
{
    public event Action<Target> Collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Target target))
            Collided?.Invoke(target);
    }
}