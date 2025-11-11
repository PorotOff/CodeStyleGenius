using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private float _impulseForce;
    [SerializeField] private GameObject _bulletPrefab;
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
            Vector3 velocity = direction * _impulseForce;
            GameObject bullet = Instantiate(_bulletPrefab, transform.position + direction, Quaternion.identity);

            bullet.transform.up = direction;
            bullet.GetComponent<Rigidbody>().velocity = velocity;

            yield return wait;
        }
    }
}