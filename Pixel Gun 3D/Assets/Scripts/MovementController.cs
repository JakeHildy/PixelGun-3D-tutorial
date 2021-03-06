﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float lookSensitivity = 3.0f;

    [SerializeField] GameObject fpsCamera;
        
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraUpAndDownRotation = 0f;
    private float currentCameraUpAndDownRotation = 0f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate movement velocity as a 3d vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;

        // Final movement velocity vector
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;

        // Apply movement
        Move(_movementVelocity);


        // calculate rotation as a 3DD vector for turning around.
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0, _yRotation) * lookSensitivity;

        // Apply rotation
        Rotate(_rotationVector);

        // Calculate look up and down camera rotation
        float _cameraUpDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Apply rotation
        RotateCamera(_cameraUpDownRotation);

    }



    private void FixedUpdate() 
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity*Time.fixedDeltaTime);
        }

        rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation));

        if (fpsCamera != null)
        {
            currentCameraUpAndDownRotation -= cameraUpAndDownRotation;
            currentCameraUpAndDownRotation = Mathf.Clamp(currentCameraUpAndDownRotation, -85, 85);

            fpsCamera.transform.localEulerAngles = new Vector3(currentCameraUpAndDownRotation, 0, 0);
        }
    }


    private void Move(Vector3 movementVelocity)
    {
        velocity = movementVelocity;
    }

    private void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    private void RotateCamera(float cameraUpAndDownRot)
    {
        cameraUpAndDownRotation = cameraUpAndDownRot;
    }


}
