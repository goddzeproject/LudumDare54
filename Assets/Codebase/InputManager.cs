using Cinemachine;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;

public class InputManager : MonoBehaviour
{   
    public static InputManager instance { get; private set; }

    private GameObject Player;
    private PlayerControls playerInput;
    private GameManager gameManager;
    [HideInInspector]
    public Vector2 mousePosition;

    

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

    private void Start()
    {
        gameManager = GameManager.instance;
        Player = GameObject.FindWithTag("Player");

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
        gameManager.ShowCDBlast();
        GetReference();

        Player.GetComponent<PsiBlastLogic>().CreatePsiBlast();
        gameManager.PlayVFX();

        Debug.Log("LeftButtonMouse");
    }


    private void RightButton(InputAction.CallbackContext context)
    {   
        gameManager.ShowCDLocator();
        GetReference();
        Player.GetComponent<PsiLocatorLogic>().PsiLocate();

        Debug.Log("RightButtonMouse");
    }

    private void MousePosition(InputAction.CallbackContext context)
    {
        var mouseNormalPosition = Camera.main.ScreenToViewportPoint(context.ReadValue<Vector2>());
        mousePosition = new Vector2(mouseNormalPosition.x * Screen.width, mouseNormalPosition.y * Screen.height);

    }

    private void OnDisable()
    {   
        playerInput.Disable();
        playerInput.Main.MousePosition.performed -= MousePosition;
        playerInput.Main.MouseLeftButton.performed -= LeftButton;
        playerInput.Main.MouseRightButton.performed -= RightButton;
    }

    private void GetReference()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }

    }
}
