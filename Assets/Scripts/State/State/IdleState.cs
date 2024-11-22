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
        player.GetComponent<Animator>().SetTrigger("Idle");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
