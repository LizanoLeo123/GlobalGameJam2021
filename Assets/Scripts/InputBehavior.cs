using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehavior : MonoBehaviour
{

    PlayerMovement pMove;

    UI_Manager _uiManager;

    private bool pKeyboard; 
    private bool pController;
    private bool pMouse;

    private void Start()
    {
        pKeyboard = true;
        pController = false;
        pMouse = false;
        pMove = GameObject.Find("Player").GetComponent<PlayerMovement>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            pMove.playingWithMouse = false;

        if((Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow)) && !pKeyboard)
        {
            pKeyboard = true;
            pController = false;
            pMouse = false;
            pMove.playingWithMouse = false;
            //Update UI with Keyboard icons
            _uiManager.ShowKeyboardUI();
            //Debug.Log("Playing with keyboard");
        }
        else if((Input.GetKeyDown(KeyCode.Joystick1Button0) ||
            Input.GetKeyDown(KeyCode.Joystick1Button1) ||
            Input.GetKeyDown(KeyCode.Joystick1Button2) ||
            Input.GetKeyDown(KeyCode.Joystick1Button3)) && !pController)
        {
            pKeyboard = false;
            pController = true;
            pMouse = false;
            //UPdate UI with Controller icons
            _uiManager.ShowControllerUI();
            //Debug.Log("Playing with controller");
        }
        else if((Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(1)) && !pMouse)
        {
            pKeyboard = false;
            pController = false;
            pMouse = true;
            //Update UI with Mouse icons
            _uiManager.ShowMouseUI();
            //Debug.Log("Playing with mouse");
            pMove.playingWithMouse = true;
        }
    }
}
