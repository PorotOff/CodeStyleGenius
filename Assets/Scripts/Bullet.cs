using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
        => _rigidbody = GetComponent<Rigidbody>();

    public void Initialize(Vector3 target, Vector3 impulseForce)
    {
        transform.LookAt(target);
        _rigidbody.velocity = impulseForce;
    }
}