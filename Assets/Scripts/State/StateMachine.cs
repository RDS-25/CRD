using System;
using UnityEngine;

public class StateMachine
{
    public IState CurrentState {  get; private set; }
    StateController player;

    public IdleState idleState;
    public RunState runState;
    public AttackState attackState;
    public DeadState deadState;
    public SpinState spinState;
    public ProductionState productionState;

    public event Action<IState> stateChanged;   
    // Action<IState> : �Ķ���ͷ� IState Ÿ���� �ǳ��ִ� void �Լ���� ��

    public StateMachine(StateController player)
    {
        this.player = player;

        idleState = new IdleState(player);
        runState = new RunState(player);
        attackState = new AttackState(player);
        deadState = new DeadState(player);
        spinState = new SpinState(player);
        productionState = new ProductionState(player);
    }

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();

        stateChanged?.Invoke(state);
    }

    public void TransitionTo(IState nextState)
    {
        IState prevState = CurrentState;
        CurrentState.Exit();
        CurrentState= nextState;
        CurrentState.Enter();
        if (prevState!=nextState) {
            stateChanged?.Invoke(CurrentState);
        }
    }

    public void Execute()
    {
        CurrentState.Execute();
    }

}
