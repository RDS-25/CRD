using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI roundCountText;
    [SerializeField] TextMeshProUGUI EnemyCountText;
    [SerializeField] TextMeshProUGUI ResultText;
    [SerializeField] private GameObject Result;
    public int roundCount;
    public int LastRound =10;
    
    float elapsedTime; //시간 재기
    [SerializeField] float reamingTime; //남은 시간
    private float cooldownTime = 1.0f; // 1초 간격
    private float cooldownTimer;
    [SerializeField] private Transform wispPos;
    public GameObject wisp;


    // Update is called once per frame
    private void Start()
    {
        cooldownTimer = cooldownTime;
        roundCountText.text = roundCount.ToString()+"Round";
    }


    void Update()
    {
        EnemyCountText.text = ObjectPool.Instance.showEnemyCount().ToString();
        if (ObjectPool.Instance.showEnemyCount() > 30)
        {
            ObjectPool.Instance.GameOver();
            ResultText.text = "Game Over";
            Result.SetActive(true);
        }
        //간단한 게임 패배 구문 적이 30명이 넘었으면 실행

        if (roundCount > LastRound)
        {
            ObjectPool.Instance.GameClear();
            ResultText.text = "Game Clear";
            Result.SetActive(true);
        }
        // deltaTime을 사용하여 시간 경과 추적
        cooldownTimer -= Time.deltaTime;
        if (reamingTime < 0)
        {
            roundCount += 1;
            var a =ObjectPool.Instance.GetObjectUnit(wisp);
            a.transform.position = wispPos.position;
            roundCountText.text = roundCount.ToString()+"Round";
            reamingTime = 20;
            RoundCounter();
        }
        else if (reamingTime > 0)
        {
            RoundCounter();
        }
        
        if (roundCount > 0)
        {
            if (cooldownTimer <= 0)
            {
                // 매초 objectPool에서 객체 가져오기
                // objectPool.GetObject(roundCount);
                var a=  ObjectPool.Instance.GetObject(roundCount);

                // 타이머 재설정
                cooldownTimer = cooldownTime;
            }
            
        }
    }

    

    void RoundCounter()
    {
        reamingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(reamingTime / 60);
        int seconds = Mathf.FloorToInt(reamingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
   
}