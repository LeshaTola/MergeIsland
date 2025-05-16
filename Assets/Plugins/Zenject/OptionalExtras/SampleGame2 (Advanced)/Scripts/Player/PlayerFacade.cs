using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class PlayerFacade : MonoBehaviour
    {
        private Player _model;
        private PlayerDamageHandler _hitHandler;

        [Inject]
        public void Construct(Player player, PlayerDamageHandler hitHandler)
        {
            _model = player;
            _hitHandler = hitHandler;
        }

        public bool IsDead => _model.IsDead;

        public Vector3 Position => _model.Position;

        public Quaternion Rotation => _model.Rotation;

        public void TakeDamage(Vector3 moveDirection)
        {
            _hitHandler.TakeDamage(moveDirection);
        }
    }
}