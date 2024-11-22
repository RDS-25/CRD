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
        //if (2초동안 돌았으면 많이 돌았지) {
        //    player.stateMachine.TransitionTo(player.stateMachine.idleState);
        //}
    }

    public void Exit()
    {
    }
}
