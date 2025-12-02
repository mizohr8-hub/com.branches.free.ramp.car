using UnityEngine;
using UnityEngine.UI;

public class Timer1 : MonoBehaviour
{
    public Text timerText;
    public float totalTime = 300f; // Total time in seconds (adjust as needed)
    private float currentTime;

    void Start()
    {
        currentTime = totalTime;

    }
    bool Once = true;
    void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {

            //Perform Things on time end here          
            ////  Debug.Log("Time's up!");
            if (OpenWorldManager.Instance)
            {
                OpenWorldManager.Instance.LevelFail();
            }
        
        }
    }

    void UpdateTimerDisplay()
    {
        // Format the time in minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the UI text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}