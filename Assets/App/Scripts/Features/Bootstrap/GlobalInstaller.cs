using App.Scripts.Features.SceneTransitions;
using App.Scripts.Features.StateMachines.States;
using App.Scripts.Modules.Commands.Provider;
using App.Scripts.Modules.FileProvider;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Configs;
using App.Scripts.Modules.Localization.Data;
using App.Scripts.Modules.Localization.Keys;
using App.Scripts.Modules.Localization.Parsers;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.Sounds.Providers;
using App.Scripts.Modules.StateMachine.States.General;
using App.Scripts.Modules.TasksSystem.Configs;
using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace App.Scripts.Features.Bootstrap
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private LocalizationDatabase _localizationDatabase;
        [SerializeField] private string _language;

        [SerializeField] private SceneTransition _sceneTransition;

        [SerializeField] private ConnectionProvider _connectionProvider;

        [Header("Audio")]
        [SerializeField] private SoundProvider _soundProvider;

        [SerializeField] private AudioMixer _audioMixer;

        public override void InstallBindings()
        {
            BindGlobalInitialState();
            BindStorage();
            BindFileProvider();

            BindParser();
            BindLocalizationDataProvider();
            BindLocalizationSystem();

            Container.Bind<ISceneTransition>().FromInstance(_sceneTransition);
            Container.Bind<ConnectionProvider>().FromInstance(_connectionProvider);

            Container.Bind<ICommandsProvider>().To<CommandsProvider>().AsSingle();
        }

        private void BindLocalizationSystem()
        {
            var lang = _language;

            Container.Bind<LocalizationDatabase>().FromInstance(_localizationDatabase);
            Container
                .Bind<ILocalizationSystem>()
                .To<LocalizationSystem>()
                .AsSingle()
                .WithArguments(lang);
        }

        private void BindLocalizationDataProvider()
        {
            Container
                .Bind<IDataProvider<LocalizationSavesData>>()
                .To<DataProvider<LocalizationSavesData>>()
                .AsSingle()
                .WithArguments(LocalizationDataKey.KEY);
        }

        private void BindParser()
        {
            Container.Bind<IParser>().To<CSVParser>().AsSingle();
        }

        private void BindFileProvider()
        {
            Container.Bind<IFileProvider>().To<ResourcesFileProvider>().AsSingle();
        }

        private void BindStorage()
        {
            Container.Bind<IStorage>().To<PlayerPrefsStorage>().AsSingle();
        }

        private void BindGlobalInitialState()
        {
            Container
                .Bind<State>()
                .To<GlobalInitialState>()
                .AsSingle();
        }
    }
}