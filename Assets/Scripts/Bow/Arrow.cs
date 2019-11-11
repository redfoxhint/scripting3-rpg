﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Inspector Fields


    // Private Variables
    private Rigidbody2D rBody;
    private bool doMovement = false;
    private Vector2 direction;
    private float speed;
    private Collider2D col;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if(doMovement)
        {
            rBody.velocity = direction * speed;
        }
    }

    public void LaunchProjectile(Vector2 _direction, float _speed)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3)mousePos - transform.position);

        direction = _direction;
        speed = _speed;
        doMovement = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            doMovement = false;
            col.enabled = false;

            IDamageable damageable = other.GetComponent<IDamageable>();

            if(damageable != null)
            {
                Vector2 direction = other.transform.position - transform.position;
                damageable.DoKnockback(direction, 15f);
            }
            
            rBody.isKinematic = true;
            rBody.velocity = Vector3.zero;

            transform.parent = other.transform;
        }
    }
}
