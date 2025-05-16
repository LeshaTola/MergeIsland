using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace App.Scripts.Features.Bootstrap
{
    public class SceneLocalizationService : SerializedMonoBehaviour
    {
        [SerializeField] private List<TMPLocalizer> _tmProLocalizers;

        private ILocalizationSystem _localizationSystem;

        [Inject]
        public void Construct(ILocalizationSystem localizationSystem)
        {
            _localizationSystem = localizationSystem;
            foreach (var localizer in _tmProLocalizers)
            {
                localizer.Initialize(localizationSystem);
                localizer.Translate();
            }
        }

        [Button] [PropertyOrder(-1)]
        private void FindAllLocalizers()
        {
            TMPLocalizer[] sceneLocalizers = FindObjectsOfType<TMPLocalizer>(true);
            _tmProLocalizers.Clear();
            _tmProLocalizers.AddRange(sceneLocalizers);
        }
    }
}