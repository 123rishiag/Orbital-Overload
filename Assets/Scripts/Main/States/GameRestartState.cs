namespace ServiceLocator.Main
{
    public class GameRestartState<T> : IGameState where T : GameController
    {
        public GameController Owner { get; set; }
        private GameGenericStateMachine<T> stateMachine;

        public GameRestartState(GameGenericStateMachine<T> _stateMachine) => stateMachine = _stateMachine;

        public void OnStateEnter()
        {
            Owner.Reset();
            stateMachine.ChangeState(GameState.Game_Play);
        }
        public void Update() { }
        public void FixedUpdate() { }
        public void OnStateExit() { }
    }
}