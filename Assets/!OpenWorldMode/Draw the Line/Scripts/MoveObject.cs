//using DrawDotGame;
//using UnityEngine;

//public class MoveObject : MonoBehaviour
//{
//    public float speed = 2;
//    public Transform target; // Reference to the PinkBall object
//    public GameManager manager;
//    private void Awake()
//    {

//    }
//    void Update()
//    {
//        if (target == null)
//        {
//            // If the target is not set, do nothing
//            return;
//        }

//        if (/*manager.stopHolding == true && manager.allowDrawing == false*/manager.baka == true)
//        {
//            // Calculate the direction from the current position to the target position
//            Vector3 direction = (target.position - transform.position).normalized;

//            // Calculate the movement based on the calculated direction
//            Vector3 movement = direction * speed * Time.deltaTime;

//            // Move the object towards the target
//            transform.Translate(movement, Space.World);
//        }
//    }
//}
