using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 1f;

    private Quaternion _rotation;

    private void Start()
    {
        gameObject.transform.rotation = _rotation;
        
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    public void ChangeRotation(Quaternion rotation)
    {
        _rotation = rotation;
    }
}
