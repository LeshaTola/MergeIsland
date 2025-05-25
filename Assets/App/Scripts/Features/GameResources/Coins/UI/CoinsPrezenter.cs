using App.Scripts.Features.GameResources.Coins.Providers;
using App.Scripts.Features.GameResources.UI;
using App.Scripts.Modules.Screens;

namespace App.Scripts.Features.GameResources.Coins.UI
{
    public class CoinsPresenter : GameScreenPresenter
    {
        private readonly CoinsResourceProvider _resource;
        private readonly ResourceView _view;

        public CoinsPresenter(CoinsResourceProvider resource, ResourceView view) : base(view)
        {
            _resource = resource;
            _view = view;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _resource.OnValueChanged += _view.Setup;
            _view.Setup(_resource.Value);
        }

        public override void Cleanup()
        {
            _resource.OnValueChanged -= _view.Setup;
        }
    }
}