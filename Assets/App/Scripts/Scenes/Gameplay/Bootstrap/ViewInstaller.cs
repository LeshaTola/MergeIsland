using App.Scripts.Features.GameResources.Coins.UI;
using App.Scripts.Features.GameResources.Energy.UI;
using App.Scripts.Features.GameResources.Energy.UI.Presenter;
using App.Scripts.Features.GameResources.Gems.UI;
using App.Scripts.Features.GameResources.UI;
using App.Scripts.Features.LevelSystem.View;
using App.Scripts.Features.Merge.Screens;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private InfoPanel _infoPanel;
        [Header("Resources")]
        [SerializeField] private EnergyView _energyView;
        [SerializeField] private ExperienceView _experienceView;
        [SerializeField] private ResourceView _gemsView;
        [SerializeField] private ResourceView _coinView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnergyPresenter>().AsSingle().WithArguments(_energyView);
            Container.BindInterfacesAndSelfTo<InfoPanelPresenter>().AsSingle().WithArguments(_infoPanel);
            
            Container.BindInterfacesAndSelfTo<GemsPresenter>().AsSingle().WithArguments(_gemsView);
            Container.BindInterfacesAndSelfTo<CoinsPresenter>().AsSingle().WithArguments(_coinView);
            Container.BindInterfacesAndSelfTo<ExperiencePresenter>().AsSingle().WithArguments(_experienceView);
        }
    }
}