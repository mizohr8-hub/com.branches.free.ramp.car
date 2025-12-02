using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changetexture : MonoBehaviour
{
    [System.Serializable]
    public class MeshColors
    {
        public Material[] allBodyColors;
    }

    [System.Serializable]
    public class MeshBody
    {
        public MeshRenderer mesh;
        public MeshColors[] meshColors;
    }

    public MeshBody[] body;

    public Material[] challengeModeTexture;
    private void Start()
    {
        if (PlayerPrefs.GetInt("IsChallenges")==0)
        {

            int y = Random.Range(0, body[Random.Range(0, body.Length)].meshColors.Length);


            for (int i = 0; i < body.Length; i++)
            {
                body[i].mesh.materials = body[i].meshColors[y].allBodyColors;
            }
        }
        else
        {
            body[0].mesh.materials = challengeModeTexture;
        }
    }

}
