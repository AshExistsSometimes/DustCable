using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private void Start()
    {
        // Initialisation
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (InputHandler.GetKey(InputHandler.CrouchKey))
        {
            characterController.height = 0.5f;
        }
        else
        {
            characterController.height = 2f;
        }
    }
}
