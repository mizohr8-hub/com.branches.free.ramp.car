//PlayerControl.cs handles user input

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RGSK
{
    public class PlayerControl : MonoBehaviour
    {
        public static PlayerControl instance;   //mjr



        public enum InputTypes { Desktop, Mobile, Automatic }
        public enum InputController { Keyboard, XboxController }
        public enum MobileSteerType { TiltToSteer, TouchSteer }

        public InputTypes inputType = InputTypes.Automatic;

        private Car_Controller car_controller;
        private Motorbike_Controller bike_controller;

        [Header("Desktop Controls")]
        public InputController inputController;
        public bool autoDetect = true;

        [Header("Mobile Controls")]
        public MobileSteerType mobileSteerType;

        [Header("Other")]
        public bool autoAcceleration;
        [HideInInspector]
        public PlayerCamera playerCam;

        void Awake()
        {
            instance = this;  //mjr

            if (GetComponent<Car_Controller>())
                car_controller = GetComponent<Car_Controller>();

            if (GetComponent<Motorbike_Controller>())
                bike_controller = GetComponent<Motorbike_Controller>();
        }

        void Start()
        {

            if (!InputManager.instance)
            {
                Debug.LogError("No Input Manager Found! Please Create an Input Manager by going to Window/RacingGameStarterKit/RaceComponents");
                enabled = false;
                return;
            }

            playerCam = GameObject.FindObjectOfType(typeof(PlayerCamera)) as PlayerCamera;

            autoAcceleration = (PlayerPrefs.GetString("AutoAcceleration") == "True");

            if (inputType == InputTypes.Mobile || inputType == InputTypes.Automatic)
            {
                if (MobileControlManager.instance)
                {
                    MobileControlManager.instance.UpdateControls(this);
                }
            }
        }

        void Update()
        {
            switch (inputType)
            {
                case InputTypes.Desktop:
                    DesktopControl();
                    break;

                case InputTypes.Mobile:
                    MobileControl();
                    break;

                case InputTypes.Automatic:
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
                    DesktopControl();
#else
					MobileControl();
#endif
                    break;
            }


            if (transform.rotation.y < 180 && transform.rotation.y > 0)
            {
                if (GeneralScript.instance)
                {
                    GeneralScript.instance.wrongWayImg.enabled = true;
                }
            }

            else
            {
                if (GeneralScript.instance)
                {
                    GeneralScript.instance.wrongWayImg.enabled = false;
                }
            }

        }

        void FixedUpdate()
        {

            if (!autoDetect) return;

            //Check for controller / keyboard presses to automatically set the input controller

            if (inputController != InputController.XboxController && DetectControllerInput())
            {
                inputController = InputController.XboxController;
                Debug.Log("Switched to Controller input");
            }

            if (inputController != InputController.Keyboard && !DetectControllerInput() && Input.anyKeyDown)
            {
                inputController = InputController.Keyboard;
                Debug.Log("Switched to Keyboard input");
            }
        }

        void DesktopControl()
        {

            switch (inputController)
            {
                case InputController.Keyboard:

                    //Get input values
                    float accel = (!autoAcceleration) ? Mathf.Clamp01(Input.GetAxis(InputManager.instance.keyboardInput.verticalAxis)) : 1.0f;
                    float brake = Mathf.Clamp01(-Input.GetAxis(InputManager.instance.keyboardInput.verticalAxis));
                    float handbrake = Mathf.Clamp01(Input.GetAxis(InputManager.instance.keyboardInput.handbrakeAxis));
                    float steer = Mathf.Clamp(Input.GetAxis(InputManager.instance.keyboardInput.horizontalAxis), -1, 1);
                    bool nitro = Input.GetKey(InputManager.instance.keyboardInput.nitroKey);

                    //Send inputs
                    SendInputs(accel, brake, steer, handbrake, nitro);


                    //Camera input
                    if (playerCam)
                    {
                        //Change Camera Views
                        if (Input.GetKeyDown(InputManager.instance.keyboardInput.switchCamera))
                        {
                            playerCam.SwitchCameras();
                        }

                        //Camera Look Left
                        playerCam.lookLeft = Input.GetKey(InputManager.instance.keyboardInput.cameraLookLEFT);


                        //Camera Look Right
                        //playerCam.lookRight = Input.GetKey(InputManager.instance.keyboardInput.cameraLookRIGHT);  //mjr


                        //Camera Look Back
                        playerCam.lookBack = playerCam.lookRight && playerCam.lookLeft || Input.GetKey(InputManager.instance.keyboardInput.cameraLookBACK) || playerCam.velocityDir.z <= -2.0f;

                        //Camera orbit values
                        playerCam.orbitX = Input.GetAxis(InputManager.instance.keyboardInput.orbitXaxis);
                        playerCam.orbitY = Input.GetAxis(InputManager.instance.keyboardInput.orbitYaxis);
                    }

                    //Pause
                    if (Input.GetKeyDown(InputManager.instance.keyboardInput.pauseKey))
                    {
                        if (RaceManager.instance)
                            RaceManager.instance.PauseRace();
                    }

                    //Respawn
                    if (Input.GetKeyDown(InputManager.instance.keyboardInput.respawnKey))
                    {
                        Respawn();
                    }

                    break;

                case InputController.XboxController:

                    //Get input values
                    float _accel = (!autoAcceleration) ? Mathf.Clamp01(Input.GetAxis(InputManager.instance.xboxControllerInput.verticalAxis)) : 1.0f;
                    float _brake = Mathf.Clamp01(Input.GetAxis(InputManager.instance.xboxControllerInput.negativeVerticalAxis));
                    float _steer = Mathf.Clamp(Input.GetAxis(InputManager.instance.xboxControllerInput.horizontalAxis), -1, 1);
                    float _handbrake = Input.GetButton(InputManager.instance.xboxControllerInput.handbrakeButton) ? 1 : 0;
                    bool _nitro = Input.GetButton(InputManager.instance.xboxControllerInput.nitroButton);

                    //Send inputs
                    SendInputs(_accel, _brake, _steer, _handbrake, _nitro);

                    
                    //Camera input
                    if (playerCam)
                    {
                        //Change Camera Views
                        if (Input.GetButtonDown(InputManager.instance.xboxControllerInput.switchCamera))
                        {
                            playerCam.SwitchCameras();
                        }

                        //Camera Look Left
                        playerCam.lookLeft = Input.GetButton(InputManager.instance.xboxControllerInput.cameraLookLEFT);



                        //Camera Look Right
                       // playerCam.lookRight = Input.GetButton(InputManager.instance.xboxControllerInput.cameraLookRIGHT);  //mjr


                        //Camera Look Back
                        playerCam.lookBack = playerCam.lookRight && playerCam.lookLeft ||  Input.GetButton(InputManager.instance.xboxControllerInput.cameraLookBACK) || playerCam.velocityDir.z <= -2.0f;

                        //Camera orbit values
                        playerCam.orbitX = Input.GetAxis(InputManager.instance.xboxControllerInput.orbitXaxis);
                        playerCam.orbitY = Input.GetAxis(InputManager.instance.xboxControllerInput.orbitYaxis);
                    }

                    //Pause
                    if (Input.GetButtonDown(InputManager.instance.xboxControllerInput.pauseButton))
                    {
                        if (RaceManager.instance)
                            RaceManager.instance.PauseRace();
                    }

                    //Respawn
                    if (Input.GetButton(InputManager.instance.xboxControllerInput.respawnButton))
                    {
                        Respawn();
                    }
                 
                    break;
            }
        }

        void MobileControl()
        {

            float steer = 0;

            if (mobileSteerType == MobileSteerType.TiltToSteer)
            {
                //steer according to the device tilt amount
                steer = Input.acceleration.x;
            }
            else
            {
                //steer with the on-screen ui buttons
                if (InputManager.instance.mobileInput.steerRight != null && InputManager.instance.mobileInput.steerLeft != null)
                {
                    steer = InputManager.instance.mobileInput.steerRight.inputValue + (-InputManager.instance.mobileInput.steerLeft.inputValue);
                }
            }

            //send inputs
            float _accel = (!autoAcceleration) ? InputManager.instance.mobileInput.accelerate.inputValue : 1.0f;
            float _brake = InputManager.instance.mobileInput.brake.inputValue;
            float _handbrake = (InputManager.instance.mobileInput.handBrake) ? InputManager.instance.mobileInput.handBrake.inputValue : 0;
            bool _nitro = (InputManager.instance.mobileInput.nitro) ? InputManager.instance.mobileInput.nitro.buttonPressed : false;

            SendInputs(_accel, _brake, steer, _handbrake, _nitro);
        }

        void SendInputs(float accel, float brake, float steer, float handbrake, bool nitro)
        {
            if (car_controller)
            {
                car_controller.motorInput = (brake <= 0) ? accel : 0;
                car_controller.brakeInput = brake;
                car_controller.steerInput = steer;
                car_controller.handbrakeInput = handbrake;
                car_controller.usingNitro = nitro;
            }

            if (bike_controller)
            {
                bike_controller.motorInput = (brake <= 0) ? accel : 0;
                bike_controller.brakeInput = brake;
                bike_controller.steerInput = steer;
                bike_controller.usingNitro = nitro;
            }
        }

        public void Respawn()
        {
            if (RaceManager.instance)
                RaceManager.instance.RespawnRacer(transform, GetComponent<Statistics>().lastPassedNode, 0.0f); //3.0 


        }
         public void RespawnNew()
        {
            if (RaceManager.instance)
            {
                //transform.rotation = Quaternion.LookRotation(transform.forward);
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                transform.position = transform.GetComponent<BehaviourSetting>().currentCheckPoint.transform.position + new Vector3(0, 2, 0);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                GetComponent<RCC_CarControllerV3>().direction = 1;

            }
        }

        void OnDisable()
        {
            //Reset player camera values
            if (!playerCam) return;

            playerCam.lookBack = false;
            playerCam.lookLeft = false;
            playerCam.lookRight = false;  //mjr
            playerCam.lookFront = false;  //mjr
        }

        private bool DetectControllerInput()
        {
            // See if the player presses a controller button / joystick

            if (Input.GetKey(KeyCode.Joystick1Button0) ||
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
               Input.GetKey(KeyCode.Joystick1Button19))
            {
                return true;
            }

            if (Input.GetAxis("LeftAnalogHorizontal") != 0.0f ||
               Input.GetAxis("LeftAnalogVertical") != 0.0f ||
               Input.GetAxis("Triggers") != 0.0f ||
               Input.GetAxis("RightAnalogHorizontal") != 0.0f ||
               Input.GetAxis("RightAnalogVertical") != 0.0f)
            {
                return true;
            }
            return false;
        }
    }
}
