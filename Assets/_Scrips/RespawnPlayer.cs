using RGSK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            GamePlayManager1.instance.health++;
            print(other.gameObject.name);
            if (GamePlayManager1.instance.health >= 2)
            {
                if (other.gameObject.tag == "Player" && !GamePlayManager1.instance.isGameOver)
                {
                    GamePlayManager1.instance.isGameOver = true;
                    print("GameOver");
                    GamePlayManager1.instance.revivePanel.SetActive(true);

                }
            }

            else
            {
               // PlayerControl.instance.Respawn();
                PlayerControl.instance.RespawnNew();
            }
            //if (collision.gameObject.tag == "Opponent")
            //{
            //    print("AI Fell down:  " + collision.gameObject.name);
            //    RGSK.OpponentControl.instance.RespawnRacer();
            //   // Destroy(collision.gameObject);
            //}
        }
    }
}
