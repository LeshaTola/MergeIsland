using App.Scripts.Features.SceneTransitions;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.States.General;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.States
{
    public class InitialState : State
    {
        private readonly string _cameraId;
        private readonly ISceneTransition _sceneTransition;
        private readonly IInitializeService _initializeService;

        public InitialState(IInitializeService initializeService,
            ISceneTransition sceneTransition)
        {
            _initializeService = initializeService;
            _sceneTransition = sceneTransition;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            _initializeService.Initialize();
            await StateMachine.ChangeState<MainState>();
        }

        public override UniTask Exit()
        {
            _sceneTransition.PlayOffAsync().Forget();
            return UniTask.CompletedTask;
        }
    }
}