public interface IState
{
    void OnEnterState();
    void StateUpdate();
    void StateFixedUpdate();
    void OnExitState();
}
