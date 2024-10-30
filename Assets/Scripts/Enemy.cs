using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private Quaternion _rotation;

    private void Start()
    {
        gameObject.transform.rotation = _rotation;
    }

	private void Awake()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	private void Update()
    {
        Move();
    }

    public void ChangeRotation(Quaternion rotation)
    {
        _rotation = rotation;
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }
}
