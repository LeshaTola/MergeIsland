using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Items
{
    public class ItemVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _emitterActive;
        [SerializeField] private GameObject _emitterReload;
        [SerializeField] private GameObject _inWeb;

        public void WebSetActive(bool active)
        {
            _inWeb.SetActive(active);
        }

        public void EmitterActiveSetActive(bool active)
        {
            _emitterActive.SetActive(active);
        }

        public void EmitterReloadSetActive(bool active)
        {
            _emitterReload.SetActive(active);
        }

        public void _emitterReloadSetValue(float value)
        {
        }
    }
}