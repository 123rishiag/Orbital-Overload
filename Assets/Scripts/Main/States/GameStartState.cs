namespace ServiceLocator.Main
{
    public class GameStartState<T> : IGameState where T : GameController
    {
        public GameController Owner { get; set; }
        private GameGenericStateMachine<T> stateMachine;

        public GameStartState(GameGenericStateMachine<T> _stateMachine) => stateMachine = _stateMachine;

        public void OnStateEnter()
        {
            stateMachine.ChangeState(GameState.Game_Menu);
        }
        public void Update() { }
        public void FixedUpdate() { }
        public void LateUpdate() { }
        public void OnStateExit() { }
    }
}