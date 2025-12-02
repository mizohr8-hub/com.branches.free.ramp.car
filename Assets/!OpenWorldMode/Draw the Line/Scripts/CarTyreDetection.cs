using DrawDotGame;
using UnityEngine;

public class CarTyreDetection : MonoBehaviour
{
    public GameManager gameManager;
    public BallController ballController;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        ballController = FindObjectOfType<BallController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ballController.TrophyAnimator.SetBool("TriggerFlag", true);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameManager.Win();
            Vector3 thisPos = this.transform.position;
            Vector3 thatPos = collision.transform.position;
            Vector3 midPoint = thisPos + (thatPos - thisPos) / 2;

            ParticleSystem particle = Instantiate(gameManager.winning, midPoint, Quaternion.identity) as ParticleSystem;
            ManageTestMode.Instance.OnWin();
            particle.Play();
            //if (gameObject.GetComponent<Collider2D>() != null)
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameManager.Win();
        }
    }
}
