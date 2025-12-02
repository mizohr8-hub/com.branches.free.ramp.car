using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrajectoryNew2 : MonoBehaviour {


	public float maxStretch = 3f;
	// TrajectoryPoint and Ball will be instantiated
    public GameObject TrajectoryPointPrefeb;
    // public GameObject BallPrefb;
    
    // private GameObject ball;
    private bool isPressed;
    [HideInInspector]
   public bool isBallThrown;
    public float power = 15;
    public int numOfTrajectoryPoints = 10;
    private List<GameObject> trajectoryPoints;
	Vector2 desiredPos;
    PlayerController1New pc;
    Vector2 initialPos;
    bool maximumstretch;
    //---------------------------------------    
    void Start ()
    {
        trajectoryPoints = new List<GameObject>();
        isPressed = isBallThrown =false;
        pc = this.GetComponent<PlayerController1New>();
        initialPos = transform.position;
//   TrajectoryPoints are instatiated
        InstantiateTrajectoryPoints();
        
    }

   public void InstantiateTrajectoryPoints()
    {
        for(int i=0;i<numOfTrajectoryPoints;i++)
        {
            GameObject dot= (GameObject) Instantiate(TrajectoryPointPrefeb);
            dot.GetComponent<Renderer>().enabled = false;
            trajectoryPoints.Insert(i,dot);
            dot.transform.SetParent(this.transform);
        }
    }
    void Update ()
    {
        if(isBallThrown)
            return;
        /* if(Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            if(!ball)
                createBall();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            if(!isBallThrown)
            {
                throwBall();
            }
        } */
		if(isPressed)
		{
			Vector2 pos = initialPos;
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(Vector2.Distance(mousePos, pos) > maxStretch)
			{
				desiredPos = pos + (pos-mousePos).normalized * maxStretch;
                maximumstretch = true;
                
				// Debug.Log("OutOfBounds");
			}
			else
			{
				desiredPos = pos + (pos - mousePos);
                maximumstretch = false;
			}            
		}
            
			

    // when mouse button is pressed, cannon is rotated as per mouse movement and projectile trajectory path is displayed.
        if(isPressed)
        {
            Vector3 vel = GetForceFrom(initialPos,desiredPos);
            /* float angle = Mathf.Atan2(vel.y,vel.x)* Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0,0,angle); */
            setTrajectoryPoints(transform.position, vel/this.GetComponent<Rigidbody2D>().mass);
        }
    }
    //---------------------------------------    
    // Following method creates new ball
    //---------------------------------------    
    /* private void createBall()
    {
        ball = (GameObject) Instantiate(BallPrefb);
        Vector3 pos = transform.position;
        pos.z=1;
        ball.transform.position = pos;
        ball.SetActive(false);
    } */
    //---------------------------------------    
// Following method gives force to the ball
    //---------------------------------------    
    private void throwBall()
    {
        // ball.SetActive(true);    
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
        this.GetComponent<Rigidbody2D>().AddForce(GetForceFrom(initialPos,/* Camera.main.ScreenToWorldPoint */(desiredPos)),ForceMode2D.Impulse);
        isBallThrown = true;
        deleteTrajectoryPoints();   
    }
    //---------------------------------------    
// Following method returns force by calculating distance between given two points
    //---------------------------------------    
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y))*power;        
    }
    //---------------------------------------    
    // Following method displays projectile trajectory path. It takes two arguments, start position of object(ball) and initial velocity of object(ball).
    //---------------------------------------    
    void setTrajectoryPoints(Vector3 pStartPosition , Vector3 pVelocity )
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg*(Mathf.Atan2(pVelocity.y , pVelocity.x));
        float fTime = 0;
        
        fTime += 0.01f;
        for (int i = 0 ; i < numOfTrajectoryPoints ; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx , pStartPosition.y + dy ,2);
            trajectoryPoints[i].transform.position = pos;
            // trajectoryPoints[i].transform.SetParent(this.transform);
            trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude)*fTime,pVelocity.x)*Mathf.Rad2Deg);
            fTime += 0.01f;
        }
    }
    void deleteTrajectoryPoints()
    {
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            Destroy(trajectoryPoints[i]);
            trajectoryPoints[i].GetComponent<Renderer>().enabled = false;
        }
    }

	void OnMouseDown()
	{
		isPressed = true;
            /* if(!ball)
                createBall(); */
	}

	
	void OnMouseUp()
	{       
		isPressed = false;
        deleteTrajectoryPoints();
        
        if(!isBallThrown && pc.minStretchAchieved)
        {
            throwBall();
        }
        else
        {
            InstantiateTrajectoryPoints();
        }
	}
}