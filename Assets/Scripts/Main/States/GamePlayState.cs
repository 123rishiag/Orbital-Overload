using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GamePlayState<T> : IGameState where T : GameController
    {
        public GameController Owner { get; set; }
        private GameGenericStateMachine<T> stateMachine;

        public GamePlayState(GameGenericStateMachine<T> _stateMachine) => stateMachine = _stateMachine;

        public void OnStateEnter()
        {
            Time.timeScale = 1f; // Resume the game
            // Camera should follow player
            Owner.GetCameraService().SetFollowTarget(
                Owner.GetActorService().GetPlayerActorController().GetActorView().GetTransform());
            Owner.GetSoundService().PlaySound(SoundType.GamePlay);
        }
        public void Update()
        {
            Owner.GetVFXService().Update();
            Owner.GetSoundService().Update();
            Owner.GetInputService().Update();
            Owner.GetSpawnService().Update();
            Owner.GetProjectileService().Update();
            Owner.GetPowerUpService().Update();
            Owner.GetActorService().Update();
            CheckGamePause();
            CheckGameOver();
        }
        public void FixedUpdate()
        {
            Owner.GetProjectileService().FixedUpdate();
            Owner.GetActorService().FixedUpdate();
        }
        public void OnStateExit()
        {
            Time.timeScale = 0f; // Stop the game
        }

        private void CheckGamePause()
        {
            if (Owner.GetInputService().IsEscapePressed)
            {
                stateMachine.ChangeState(GameState.Game_Pause);
            }
        }
        private void CheckGameOver()
        {
            if (!Owner.GetActorService().GetPlayerActorController().IsAlive())
            {
                stateMachine.ChangeState(GameState.Game_Over);
            }
        }
    }
}