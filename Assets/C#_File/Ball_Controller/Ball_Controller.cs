using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball_Controller : MonoBehaviour
{
    private Rigidbody RB;
    [SerializeField] private float Jump = 0;
    [SerializeField] private float playerSpeed = 2.0f;
    private Transform camTranform;
    private float _time;
    private bool Is_Landed;
    [SerializeField]private GameObject Camera_Main;
    private Collider other_object;
    private PlayerInput playerInput;
    private InputAction moveAction;

    private void OnTriggerStay(Collider other)
    {
        Is_Landed = true;
        other_object = other;
        //print(Name_Trigger);
    }

    private void OnTriggerExit(Collider other)
    {
        Is_Landed = false;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        _time = 0;
        playerInput = GetComponent<PlayerInput>();
        camTranform = Camera.main.transform;
        RB = gameObject.GetComponent<Rigidbody>();
        moveAction = playerInput.actions["Move"];
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x * playerSpeed, 0, input.y * playerSpeed);
        move = move.x * camTranform.right.normalized + move.z * camTranform.forward.normalized;
        move.y = 0f;

        // Jump
        if (Input.GetButtonDown("Jump") && (Is_Landed == true && _time < 0.15f))
        {
            print("UPPPPP");
            RB.AddForce(0, this.transform.position.y * Jump, 0, ForceMode.Impulse);
            _time += Time.deltaTime;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            move.y = 0;
            _time = 0;
        }
        if (move != Vector3.zero)
        {
            RB.AddForce(move);
        }

        
    }
}