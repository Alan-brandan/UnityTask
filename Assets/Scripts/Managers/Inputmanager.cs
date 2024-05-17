using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inputmanager : MonoBehaviour
{
    private bool assignAtStart = true;

    public float padX;
    public float padY;
    public float lStickX;
    public float lStickY;

    public KeyCode Interact;
    public KeyCode Run;

    public enum Mode {Keyboard,Joystick }
    public Mode InputMode = Mode.Joystick;

    public enum InputType { MouseKeyboard, Controller };
    public InputType InputState = InputType.MouseKeyboard;


    private static Inputmanager _instance;
    public static Inputmanager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        isControllerInput();
    }

    void Update()
    {

        switch (InputState)
        {
            case InputType.MouseKeyboard:
                if (isControllerInput() || assignAtStart)
                {
                    assignAtStart = false;
                    InputState = InputType.Controller;
                    InputMode=Mode.Joystick;
                    Debug.Log("<color=#11A011>< < Switched Input to Controller > ></color>");

                    Interact = KeyCode.Joystick1Button0;
                    Run = KeyCode.Joystick1Button1;

                }
                //keyboard axis

                if (Input.GetKey(KeyCode.LeftArrow)) { padX = -1; }
                else if (Input.GetKey(KeyCode.RightArrow)) { padX = 1; }
                else { padX = 0; }

                if (Input.GetKey(KeyCode.UpArrow)) { padY = 1; }
                else if (Input.GetKey(KeyCode.DownArrow)) { padY = -1; }
                else { padY = 0; }

                EventSystem.current.gameObject.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal";
                EventSystem.current.gameObject.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical";

                break;
            case InputType.Controller:
                if (isMouseKeyboard())
                {
                    InputState = InputType.MouseKeyboard;
                    InputMode = Mode.Keyboard;
                    Debug.Log("<color=#11A011>< < Switched Input to Keyboard > ></color>");

                    Interact = KeyCode.Return;
                    Run = KeyCode.LeftShift;

                }
                //joystick axis
                padX = Input.GetAxis("DPad X");
                padY = Input.GetAxis("DPad Y");

                lStickX = Input.GetAxis("LeftStickX");
                lStickY = Input.GetAxis("LeftStickY");

                EventSystem.current.gameObject.GetComponent<StandaloneInputModule>().horizontalAxis = "DPad X";
                EventSystem.current.gameObject.GetComponent<StandaloneInputModule>().verticalAxis = "DPad Y";


                break;
        }
    }

    public InputType GetInputType()
    {
        return InputState;
    }

    public bool UsingController()
    {
        return InputState == InputType.Controller;
    }

    private bool isMouseKeyboard()
    {
        return Input.anyKey && !isControllerInput() &&
           Input.GetAxis("HorizontalJoystick") == 0.0f &&
           Input.GetAxis("VerticalJoystick") == 0.0f &&
           Input.GetAxis("DPad Y") == 0.0f &&
           Input.GetAxis("DPad X") == 0.0f;
    }

    private bool isControllerInput()
    {
        return Input.GetKey(KeyCode.Joystick1Button0) ||
           Input.GetKey(KeyCode.Joystick1Button1) ||
           Input.GetKey(KeyCode.Joystick1Button2) ||
           Input.GetKey(KeyCode.Joystick1Button3) ||
           Input.GetKey(KeyCode.Joystick1Button4) ||
           Input.GetKey(KeyCode.Joystick1Button5) ||
           Input.GetKey(KeyCode.Joystick1Button6) ||
           Input.GetKey(KeyCode.Joystick1Button7) ||
           Input.GetKey(KeyCode.Joystick1Button8) ||
           Input.GetKey(KeyCode.Joystick1Button9) ||
           Input.GetKey(KeyCode.Joystick1Button10) ||
           Input.GetKey(KeyCode.Joystick1Button11) ||
           Input.GetKey(KeyCode.Joystick1Button12) ||
           Input.GetKey(KeyCode.Joystick1Button13) ||
           Input.GetKey(KeyCode.Joystick1Button14) ||
           Input.GetKey(KeyCode.Joystick1Button15) ||
           Input.GetKey(KeyCode.Joystick1Button16) ||
           Input.GetKey(KeyCode.Joystick1Button17) ||
           Input.GetKey(KeyCode.Joystick1Button18) ||
           Input.GetKey(KeyCode.Joystick1Button19) ||
           Input.GetAxis("HorizontalJoystick") != 0.0f ||
           Input.GetAxis("VerticalJoystick") != 0.0f ||
           Input.GetAxis("DPad Y") != 0.0f ||
           Input.GetAxis("DPad X") != 0.0f;
    }
}
