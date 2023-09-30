using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;

public class InputManager : MonoBehaviour
{   
    public static InputManager instance { get; private set; }

    public GameObject Player;
    private PlayerControls playerInput;

    public Vector2 mousePosition;
    private Vector3 intersectionPoint;
    
    //public Camera activeCamera;

    

    private void Awake()
    {
        playerInput = new PlayerControls();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Main.MousePosition.performed += MousePosition;
        playerInput.Main.MouseLeftButton.performed += LeftButton;
        playerInput.Main.MouseRightButton.performed += RightButton;
        
    }

    private void LeftButton(InputAction.CallbackContext context)
    {
        Player.GetComponent<PsiBlastLogic>().CreatePsiWave();
        Debug.Log("AAAAAAA");
    }

    private void RightButton(InputAction.CallbackContext context)
    {
        Player.GetComponent<PsiLocatorLogic>().PsiLocate();
        Debug.Log("BBBBBBB");
    }

    private void MousePosition(InputAction.CallbackContext context)
    {
        Vector2 normalPosition = Camera.main.ScreenToViewportPoint(context.ReadValue<Vector2>());
        mousePosition = new Vector2(normalPosition.x * Screen.width, normalPosition.y * Screen.height);
        //Debug.Log(mousePosition);
    }

    private void OnDisable()
    {   
        playerInput.Disable();
        playerInput.Main.MousePosition.performed -= MousePosition;
        playerInput.Main.MouseLeftButton.performed -= LeftButton;
        playerInput.Main.MouseRightButton.performed -= RightButton;

    }
}
