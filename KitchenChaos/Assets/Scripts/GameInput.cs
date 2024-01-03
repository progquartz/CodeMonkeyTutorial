using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // 함수 자체를 변수로 넘기는 것임. (interact_performed)가 괄호가 없는 이유. 
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        /* 
        if(OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        }
        */

        // 위 부분과 아래 부분이 똑같은 코드를 의미함. 관련해서 내용 찾아볼 것.
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;


        return inputVector;
    }
}
