using ServiceLocator.Sound;

namespace ServiceLocator.Main
{
    public class GameOverState<T> : IGameState where T : GameController
    {
        public GameController Owner { get; set; }
        private GameGenericStateMachine<T> stateMachine;

        public GameOverState(GameGenericStateMachine<T> _stateMachine) => stateMachine = _stateMachine;

        public void OnStateEnter()
        {
            Owner.GetUIService().GetUIController().GetUIView().gameOverMenuPanel.SetActive(true); // Show Game Over Menu
            Owner.GetSoundService().PlaySound(SoundType.GameOver);
        }
        public void Update() { }
        public void FixedUpdate() { }
        public void OnStateExit()
        {
            Owner.GetUIService().GetUIController().GetUIView().gameOverMenuPanel.SetActive(false); // Hide Game Over Menu
        }
    }
}