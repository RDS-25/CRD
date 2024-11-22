using UnityEngine;

public class RunState : IState
{
    StateController player;

    public RunState(StateController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Run");

    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
