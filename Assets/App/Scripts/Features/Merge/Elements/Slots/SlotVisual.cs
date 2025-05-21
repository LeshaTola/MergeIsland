using App.Scripts.Features.Merge.Services.Hint;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace App.Scripts.Features.Merge.Elements.Slots
{
    public class SlotVisual : MonoBehaviour
    {
        [SerializeField] private Image _selector;

        private HintsProvider _hintsProvider;

        [Inject]
        public void Construct(HintsProvider hintsProvider)
        {
            _hintsProvider = hintsProvider;
        }

        public void ShowSelector(bool show)
        {
            _selector.gameObject.SetActive(show);
        }

        public void ShowMergeHint()
        {
            _hintsProvider.ShowMergeHint(transform.position);
        }

        public void HideMergeHint()
        {
            _hintsProvider.HideMergeHint();
        }
    }
}