using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private List<Transform> _targets = new List<Transform>();

    private int _currentTargetIndex;

    private void Update()
    {
        Transform currentTarget = _targets[_currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, _speed * Time.deltaTime);

        if (transform.position == currentTarget.position)
            transform.forward = GetNextTarget() - transform.position;
    }

    private Vector3 GetNextTarget()
    {
        _currentTargetIndex = ++_currentTargetIndex % _targets.Count;

        Vector3 currentTargetPosition = _targets[_currentTargetIndex].position;
        return currentTargetPosition;
    }
}