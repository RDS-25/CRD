using UnityEngine;

public class DeadState : IState
{
    StateController player;

    public DeadState(StateController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Dead");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
