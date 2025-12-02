using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RGSK;
using DG.Tweening;

public class BehaviourSetting : MonoBehaviour {
    public static BehaviourSetting instance;
    public bool inAir = false;
    public int type, currency;
    public GameObject checkPointCfx, checkPointTxt, starImage, fireWroks;
    public int a = 0;
    public GameObject nosEffect;
    public AudioSource aSource;
    public GameObject primaryCamera;
    public GameObject secondaryCamera;
    public AudioSource bSource;
    public AudioClip[] sounds;
    public GameObject chasis;
    public GameObject wheels, fakeWheels;
    public GameObject[] wheelsModes;
    public Transform currentCheckPoint;

    private bool afterJump;
    public bool isLevelCompleted;
    public bool activeBool;
    public Transform crackPos;
    public GameObject[] neons;
    public GameObject nos;

    public bool isLagone = false;
    public float maxSpeed;
    public float nosSpeed;

    private void Awake()
    {
        if (GeneralScript.instance)
        {
            currentCheckPoint = GeneralScript.instance.checkPoints[PlayerPrefs.GetInt("LevelNum")].transform.GetChild(0);
        }
    }

    void OnEnable()
    {
        instance = this;

        currency = PlayerPrefs.GetInt("Currency");
        aSource = GetComponent<AudioSource>();
        fireWroks = GameObject.FindGameObjectWithTag("Effects");
        checkPointCfx = GameObject.FindGameObjectWithTag("CFX");
        checkPointTxt = GameObject.FindGameObjectWithTag("CheckPointText");
        starImage = GameObject.FindGameObjectWithTag("StarImage");
    }

    public bool temp = false;
    private void Update()
    {
        //    if (inAir && !temp)
        //    {

        //        GetComponent<RCC_CarControllerV3>().enabled = false;
        //        ControlRotation();
        //        temp = true;
        //        print("Dum Dum");
        //    }
        //    else
        //    {

        //        GetComponent<RCC_CarControllerV3>().enabled = true;
        //        print("Dum Dum n");
        //    }

    }

    private void Start()
    {
        primaryCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //if (isLagone)
        //{
        //    GetComponent<RCC_CarControllerV3>().steerAngle = 5;
        //}

        maxSpeed = GetComponent<RCC_CarControllerV3>().maxspeed;
        nosSpeed = GetComponent<RCC_CarControllerV3>().maxspeed + 80;
    }
    void EnableIT()
    {

        GetComponent<RCC_CarControllerV3>().enabled = true;
    }

    public void ControlRotation()
    {
        print("before Magic rotation");
        if (transform.localEulerAngles.x > GetComponent<Statistics>().lastPassedNode.transform.localRotation.x + 10 || transform.localEulerAngles.x > GetComponent<Statistics>().lastPassedNode.transform.localRotation.x - 10
            || transform.localEulerAngles.y > GetComponent<Statistics>().lastPassedNode.transform.localRotation.y + 10 || transform.localEulerAngles.y > GetComponent<Statistics>().lastPassedNode.transform.localRotation.y - 10
            || transform.localEulerAngles.z > GetComponent<Statistics>().lastPassedNode.transform.localRotation.z + 10 || transform.localEulerAngles.y > GetComponent<Statistics>().lastPassedNode.transform.localRotation.z - 10)
        {
                print("Magic rotation");
            transform.DORotate(new Vector3(0, -90, 0), 1f);
        }
        Invoke("EnableIT", 3f);
    }

    private void LevelComplete()
    {
        isLevelCompleted = true;
        if (GamePlayManager1.instance) GamePlayManager1.instance.levelCompleted = true;
        if (GamePlayManager.instance) GamePlayManager.instance.levelCompleted = true;
        RGSK.RaceUI.instance.UpdateUIPanels();
        CancelInvoke("LevelComplete");
    }

    public float[] durations;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "Checkpoint")
        //{
        //    checkPointCfx.GetComponent<ParticleSystem>().Play();
        //    checkPointTxt.transform.GetChild(0).gameObject.SetActive(true);

        //    Invoke("TextOff", 1f);
        //}

        if (other.gameObject.name == "Finish" || other.gameObject.tag == "FinishLine" ||other.gameObject.tag == "Finish")
        {
            if (PlayerPrefs.GetInt("Modeis") == 0 || PlayerPrefs.GetInt("Modeis") == 1 || PlayerPrefs.GetInt("Modeis") == 3)
            {
                print("Game Completed");
                // fireWroks.transform.GetChild(0).gameObject.SetActive(true);
                RCC_CarControllerV3.instance.handbrakeInput = 1f;
                GetComponent<Rigidbody>().drag = 8f;
                RGSK.RaceManager.instance._raceState = RGSK.RaceManager.RaceState.Complete; //ForOnce
                if (TournamentManager.Instance)
                {
                    TournamentManager.Instance.RCCControllers.SetActive(false);
                }
                //RGSK.PlayerCamera.instance.lookRight = true;
                RGSK.RaceManager.instance.raceCompleted = true;
                RGSK.Statistics.instance.NewLap();
                AudioSource[] as_ = FindObjectsOfType<AudioSource>();

                foreach (var item in as_)
                {
                    item.mute = true;
                }

                bSource.mute = false;
                bSource.PlayOneShot(sounds[1]);

                Invoke("LevelComplete", 3.5f);
                if (GeneralScript.instance)
                {
                    GeneralScript.instance.leftBtn.GetComponent<RCC_UIController>().pressing = true;
                    GeneralScript.instance.gasBtn.GetComponent<RCC_UIController>().pressing = true;
                }
            }
        }
        if (other.gameObject.tag == "Star")
        {
            
            checkPointCfx.GetComponent<ParticleSystem>().Play();
            // RGSK.PlayerData.AddCurrency(100);
            //StartCoroutine(AddCoin(100));

            Destroy(other.gameObject);
            print("star" + a);
            starImage.transform.GetChild(0).gameObject.SetActive(true);
            if (GamePlayManager1.instance)
            {
                GamePlayManager1.instance.starPng[a].SetActive(true);
            }
            if (GamePlayManager.instance)
            {
                GamePlayManager.instance.starPng[a].SetActive(true);
            }

            a++;
            if (GeneralScript.instance)
            {
            GeneralScript.instance.source.volume = 0.4f;
            GeneralScript.instance.source.PlayOneShot(sounds[0]);

            }
            //asource.PlayOneShot(sounds[0]);
            //asource.clip = sounds[1]; asource.Play();
        }
        if (other.gameObject.tag == "CameraSwitcher")
        {            
            RGSK.PlayerCamera.instance.lookRight = true;

            //asource.PlayOneShot(sounds[i]);
            afterJump = true;
            //print("Triggered");
        }
        if (other.gameObject.tag == "RotateCar")
        {
            wheels.SetActive(false);
            fakeWheels.SetActive(true);

            chasis.GetComponent<Animator>().SetTrigger("Rotate");
            //RaceManager.instance.activePlayer.transform.rotation = Quaternion.Euler(0, -90, 0);
            if (GeneralScript.instance)
            {
                GeneralScript.instance.PlayClip();
            }
            //Invoke("DisaleWheels", durations[0]);
        }
        if (other.gameObject.tag == "NOSS")
        {
            GeneralScript.instance.NOSButton.GetComponent<RCC_UIController>().pressing = true;
            //RaceManager.instance.activePlayer.transform.GetComponent<RCC_CarControllerV3>().useNOS = true;
            //RaceManager.instance.activePlayer.transform.GetComponent<RCC_CarControllerV3>().NOS();
            Invoke("DeactivateNos", Random.Range(7f, 10f));
        }
        if (other.gameObject.tag == "Flip")
        {
            wheels.SetActive(false);
            for (int i = 0; i < wheelsModes.Length; i++)
            {
                wheelsModes[i].GetComponent<MeshRenderer>().enabled = false;
            }
            fakeWheels.SetActive(true);
            chasis.GetComponent<Animator>().SetTrigger("Flip");
            //RaceManager.instance.activePlayer.transform.rotation = Quaternion.Euler(0, -90, 0);
            if (GeneralScript.instance)
            {
                GeneralScript.instance.PlayClip();
            }
            //Invoke("DisaleWheels", durations[1]);
        }
        if (other.gameObject.tag == "LeftFlip")
        {
            wheels.SetActive(false);
            for (int i = 0; i < wheelsModes.Length; i++)
            {
                wheelsModes[i].GetComponent<MeshRenderer>().enabled = false;
            }
            fakeWheels.SetActive(true);
            chasis.GetComponent<Animator>().SetTrigger("LeftFlip");
            //RaceManager.instance.activePlayer.transform.rotation = Quaternion.Euler(0, -90, 0);
            if (GeneralScript.instance)
            {
                GeneralScript.instance.PlayClip();
            }
            //Invoke("DisaleWheels", durations[2]);
        }
        if (other.gameObject.tag == "Run")
        {
            StartCoroutine(Run());
            other.gameObject.SetActive(false);
            
        }
        if (other.gameObject.tag == "RightFlip")
        {
            wheels.SetActive(false); 
            for (int i = 0; i < wheelsModes.Length; i++)
            {
                wheelsModes[i].GetComponent<MeshRenderer>().enabled = false;
            }
            fakeWheels.SetActive(true);
            chasis.GetComponent<Animator>().SetTrigger("RightFlip");
            //RaceManager.instance.activePlayer.transform.rotation = Quaternion.Euler(0, -90, 0);
            if (GeneralScript.instance)
            {
                GeneralScript.instance.PlayClip();
            }
            //Invoke("DisaleWheels", durations[3]);
        }
        if (other.gameObject.tag == "Pumpkin")
        {
            checkPointCfx.GetComponent<ParticleSystem>().Play();
            starImage.transform.GetChild(1).gameObject.SetActive(true);
            GeneralScript.instance.source.PlayOneShot(GeneralScript.instance.pumpkinClip);
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 100);
            GamePlayManager1.instance.coinsText.text = PlayerPrefs.GetInt("Currency").ToString();

        }
        if (other.gameObject.tag == "IceCream")
        {
            checkPointCfx.GetComponent<ParticleSystem>().Play();
            starImage.transform.GetChild(1).gameObject.SetActive(true);
            GeneralScript.instance.source.PlayOneShot(sounds[0]);
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 100);
            GamePlayManager1.instance.coinsText.text = PlayerPrefs.GetInt("Currency").ToString();
        }
        if (other.gameObject.tag == "Penguin")
        {
            gameObject.GetComponent<Animator>().SetTrigger("Fly");
        }
        if (other.gameObject.tag == "Drift")
        {
            StartCoroutine(Drift());
        }
        if (other.gameObject.tag == "SpeedLimit")
        {
           StartCoroutine(ElectricShock());
            Handheld.Vibrate();
        }
        if (other.gameObject.tag == "CameraSwitcherLeft")
        {            
            RGSK.PlayerCamera.instance.lookLeft = true;

            //asource.PlayOneShot(sounds[i]);
            afterJump = true;
            //print("Triggered");
        }
        if (other.gameObject.tag == "CameraSwitcherFront")
        {            
            RGSK.PlayerCamera.instance.lookRight = false;
            RGSK.PlayerCamera.instance.lookLeft = false;
            RGSK.PlayerCamera.instance.lookBack = false;
            Invoke("FrontTrue", 0.1f);

            //asource.PlayOneShot(sounds[i]);
            afterJump = true;
            //print("Triggered");
        }
        if (other.gameObject.tag == "Slomo")
        {
            Time.timeScale = 0.4f;
           GeneralScript.instance.slomo = true;
            Invoke("DeactivateSlomo", 1.6f);
        }
        if (other.gameObject.tag == "GlassCrack")
        {
            var crack = Instantiate(GeneralScript.instance.crackParticle, crackPos.position - new Vector3(15f, 0f, 0f), Quaternion.identity);           
        }
        if (other.gameObject.tag == "CameraSwitcherHeight")
        {
            RGSK.PlayerCamera.instance.lookAtHeight = 100f;

            //asource.PlayOneShot(sounds[i]);
            afterJump = true;
            //print("Triggered");
        }
        if (other.gameObject.tag == "No2")
        {
            FindObjectOfType<AudioSource>().PlayOneShot(sounds[6]);
            StartCoroutine("FillNos");
            Destroy(other.gameObject);
            print("NOs");
        }
        if (other.gameObject.tag == "CheckPoints")
        {
            currentCheckPoint = other.transform;
        }
    }


    IEnumerator Drift()
    {
        RaceManager.instance.activePlayer.GetComponent<RCC_CarControllerV3>().steerAngle = 100f;

        GeneralScript.instance.gasBtn.GetComponent<RCC_UIController>().pressing = true;
        yield return new WaitForSeconds(0.1f);
        GeneralScript.instance.leftBtn.GetComponent<RCC_UIController>().pressing = true;
    }
    
    IEnumerator Run()
    {
        yield return new WaitForSeconds(0.1f);
        GeneralScript.instance.gasButton.GetComponent<RCC_UIController>().pressing = true;
        yield return new WaitForSeconds(8f);
        GeneralScript.instance.gasButton.GetComponent<RCC_UIController>().pressing = false;
       
    }



    IEnumerator ElectricShock()
    {
        //GeneralScript.instance.gasBtn.GetComponent<RCC_UIController>().pressing = true;
        //yield return new WaitForSeconds(1f);
        //GeneralScript.instance.gasBtn.GetComponent<RCC_UIController>().pressing = false;

        GeneralScript.instance.source.PlayOneShot(GeneralScript.instance.shockSound);
        RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 4;
        yield return new WaitForSeconds(0.5f);
        RaceManager.instance.activePlayer.GetComponent<Rigidbody>().drag = 0.01f;
    }

    void FrontTrue()
    {

        RGSK.PlayerCamera.instance.lookFront = true;
    }

    void DeactivateNos()
    {
        GeneralScript.instance.NOSButton.GetComponent<RCC_UIController>().pressing = false;
    }

    void DisaleWheels()
    {
        fakeWheels.SetActive(false);
        //wheels.SetActive(true);
        for (int i = 0; i < wheelsModes.Length; i++)
        {
            wheelsModes[i].GetComponent<MeshRenderer>().enabled = true;
        }
        print(RaceManager.instance.activePlayer.transform.rotation);
    }

    IEnumerator AddCoin(int coins)
    {
          //  for (int i = 0; i < (coins / 20); i++)
            {
                yield return new WaitForSeconds(0.015f);
                currency += 20;
            RGSK.PlayerData.SaveCurrency();
            }
    }

    IEnumerator FillNos()
    {
        for (int i = 0; i <= 10; i++)
        {
            yield return new WaitForSeconds(0.015f);
            if (RCC_CarControllerV3.instance.NoS <= 100)
            {
                RCC_CarControllerV3.instance.NoS += 10;
            }
        }
        StopCoroutine("FillNos");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CameraSwitcher")
        {           
            Invoke("SwitchBackCam", 1f);
        }
        if (other.gameObject.tag == "CameraSwitcherFront")
        {
           
            Invoke("SwitchBackCam", 1f);
        }
        if (other.gameObject.tag == "CameraSwitcherRight")
        {
           
            Invoke("SwitchBackCam", 1f);
        }
        if (other.gameObject.tag == "CameraSwitcherLeft")
        {
           
            Invoke("SwitchBackCam", 1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (afterJump)
            {
                print("Impressive");
                int i = Random.Range(2, 5);
                checkPointCfx.GetComponent<ParticleSystem>().Play();
                // FindObjectOfType<AudioSource>().PlayOneShot(sounds[i]);
               
                //bSource.PlayOneShot(sounds[i]);

                print(i + "  i ");
                afterJump = false;
            }
            print(collision.gameObject.tag + "  :after jump");
        }
    }

    private void SwitchBackCam()
    {
        int a = Random.Range(2, 4);
        RGSK.PlayerCamera.instance.lookRight = false;
        RGSK.PlayerCamera.instance.lookFront = false;
        RGSK.PlayerCamera.instance.lookBack = false;
        RGSK.PlayerCamera.instance.lookLeft = false;
        
    }


    void DeactivateSlomo()
    {
        GeneralScript.instance.slomo = false;
        Time.timeScale = 1;
    }

    private void TextOff()
    {
        checkPointTxt.transform.GetChild(0).gameObject.SetActive(false);
        print("CFX");
    }


}
