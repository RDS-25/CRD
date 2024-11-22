using UnityEngine;

public class AttackState : IState
{
    StateController player;

    public AttackState(StateController player)
    {
        this.player = player;
    }


    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Attack");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
