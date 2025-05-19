using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.Merge.Services.Hint
{
    public class HintsProvider
    {
        private readonly Image _hintImage;

        public HintsProvider(Image hintImage)
        {
            _hintImage = hintImage;
        }

        public void ShowMergeHint(Vector3 position)
        {
            _hintImage.transform.position = position;
            
            _hintImage.gameObject.SetActive(true);
            _hintImage.transform.localScale = Vector3.zero;
            _hintImage.transform.DOScale(1f, 0.25f);
        }

        public void HideMergeHint()
        {
            _hintImage.transform.DOComplete();
            _hintImage.gameObject.SetActive(false);
        }
    }
}