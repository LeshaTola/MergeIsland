using App.Scripts.Features.LevelSystem.Services;
using App.Scripts.Modules.Screens;

namespace App.Scripts.Features.LevelSystem.View
{
    public class ExperiencePresenter : GameScreenPresenter
    {
        private readonly ExperienceService _service;
        private readonly ExperienceView _view;

        public ExperiencePresenter(ExperienceService service, ExperienceView view) : base(view)
        {
            _service = service;
            _view = view;

            _service.OnExperienceChanged += OnExperienceChanged;

            _view.SetExperience(_service.CurrentExperience, _service.ExperienceToNextLevel);
        }

        private void OnExperienceChanged(int current, int max)
        {
            _view.SetExperience(current, max);
        }

        public void Dispose()
        {
            _service.OnExperienceChanged -= OnExperienceChanged;
        }
    }
}