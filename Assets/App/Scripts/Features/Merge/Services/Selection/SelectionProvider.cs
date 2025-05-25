using System;
using App.Scripts.Features.Merge.Elements.Slots;

namespace App.Scripts.Features.Merge.Services.Selection
{
    public class SelectionProvider
    {
        public event Action<Slot> OnSlotSelected;
        public event Action OnSelectionCleared;

        public Slot Selected { get; private set; }

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
            Selected = slot;
            Selected.SetSelected(true);
        }

        public void ClearSelectionWithoutNotification()
        {
            if (Selected == null)
            {
                return;
            }

            Selected.SetSelected(false);
            Selected = null;
        }
    }
}