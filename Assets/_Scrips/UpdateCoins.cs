using RGSK;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoins : MonoBehaviour
{
    public Text coinsText;

    private void Awake()
    {
        coinsText = GetComponent<Text>();
    }

    private void Start()
    {
        InvokeRepeating("UpdatingCoins", 0f, 1f);
    }


    void UpdatingCoins()
    {
        coinsText.text = PlayerPrefs.GetInt("Currency").ToString();
        //print(PlayerData.currency);
    }

}
