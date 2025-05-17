using System;
using App.Scripts.Features.Merge.Elements;

namespace App.Scripts.Features.Merge.Services.SelectionProviders
{
    public class SelectionProvider
    {
        public event Action<Slot> OnSlotSelected;

        private Slot _selected;

        public void Select(Slot slot)
        {
            SelectWithoutNotification(slot);
            OnSlotSelected?.Invoke(slot);
        }

        public void SelectWithoutNotification(Slot slot)
        {
            ClearSelection();
            _selected = slot;
            _selected.SetSelected(true);
        }

        public void ClearSelection()
        {
            if (_selected == null)
            {
                return;
            }

            _selected.SetSelected(false);
            _selected = null;
        }
    }
}