using UnityEngine;

public class SpinState : IState
{
    StateController player;

    public SpinState(StateController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Spin");
    }

    public void Execute()
    {
        //if (2�ʵ��� �������� ���� ������) {
        //    player.stateMachine.TransitionTo(player.stateMachine.idleState);
        //}
    }

    public void Exit()
    {
    }
}
