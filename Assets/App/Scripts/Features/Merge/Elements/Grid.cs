using System.Collections.Generic;
using System.Linq;
using App.Scripts.Features.Merge.Elements.Slots;
using App.Scripts.Features.Merge.Services;
using App.Scripts.Features.Merge.Services.Hand;
using UnityEngine;
using Zenject;

namespace App.Scripts.Features.Merge.Elements
{
    public class Grid : IInitializable
    {
        private readonly List<Slot> _slots;
        private readonly MergeResolver _mergeResolver;
        private readonly HandProvider _handProvider;

        public Grid(List<Slot> slots, MergeResolver mergeResolver, HandProvider handProvider)
        {
            _slots = slots;
            _mergeResolver = mergeResolver;
            _handProvider = handProvider;
        }

        public void Initialize()
        {
            foreach (var slot in _slots)
            {
                slot.Initialize(_mergeResolver, _handProvider);
            }
        }
        
        public Slot GetNearestUnusedSlot(Vector2 centerPosition)
        {
            Slot nearest = null;
            float minDistance = float.MaxValue;

            foreach (var slot in _slots)
            {
                if (slot.Item != null) continue;

                float distance = Vector2.Distance(slot.transform.position, centerPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = slot;
                }
            }

            return nearest;
        }
        
        public Slot GetUnusedSlot()
        {
            return _slots.FirstOrDefault(x => x.Item == null);
        }

        public Slot GetSlot(Vector2Int position)
        {
            return _slots[position.y * 9 + position.x];
        }
    }
}