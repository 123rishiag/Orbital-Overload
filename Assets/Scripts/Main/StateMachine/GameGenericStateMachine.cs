using System.Collections.Generic;
using System.Linq;

namespace ServiceLocator.Main
{
    // Creating a generic state Machine, to handle Sub Game Controllers if occurs in future
    public class GameGenericStateMachine<T> where T : GameController
    {
        protected T owner;
        protected IGameState currentState;
        protected Dictionary<GameState, IGameState> GameStates = new Dictionary<GameState, IGameState>();

        public GameGenericStateMachine(T _owner) => owner = _owner;

        public void Update() => currentState?.Update();
        public void FixedUpdate() => currentState?.FixedUpdate();

        public GameState GetCurrentState()
        {
            return GameStates.Keys.ToList().Find(key => GameStates[key] == currentState);
        }

        protected void ChangeState(IGameState _newState)
        {
            currentState?.OnStateExit();
            currentState = _newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(GameState _newState) => ChangeState(GameStates[_newState]);

        protected void SetOwner()
        {
            foreach (IGameState _gameState in GameStates.Values)
            {
                _gameState.Owner = owner;
            }
        }
    }
}