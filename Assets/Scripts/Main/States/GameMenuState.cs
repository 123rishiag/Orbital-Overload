namespace ServiceLocator.Main
{
    public class GameMenuState<T> : IGameState where T : GameController
    {
        public GameController Owner { get; set; }
        private GameGenericStateMachine<T> stateMachine;

        public GameMenuState(GameGenericStateMachine<T> _stateMachine) => stateMachine = _stateMachine;

        public void OnStateEnter()
        {
            Owner.GetUIService().GetUIController().GetUIView().mainMenuPanel.SetActive(true); // Show Main Menu
        }
        public void Update() { }
        public void FixedUpdate() { }
        public void LateUpdate() { }
        public void OnStateExit()
        {
            Owner.GetUIService().GetUIController().GetUIView().mainMenuPanel.SetActive(false); // Hide Main Menu
        }
    }
}