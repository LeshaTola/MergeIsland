using App.Scripts.Features.GameResources.Energy.UI;
using App.Scripts.Features.GameResources.Energy.UI.Presenter;
using App.Scripts.Features.Merge.Screens;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private EnergyView _energyView;
        [SerializeField] private InfoPanel _infoPanel;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnergyPresenter>().AsSingle().WithArguments(_energyView);
            Container.BindInterfacesAndSelfTo<InfoPanelPresenter>().AsSingle().WithArguments(_infoPanel);
        }
    }
}