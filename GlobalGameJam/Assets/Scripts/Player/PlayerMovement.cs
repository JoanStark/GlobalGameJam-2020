﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputMaster controls;

    public int PlayerNumber = 1;
    public float playerSpeed = 4f;
    public float dashSpeed = 15;

    private Vector2 movement;

    private Rigidbody2D rigidbody2d;

    private bool sprinting = false;

    private float dashTime = 0;
    public float configDashTime = 0.15f;
    private float sprintCooldown = 0.5f;
    public float configSprintCooldown = 0.5f;

    private float x;


    private void Awake()
    {
        controls = new InputMaster();
        switch (PlayerNumber)
        {
            case 1:
                controls.Player1.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
                controls.Player1.Move.canceled += ctx => movement = Vector2.zero;
                controls.Player1.Sprint.started += ctx => Sprint();
                break;
            case 2:
                controls.Player2.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
                controls.Player2.Move.canceled += ctx => movement = Vector2.zero;
                controls.Player2.Sprint.started += ctx => Sprint();

                break;
            case 3:
                controls.Player3.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
                controls.Player3.Move.canceled += ctx => movement = Vector2.zero;
                controls.Player3.Sprint.started += ctx => Sprint();

                break;
            case 4:
                controls.Player4.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
                controls.Player4.Move.canceled += ctx => movement = Vector2.zero;
                controls.Player4.Sprint.started += ctx => Sprint();

                break;
        } 
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 moveDirection = rigidbody2d.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (!sprinting)
        {
            rigidbody2d.velocity = movement * playerSpeed;
        }
        else
        {
            dashTime -= Time.deltaTime;

            if(dashTime <= 0)
            {
                sprinting = false;
            }
        }

        sprintCooldown -= Time.deltaTime;
    }

    private void Sprint()
    {
        if (sprintCooldown < 0) 
        {
            sprinting = true;
            rigidbody2d.velocity = movement * dashSpeed;
            dashTime = configDashTime;
            sprintCooldown = configSprintCooldown;
        }
        
    }
}
