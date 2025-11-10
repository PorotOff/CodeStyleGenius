using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [Header("Pool settings")]
    [SerializeField] private Bullet _prefab;
    [Header("Bullet settings")]
    [SerializeField] private float _impulseForce;
    [SerializeField] private float _destroyTimeSeconds;
    [Header("Spawner settings")]
    [SerializeField] private Transform _container;
    [SerializeField] private float _shootingPauseSeconds;
    [SerializeField] private List<Target> _targets;
    private Target _currentTarget;

    private IObjectPool<Bullet> _bulletsPool;

    private void Awake()
        => _bulletsPool = new ObjectPool<Bullet>(OnPoolCreate, OnPoolGet, OnPoolRelease, OnPoolDestroy);

    private void Start()
        => StartCoroutine(Spawn());

    private Bullet OnPoolCreate()
        => Instantiate(_prefab, _container);

    private void OnPoolGet(Bullet bullet)
        => bullet.gameObject.SetActive(true);

    private void OnPoolRelease(Bullet bullet)
        => bullet.gameObject.SetActive(false);

    private void OnPoolDestroy(Bullet bullet)
        => Destroy(bullet.gameObject);

    private IEnumerator Spawn()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_shootingPauseSeconds);
        SetNewTarget();

        while (_currentTarget != null)
        {
            Debug.Log($"Current target: {_currentTarget.name}");

            Vector3 direction = (_currentTarget.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            Bullet bullet = _bulletsPool.Get();
            bullet.Initialize(rotation, _impulseForce, _destroyTimeSeconds);
            bullet.transform.position = transform.position;

            bullet.Destroyed += OnBulletDestroyed;

            yield return wait;
        }
    }

    private void SetNewTarget()
    {
        if (_currentTarget != null)
            _currentTarget.Destroyed -= SetNewTarget;

        if (_targets.Count != 0)
        {
            _currentTarget = _targets[0];
            _targets.Remove(_currentTarget);
        }

        _currentTarget.Destroyed += SetNewTarget;
    }

    private void OnBulletDestroyed(Bullet bullet)
    {
        bullet.Destroyed -= OnBulletDestroyed;
        _bulletsPool.Release(bullet);
    }
}