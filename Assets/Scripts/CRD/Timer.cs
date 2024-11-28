using System;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI roundCountText;
    public int roundCount;
    float elapsedTime; //시간 재기
    [SerializeField] float reamingTime; //남은 시간
    [SerializeField] ObjectPool objectPool;
    private float cooldownTime = 1.0f; // 1초 간격
    private float cooldownTimer;


    // Update is called once per frame
    private void Start()
    {
        cooldownTimer = cooldownTime;
    }


    void Update()
    {
        // deltaTime을 사용하여 시간 경과 추적
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            // 매초 objectPool에서 객체 가져오기
            objectPool.GetObject(roundCount);

            // 타이머 재설정
            cooldownTimer = cooldownTime;
        }
       

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