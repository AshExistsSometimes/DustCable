using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /////////////////////////////////////////////

    // Public Fields
    [Header("Movement")]
    public float MaxSprintSpeed = 10f;
    [Range(0, 1)]
    public float SprintSpeedAccelRate = 0.5f;
    [Space]
    public float JumpForce = 5f;

    [Header("Camera")]
    public float Sensitivity = 1f;

    [Header("Gravity")]
    public float GravityForce = 1.5f;

    // Private Fields
    private Rigidbody _rb;
    private CapsuleCollider _capCollider;

    private Transform _camTransform;
    private float _camXRot;

    private bool _grounded;

    private float _standardGravity;

    /////////////////////////////////////////////


    private void Start()
    {
        // Initialisation
        _rb = GetComponent<Rigidbody>();
        _capCollider = GetComponent<CapsuleCollider>();
        _camTransform = Camera.main.transform;
        _standardGravity = GravityForce;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        GroundCheck();

        Crouching();

        Jumping();

        Movement();

        CameraRotation();

        if (!_grounded)
        {
            _rb.AddForce(-transform.up * GravityForce);
        }
    }  

    #region PLAYER MOVEMENT

    #region MOVEMENT
    private void Movement()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 velocitynoy = _rb.velocity;
        velocitynoy.y = 0;

        dir = transform.forward * dir.z + transform.right * dir.x;

        _rb.AddForce(((dir.normalized * MaxSprintSpeed) - velocitynoy) * SprintSpeedAccelRate);
    }
    #endregion

    #region CROUCHING
    private void Crouching()
    {
        if (InputHandler.GetKey(InputHandler.CrouchKey))
        {
            _capCollider.height = 0.5f;// Crouched Height
        }
        else
        {
            _capCollider.height = 2f;// Standard Height
        }
    }
    #endregion

    #region JUMPING
    private void Jumping()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }
    }
    #endregion

    #region CAMERA MOVEMENT
    private void CameraRotation()
    {
        // Up/Down Look
        Vector2 lookDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        _camXRot -= lookDir.y * Sensitivity;
        _camXRot = Mathf.Clamp(_camXRot, -80, 80);
        _camTransform.localRotation = Quaternion.Euler(_camXRot, 0, 0);

        // Left/Right Look
        transform.Rotate(0, lookDir.x * Sensitivity, 0);
    }
    #endregion
    #endregion

    #region GROUND CHECK
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, (_capCollider.height / 2 + 0.1f) ) )
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
    }
    #endregion
}
