using Unity.VisualScripting;
using UnityEngine;

public class MachineEnemy : MonoBehaviour
{
    [SerializeField]private State _stateBase;
    [SerializeField]private State _currentState;
    [SerializeField] private Enemy _character;

    private void OnEnable()
    {
        _currentState = _stateBase;
    }
    public void StateChange(State newState)
    {
        if (!_currentState.isContinued) 
        {
            _currentState = newState;

        }
    }
    public void UpdateCurrentState() 
    {
        _currentState.Entry();
        _currentState.Do();
        //Debug.Log("Recorremos el Do del current state: " + _currentState);
    }

    void Update()
    {
        UpdateCurrentState();
    }
}
