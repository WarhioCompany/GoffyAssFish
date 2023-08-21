using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInputScript : MonoBehaviour
{
    // recognize: E (Interact), Q (Upgrade Campfire), ESC (close Menus, open ESC Menu), 1 (place Fire), 2 (place Barricade)
    public PlayerInputActions playerInput;

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }
    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }


    private void Start()
    {
        //playerInput.Player.interact.performed += Interact_performed;
        playerInput.Player.Shoot.started += Shoot;
        playerInput.Player.Shoot.canceled += Shoot;
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            // prepare launching
            Debug.Log("Prepare...");
        }
        else if (ctx.canceled)
        {
            // shoot tentacle
            Debug.Log("Launch...");
            GetComponent<PlayerMovement>().Shoot();
        }
    }
}
