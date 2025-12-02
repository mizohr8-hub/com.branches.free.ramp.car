using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTrigger : MonoBehaviour
{
    public float Delay;

    private void Awake()
    {
        this.gameObject.SetActive(false);
        Invoke(nameof(TurnItON), Delay);
    }

    public void TurnItON()
    {
        this.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
