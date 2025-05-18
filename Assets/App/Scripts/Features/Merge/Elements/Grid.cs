using System.Collections.Generic;
using System.Linq;
using App.Scripts.Features.Merge.Services;
using UnityEngine;
using Zenject;

namespace App.Scripts.Features.Merge.Elements
{
    public class Grid : IInitializable
    {
        private readonly List<Slot> _slots;
        private readonly MergeResolver _mergeResolver;

        public Grid(List<Slot> slots, MergeResolver mergeResolver)
        {
            _slots = slots;
            _mergeResolver = mergeResolver;
        }

        public void Initialize()
        {
            foreach (var slot in _slots)
            {
                slot.Initialize(_mergeResolver);
            }
        }

        public Slot GetUnusedSlot()
        {
            return _slots.FirstOrDefault(x => x.Item == null);
        }
        
        public Slot GetSlot(Vector2Int position)
        {
            return _slots[position.y * 8 + position.x];
        }
    }
}