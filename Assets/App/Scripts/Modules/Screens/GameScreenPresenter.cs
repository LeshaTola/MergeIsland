using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Modules.Screens
{
    public abstract class GameScreenPresenter: IInitializable, ICleanupable
    {
        public GameScreen GameScreen { get; }

        protected GameScreenPresenter(GameScreen gameScreen)
        {
            GameScreen = gameScreen;
        }

        public virtual void Initialize()
        {
            GameScreen.Initialize();
        }

        public virtual void Cleanup()
        {
            GameScreen.Cleanup();
        }

        public virtual void Default()
        {
            GameScreen.Default();
        }

        public virtual async UniTask Show()
        {
            await GameScreen.Show();
        }

        public virtual async UniTask Hide()
        {
            await GameScreen.Hide();
        }
    }
}