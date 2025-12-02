using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneHandler : MonoBehaviour
{
    public GameObject[] cutScenes; //0--- Snow, 1--- Scary, 2,3---General
    public float[] duration;
    public GameObject[] items;
    public GameObject[] levels;
    public GameObject[] vehicles, vehicle2, vehicle3, vehicle4;
    public GameObject fadeInImg,canvas;
    public Transform[] PlanCutSceneTransforms, containerCutSceneTransforms;
    public AudioClip[] bgClips; ////0--- Snow, 1--- Scary, 2---Dessert, 3---General
    AudioSource source;
    public Material[] skyboxes; //0--> Scary, 1--> Dessert, 2--> Galaxy
    private void Awake()
    {
        print(PlayerPrefs.GetInt("LevelNum") + "levle nu");
        source = GetComponent<AudioSource>();
        SkyBox();
    }
    private void Start()
    {
        StartCoroutine(PlayCutScene());
    }

    IEnumerator PlayCutScene()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levels[PlayerPrefs.GetInt("LevelNum") - 1].SetActive(true);
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].SetActive(false);
            vehicle2[i].SetActive(false);
            vehicle3[i].SetActive(false);
            vehicle4[i].SetActive(false);
        }
        vehicles[PlayerPrefs.GetInt("Carid")].SetActive(true);
        vehicle2[PlayerPrefs.GetInt("Carid")].SetActive(true);
        vehicle3[PlayerPrefs.GetInt("Carid")].SetActive(true);
        vehicle4[PlayerPrefs.GetInt("Carid")].SetActive(true);


        if ((PlayerPrefs.GetInt("LevelNum") - 1) == 2 /*|| (PlayerPrefs.GetInt("LevelNum") - 1) == 9 || (PlayerPrefs.GetInt("LevelNum") - 1) == 15*/)
        {
            //snow
            cutScenes[0].SetActive(true);
            yield return new WaitForSeconds(duration[0]-0.5f);
            fadeInImg.SetActive(true);
            canvas.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            cutScenes[0].SetActive(false);
            ActivateObjects();
            yield return new WaitForSeconds(0.1f);
            fadeInImg.SetActive(false);
            canvas.SetActive(false);
            //cutScenes[0].SetActive(false);
            //ActivateObjects();
            
        }
        else if ((PlayerPrefs.GetInt("LevelNum") - 1) == 3/* || (PlayerPrefs.GetInt("LevelNum") - 1) == 10 || (PlayerPrefs.GetInt("LevelNum") - 1) == 16*/)
        {
            //scary
            cutScenes[1].SetActive(true);
            yield return new WaitForSeconds(duration[1]);
            fadeInImg.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            fadeInImg.SetActive(false);
            canvas.SetActive(false);
            cutScenes[1].SetActive(false);
            ActivateObjects();
            //source.clip = bgClips[1];
            //source.volume = 0.6f;
            //source.Play();
        }
        else if ((PlayerPrefs.GetInt("LevelNum") - 1) == 0 || (PlayerPrefs.GetInt("LevelNum") - 1) == 4 || (PlayerPrefs.GetInt("LevelNum") - 1) == 6 || (PlayerPrefs.GetInt("LevelNum") - 1) == 8 ||  (PlayerPrefs.GetInt("LevelNum") - 1) == 10 || (PlayerPrefs.GetInt("LevelNum") - 1) == 16 || (PlayerPrefs.GetInt("LevelNum") - 1) == 12 || (PlayerPrefs.GetInt("LevelNum") - 1) == 14 || (PlayerPrefs.GetInt("LevelNum") - 1) == 18)
        {
            cutScenes[2].transform.position = PlanCutSceneTransforms[PlayerPrefs.GetInt("LevelNum") - 1].transform.position;
            cutScenes[2].SetActive(true);
            yield return new WaitForSeconds(duration[2]);
            yield return new WaitForSeconds(0.5f);
            fadeInImg.SetActive(false);
            canvas.SetActive(false);
            cutScenes[2].SetActive(false);
            ActivateObjects();
            //source.clip = bgClips[2];
            //source.volume = 0.2f;
            //source.Play();
        }


        else if ((PlayerPrefs.GetInt("LevelNum") - 1) == 1 || (PlayerPrefs.GetInt("LevelNum") - 1) == 5 || (PlayerPrefs.GetInt("LevelNum") - 1) == 7 || (PlayerPrefs.GetInt("LevelNum") - 1) == 9 || (PlayerPrefs.GetInt("LevelNum") - 1) == 15 || (PlayerPrefs.GetInt("LevelNum") - 1) == 11 || (PlayerPrefs.GetInt("LevelNum") - 1) == 13 || (PlayerPrefs.GetInt("LevelNum") - 1) == 17 || (PlayerPrefs.GetInt("LevelNum") - 1) == 19)
        {
            cutScenes[3].transform.position = containerCutSceneTransforms[PlayerPrefs.GetInt("LevelNum") - 1].transform.position;
            print(containerCutSceneTransforms[PlayerPrefs.GetInt("LevelNum") - 1].transform.name);
            cutScenes[3].SetActive(true);
            yield return new WaitForSeconds(duration[3]);
            yield return new WaitForSeconds(0.5f);
            fadeInImg.SetActive(false);
            canvas.SetActive(false);
            cutScenes[3].SetActive(false);
            ActivateObjects();
            //source.clip = bgClips[2];
            //source.volume = 0.2f;
            //source.Play();
        }
        BgSound();

    }


    void SkyBox()
    {
        if ((PlayerPrefs.GetInt("LevelNum") - 1) == 3)
        {
            RenderSettings.skybox = skyboxes[0];
        }
        else if ((PlayerPrefs.GetInt("LevelNum") - 1) == 7)
        {
            RenderSettings.skybox = skyboxes[2];
            //RenderSettings.fog = true;
        }
        else if((PlayerPrefs.GetInt("LevelNum") - 1) == 8)
        {
            RenderSettings.skybox = skyboxes[1];
            //RenderSettings.fog = true;
        }
        else if((PlayerPrefs.GetInt("LevelNum") - 1) == 9)
        {
            RenderSettings.skybox = skyboxes[2];
            //RenderSettings.fog = true;
        }
        else
        {
            RenderSettings.skybox = skyboxes[3];
        }
    }


    void BgSound()
    {
        if ((PlayerPrefs.GetInt("LevelNum") - 1) == 2)
        {
            source.clip = bgClips[0];
            source.volume = 0.05f;
            source.Play();
        }
        else if ((PlayerPrefs.GetInt("LevelNum") - 1) == 3)
        {
            source.clip = bgClips[1];
            source.volume = 0.4f;
            source.Play();
        }
        else if ((PlayerPrefs.GetInt("LevelNum") - 1) == 8)
        {
            source.clip = bgClips[2];
            source.volume = 0.3f;
            source.Play();
        }
        else
        {
            source.clip = bgClips[3];
            source.volume = 0.2f;
            source.Play();
        }
    }

    void ActivateObjects()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(true);
        }
    }
}
