//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2019 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles RCC Canvas dashboard elements.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Dashboard Displayer")]
[RequireComponent(typeof(RCC_DashboardInputs))]
public class RCC_UIDashboardDisplay : MonoBehaviour
{
	public static RCC_UIDashboardDisplay Instance;

	// Getting an Instance of Main Shared RCC Settings.
	#region RCC Settings Instance

	private RCC_Settings RCCSettingsInstance;
	private RCC_Settings RCCSettings
	{
		get
		{
			if (RCCSettingsInstance == null)
			{
				RCCSettingsInstance = RCC_Settings.Instance;
				return RCCSettingsInstance;
			}
			return RCCSettingsInstance;
		}
	}

	#endregion

	private RCC_DashboardInputs inputs;

	public DisplayType displayType;
	public enum DisplayType { Full, Customization, TopButtonsOnly, Off }

	public GameObject topButtons;
	public GameObject controllerButtons;
	public GameObject gauges;
	public GameObject customizationMenu;

	public Text RPMLabel;
	public Text KMHLabel;
	public Text GearLabel;
	public Text recordingLabel;

	public Image ABS;
	public Image ESP;
	public Image Park;
	public Image Headlights;
	public Image leftIndicator;
	public Image rightIndicator;
	public Image heatIndicator;
	public Image fuelIndicator;
	public Image rpmIndicator;

	public Dropdown mobileControllers;





	public HR_PlayerHandler handler;

	public Text score;
	public Text timeLeft;
	public Text combo;

	public Text speed;
	public Text distance;
	public Text highSpeed;
	public Text oppositeDirection;
	public Slider bombSlider;

	private Image comboMImage;
	private Vector2 comboDefPos;

	private Image highSpeedImage;
	private Vector2 highSpeedDefPos;

	private Image oppositeDirectionImage;
	private Vector2 oppositeDirectionDefPos;

	private Image timeAttackImage;

	private RectTransform bombRect;
	private Vector2 bombDefPos;






	void Awake()
	{
		if(Instance == null)
        {
			Instance = this;
        }	

		if (SceneManager.GetActiveScene().name == "HighwayRainy" || SceneManager.GetActiveScene().name == "HighwaySunny" || SceneManager.GetActiveScene().name == "HighwayNight")
		{
			comboMImage = combo.GetComponentInParent<Image>();
			comboDefPos = comboMImage.rectTransform.anchoredPosition;
			highSpeedImage = highSpeed.GetComponentInParent<Image>();
			highSpeedDefPos = highSpeedImage.rectTransform.anchoredPosition;
			oppositeDirectionImage = oppositeDirection.GetComponentInParent<Image>();
			oppositeDirectionDefPos = oppositeDirectionImage.rectTransform.anchoredPosition;
			timeAttackImage = timeLeft.GetComponentInParent<Image>();
			bombRect = bombSlider.GetComponent<RectTransform>();
			bombDefPos = bombRect.anchoredPosition;

			StartCoroutine("LateDisplay");
		}
		else
		{

			inputs = GetComponent<RCC_DashboardInputs>();

			if (!inputs)
			{

				enabled = false;
				return;
			}

		}
	}

	void Start()
	{
		if (SceneManager.GetActiveScene().name == "HighwayRainy")
		{

		}
		else
		{

			CheckController();

		}
	}
	void OnEnable()
	{
		if (SceneManager.GetActiveScene().name == "HighwayRainy"|| SceneManager.GetActiveScene().name == "HighwaySunny"|| SceneManager.GetActiveScene().name == "HighwayNight")
		{
			HR_PlayerHandler.OnPlayerSpawned += OnPlayerSpawned;

			StopAllCoroutines();
			StartCoroutine("LateDisplay");
		}
		else
		{

			RCC_SceneManager.OnMainControllerChanged += CheckController;

		}
	}
	IEnumerator LateDisplay()
	{

		while (true)
		{

			yield return new WaitForSeconds(.04f);

			score.text = handler.score.ToString("F0");
			speed.text = handler.speed.ToString("F0");
			distance.text = (handler.distance).ToString("F2");
			highSpeed.text = handler.highSpeedCurrent.ToString("F1");
			oppositeDirection.text = handler.opposideDirectionCurrent.ToString("F1");
			timeLeft.text = handler.timeLeft.ToString("F1");
			combo.text = handler.combo.ToString();

			if (HR_GamePlayHandler.Instance.mode == HR_GamePlayHandler.Mode.Bomb)
				bombSlider.value = handler.bombHealth / 100f;

		}

	}
	private void CheckController()
	{

		if (RCCSettings.controllerType == RCC_Settings.ControllerType.Keyboard || RCCSettings.controllerType == RCC_Settings.ControllerType.XBox360One)
		{

			if (mobileControllers)
				mobileControllers.interactable = false;

		}

		if (RCCSettings.controllerType == RCC_Settings.ControllerType.Mobile)
		{

			if (mobileControllers)
				mobileControllers.interactable = true;

		}

	}

	void Update()
	{

		if (SceneManager.GetActiveScene().name == "HighwayRainy" || SceneManager.GetActiveScene().name == "HighwaySunny" || SceneManager.GetActiveScene().name == "HighwayNight")
		{

			if (!handler)
				return;

			if (handler.combo > 1)
			{
				comboMImage.rectTransform.anchoredPosition = Vector2.Lerp(comboMImage.rectTransform.anchoredPosition, comboDefPos, Time.deltaTime * 5f);
			}
			else
			{
				comboMImage.rectTransform.anchoredPosition = Vector2.Lerp(comboMImage.rectTransform.anchoredPosition, new Vector2(comboDefPos.x - 500, comboDefPos.y), Time.deltaTime * 5f);
			}

			if (handler.highSpeedCurrent > .1f)
			{
				highSpeedImage.rectTransform.anchoredPosition = Vector2.Lerp(highSpeedImage.rectTransform.anchoredPosition, highSpeedDefPos, Time.deltaTime * 5f);
			}
			else
			{
				highSpeedImage.rectTransform.anchoredPosition = Vector2.Lerp(highSpeedImage.rectTransform.anchoredPosition, new Vector2(highSpeedDefPos.x + 500, highSpeedDefPos.y), Time.deltaTime * 5f);
			}

			if (handler.opposideDirectionCurrent > .1f)
			{
				oppositeDirectionImage.rectTransform.anchoredPosition = Vector2.Lerp(oppositeDirectionImage.rectTransform.anchoredPosition, oppositeDirectionDefPos, Time.deltaTime * 5f);
			}
			else
			{
				oppositeDirectionImage.rectTransform.anchoredPosition = Vector2.Lerp(oppositeDirectionImage.rectTransform.anchoredPosition, new Vector2(oppositeDirectionDefPos.x - 500, oppositeDirectionDefPos.y), Time.deltaTime * 5f);
			}

			if (HR_GamePlayHandler.Instance.mode == HR_GamePlayHandler.Mode.TimeAttack)
			{
				if (!timeLeft.gameObject.activeSelf)
					timeAttackImage.gameObject.SetActive(true);
			}
			else
			{
				if (timeLeft.gameObject.activeSelf)
					timeAttackImage.gameObject.SetActive(false);
			}

			if (HR_GamePlayHandler.Instance.mode == HR_GamePlayHandler.Mode.Bomb)
			{
				if (!bombSlider.gameObject.activeSelf)
					bombSlider.gameObject.SetActive(true);
			}
			else
			{
				if (bombSlider.gameObject.activeSelf)
					bombSlider.gameObject.SetActive(false);
			}

			if (handler.bombTriggered)
			{
				bombRect.anchoredPosition = Vector2.Lerp(bombRect.anchoredPosition, bombDefPos, Time.deltaTime * 5f);
			}
			else
			{
				bombRect.anchoredPosition = Vector2.Lerp(bombRect.anchoredPosition, new Vector2(bombDefPos.x - 500, bombDefPos.y), Time.deltaTime * 5f);
			}

		}
		else
		{

			switch (displayType)
			{

				case DisplayType.Full:

					if (topButtons && !topButtons.activeInHierarchy)
						topButtons.SetActive(true);

					if (controllerButtons && !controllerButtons.activeInHierarchy)
						//controllerButtons.SetActive(true);

					if (gauges && !gauges.activeInHierarchy)
						gauges.SetActive(true);

					if (customizationMenu && customizationMenu.activeInHierarchy)
						customizationMenu.SetActive(false);

					break;

				case DisplayType.Customization:

					if (topButtons && topButtons.activeInHierarchy)
						topButtons.SetActive(false);

					if (controllerButtons && controllerButtons.activeInHierarchy)
						controllerButtons.SetActive(false);

					if (gauges && gauges.activeInHierarchy)
						gauges.SetActive(false);

					if (customizationMenu && !customizationMenu.activeInHierarchy)
						customizationMenu.SetActive(true);

					break;

				case DisplayType.TopButtonsOnly:

					if (!topButtons.activeInHierarchy)
						topButtons.SetActive(true);

					if (controllerButtons.activeInHierarchy)
						controllerButtons.SetActive(false);

					if (gauges.activeInHierarchy)
						gauges.SetActive(false);

					if (customizationMenu.activeInHierarchy)
						customizationMenu.SetActive(false);

					break;

				case DisplayType.Off:

					if (topButtons && topButtons.activeInHierarchy)
						topButtons.SetActive(false);

					if (controllerButtons && controllerButtons.activeInHierarchy)
						controllerButtons.SetActive(false);

					if (gauges && gauges.activeInHierarchy)
						gauges.SetActive(false);

					if (customizationMenu && customizationMenu.activeInHierarchy)
						customizationMenu.SetActive(false);

					break;

			}
		}
	}

	void LateUpdate()
	{

		if (SceneManager.GetActiveScene().name == "HighwayRainy" || SceneManager.GetActiveScene().name == "HighwaySunny" || SceneManager.GetActiveScene().name == "HighwayNight")
		{

		}
		else
		{

			if (RCC_SceneManager.Instance.activePlayerVehicle)
			{

				if (RPMLabel)
					RPMLabel.text = inputs.RPM.ToString("0");

				if (KMHLabel)
				{

					if (RCCSettings.units == RCC_Settings.Units.KMH)
						KMHLabel.text = inputs.KMH.ToString("0") + "\nKMH";
					else
						KMHLabel.text = (inputs.KMH * 0.62f).ToString("0") + "\nMPH";

				}

				if (GearLabel)
				{

					if (!inputs.NGear && !inputs.changingGear)
						GearLabel.text = inputs.direction == 1 ? (inputs.Gear + 1).ToString("0") : "R";
					else
						GearLabel.text = "N";

				}

				if (recordingLabel)
				{

					switch (RCC_SceneManager.Instance.recordMode)
					{

						case RCC_SceneManager.RecordMode.Neutral:

							if (recordingLabel.gameObject.activeInHierarchy)
								recordingLabel.gameObject.SetActive(false);

							recordingLabel.text = "";

							break;

						case RCC_SceneManager.RecordMode.Play:

							if (!recordingLabel.gameObject.activeInHierarchy)
								recordingLabel.gameObject.SetActive(true);

							recordingLabel.text = "Playing";
							recordingLabel.color = Color.green;

							break;

						case RCC_SceneManager.RecordMode.Record:

							if (!recordingLabel.gameObject.activeInHierarchy)
								recordingLabel.gameObject.SetActive(true);

							recordingLabel.text = "Recording";
							recordingLabel.color = Color.red;

							break;

					}

				}

				if (ABS)
					ABS.color = inputs.ABS == true ? Color.yellow : Color.white;
				if (ESP)
					ESP.color = inputs.ESP == true ? Color.yellow : Color.white;
				if (Park)
					Park.color = inputs.Park == true ? Color.red : Color.white;
				if (Headlights)
					Headlights.color = inputs.Headlights == true ? Color.green : Color.white;
				if (heatIndicator)
					heatIndicator.color = RCC_SceneManager.Instance.activePlayerVehicle.engineHeat >= 100f ? Color.red : new Color(.1f, 0f, 0f);
				if (fuelIndicator)
					fuelIndicator.color = RCC_SceneManager.Instance.activePlayerVehicle.fuelTank < 10f ? Color.red : new Color(.1f, 0f, 0f);
				if (rpmIndicator)
					rpmIndicator.color = RCC_SceneManager.Instance.activePlayerVehicle.engineRPM >= RCC_SceneManager.Instance.activePlayerVehicle.maxEngineRPM - 500f ? Color.red : new Color(.1f, 0f, 0f);

				if (leftIndicator && rightIndicator)
				{

					switch (inputs.indicators)
					{

						case RCC_CarControllerV3.IndicatorsOn.Left:
							leftIndicator.color = new Color(1f, .5f, 0f);
							rightIndicator.color = new Color(.5f, .25f, 0f);
							break;
						case RCC_CarControllerV3.IndicatorsOn.Right:
							leftIndicator.color = new Color(.5f, .25f, 0f);
							rightIndicator.color = new Color(1f, .5f, 0f);
							break;
						case RCC_CarControllerV3.IndicatorsOn.All:
							leftIndicator.color = new Color(1f, .5f, 0f);
							rightIndicator.color = new Color(1f, .5f, 0f);
							break;
						case RCC_CarControllerV3.IndicatorsOn.Off:
							leftIndicator.color = new Color(.5f, .25f, 0f);
							rightIndicator.color = new Color(.5f, .25f, 0f);
							break;

					}

				}

			}
		}
	}

	public void SetDisplayType(DisplayType _displayType)
	{

		if (SceneManager.GetActiveScene().name == "HighwayRainy" || SceneManager.GetActiveScene().name == "HighwaySunny" || SceneManager.GetActiveScene().name == "HighwayNight")
		{

		}
		else
		{
			displayType = _displayType;
		}
	}
	void OnPlayerSpawned(HR_PlayerHandler player)
	{

		handler = player;

	}

	void OnDisable()
	{

		if (SceneManager.GetActiveScene().name == "HighwayRainy" || SceneManager.GetActiveScene().name == "HighwaySunny" || SceneManager.GetActiveScene().name == "HighwayNight")
		{
			HR_PlayerHandler.OnPlayerSpawned -= OnPlayerSpawned;
		}
		else
		{
			RCC_SceneManager.OnMainControllerChanged -= CheckController;
		}
	}
}
