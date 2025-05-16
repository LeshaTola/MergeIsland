using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class Player
    {
        private readonly Rigidbody _rigidBody;
        private readonly MeshRenderer _renderer;

        private float _health = 100.0f;

        public Player(
            Rigidbody rigidBody,
            MeshRenderer renderer)
        {
            _rigidBody = rigidBody;
            _renderer = renderer;
        }

        public MeshRenderer Renderer => _renderer;

        public bool IsDead { get; set; }

        public float Health => _health;

        public Vector3 LookDir => -_rigidBody.transform.right;

        public Quaternion Rotation
        {
            get => _rigidBody.rotation;
            set => _rigidBody.rotation = value;
        }

        public Vector3 Position
        {
            get => _rigidBody.position;
            set => _rigidBody.position = value;
        }

        public Vector3 Velocity => _rigidBody.velocity;

        public void TakeDamage(float healthLoss)
        {
            _health = Mathf.Max(0.0f, _health - healthLoss);
        }

        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force);
        }
    }
}