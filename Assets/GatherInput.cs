using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput1 : MonoBehaviour
{
    private Controls myControl;
    public float valueX;
    public bool jumpInput;
    public bool dashInput;
    public bool tryAttack;

    public void Awake()
    {
        myControl = new Controls();
    }
    private void OnEnable()
    {
        myControl.Platyer.Move.performed += StartMove;
        myControl.Platyer.Move.canceled += StopMove;
        myControl.Platyer.Enable();

        //เอาไวกระโดด
        myControl.Platyer.Jump.performed += JumpStart;
        myControl.Platyer.Jump.canceled += JumpStop;

        myControl.Platyer.Dash.performed += DashStart;
        myControl.Platyer.Dash.canceled += DashStop;

        myControl.Platyer.Attack.performed += TryToAttach;
        myControl.Platyer.Attack.canceled += StopTryToAttack;


        myControl.Platyer.Enable();
    }
    private void OnDisable()
    {
        myControl.Platyer.Move.performed -= StartMove;
        myControl.Platyer.Move.canceled -= StopMove;

        //เอาไวกระโดด
        myControl.Platyer.Jump.performed -= JumpStart;
        myControl.Platyer.Jump.canceled -= JumpStop;

        myControl.Platyer.Dash.performed -= DashStart;
        myControl.Platyer.Dash.canceled -= DashStop;

        myControl.Platyer.Attack.performed -= TryToAttach;
        myControl.Platyer.Attack.canceled -= StopTryToAttack;
        

        myControl.Platyer.Disable();
       
        
    }
    
    public void DisableControls() 
    {
        myControl.Platyer.Move.performed -= StartMove;
        myControl.Platyer.Move.canceled -= StopMove;

        myControl.Platyer.Jump.performed -= JumpStart;
        myControl.Platyer.Jump.canceled -= JumpStop;

        myControl.Platyer.Dash.performed -= DashStart;
        myControl.Platyer.Dash.canceled -= DashStop;

        myControl.Platyer.Attack.performed -= TryToAttach;
        myControl.Platyer.Attack.canceled -= StopTryToAttack;


        myControl.Platyer.Disable();
        valueX = 0;

        
    }
    private void StartMove(InputAction.CallbackContext ctx) {
        valueX = ctx.ReadValue<float>();
    }

    private void StopMove(InputAction.CallbackContext ctx) {
        valueX = 0;
    }

    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }

    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }

    private void DashStart(InputAction.CallbackContext ctx)
    {
        dashInput = true;
    }

    private void DashStop(InputAction.CallbackContext ctx)
    {
        dashInput = false;
    }

    private void TryToAttach(InputAction.CallbackContext ctx)
    {
       tryAttack = true;
    }
    private void StopTryToAttack(InputAction.CallbackContext ctx)
    {
       tryAttack = false;
    }

}
