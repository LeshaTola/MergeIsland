using App.Scripts.Features.Energy.UI;
using App.Scripts.Features.Energy.UI.Presenter;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private EnergyView _energyView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnergyPresenter>().AsSingle().WithArguments(_energyView);
        }
    }
}