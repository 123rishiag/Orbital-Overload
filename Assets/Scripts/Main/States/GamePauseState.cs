using ServiceLocator.Sound;

namespace ServiceLocator.Main
{
    public class GamePauseState<T> : IGameState where T : GameController
    {
        public GameController Owner { get; set; }
        private GameGenericStateMachine<T> stateMachine;

        public GamePauseState(GameGenericStateMachine<T> _stateMachine) => stateMachine = _stateMachine;

        public void OnStateEnter()
        {
            Owner.GetUIService().GetUIController().GetUIView().pauseMenuPanel.SetActive(true); // Show Pause Menu
            Owner.GetSoundService().PlaySoundEffect(SoundType.GamePause);
        }
        public void Update() { }
        public void FixedUpdate() { }
        public void LateUpdate() { }
        public void OnStateExit()
        {
            Owner.GetUIService().GetUIController().GetUIView().pauseMenuPanel.SetActive(false); // Hide Pause Menu
        }
    }
}