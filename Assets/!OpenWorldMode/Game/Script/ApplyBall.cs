using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBall : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public Sprite[] SetSprite;
    private void OnEnable()
    {
        Sprite.sprite = SetSprite[PlayerPrefs.GetInt("SelectedBall")];
    }
}
