using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TargetCollisionChecker))]
public class Bullet : MonoBehaviour
{
    private Quaternion _rotation;
    private float _impulseForce;
    private float _destroyTimeSeconds;

    private Rigidbody _rigidbody;
    private TargetCollisionChecker _targetCollisionChecker;

    public event Action<Bullet> Destroyed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetCollisionChecker = GetComponent<TargetCollisionChecker>();
    }

    public void Initialize(Quaternion rotation, float impulseForce, float destroyTimeSeconds)
    {
        _rotation = rotation;
        _impulseForce = impulseForce;
        _destroyTimeSeconds = destroyTimeSeconds;

        _rigidbody.velocity = Vector3.zero;
        transform.rotation = _rotation;
        _rigidbody.AddForce(transform.forward * _impulseForce, ForceMode.Impulse);
        StartCoroutine(DestroyDelayed());
    }

    private void OnEnable()
        => _targetCollisionChecker.Collided += OnTargetCollided;

    private void OnDisable()
        => _targetCollisionChecker.Collided -= OnTargetCollided;

    private void OnTargetCollided(Target target)
    {
        target.TakeDamage();
        Destroyed?.Invoke(this);
    }

    private IEnumerator DestroyDelayed()
    {
        yield return new WaitForSecondsRealtime(_destroyTimeSeconds);
        Destroyed?.Invoke(this);
    }
}