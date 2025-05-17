using System.Collections.Generic;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements;
using App.Scripts.Features.Merge.Elements.Items;
using App.Scripts.Features.Merge.Factory;
using App.Scripts.Features.Merge.Services;
using App.Scripts.Features.Merge.Services.SelectionProviders;
using App.Scripts.Modules.ObjectPool.MonoObjectPools;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using UnityEngine;
using Zenject;
using Grid = App.Scripts.Features.Merge.Elements.Grid;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private CatalogsDatabase _catalogsDatabase;
        
        [Header("Grid")]
        [SerializeField] private List<Slot> _slots; 
        [SerializeField] private Transform _overlayContainer;
        
        [Header("Pools")]
        [SerializeField] private Item _itemTemplate;
        [SerializeField] private Transform _itemsContainer;

        public override void InstallBindings()
        {
            BindCycleServices();

            BindItemsPool();

            Container.Bind<MergeResolver>().AsSingle().WithArguments(_catalogsDatabase);
            Container.Bind<SelectionProvider>().AsSingle();
            BindItemsFactories();
            Container.BindInterfacesAndSelfTo<Grid>().AsSingle().WithArguments(_slots);
        }

        private void BindItemsFactories()
        {
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