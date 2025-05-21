using System;
using App.Scripts.Modules.StateMachine.States.General;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Features.StateMachines.States
{
    public class GlobalInitialState : State
    {
        private bool _isValid = true;

        public Type NextState { get; set; }


        public override async UniTask Enter()
        {
            if (!_isValid)
            {
                await StateMachine.ChangeState(NextState);
                return;
            }

            SetTargetFPS();
            // YG2.StickyAdActivity(YG2.saves.IsCanShowAd);

            _isValid = false;
            await StateMachine.ChangeState(NextState);
        }


        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }

        private static void SetTargetFPS()
        {
            Application.targetFrameRate = 60;
        }
    }
}