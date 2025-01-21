namespace ServiceLocator.Main
{
    public interface IGameState
    {
        public GameController Owner { get; set; }
        public void OnStateEnter();
        public void Update();
        public void FixedUpdate();
        public void OnStateExit();
    }
}