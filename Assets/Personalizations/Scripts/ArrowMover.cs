using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMover : MonoBehaviour {

    Material Mat;
    public float x, y;
    public float speed = 0.7f;

    void Awake()
    {
        Mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        x = (Time.deltaTime *  speed) + x;

        Mat.mainTextureOffset = new Vector2(x, y);
    }

}
