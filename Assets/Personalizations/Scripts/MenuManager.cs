/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//The MenuManager handles all menu activity. Feel free to extend it, learn from it or even use it in your own menu.
//Please note that the menu manager was intended for demo purposes.


    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;
        public GameObject _success, _failed;

        [System.Serializable]
        public class MenuVehicle
        {
            [Header("Details")]
            public string name;
            public string resourceName;
            // Make sure this string matches the coresponding vehicle name in a Reources/PlayerVehicles folder!
            public Transform vehicle;
            public int price;
            public bool unlocked;
            public Material vehicleBody;
            public Material VehicleRims;

            [Header("Specs")]
            [Range(0, 1)]
            public float speed;
            [Range(0, 1)]
            public float acceleration;
            [Range(0, 1)]
            public float handling;
            [Range(0, 1)]
            public float braking;
        }

        [System.Serializable]
        public class MenuTrack
        {
            public string name;
            public string trackLength;
            public string sceneName;
            public Sprite image;
            public RaceManager.RaceType raceType = RaceManager.RaceType.Circuit;
            public OpponentControl.AiDifficulty aiDifficulty = OpponentControl.AiDifficulty.Meduim;
            public int laps = 3;
            public int aiCount = 4;
            public int price;
            public bool unlocked;
            public int _mapnumber;
        }

        #region Customization Classes

        [System.Serializable]
        public class CustomizeItem
        {
            public string name;
            public int ID;
            public int price;
            public Text priceText;
            [HideInInspector] public bool unlocked;
        }

        [System.Serializable]
        public class VisualUpgrade : CustomizeItem
        {
            public BodyColorAndRims[] visualUpgrade;
        }

        [System.Serializable]
        public class BodyColorAndRims
        {
            public string vehicle_name;
            public Texture texture;
        }


        [System.Serializable]
        public class VehicleUpgrade
        {
            public string vehicle_name;
            [Space(10)]
            [Range(0, 1)]
            public float speed;
            [Range(0, 1)]
            public float acceleration;
            [Range(0, 1)]
            public float handling;
            [Range(0, 1)]
            public float braking;
        }

        #endregion

        public enum State
        {
            Main,
            VehicleSelect,
            TrackSelect,
            Customize,
            Settings,
            Loading,
            custom

        }

        public State state;

        [Header("Vehicle settings")]
        public MenuVehicle[] menuVehicles;

        [Header("Track Settings")]
        public MenuTrack[] menuTracks;

        [Header("Customize Settings")]
        public VisualUpgrade[] bodyColors;
        public VisualUpgrade[] rims;

        [Header("Panels")]
        public GameObject mainPanel;
        public GameObject vehicleSelectPanel;
        public GameObject trackSelectPanel;
        public GameObject customizePanel, vehicleStats;
        public GameObject settingsPanel;
        public GameObject promptPanel;
        public GameObject loadingPanel;

        [Header("Top Panel UI")]
        public Text playerCurrency;
        public Text menuState;

        [Header("Vehicle Select UI")]
        public Text vehicleName;
        public Button selectVehicleButton, buyVehicleButton, customizeButton;
        public Image speed, accel, handling, braking;

        [Header("Track Select UI")]
        public Image trackImage;
        public Text trackName, raceType, lapCount, aiCount, aiDifficulty, bestTime, trackLength;
        public Button raceButton, buyTrackButton;
        public Button RaceToCustom;
        public GameObject _easy, _mediam, _hard;
        [Header("Customization UI")]
        public Button apply;
        public Button buy;
        public GameObject colorsPanel;
        private int incartCr, bodyColPrice, rimPrice, upgradePrice, selectedColorID, selectedRimID, selectedUpgradeID;

        [Header("Settings UI")]
        public InputField playerName;
        public Slider masterVolume;
        public Dropdown graphicLevel;
        public Toggle mobileTouchSteer, mobileTiltSteer, mobileAutoAcceleration;
        public bool applyExpensiveGraphicChanges = false;

        [Header("Loading UI")]
        public Image loadingBar;

        [Header("Prompt Panel UI")]
        public Text promptTitle;
        public Text promptText;
        public Button accept, cancel;

        [Header("Misc UI")]
        public Text itemPrice;
        public Image locked;
        public Image cart;
        public Button nextArrow, prevArrow;

        [Header("Extra Settings")]
        public bool autoRotateVehicles = true;
        public bool rotateVehicleByDrag = true;
        public float rotateSpeed = 5.0f;
        public int maxOpponents = 5;
        [Range(1, 7)] public int raceTypes = 7;

        //Private vars
        private int vehicleIndex;
        private int prevVehicleIndex;
        private int trackIndex;
        private int raceTypeIndex = 1;
        private int aiDiffIndex = 1;
        private AsyncOperation async;
        private State previousState;
        private bool raycastTarget;
        private bool _autoRotate;
        // cache
        private float rotateDir = 1;
        private Texture lastColTex;
        private Texture lastRimTex;

        void Awake()
        {
            instance = this;
            LoadValues();
        }

        void Start()
        {
            //			PlayerPrefs.SetInt ("unlockall", 1);
            //			PlayerPrefs.DeleteAll ();
            state = State.Main;

            CycleVehicles();

            if (masterVolume)
                masterVolume.onValueChanged.AddListener(delegate {
                    SetMasterVolFromSlider();
                });
            if (playerName)
                playerName.onEndEdit.AddListener(delegate {
                    SetPlayerNameFromInputField();
                });
            if (graphicLevel)
                graphicLevel.onValueChanged.AddListener(delegate {
                    GetGrahicLevelFromDropdown();
                });
            if (graphicLevel)
                graphicLevel.value = QualitySettings.GetQualityLevel();
            if (mobileAutoAcceleration)
                mobileAutoAcceleration.onValueChanged.AddListener(delegate {
                    ToggleAutoAccel();
                });
            _autoRotate = autoRotateVehicles;
            selectedColorID = -1;
            selectedRimID = -1;
            selectedUpgradeID = -1;
            adsmanager.instance.showbanner();
        }


        void Update()
        {


            RotateVehicle();

            LerpStats();
        }

        public GameObject _unlockAllbtn, _removeAdsbtn;

        public void _unlockall()
        {
            adsmanager.instance._unlockAll();
        }

        public void _unlockALLDone()
        {
            _unlockAllbtn.SetActive(false);
            _removeAdsbtn.SetActive(false);
            _setBtns();
            //			Debug.LogError ("check work to unlockall");
        }

        public void _removeAds()
        {
            adsmanager.instance._removeAds();
        }

        public void _removeAdsDone()
        {
            _removeAdsbtn.SetActive(false);
            //			Debug.LogError ("check work to remove ads");
        }

        void CycleVehicles()
        {
            // Cycle between vehicles based on the "vehicleIndex" value
            for (int i = 0; i < menuVehicles.Length; i++)
            {
                if (vehicleIndex == i)
                {
                    menuVehicles[i].vehicle.rotation = menuVehicles[prevVehicleIndex].vehicle.rotation;
                    menuVehicles[i].vehicle.gameObject.SetActive(true);
                    UpdateUI();
                }
                else
                {
                    menuVehicles[i].vehicle.gameObject.SetActive(false);
                }
            }
        }

        void CycleTracks()
        {
            // Cycle between tracks based on the "trackIndex" value
            UpdateUI();
        }

        void _setBtns()
        {
            if (PlayerPrefs.GetInt("unlockall") == 2)
            {
                _unlockAllbtn.SetActive(false);
                itemPrice.enabled = (false);
                if (_img != null)
                    _img.enabled = false;
                _removeAdsbtn.SetActive(false);
            }
            else
            {
                _unlockAllbtn.SetActive(true);
                itemPrice.enabled = (true);
                if (_img != null)
                    _img.enabled = true;
                if (PlayerPrefs.GetInt("removeads") != 2)
                    _removeAdsbtn.SetActive(true);

            }
            if (PlayerPrefs.GetInt("removeads") == 2)
            {
                _removeAdsbtn.SetActive(false);
            }

        }

        public Image _img;

        void UpdateUI()
        {

            if (playerCurrency)
                playerCurrency.text = PlayerData.currency.ToString();
            //playerCurrency.text = PlayerData.currency.ToString ("N0") + "";

            if (cart)
                cart.enabled = state == State.Customize;

            if (nextArrow)
                nextArrow.gameObject.SetActive(state == State.VehicleSelect || state == State.TrackSelect);

            if (prevArrow)
                prevArrow.gameObject.SetActive(state == State.VehicleSelect || state == State.TrackSelect);

            //if (vehicleStats)
            //	vehicleStats.SetActive (state == State.VehicleSelect || state == State.Customize);

            switch (state)
            {

                case State.Main:
                    if (PlayerPrefs.GetInt("unlockall") != 2)
                    {
                        _unlockAllbtn.SetActive(true);
                        _removeAdsbtn.SetActive(true);
                    }
                    else
                    {
                        _unlockAllbtn.SetActive(false);
                        _removeAdsbtn.SetActive(false);
                    }
                    if (PlayerPrefs.GetInt("removeads") != 2)
                    {
                        if (PlayerPrefs.GetInt("unlockall") != 2)
                            _removeAdsbtn.SetActive(true);
                    }
                    else
                    {
                        _removeAdsbtn.SetActive(false);
                    }
                    _img.enabled = false;
                    //mainPanel.SetActive (true);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);

                    if (itemPrice)
                        itemPrice.text = string.Empty;

                    if (locked)
                    {
                        locked.enabled = false;
                        //					Debug.LogError ("locked");
                    }

                    break;

                case State.VehicleSelect:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(true);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);

                    if (vehicleName)
                        vehicleName.text = menuVehicles[vehicleIndex].name;

                    if (itemPrice)
                        itemPrice.text = menuVehicles[vehicleIndex].unlocked ? string.Empty : menuVehicles[vehicleIndex].price.ToString("N0") + "";

                    if (locked)
                    {
                        _setBtns();
                        if (PlayerPrefs.GetInt("unlockall") == 2)
                        {
                            locked.enabled = false;
                        }
                        else
                        {
                            locked.enabled = !menuVehicles[vehicleIndex].unlocked;
                        }
                    }

                    if (selectVehicleButton)
                        if (PlayerPrefs.GetInt("unlockall") == 2)
                        {
                            selectVehicleButton.gameObject.SetActive(true);
                        }
                        else
                            selectVehicleButton.gameObject.SetActive(menuVehicles[vehicleIndex].unlocked);

                    if (buyVehicleButton)
                    {
                        if (PlayerPrefs.GetInt("unlockall") == 2)
                        {
                            _setBtns();
                            buyVehicleButton.gameObject.SetActive(false);
                        }
                        else
                        {
                            buyVehicleButton.gameObject.SetActive(!menuVehicles[vehicleIndex].unlocked);
                            if (menuVehicles[vehicleIndex].unlocked)
                            {
                                _unlockAllbtn.SetActive(false);
                                itemPrice.enabled = (false);
                                if (_img != null)
                                    _img.enabled = false;
                                _removeAdsbtn.SetActive(false);
                            }
                            else
                                _setBtns();
                        }
                    }

                    if (customizeButton)
                        customizeButton.gameObject.SetActive(menuVehicles[vehicleIndex].unlocked);

                    if (menuState)
                        menuState.text = "VEHICLE SELECT";

                    break;

                case State.TrackSelect:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(true);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);

                    if (trackName)
                        trackName.text = menuTracks[trackIndex].name;

                    if (trackLength)
                        trackLength.text = menuTracks[trackIndex].trackLength;

                    if (trackImage && menuTracks[trackIndex].image)
                    {
                        trackImage.sprite = menuTracks[trackIndex].image;
                    }

                    if (raceType)
                        raceType.text = menuTracks[trackIndex].raceType.ToString();

                    if (lapCount)
                        lapCount.text = menuTracks[trackIndex].laps.ToString();

                    if (aiCount)
                        aiCount.text = menuTracks[trackIndex].aiCount.ToString();

                    if (aiDifficulty)
                        aiDifficulty.text = menuTracks[trackIndex].aiDifficulty.ToString();

                    if (itemPrice)
                    {
                        itemPrice.text = menuTracks[trackIndex].unlocked ? string.Empty : menuTracks[trackIndex].price.ToString("N0") + "";
                        if (itemPrice.text == "")
                            itemPrice.gameObject.transform.parent.gameObject.GetComponent<Image>().enabled = false;
                        else
                            itemPrice.gameObject.transform.parent.gameObject.GetComponent<Image>().enabled = true;

                    }

                    if (locked)
                    {
                        locked.enabled = !menuTracks[trackIndex].unlocked;
                        //					Debug.LogError ("locked");
                        _setBtns();
                        if (PlayerPrefs.GetInt("unlockall") == 2)
                        {
                            locked.enabled = false;
                        }
                        else
                        {
                            locked.enabled = !menuTracks[trackIndex].unlocked;
                        }
                    }

                    if (raceButton)
                        if (PlayerPrefs.GetInt("unlockall") == 2)
                        {
                            //					raceButton.gameObject.SetActive (false);
                            //					_easy.GetComponent <Button> ().interactable = true;
                            //					_mediam.GetComponent <Button> ().interactable = true;
                            //					_hard.GetComponent <Button> ().interactable = true;
                        }
                        else
                        {
                            //					raceButton.gameObject.SetActive (menuTracks [trackIndex].unlocked);
                            //					raceButton.gameObject.SetActive (false);
                            _easy.GetComponent<Button>().interactable = menuTracks[trackIndex].unlocked;
                            _mediam.GetComponent<Button>().interactable = menuTracks[trackIndex].unlocked;
                            _hard.GetComponent<Button>().interactable = menuTracks[trackIndex].unlocked;
                            if (!menuTracks[trackIndex].unlocked)
                                RaceToCustom.gameObject.SetActive(false);
                        }

                    if (buyTrackButton)
                    {

                        if (PlayerPrefs.GetInt("unlockall") == 2)
                        {
                            _setBtns();
                            buyTrackButton.gameObject.SetActive(false);
                        }
                        else
                        {
                            buyTrackButton.gameObject.SetActive(!menuTracks[trackIndex].unlocked);
                            if (menuTracks[trackIndex].unlocked)
                            {
                                _unlockAllbtn.SetActive(false);
                                _removeAdsbtn.SetActive(false);
                                itemPrice.enabled = (false);
                                if (_img != null)
                                    _img.enabled = false;
                            }
                            else
                                _setBtns();
                        }

                        //					buyTrackButton.gameObject.SetActive (!menuTracks [trackIndex].unlocked);
                    }

                    if (bestTime)
                        bestTime.text = (PlayerPrefs.HasKey("BestTime" + menuTracks[trackIndex].sceneName)) ? PlayerPrefs.GetString("BestTime" + menuTracks[trackIndex].sceneName) : "--:--:--";

                    if (menuState)
                        menuState.text = "TRACK SELECT";

                    break;


                case State.custom:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    //				trackSelectPanel.SetActive (true);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);

                    //				if (trackName)
                    //					trackName.text = menuTracks [trackIndex].name;

                    //				if (trackLength)
                    //					trackLength.text = menuTracks [trackIndex].trackLength;

                    //				if (trackImage && menuTracks [trackIndex].image) {
                    //					trackImage.sprite = menuTracks [trackIndex].image;
                    //				}

                    if (raceType)
                        raceType.text = menuTracks[trackIndex].raceType.ToString();

                    if (lapCount)
                        lapCount.text = menuTracks[trackIndex].laps.ToString();

                    if (aiCount)
                        aiCount.text = menuTracks[trackIndex].aiCount.ToString();

                    //				if (aiDifficulty)
                    //					aiDifficulty.text = menuTracks [trackIndex].aiDifficulty.ToString ();

                    //				if (itemPrice) {
                    //					itemPrice.text = menuTracks [trackIndex].unlocked ? string.Empty : menuTracks [trackIndex].price.ToString ("N0") + "";
                    //					if (itemPrice.text == "")
                    //						itemPrice.gameObject.transform.parent.gameObject.GetComponent<Image> ().enabled = false;
                    //					else
                    //						itemPrice.gameObject.transform.parent.gameObject.GetComponent<Image> ().enabled = true;
                    //
                    //				}

                    //				if (locked) {
                    //					locked.enabled = !menuTracks [trackIndex].unlocked;
                    //					//					Debug.LogError ("locked");
                    //					_setBtns ();
                    //					if (PlayerPrefs.GetInt ("unlockall") == 2) {
                    //						locked.enabled = false;
                    //					} else {
                    //						locked.enabled = !menuTracks [trackIndex].unlocked;
                    //					}
                    //				}

                    //				if (raceButton)
                    //				if (PlayerPrefs.GetInt ("unlockall") == 2) {
                    //					//					raceButton.gameObject.SetActive (false);
                    //					//					_easy.GetComponent <Button> ().interactable = true;
                    //					//					_mediam.GetComponent <Button> ().interactable = true;
                    //					//					_hard.GetComponent <Button> ().interactable = true;
                    //				} else {
                    //					//					raceButton.gameObject.SetActive (menuTracks [trackIndex].unlocked);
                    //					//					raceButton.gameObject.SetActive (false);
                    //					_easy.GetComponent <Button> ().interactable = menuTracks [trackIndex].unlocked;
                    //					_mediam.GetComponent <Button> ().interactable = menuTracks [trackIndex].unlocked;
                    //					_hard.GetComponent <Button> ().interactable = menuTracks [trackIndex].unlocked;
                    //					if (!menuTracks [trackIndex].unlocked)
                    //						RaceToCustom.gameObject.SetActive (false);
                    //				}

                    //				if (buyTrackButton) {
                    //
                    //					if (PlayerPrefs.GetInt ("unlockall") == 2) {
                    //						_setBtns ();
                    //						buyTrackButton.gameObject.SetActive (false);
                    //					} else {
                    //						buyTrackButton.gameObject.SetActive (!menuTracks [trackIndex].unlocked);
                    //						if (menuTracks [trackIndex].unlocked) {
                    //							_unlockAllbtn.SetActive (false);
                    //							_removeAdsbtn.SetActive (false);
                    //							itemPrice.enabled = (false);
                    //							if (_img != null)
                    //								_img.enabled = false;
                    //						} else
                    //							_setBtns ();
                    //					}
                    //
                    //					//					buyTrackButton.gameObject.SetActive (!menuTracks [trackIndex].unlocked);
                    //				}

                    //				if (bestTime)
                    //					bestTime.text = (PlayerPrefs.HasKey ("BestTime" + menuTracks [trackIndex].sceneName)) ? PlayerPrefs.GetString ("BestTime" + menuTracks [trackIndex].sceneName) : "--:--:--";

                    if (menuState)
                        menuState.text = "Custom";

                    break;


                case State.Customize:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(true);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);

                    //Calculate the in cart currency
                    incartCr = bodyColPrice + rimPrice + upgradePrice;

                    //Fill in the price texts (BODY COLORS)
                    for (int c = 0; c < bodyColors.Length; c++)
                    {
                        if (bodyColors[c].priceText)
                            bodyColors[c].priceText.text = !bodyColors[c].unlocked ? bodyColors[c].price.ToString("N0") : "Owned";
                    }

                    //Fill in the price texts (RIMS)
                    for (int r = 0; r < rims.Length; r++)
                    {
                        if (rims[r].priceText)
                            rims[r].priceText.text = !rims[r].unlocked ? rims[r].price.ToString("N0") : "Owned";
                    }

                    if (colorsPanel)
                        colorsPanel.SetActive(true);

                    if (apply)
                        apply.gameObject.SetActive(incartCr <= 0 && selectedColorID >= 0 || incartCr <= 0 && selectedRimID >= 0 || incartCr <= 0 && selectedUpgradeID >= 0);

                    if (buy)
                        buy.gameObject.SetActive(incartCr > 0);

                    if (itemPrice)
                        itemPrice.text = incartCr.ToString("N0") + "";

                    if (menuState)
                        menuState.text = "CUSTOMIZE";

                    break;

                case State.Settings:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(true);
                    loadingPanel.SetActive(false);

                    if (menuState)
                        menuState.text = "SETTINGS";

                    break;

                case State.Loading:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(true);

                    break;
            }
        }

        /// <summary>
        /// Lerps the stat values to suit the selected vehicle
        /// </summary>
        private void LerpStats()
        {
            //Normal Stats
            if (speed)
                speed.fillAmount = Mathf.Lerp(speed.fillAmount, menuVehicles[vehicleIndex].speed, Time.deltaTime * 3.0f);

            if (accel)
                accel.fillAmount = Mathf.Lerp(accel.fillAmount, menuVehicles[vehicleIndex].acceleration, Time.deltaTime * 3.0f);

            if (handling)
                handling.fillAmount = Mathf.Lerp(handling.fillAmount, menuVehicles[vehicleIndex].handling, Time.deltaTime * 3.0f);

            if (braking)
                braking.fillAmount = Mathf.Lerp(braking.fillAmount, menuVehicles[vehicleIndex].braking, Time.deltaTime * 3.0f);
        }

        public GameObject _trackSel, __trackCustom;

        public void _raceBtnToCustomization()
        {
            _trackSel.SetActive(false);
            __trackCustom.SetActive(true);
            nextArrow.gameObject.SetActive(false);
            prevArrow.gameObject.SetActive(false);
            state = State.custom;
        }

        public void _BackraceBtnToCustomization()
        {
            _trackSel.SetActive(true);
            __trackCustom.SetActive(false);
            nextArrow.gameObject.SetActive(true);
            prevArrow.gameObject.SetActive(true);
            state = State.TrackSelect;
        }

        private void RotateVehicle()
        {
            if (autoRotateVehicles)
                menuVehicles[vehicleIndex].vehicle.Rotate(0, (rotateSpeed * Time.deltaTime) * rotateDir, 0);


            //Rotate by drag raycast check
            if (rotateVehicleByDrag)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Collider[] childTransforms = menuVehicles[vehicleIndex].vehicle.GetComponentsInChildren<Collider>();

                        foreach (Collider t in childTransforms)
                        {
                            if (hit.collider == t)
                            {
                                autoRotateVehicles = false;
                                raycastTarget = true;
                            }
                            else
                            {
                                raycastTarget = false;
                                if (_autoRotate)
                                    autoRotateVehicles = true;
                            }
                        }
                    }
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    Vector3 mPos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

                    if (raycastTarget)
                        rotateDir = (mPos.x < 0.5f) ? 1 : -1;

                    if (_autoRotate)
                        autoRotateVehicles = true;

                    raycastTarget = false;
                }

                if (!raycastTarget)
                    return;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL

                menuVehicles[vehicleIndex].vehicle.Rotate(0, -Input.GetAxis("Mouse X"), 0);

#else
         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
         {
             Vector2 fingerPos = Input.GetTouch(0).deltaPosition;
        
             menuVehicles[vehicleIndex].vehicle.Rotate(0, -fingerPos.x, 0);
         }
#endif
            }
        }

        private void ApplyColorCustomization(int bodyCol, int tovehicleIndex)
        {

            bodyColors[bodyCol].unlocked = true;

            //Unclock the color
            if (!PlayerPrefs.HasKey("BodyColor" + bodyColors[bodyCol].ID + menuVehicles[tovehicleIndex].name))
                PlayerPrefs.SetInt("BodyColor" + bodyColors[bodyCol].ID + menuVehicles[tovehicleIndex].name, 1);

            //Save as the vehicle's current color
            PlayerPrefs.SetInt("CurrentBodyColor" + menuVehicles[tovehicleIndex].name, bodyCol);

            try
            {
                menuVehicles[tovehicleIndex].vehicleBody.mainTexture = bodyColors[bodyCol].visualUpgrade[tovehicleIndex].texture;
            }
            catch
            {
                Debug.LogError("You haven't properly configured color customizations for this vehicle! Ensure you have assigned a material for your vehicle and the index [" + bodyCol + "] of this customization exists or isn't null.");
            }

            lastColTex = null;
        }

        private void ApplyRimCustomization(int rimIndex, int tovehicleIndex)
        {

            rims[rimIndex].unlocked = true;

            if (!PlayerPrefs.HasKey("VehicleRim" + rims[rimIndex].ID + menuVehicles[vehicleIndex].name))
                PlayerPrefs.SetInt("VehicleRim" + rims[rimIndex].ID + menuVehicles[vehicleIndex].name, 1);

            //Save as the vehicle's current rim
            PlayerPrefs.SetInt("CurrentRim" + menuVehicles[tovehicleIndex].name, rimIndex);

            try
            {
                menuVehicles[tovehicleIndex].VehicleRims.mainTexture = rims[rimIndex].visualUpgrade[tovehicleIndex].texture;
            }
            catch
            {
                Debug.LogError("You haven't properly configured rim customizations for this vehicle! Ensure you have assigned a material for your vehicle and the index [" + rimIndex + "] of this customization exists or isn't null.");
            }


            lastRimTex = null;
        }

        /// <summary>
        /// Loads important values such as currency & preferences
        /// </summary>
        private void LoadValues()
        {
            PlayerData.LoadCurrency();

            //Last selected vehicle
            if (PlayerPrefs.HasKey("SelectedVehicle"))
                vehicleIndex = PlayerPrefs.GetInt("SelectedVehicle");

            //Master Vol
            if (masterVolume)
                masterVolume.value = (PlayerPrefs.HasKey("MasterVolume")) ? PlayerPrefs.GetFloat("MasterVolume") : 1;

            //Graphic Level
            if (PlayerPrefs.HasKey("GraphicLevel"))
                SetGraphicsQuality(PlayerPrefs.GetInt("GraphicLevel"));

            //Player Name
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                if (playerName)
                    playerName.text = PlayerPrefs.GetString("PlayerName");
            }

            //Toggles
            if (mobileAutoAcceleration)
                mobileAutoAcceleration.isOn = PlayerPrefs.GetString("AutoAcceleration") == "True";
            if (mobileTouchSteer)
                mobileTouchSteer.isOn = PlayerPrefs.GetString("MobileControlType") == "Touch";
            if (mobileTiltSteer)
                mobileTiltSteer.isOn = PlayerPrefs.GetString("MobileControlType") == "Tilt";

            //Other important stuff
            CheckForUnlockedVehiclesAndTracks();
            LoadCustomizations();

        }

        private void LoadCustomizations()
        {
            for (int i = 0; i < menuVehicles.Length; i++)
            {
                if (PlayerPrefs.HasKey("CurrentBodyColor" + menuVehicles[i].name))
                {
                    ApplyColorCustomization(PlayerPrefs.GetInt("CurrentBodyColor" + menuVehicles[i].name), i);
                }

                if (PlayerPrefs.HasKey("CurrentRim" + menuVehicles[i].name))
                {
                    ApplyRimCustomization(PlayerPrefs.GetInt("CurrentRim" + menuVehicles[i].name), i);
                }
            }
        }


        private void CheckForUnlockedVehiclesAndTracks()
        {

            //Check for unlokced vehicles
            for (int i = 0; i < menuVehicles.Length; i++)
            {
                //First check if the vehicle is pre-unlocked
                if (menuVehicles[i].unlocked)
                {
                    PlayerPrefs.SetInt(menuVehicles[i].name + "Unlocked", 1);
                }

                if (PlayerPrefs.GetInt(menuVehicles[i].name + "Unlocked") == 1)
                {
                    menuVehicles[i].unlocked = true;
                }
                else
                {
                    menuVehicles[i].unlocked = false;
                }
            }

            //Check for unlokced tracks
            for (int i = 0; i < menuTracks.Length; i++)
            {
                //First check if the track is pre-unlocked
                if (menuTracks[i].unlocked)
                {
                    PlayerPrefs.SetInt(menuTracks[i].name + "Unlocked", 1);
                }

                if (PlayerPrefs.GetInt(menuTracks[i].name + "Unlocked") == 1)
                {
                    menuTracks[i].unlocked = true;
                }
                else
                {
                    menuTracks[i].unlocked = false;
                }
            }
        }

        private void CheckForUnlockedCustomizations()
        {
            for (int i = 0; i < bodyColors.Length; i++)
            {
                if (PlayerPrefs.GetInt("BodyColor" + bodyColors[i].ID + menuVehicles[vehicleIndex].name) == 1)
                {
                    bodyColors[i].unlocked = true;
                }
                else
                {
                    bodyColors[i].unlocked = false;
                }
            }

            for (int i = 0; i < rims.Length; i++)
            {
                if (PlayerPrefs.GetInt("VehicleRim" + rims[i].ID + menuVehicles[vehicleIndex].name) == 1)
                {
                    rims[i].unlocked = true;
                }
                else
                {
                    rims[i].unlocked = false;
                }
            }
        }

        private void RevertCustomizationChanges()
        {
            if (lastColTex && menuVehicles[vehicleIndex].vehicleBody)
                menuVehicles[vehicleIndex].vehicleBody.mainTexture = lastColTex;
            if (lastRimTex && menuVehicles[vehicleIndex].VehicleRims)
                menuVehicles[vehicleIndex].VehicleRims.mainTexture = lastRimTex;

            incartCr = 0;
            bodyColPrice = 0;
            rimPrice = 0;
            upgradePrice = 0;
            selectedColorID = -1;
            selectedRimID = -1;
            selectedUpgradeID = -1;
            lastColTex = null;
            lastRimTex = null;

            //for (int i = 0; i < bodyColors.Length; i++)
            //{
            //    bodyColors[i].unlocked = false;
            //}
        }

        private void CreatePromptPanel(string title, string prompt)
        {

            if (promptTitle)
                promptTitle.text = title;

            if (promptText)
                promptText.text = prompt;

            if (promptPanel)
                promptPanel.SetActive(true);
        }

        #region Button Functions

        public void NextArrow()
        {
            //ButtonSFX (3);

            if (state == State.VehicleSelect)
            {
                if (vehicleIndex < menuVehicles.Length - 1)
                {
                    prevVehicleIndex = vehicleIndex;
                    vehicleIndex++;
                }
                else
                {
                    prevVehicleIndex = vehicleIndex;
                    vehicleIndex = 0;
                }

                CycleVehicles();
            }

            if (state == State.TrackSelect)
            {
                if (trackIndex < menuTracks.Length - 1)
                {
                    trackIndex++;
                }
                else
                {
                    trackIndex = 0;
                }

                CycleTracks();
            }
        }

        public void PreviousArrow()
        {
            ButtonSFX(4);

            if (state == State.VehicleSelect)
            {
                if (vehicleIndex > 0)
                {
                    prevVehicleIndex = vehicleIndex;
                    vehicleIndex--;
                }
                else
                {
                    prevVehicleIndex = vehicleIndex;
                    vehicleIndex = menuVehicles.Length - 1;
                }

                CycleVehicles();
            }

            if (state == State.TrackSelect)
            {
                if (trackIndex > 0)
                {
                    trackIndex--;
                }
                else
                {
                    trackIndex = menuTracks.Length - 1;
                }

                CycleTracks();
            }
        }

        public static int _mapno;

        public void TestingPlayMethod()
        {
            PlayerPrefs.SetString("PlayerVehicle", menuVehicles[vehicleIndex].resourceName);
        }

        public void Play()
        {
            state = State.Loading;

            UpdateUI();
            _mapno = menuTracks[trackIndex]._mapnumber;
            //Save all preferences
            PlayerPrefs.SetString("PlayerVehicle", menuVehicles[vehicleIndex].resourceName);
            PlayerPrefs.SetString("RaceType", menuTracks[trackIndex].raceType.ToString());
            PlayerPrefs.SetString("AiDifficulty", menuTracks[trackIndex].aiDifficulty.ToString());
            PlayerPrefs.SetInt("Opponents", menuTracks[trackIndex].aiCount);
            PlayerPrefs.SetInt("Laps", menuTracks[trackIndex].laps);
            ButtonSFX(10);

            StartCoroutine(LoadScene());
        }

        public void ok()
        {
            _success.SetActive(false);
            _failed.SetActive(false);
        }

        public void Buy()
        {
            ButtonSFX(8);

            //BUY VEHILCE
            if (state == State.VehicleSelect)
            {
                //				if (PlayerData.currency >= menuVehicles [vehicleIndex].price) {
                if (accept)
                {
                    accept.onClick.RemoveAllListeners();
                    accept.onClick.AddListener(() => AcceptPrompt());
                }

                if (cancel)
                {
                    cancel.gameObject.SetActive(true);
                    cancel.onClick.RemoveAllListeners();
                    cancel.onClick.AddListener(() => ClosePromptPanel());
                }
                Debug.LogError("success");
                CreatePromptPanel("CONFIRM ACTION", "Do you really want to purchase this vehicle?");
                //				} else {
                //					if (accept) {
                //						accept.onClick.RemoveAllListeners ();
                //						accept.onClick.AddListener (() => ClosePromptPanel ());
                //					}
                //
                //					if (cancel)
                //						cancel.gameObject.SetActive (false);
                //
                ////					CreatePromptPanel ("", "");
                //					CreatePromptPanel ("NOT ENOUGH CURRENCY", "You do not have enough currency to buy this vehicle");
                //				}
            }

            //BUY TRACK
            if (state == State.TrackSelect)
            {
                //				if (PlayerData.currency >= menuTracks [trackIndex].price) {
                if (accept)
                {
                    accept.onClick.RemoveAllListeners();
                    accept.onClick.AddListener(() => AcceptPrompt());
                }

                if (cancel)
                {
                    cancel.gameObject.SetActive(true);
                    cancel.onClick.RemoveAllListeners();
                    cancel.onClick.AddListener(() => ClosePromptPanel());
                }

                CreatePromptPanel("CONFIRM ACTION", "Do you really want to purchase this track?");
                //				} else {
                //					if (accept) {
                //						accept.onClick.RemoveAllListeners ();
                //						accept.onClick.AddListener (() => ClosePromptPanel ());
                //					}
                //
                //					if (cancel)
                //						cancel.gameObject.SetActive (false);
                //
                //					CreatePromptPanel ("NOT ENOUGH CURRENCY", "You do not have enough currency to buy this track");
                //				}
            }


            //BUY CUSTOMIZATION
            if (state == State.Customize)
            {
                if (PlayerData.currency >= incartCr)
                {
                    if (accept)
                    {
                        accept.onClick.RemoveAllListeners();
                        accept.onClick.AddListener(() => AcceptPrompt());
                    }

                    if (cancel)
                    {
                        cancel.gameObject.SetActive(true);
                        cancel.onClick.RemoveAllListeners();
                        cancel.onClick.AddListener(() => ClosePromptPanel());
                    }

                    CreatePromptPanel("CONFIRM ACTION", "Do you really want to make this purchase?");
                }
                else
                {
                    if (accept)
                    {
                        accept.onClick.RemoveAllListeners();
                        accept.onClick.AddListener(() => ClosePromptPanel());
                    }

                    if (cancel)
                        cancel.gameObject.SetActive(false);

                    CreatePromptPanel("NOT ENOUGH CURRENCY", "You do not have enough currency to make this purchase");
                }
            }
        }

        public void VehicleSelect()
        {
            ButtonSFX(2);

            state = State.VehicleSelect;

            UpdateUI();
        }

        public void TrackSelect()
        {
            ButtonSFX(12);
            adsmanager.instance._callAds(0);
            state = State.TrackSelect;

            UpdateUI();
        }

        public void Customize()
        {
            //			ButtonSFX ();

            state = State.Customize;

            CheckForUnlockedCustomizations();

            UpdateUI();
        }

        public void Settings()
        {
            ButtonSFX(4);

            if (state != State.Settings)
                previousState = state;

            state = state != State.Settings ? State.Settings : previousState;

            UpdateUI();
        }

        public void SetGraphicsQuality(int level)
        {
            QualitySettings.SetQualityLevel(level, applyExpensiveGraphicChanges);

            PlayerPrefs.SetInt("GraphicLevel", level);
        }

        private void GetGrahicLevelFromDropdown()
        {
            SetGraphicsQuality(graphicLevel.value);
        }

        private void SetMasterVolFromSlider()
        {
            PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);

            if (SoundManager.instance)
                SoundManager.instance.SetVolume();
        }

        private void SetPlayerNameFromInputField()
        {
            PlayerPrefs.SetString("PlayerName", playerName.text);
        }

        public void ToggleTouchControl(bool b)
        {
            mobileTouchSteer.isOn = true;

            mobileTiltSteer.isOn = false;

            PlayerPrefs.SetString("MobileControlType", "Touch");
        }

        public void ToggleTiltControl(bool b)
        {
            mobileTouchSteer.isOn = false;

            mobileTiltSteer.isOn = true;

            PlayerPrefs.SetString("MobileControlType", "Tilt");
        }

        public void ToggleAutoAccel()
        {
            string isOn = mobileAutoAcceleration.isOn ? "True" : "False";
            PlayerPrefs.SetString("AutoAcceleration", isOn);
        }

        public void ChooseVehicle()
        {
            PlayerPrefs.SetInt("SelectedVehicle", vehicleIndex);//
            adsmanager.instance._callAds(1);
            print("VehicleIndex" + vehicleIndex);
            //Back ();
        }

        public void AdjustRaceType(int val)
        {
            raceTypeIndex += val;
            raceTypeIndex = Mathf.Clamp(raceTypeIndex, 1, raceTypes);
            ButtonSFX(6);

            switch (raceTypeIndex)
            {

                case 1:
                    menuTracks[trackIndex].raceType = RaceManager.RaceType.Circuit;
                    break;

                case 2:
                    menuTracks[trackIndex].raceType = RaceManager.RaceType.LapKnockout;
                    break;

                case 3:
                    menuTracks[trackIndex].raceType = RaceManager.RaceType.TimeTrial;
                    break;

                case 4:
                    menuTracks[trackIndex].raceType = RaceManager.RaceType.SpeedTrap;
                    break;

                case 5:
                    menuTracks[trackIndex].raceType = RaceManager.RaceType.Checkpoints;
                    break;

                case 6:
                    menuTracks[trackIndex].raceType = RaceManager.RaceType.Elimination;
                    break;

                case 7:
                    menuTracks[trackIndex].raceType = RaceManager.RaceType.Drift;
                    break;
            }

            UpdateUI();
        }

        public void AdjustLaps(int val)
        {
            menuTracks[trackIndex].laps += val;
            menuTracks[trackIndex].laps = Mathf.Clamp(menuTracks[trackIndex].laps, 1, 1000);
            ButtonSFX(11);

            UpdateUI();
        }

        public void AdjustAiCount(int val)
        {
            menuTracks[trackIndex].aiCount += val;
            menuTracks[trackIndex].aiCount = Mathf.Clamp(menuTracks[trackIndex].aiCount, 0, maxOpponents);
            ButtonSFX(6);

            UpdateUI();
        }

        public Sprite _easySelected, _medSelected, _hardSelected;
        public Sprite _easyNotSelected, _medNotSelected, _hardNotSelected;

        public void AdjustAiDifficulty(int val)
        {
            ButtonSFX(11);
            //aiDiffIndex += val;
            //aiDiffIndex = Mathf.Clamp (aiDiffIndex, 1, 4);
            aiDiffIndex = val;
            switch (aiDiffIndex)
            {

                case 1:
                    menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.Custom;
                    break;

                case 2:
                    menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.Easy;
                    _easy.GetComponent<Image>().sprite = _easySelected;
                    _mediam.GetComponent<Image>().sprite = _medNotSelected;
                    _hard.GetComponent<Image>().sprite = _hardNotSelected;

                    break;

                case 3:
                    menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.Meduim;
                    _mediam.GetComponent<Image>().sprite = _medSelected;
                    _easy.GetComponent<Image>().sprite = _easyNotSelected;
                    _hard.GetComponent<Image>().sprite = _hardNotSelected;
                    break;

                case 4:
                    menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.Hard;
                    _hard.GetComponent<Image>().sprite = _hardSelected;
                    _mediam.GetComponent<Image>().sprite = _medNotSelected;
                    _easy.GetComponent<Image>().sprite = _easyNotSelected;
                    break;
            }
            RaceToCustom.gameObject.SetActive(true);
            //	UpdateUI ();
        }

        public void SelectColor(int c)
        {
            ButtonSFX(5);

            if (!menuVehicles[vehicleIndex].vehicleBody)
                return;

            if (!lastColTex)
                lastColTex = menuVehicles[vehicleIndex].vehicleBody.mainTexture;

            for (int i = 0; i < bodyColors.Length; i++)
            {
                if (c == bodyColors[i].ID)
                {
                    selectedColorID = i;
                    bodyColPrice = !bodyColors[selectedColorID].unlocked ? bodyColors[selectedColorID].price : 0;

                    try
                    {
                        menuVehicles[vehicleIndex].vehicleBody.mainTexture = bodyColors[i].visualUpgrade[vehicleIndex].texture;
                    }
                    catch
                    {
                        Debug.Log("You haven't properly configured color customizations for this vehicle! Ensure you have assigned a material for your vehicle and the index [" + i + "] of this customization exists or isn't null.");
                    }
                }
            }

            UpdateUI();
        }

        public void SelectRim(int r)
        {
            ButtonSFX(5);

            if (!menuVehicles[vehicleIndex].VehicleRims)
                return;

            if (!lastRimTex)
                lastRimTex = menuVehicles[vehicleIndex].VehicleRims.mainTexture;

            for (int i = 0; i < rims.Length; i++)
            {
                if (r == rims[i].ID)
                {
                    selectedRimID = i;
                    rimPrice = !rims[selectedRimID].unlocked ? rims[selectedRimID].price : 0;

                    try
                    {
                        menuVehicles[vehicleIndex].VehicleRims.mainTexture = rims[i].visualUpgrade[vehicleIndex].texture;
                    }
                    catch
                    {
                        Debug.Log("You haven't properly configured rim customizations for this vehicle! Ensure you have assigned a material for your vehicle and the index [" + i + "] of this customization exists or isn't null.");
                    }
                }
            }

            UpdateUI();
        }

        public void ApplyCustomizationChanges()
        {
            if (selectedColorID >= 0)
                ApplyColorCustomization(selectedColorID, vehicleIndex);

            if (selectedRimID >= 0)
                ApplyRimCustomization(selectedRimID, vehicleIndex);

            Back();
        }

    public void AcceptPrompt()
    {
        ButtonSFX(9);

        /* switch (state)
         {
             case State.VehicleSelect:
                 Debug.LogError("not working");
                 if (PlayerData.currency >= menuVehicles[vehicleIndex].price)
                 {
                     PlayerData.DeductCurrency(menuVehicles[vehicleIndex].price);
                     _success.SetActive(true);

                     menuVehicles[vehicleIndex].unlocked = true;
                     PlayerPrefs.SetInt(menuVehicles[vehicleIndex].name + "Unlocked", 1);
                 }
                 else
                 {
                     _failed.SetActive(true);
                 }
                 break;

             case State.TrackSelect:
                 Debug.LogError("not working");
                 if (PlayerData.currency >= menuTracks[trackIndex].price)
                 {
                     PlayerData.DeductCurrency(menuTracks[trackIndex].price);
                     _success.SetActive(true);

                     menuTracks[trackIndex].unlocked = true;
                     PlayerPrefs.SetInt(menuTracks[trackIndex].name + "Unlocked", 1);
                 }
                 else
                 {
                     _failed.SetActive(true);
                 }
                 break;

             case State.Customize:
                 PlayerData.DeductCurrency(incartCr);

                 if (selectedColorID >= 0)
                     ApplyColorCustomization(selectedColorID, vehicleIndex);

                 if (selectedRimID >= 0)
                     ApplyRimCustomization(selectedRimID, vehicleIndex);

                 Back();
                 break;
         }

         UpdateUI();
         ClosePromptPanel();
     }

     public void ClosePromptPanel()
     {
         ButtonSFX(5);
         if (promptPanel)
             promptPanel.SetActive(false);

         RevertCustomizationChanges();

         UpdateUI();
     }

     public void Back()
     {

         switch (state)
         {
             case State.Main:
                 ButtonSFX(2);
                 Application.Quit();
                 break;

             case State.VehicleSelect:
                 state = State.Main;
                 ButtonSFX(10);
                 vehicleIndex = PlayerPrefs.GetInt("SelectedVehicle");
                 CycleVehicles();
                 break;

             case State.TrackSelect:
                 ButtonSFX(9);
                 state = State.Main;
                 break;

             case State.Customize:
                 RevertCustomizationChanges();
                 state = State.VehicleSelect;
                 break;

             case State.Settings:
                 ButtonSFX(5);
                 state = previousState;
                 break;
         }

         UpdateUI();

    }
    #endregion

        IEnumerator LoadScene()
        {
            async = SceneManager.LoadSceneAsync(menuTracks[trackIndex].sceneName);

            while (!async.isDone)
            {
                if (loadingBar)
                    loadingBar.fillAmount = async.progress;

                yield return null;
            }
        }

        void ButtonSFX(int _sound)
        {
            //if (SoundManager.instance)
            //SoundManager.instance.PlaySound (SoundManager.instance.additionalGameSounds [_sound].soundName, true);
        }

        public void _moreGames()
        {
            ButtonSFX(5);
            //			Debug.Log ("more games");
            //Application.OpenURL(ConsoliAds.Instance.MoreFunURL());
        }

        public void _Rateus()
        {
            ButtonSFX(5);
            //Application.OpenURL(ConsoliAds.Instance.RateUsURL());
            //			ConsoliAds.Instance.RateUsURL ();
            //			Debug.Log ("Rate us");
        }
    }

    */