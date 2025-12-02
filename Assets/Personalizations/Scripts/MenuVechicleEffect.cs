using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVechicleEffect : MonoBehaviour {
    public Vector3 _scale;
	// Use this for initialization
	void OnEnable () {
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        iTween.ScaleTo(gameObject, iTween.Hash("scale", _scale, "time", 0.5f, "easetype", iTween.EaseType.easeInOutBack));

    }
}
