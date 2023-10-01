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

    public GameObject Player;
    private PlayerControls playerInput;
    private GameManager gameManager;

    public Vector2 mousePosition;
    public Vector2 realesePosition;

    

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
        Player.GetComponent<PsiBlastLogic>().CreatePsiBlast();
        gameManager.PlayVFX(0);
        Debug.Log("LeftButtonMouse");
    }


    private void RightButton(InputAction.CallbackContext context)
    {
        Player.GetComponent<PsiLocatorLogic>().PsiLocate();

        Debug.Log("RightButtonMouse");
    }

    private void MousePosition(InputAction.CallbackContext context)
    {
        Vector2 normalPosition = Camera.main.ScreenToViewportPoint(context.ReadValue<Vector2>());
        mousePosition = new Vector2(normalPosition.x * Screen.width, normalPosition.y * Screen.height);

    }

    private void OnDisable()
    {   
        playerInput.Disable();
        playerInput.Main.MousePosition.performed -= MousePosition;
        playerInput.Main.MouseLeftButton.performed -= LeftButton;
        playerInput.Main.MouseRightButton.performed -= RightButton;

    }
}
