using DrawDotGame;
using System.Collections;
using UnityEngine;
public class CarController : MonoBehaviour
{

    [SerializeField]
    private WheelJoint2D frontTire, backTire;

    [SerializeField]
    private float speed;

    private float movement, moveSpeed, fuel = 1, fuelConsumption = 0.1f;
    public float Fuel { get => fuel; set { fuel = value; } }

    public bool moveStop = false;

    public Vector3 StartPos { get; set; }
    public float force;
    public Rigidbody2D carRigidbody;
    public GameManager manager;
    bool _once;
    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody2D>();
        carRigidbody.bodyType = RigidbodyType2D.Static;
        carRigidbody.centerOfMass = new Vector2(0f, -0.5f);
    }
    private void Update()
    {
        //PC : movement = Input.GetAxis("Horizontal");
        //엔진 버튼 누를 시

        if (manager.enableCarPhysics && !_once)
        {
            PlaySoundRepeatedly();
        }
        if (/*GameManager.Instance.GasBtnPressed*/manager.enableCarPhysics)
        {
            if (carRigidbody.bodyType == RigidbodyType2D.Static)
                carRigidbody.bodyType = RigidbodyType2D.Dynamic;
            movement += 0.009f;
            if (movement > 1f)
                movement = 1f;
        }


        //else if(GameManager.Instance.BrakeBtnPressed) {
        //    movement -= 0.009f;
        //    if(movement < -1f)
        //        movement = -1f;
        //}

        //아무 버튼도 누르지 않을 시
        else if (/*!GameManager.Instance.GasBtnPressed && !GameManager.Instance.BrakeBtnPressed*/!manager.enableCarPhysics)
        {
            movement = 0;
        }
        //Debug.Log("movement speed is " + movement);
        moveSpeed = movement * speed;
        float slopeForce = CalculateSlopeForce();
        force += slopeForce;
        Vector2 forceDirection = new Vector2(force, 0);
        Vector2 carPosition = carRigidbody.position;

        // Apply force at the center of the car
        carRigidbody.AddForceAtPosition(forceDirection, carPosition);

        // Apply force at the position of each wheel
        ApplyForceAtWheelPosition(forceDirection, 1f);
        ApplyForceAtWheelPosition(forceDirection, -1f);
        /* GameManager.Instance.FuelConsume();*/  //연료 소모에 따라 여러가지를 갱신
    }

    private void FixedUpdate()
    {
        if (manager.enableCarPhysics)
        {

            // transform.position = new Vector3(Mathf.Clamp(transform.position.x, StartPos.x, transform.position.x), transform.position.y);

            if (moveSpeed.Equals(0) || fuel <= 0)
            {   //버튼을 누르지 않거나 연료가 없을 경우 차를 멈춤
                frontTire.useMotor = false;
                backTire.useMotor = false;
            }
            else
            {
                frontTire.useMotor = true;
                backTire.useMotor = true;
                JointMotor2D motor = new JointMotor2D();
                motor.motorSpeed = moveSpeed;
                motor.maxMotorTorque = 95000;

                frontTire.motor = motor;
                backTire.motor = motor;
            }

            //게임 오버 시에 차량 속도 0으로
            //if(GameManager.Instance.isDie && moveStop) {
            //    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //}

            //움직이는 만큼 계속해서 연료 소비
            //fuel -= fuelConsumption * Mathf.Abs(movement) * Time.fixedDeltaTime;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ball"))
    //    {
    //        //if (gameObject.GetComponent<Collider2D>() != null)
    //        gameObject.GetComponent<Collider2D>().enabled = false;
    //        manager.Win();
    //    }
    //}

    //    private void OnTriggerEnter2D(Collider2D collision)
    //    {
    //        if (!manager.win && !manager.gameOver)
    //        {
    //            if (collision.gameObject.CompareTag("Ball"))
    //            {
    //                Debug.Log("On Trigger enter car controller");
    //               //TrophyAnimator.SetBool("TriggerFlag", true);
    //                gameObject.GetComponent<CircleCollider2D>().enabled = false;
    //                manager.Win();
    //                Vector3 thisPos = this.transform.position;
    //                Vector3 thatPos = collision.transform.position;
    //                Vector3 midPoint = thisPos + (thatPos - thisPos) / 2;

    //                ParticleSystem particle = Instantiate(manager.winning, midPoint, Quaternion.identity) as ParticleSystem;
    //                ManageTestMode.Instance.OnWin();
    //                particle.Play();

    //#if UNITY_5_5_OR_NEWER
    //                Destroy(particle.gameObject, particle.main.startLifetimeMultiplier);
    //#else
    //                Destroy(particle.gameObject, particle.startLifetime);
    //#endif

    //            }
    //        }
    //    }
    private float CalculateSlopeForce()
    {
        RaycastHit2D hit = Physics2D.Raycast(carRigidbody.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            // Calculate the slope angle
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            // Calculate the slope force using the sine of the angle
            float slopeForce = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * carRigidbody.mass * Physics2D.gravity.magnitude;

            return slopeForce;
        }

        return 0f;
    }

    private void ApplyForceAtWheelPosition(Vector2 forceDirection, float offset)
    {
        // Offset represents the distance from the car center to the wheel
        Vector2 wheelPosition = carRigidbody.position + offset * Vector2.right;

        // Apply force at the wheel position
        carRigidbody.AddForceAtPosition(forceDirection, wheelPosition);
    }

    void PlaySoundRepeatedly()
    {
        StartCoroutine(PlaySoundCoroutine());
    }

    IEnumerator PlaySoundCoroutine()
    {
        if (!manager.WinPanel.activeInHierarchy)
        {
            while (!manager.win || !manager.gameOver)
            {
                // Play the sound
                SoundManager.Instance.PlaySound(SoundManager.Instance.car, true);

                // Wait for the sound to finish playing before playing it again
                yield return new WaitForSeconds(SoundManager.Instance.car.clip.length);
            }
        }
        else
        {
            StopAllCoroutines();
            SoundManager.Instance.StopMusic();

        }

    }
    


}
