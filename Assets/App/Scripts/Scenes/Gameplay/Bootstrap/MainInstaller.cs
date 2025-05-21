using System.Collections.Generic;
using App.Scripts.Features.GameResources.Coins.Providers;
using App.Scripts.Features.GameResources.Energy.Configs;
using App.Scripts.Features.GameResources.Energy.Providers;
using App.Scripts.Features.GameResources.Energy.Saves;
using App.Scripts.Features.GameResources.Energy.Saves.Keys;
using App.Scripts.Features.GameResources.Gems.Providers;
using App.Scripts.Features.GameResources.Providers;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements.Filler;
using App.Scripts.Features.Merge.Elements.Items;
using App.Scripts.Features.Merge.Elements.Slots;
using App.Scripts.Features.Merge.Factory;
using App.Scripts.Features.Merge.Services;
using App.Scripts.Features.Merge.Services.Hand;
using App.Scripts.Features.Merge.Services.Hint;
using App.Scripts.Features.Merge.Services.Selection;
using App.Scripts.Modules.ObjectPool.MonoObjectPools;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Grid = App.Scripts.Features.Merge.Elements.Grid;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private CatalogsDatabase _catalogsDatabase;

        [Header("Energy")]
        [SerializeField] private EnergyConfig _energyConfig;

        [Header("Grid")]
        [SerializeField] private List<Slot> _slots;

        [SerializeField] private GridFillConfig _gridFillConfig;

        [SerializeField] private Transform _overlayContainer;
        [SerializeField] private Image _hintImage;

        [Header("Pools")]
        [SerializeField] private Item _itemTemplate;

        [SerializeField] private Transform _itemsContainer;

        public override void InstallBindings()
        {
            BindCycleServices();

            BindItemsPool();

            Container.Bind<MergeResolver>().AsSingle().WithArguments(_catalogsDatabase);
            Container.Bind<SelectionProvider>().AsSingle();
            Container.Bind<HandProvider>().AsSingle();
            Container.Bind<HintsProvider>().AsSingle().WithArguments(_hintImage);
            BindItemsFactories();
            Container.BindInterfacesAndSelfTo<Grid>().AsSingle().WithArguments(_slots);
            Container.BindInterfacesAndSelfTo<GridFiller>().AsSingle().WithArguments(_gridFillConfig);

            BindResources();
        }

        private void BindResources()
        {
            BindEnergy();
            Container.Bind<CoinsResourceProvider>().AsSingle();
            Container.Bind<GemsResourceProvider>().AsSingle();
            Container.Bind<ResourcesProvider>().AsSingle();
            
        }

        private void BindEnergy()
        {
            BindEnergyDataProvider();
            Container.BindInterfacesAndSelfTo<EnergyProvider>().AsSingle().WithArguments(_energyConfig);
        }

        private void BindEnergyDataProvider()
        {
            Container
                .Bind<IDataProvider<EnergyData>>()
                .To<DataProvider<EnergyData>>()
                .AsSingle()
                .WithArguments(EnergyDataKey.KEY);
        }

        private void BindItemsFactories()
        {
            Container.Bind<ItemSystemActionFactory>().AsSingle();
            Container.Bind<ItemSystemsFactory>().AsSingle();
            Container.Bind<ItemConfigsFactory>().AsSingle();
            Container.Bind<ItemFactory>().AsSingle().WithArguments(_overlayContainer);
        }

        private void BindItemsPool()
        {
            Container.Bind<IPool<Item>>()
                .To<MonoObjectPool<Item>>()
                .AsSingle()
                .WithArguments(_itemTemplate, 63, _itemsContainer);
        }

        private void BindCycleServices()
        {
            Container.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            Container.Bind<IInitializeService>().To<InitializeService>().AsSingle();
            Container.Bind<ICleanupService>().To<CleanupService>().AsSingle();
        }
    }
}