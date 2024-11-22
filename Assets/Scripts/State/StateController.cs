using UnityEngine;

public class StateController : MonoBehaviour
{ 
    public StateMachine stateMachine;

    private void Awake() 
    {
        stateMachine = new StateMachine(this);
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe(GlobalEvent.AllDead, OnDead);
        EventBus.Instance.Subscribe(GlobalEvent.AllIdle, OnIdle);
        EventBus.Instance.Subscribe(GlobalEvent.AllRun, OnRun);
        EventBus.Instance.Subscribe(GlobalEvent.AllSpin, OnSpin);
        EventBus.Instance.Subscribe(GlobalEvent.AllAttack, OnAttack);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe(GlobalEvent.AllDead, OnDead);
        EventBus.Instance.Unsubscribe(GlobalEvent.AllIdle, OnIdle);
        EventBus.Instance.Unsubscribe(GlobalEvent.AllRun, OnRun);
        EventBus.Instance.Unsubscribe(GlobalEvent.AllSpin, OnSpin);
        EventBus.Instance.Unsubscribe(GlobalEvent.AllAttack, OnAttack);
    }

    void Start() 
    {
        stateMachine.Initialize(stateMachine.idleState);
    }

    void Update()
    {
        stateMachine.Execute();
    }

    public void OnIdle()
    {
        stateMachine.TransitionTo(stateMachine.idleState);
    }
    public void OnDead()
    {
        stateMachine.TransitionTo(stateMachine.deadState);
    }
    public void OnAttack()
    {
        stateMachine.TransitionTo(stateMachine.attackState);
    }
    public void OnRun()
    {
        stateMachine.TransitionTo(stateMachine.runState);
    }
    public void OnSpin()
    {
        stateMachine.TransitionTo(stateMachine.spinState);
    }

}
