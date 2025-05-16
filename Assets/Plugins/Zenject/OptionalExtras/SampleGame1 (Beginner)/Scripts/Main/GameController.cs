using System;
using ModestTree;
using UnityEngine;

namespace Zenject.Asteroids
{
    public enum GameStates
    {
        WaitingToStart,
        Playing,
        GameOver
    }

    public class GameController : IInitializable, ITickable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Ship _ship;
        private readonly AsteroidManager _asteroidSpawner;

        private GameStates _state = GameStates.WaitingToStart;
        private float _elapsedTime;

        public GameController(
            Ship ship, AsteroidManager asteroidSpawner,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _asteroidSpawner = asteroidSpawner;
            _ship = ship;
        }

        public float ElapsedTime => _elapsedTime;

        public GameStates State => _state;

        public void Initialize()
        {
            Physics.gravity = Vector3.zero;

            Cursor.visible = false;

            _signalBus.Subscribe<ShipCrashedSignal>(OnShipCrashed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ShipCrashedSignal>(OnShipCrashed);
        }

        public void Tick()
        {
            switch (_state)
            {
                case GameStates.WaitingToStart:
                {
                    UpdateStarting();
                    break;
                }
                case GameStates.Playing:
                {
                    UpdatePlaying();
                    break;
                }
                case GameStates.GameOver:
                {
                    UpdateGameOver();
                    break;
                }
                default:
                {
                    Assert.That(false);
                    break;
                }
            }
        }

        private void UpdateGameOver()
        {
            Assert.That(_state == GameStates.GameOver);

            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }

        private void OnShipCrashed()
        {
            Assert.That(_state == GameStates.Playing);
            _state = GameStates.GameOver;
            _asteroidSpawner.Stop();
        }

        private void UpdatePlaying()
        {
            Assert.That(_state == GameStates.Playing);
            _elapsedTime += Time.deltaTime;
        }

        private void UpdateStarting()
        {
            Assert.That(_state == GameStates.WaitingToStart);

            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            Assert.That(_state == GameStates.WaitingToStart || _state == GameStates.GameOver);

            _ship.Position = Vector3.zero;
            _elapsedTime = 0;
            _asteroidSpawner.Start();
            _ship.ChangeState(ShipStates.Moving);
            _state = GameStates.Playing;
        }
    }
}