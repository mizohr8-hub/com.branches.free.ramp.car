using UnityEngine;
using System.Collections;

namespace RGSK
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;
        
        [System.Serializable]
        public class KeyboardInput
        {
            public string verticalAxis = "Vertical";
            public string horizontalAxis = "Horizontal";
            public string handbrakeAxis = "Jump";
            public KeyCode nitroKey = KeyCode.LeftShift;           
            public KeyCode respawnKey = KeyCode.Return;
            public KeyCode pauseKey = KeyCode.Escape;

            [Header("Camera Control")]
            public KeyCode switchCamera = KeyCode.V;
            public KeyCode cameraLookLEFT = KeyCode.Q;
            public KeyCode cameraLookRIGHT = KeyCode.E;
            public KeyCode cameraLookBACK = KeyCode.C;
            public string orbitYaxis = "Mouse Y";
            public string orbitXaxis = "Mouse X";
        }

        [System.Serializable]
        public class XboxControllerInput
        {
            public string verticalAxis = "RT";
            public string negativeVerticalAxis = "LT";
            public string horizontalAxis = "LeftAnalogHorizontal";
            public string handbrakeButton = "B";
            public string nitroButton = "A";            
            public string respawnButton = "Y";
            public string pauseButton = "Start";

            [Header("Camera Control")]
            public string switchCamera = "Back";
            public string cameraLookLEFT = "LB";
            public string cameraLookRIGHT = "RB";
            public string cameraLookBACK = "RightAnalogueClick";
            public string orbitYaxis = "RightAnalogVertical";
            public string orbitXaxis = "RightAnalogHorizontal";
        }

        [System.Serializable]
        public class MobileInput
        {
            public UIButton accelerate;
            public UIButton brake;
            public UIButton handBrake;
            public UIButton nitro;
            public UIButton steerRight;
            public UIButton steerLeft;
        }

        [Header("Keyboard Input")]
        public KeyboardInput keyboardInput;

        [Header("Xbox Controller Input")]
        public XboxControllerInput xboxControllerInput;

        [Header("Mobile Input")]
        [Tooltip("These are automatically assigned by the MobileControlManager")]
        public MobileInput mobileInput;

        [Space(10)]
        public bool dontDestroyOnLoad;

        void Awake()
        {
            //Create an instance
            instance = this;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}
