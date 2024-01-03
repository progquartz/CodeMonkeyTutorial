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

        // �Լ� ��ü�� ������ �ѱ�� ����. (interact_performed)�� ��ȣ�� ���� ����. 
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

        // �� �κа� �Ʒ� �κ��� �Ȱ��� �ڵ带 �ǹ���. �����ؼ� ���� ã�ƺ� ��.
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;


        return inputVector;
    }
}
