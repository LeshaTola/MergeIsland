using App.Scripts.Features.Merge.Grid;
using App.Scripts.Modules.Factories.MonoFactories;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private Slot _slotTemplate;
        [SerializeField] private MergeItem _itemTemplate;

        public override void InstallBindings()
        {
            Container.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            Container.Bind<IInitializeService>().To<InitializeService>().AsSingle();
            Container.Bind<ICleanupService>().To<CleanupService>().AsSingle();

            BindSlotFactory();
            BindItemFactory();
        }

        private void BindItemFactory()
        {
            Container.Bind<Modules.Factories.IFactory<MergeItem>>()
                .To<MonoFactory<MergeItem>>()
                .AsSingle()
                .WithArguments(_itemTemplate);
        }

        private void BindSlotFactory()
        {
            Container
                .Bind<Modules.Factories.IFactory<Slot>>()
                .To<MonoFactory<Slot>>()
                .AsSingle()
                .WithArguments(_slotTemplate);
        }
    }
}