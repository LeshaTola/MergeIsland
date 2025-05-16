using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Zenject.SpaceFighter
{
    public class EnemyStateFollow : IEnemyState
    {
        private readonly EnemyRotationHandler _rotationHandler;
        private readonly EnemyCommonSettings _commonSettings;
        private readonly Settings _settings;
        private readonly EnemyTunables _tunables;
        private readonly EnemyStateManager _stateManager;
        private readonly EnemyView _view;
        private readonly PlayerFacade _player;

        private bool _strafeRight;
        private float _lastStrafeChangeTime;

        public EnemyStateFollow(
            PlayerFacade player,
            EnemyView view,
            EnemyStateManager stateManager,
            EnemyTunables tunables,
            Settings settings,
            EnemyCommonSettings commonSettings,
            EnemyRotationHandler rotationHandler)
        {
            _rotationHandler = rotationHandler;
            _commonSettings = commonSettings;
            _settings = settings;
            _tunables = tunables;
            _stateManager = stateManager;
            _view = view;
            _player = player;
        }

        public void EnterState()
        {
            _strafeRight = Random.Range(0, 1) == 0;
            _lastStrafeChangeTime = Time.realtimeSinceStartup;
        }

        public void ExitState()
        {
        }

        public void Update()
        {
            if (_player.IsDead)
            {
                _stateManager.ChangeState(EnemyStates.Idle);
                return;
            }

            var distanceToPlayer = (_player.Position - _view.Position).magnitude;

            // Always look towards the player
            _rotationHandler.DesiredLookDir = (_player.Position - _view.Position).normalized;

            // Strafe back and forth over the given interval
            // This helps avoiding being too easy a target
            if (Time.realtimeSinceStartup - _lastStrafeChangeTime > _settings.StrafeChangeInterval)
            {
                _lastStrafeChangeTime = Time.realtimeSinceStartup;
                _strafeRight = !_strafeRight;
            }

            if (distanceToPlayer < _commonSettings.AttackDistance)
            {
                _stateManager.ChangeState(EnemyStates.Attack);
            }
        }

        public void FixedUpdate()
        {
            MoveTowardsPlayer();
            Strafe();
        }

        private void Strafe()
        {
            // Strafe to avoid getting hit too easily
            if (_strafeRight)
            {
                _view.AddForce(_view.RightDir * _settings.StrafeMultiplier * _tunables.Speed);
            }
            else
            {
                _view.AddForce(-_view.RightDir * _settings.StrafeMultiplier * _tunables.Speed);
            }
        }

        private void MoveTowardsPlayer()
        {
            var playerDir = (_player.Position - _view.Position).normalized;

            _view.AddForce(playerDir * _tunables.Speed);
        }

        [Serializable]
        public class Settings
        {
            public float StrafeMultiplier;
            public float StrafeChangeInterval;
            public float TeleportNewDistance;
        }
    }
}