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
        if ( player.GetComponent<Animator>()==null)
        {
            return;
        }
        player.GetComponent<Animator>().SetTrigger("Run");

    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
