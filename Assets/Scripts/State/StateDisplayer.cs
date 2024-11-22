using TMPro;
using UnityEngine;

public class StateDisplayer : MonoBehaviour
{

    public TextMeshPro stateLabel;
    StateController player;

    private void Awake()
    {
        player = GetComponent<StateController>();
    }

    private void OnEnable()
    {
        player.stateMachine.stateChanged += OnStateChanged; 
    }

    private void OnDisable()
    {
        player.stateMachine.stateChanged -= OnStateChanged;
    }

    void OnStateChanged(IState state)
    {
        stateLabel.text = state.GetType().Name;
    }
}
