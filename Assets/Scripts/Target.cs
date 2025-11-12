using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _reachingTargetRange = 0.1f;

    private int _currentWaypointIndex = 0;
    private Transform _currentWaypoint;

    private void Awake()
        => _currentWaypoint = _waypoints[_currentWaypointIndex];

    private void Start()
        => LookAtWaypoint();

    private void Update()
        => Move();

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _speed * Time.deltaTime);

        if (transform.position.IsEnoughClose(_currentWaypoint.position, _reachingTargetRange))
        {
            SetNextWaypoint();
            LookAtWaypoint();
        }
    }

    private void SetNextWaypoint()
    {
        _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Count;
        _currentWaypoint = _waypoints[_currentWaypointIndex];
    }

    private void LookAtWaypoint()
        => transform.LookAt(_currentWaypoint);
}