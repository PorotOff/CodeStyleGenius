using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private float _impulseForce;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private Transform _target;

    private void Start()
        => StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_spawnInterval);

        while (enabled)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Vector3 impulseForce = direction * _impulseForce;

            Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.Initialize(_target.position, impulseForce);

            yield return wait;
        }
    }
}