using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    [SerializeField] private Transform direction;

    private Vector2 _input;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _input = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
    }

    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// AddForceでの移動
    /// </summary>
    private void Move()
    {
        if (_rb.velocity.magnitude <= maxSpeed)
        {
            Vector3 move = _input.y * direction.transform.forward + _input.x * direction.transform.right; 
            _rb.AddForce(move * acceleration * Time.deltaTime);
        }
    }
}
