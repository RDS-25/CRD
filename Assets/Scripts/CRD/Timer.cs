using System;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI roundCountText;
    [SerializeField] int roundCount;
    float elapsedTime; //시간 재기
    [SerializeField] float reamingTime; //남은 시간 

    // Update is called once per frame
    private void Start()
    {
        roundCountText.text = roundCount.ToString()+"Round";
    }

    void Update()
    {
        if (reamingTime < 0)
        {
            roundCount += 1;
            roundCountText.text = roundCount.ToString()+"Round";
            reamingTime = 10;
            RoundCounter();
        }
        else if (reamingTime > 0)
        {
            RoundCounter();
        }
    }

    void RoundCounter()
    {
        reamingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(reamingTime / 60);
        int seconds = Mathf.FloorToInt(reamingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
}