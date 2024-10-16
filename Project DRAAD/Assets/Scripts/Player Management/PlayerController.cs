﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float terminalVelocity = 30f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 5f; //jump higher at higher speed
    [SerializeField] private float coyoteTimeCurrent;
    [SerializeField] private float coyoteTimeMax = .5f;
    [SerializeField] private float jumpTimerCurrent;
    [SerializeField] private float jumpTimerMax = .5f;

    [Header("Drag")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f; //dictate how dampened your up/down movements are

    //see gravity as a parabella width

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    private float playerHeight = 2f;

    private float horizontalMovement;
    private float verticalMovement;

    public bool isGrounded;

    private bool canMove;

    private Vector3 moveDir;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            return;
        } else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
            

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);

        ControlDrag();

        PlayerInput();

        if (jumpTimerCurrent > 0)
            jumpTimerCurrent -= Time.deltaTime;

        if (!isGrounded)
            coyoteTimeCurrent -= Time.deltaTime;
        else if (isGrounded)
            coyoteTimeCurrent = coyoteTimeMax;

        if (Input.GetKeyDown(jumpKey) && coyoteTimeCurrent > 0f && jumpTimerCurrent <= 0f)
            Jump();

        if (Input.GetKeyDown(crouchKey))
            playerHeight = 1f;
        else
            playerHeight = 2f;

        //print(isGrounded);
        //print(rb.velocity.magnitude);
    }

    void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDir = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void Jump()
    {
        jumpTimerCurrent = jumpTimerMax;
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ControlDrag()
    {
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = airDrag;
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 flatVector = new Vector2(rb.velocity.x, rb.velocity.z);

        if (isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        } else if (!isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }

        if (flatVector.magnitude >= terminalVelocity)
        {
            flatVector = flatVector.normalized * terminalVelocity;
            rb.velocity = new Vector3(flatVector.x, rb.velocity.y, flatVector.y);
        }
    }

    public void TogglePlayerMovement()
    {
        canMove = !canMove;
        
    }
}