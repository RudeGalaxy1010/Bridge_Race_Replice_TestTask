using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollower : MonoBehaviour
{
    [SerializeField] private float _smoothDelta = 0.05f;
    [SerializeField] private Transform _target;

    private Vector3 _offset;
    private Vector3 _targetPosition;

    private void Start()
    {
        _offset = transform.position - _target.position;
    }

    private void Update()
    {
        if (transform.position != _target.position)
        {
            _targetPosition = new Vector3(_target.position.x, _target.position.y, _target.position.z) + _offset;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _smoothDelta);
        }
    }
}
