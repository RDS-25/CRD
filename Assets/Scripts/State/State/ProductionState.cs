using UnityEngine;

public class ProductionState : IState
{
    StateController player;

    public ProductionState(StateController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Production");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
