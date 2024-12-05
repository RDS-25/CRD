using UnityEngine;

public class IdleState : IState
{
    StateController player;

    public IdleState(StateController player)
    {
        this.player = player;
    }


    public void Enter()
    {
        if (player.GetComponent<Animator>()==null)
        {
            return;
        }
        player.GetComponent<Animator>().SetTrigger("Idle");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
