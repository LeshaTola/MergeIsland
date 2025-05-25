using App.Scripts.Features.GameResources.Gems.Providers;
using App.Scripts.Features.GameResources.UI;
using App.Scripts.Modules.Screens;

namespace App.Scripts.Features.GameResources.Gems.UI
{
    public class GemsPresenter : GameScreenPresenter
    {
        private readonly GemsResourceProvider _resource;
        private readonly ResourceView _view;

        public GemsPresenter(GemsResourceProvider resource, ResourceView view):base(view)
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