using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.StateMachine.States.General;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.States
{
    public class MainState : State
    {
        private readonly IUpdateService _updateService;

        public MainState(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        public override async UniTask Enter()
        {
        }

        public override async UniTask Exit()
        {
        }

        public override UniTask Update()
        {
            _updateService.Update();
            return UniTask.CompletedTask;
        }
    }
}