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
        var a = player.GetComponent<Unit>().attackSpeed;
        player.GetComponent<Animator>().SetTrigger("Attack");
        player.GetComponent<Animator>().SetFloat("AttackSpeed",a);
        
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
