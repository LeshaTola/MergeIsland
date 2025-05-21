using System;
using App.Scripts.Features.Merge.Elements.Slots;

namespace App.Scripts.Features.Merge.Services.Selection
{
    public class SelectionProvider
    {
        public event Action<Slot> OnSlotSelected;
        public event Action OnSelectionCleared;

        private Slot _selected;

        public void Select(Slot slot)
        {
            SelectWithoutNotification(slot);
            OnSlotSelected?.Invoke(slot);
        }

        public void ClearSelection()
        {
            ClearSelectionWithoutNotification();
            OnSelectionCleared?.Invoke();
        }

        public void SelectWithoutNotification(Slot slot)
        {
            ClearSelectionWithoutNotification();
            _selected = slot;
            _selected.SetSelected(true);
        }

        public void ClearSelectionWithoutNotification()
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