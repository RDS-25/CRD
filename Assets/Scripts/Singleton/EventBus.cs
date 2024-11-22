using System;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalEvent
{
    AllDead,
    AllRun,
    AllIdle,
    AllSpin,
    AllAttack
}

public class EventBus : MonoBehaviour
{
    // EventBus는 굳이 싱글턴일 필요가 없음
    // 한 GameObject내에서의 통신을 위해 GameObject의 컴포넌트로 사용할 수도 있다.
    // 여기서 싱글턴으로 만든 이유는 그냥 싱글턴의 다양한 구현 방법을 보여주려고

    static bool IsApplicationQuitting = false;

    static EventBus instance;
    public static EventBus Instance
    {
        get
        {
            if (instance == null && !IsApplicationQuitting)
            {
                GameObject go = new GameObject("EventBus");
                instance= go.AddComponent<EventBus>();
                DontDestroyOnLoad(go);
            }

            return instance;
        }
    }
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private Dictionary<GlobalEvent, Action> eventDictionary = new Dictionary<GlobalEvent, Action>();

    public void Subscribe(GlobalEvent eventType, Action listener)
    {
        ////  1번구현
        //if (!eventDictionary.ContainsKey(eventType))
        //{
        //    eventDictionary[eventType] = null;
        //}
        //eventDictionary[eventType] += listener;

        // 2번 구현
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] += listener;
        }
        else
        {
            eventDictionary[eventType] = listener;
        }
    }

    public void Unsubscribe(GlobalEvent eventType,Action listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= listener;
            if (eventDictionary[eventType] == null)
            {
                eventDictionary.Remove(eventType);
            }
        }
    }

    public void Publish(GlobalEvent eventType)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType]?.Invoke();
        }
    }

    private void OnApplicationQuit()
    {
        IsApplicationQuitting= true;
    }

}
