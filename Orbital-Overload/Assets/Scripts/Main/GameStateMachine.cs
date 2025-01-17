namespace ServiceLocator.Main
{
    public class GameStateMachine : GameGenericStateMachine<GameController>
    {
        public GameStateMachine(GameController _owner) : base(_owner)
        {
            owner = _owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            GameStates.Add(GameState.Game_Start, new GameStartState<GameController>(this));
            GameStates.Add(GameState.Game_Menu, new GameMenuState<GameController>(this));
            GameStates.Add(GameState.Game_Play, new GamePlayState<GameController>(this));
            GameStates.Add(GameState.Game_Pause, new GamePauseState<GameController>(this));
            GameStates.Add(GameState.Game_Restart, new GameRestartState<GameController>(this));
            GameStates.Add(GameState.Game_Over, new GameOverState<GameController>(this));
        }
    }
}