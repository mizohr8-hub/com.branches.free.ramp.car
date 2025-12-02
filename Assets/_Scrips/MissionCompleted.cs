using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCompleted : MonoBehaviour
{

    public int showing;
    public Text coinsEarned, distance;


    private void OnEnable()
    {
        StartCoroutine(AddCoin(2000));
        StartCoroutine(AddCoins(2000));
    }




    IEnumerator AddCoin(int coins)
    {
        print("Running");
        yield return new WaitForSeconds(1);
        //BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[0]);
        for (int i = 0; i < (coins / 50); i++)
        {
            yield return new WaitForSeconds(0.01f);
            showing += 50;
            coinsEarned.text = showing.ToString();
        }
    }
    
    IEnumerator AddCoins(int coins)
    {
        print("Running");
        yield return new WaitForSeconds(1);
        //BehaviourSetting.instance.bSource.PlayOneShot(BehaviourSetting.instance.sounds[0]);
        for (int i = 0; i < (coins / 50); i++)
        {
            yield return new WaitForSeconds(0.01f);
            showing += 50;
            distance.text = showing.ToString();
        }
    }






}
