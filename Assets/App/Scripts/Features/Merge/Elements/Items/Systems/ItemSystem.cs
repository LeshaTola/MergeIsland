﻿using System;
using App.Scripts.Features.Merge.Elements.Items.Actions;
using App.Scripts.Features.Merge.Screens;
using App.Scripts.Modules.PopupAndViews;
using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public class ItemSystem
    {
        public event Action OnValueChanged;

        [field: SerializeField] public ItemSystemAction Action { get; set; }

        public Item Item { get; private set; }

        public virtual void Initialize(Item item)
        {
            Item = item;
            Action?.Initialize(item);
        }

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }

        public virtual void Execute()
        {
        }

        public virtual void Import(ItemSystem original)
        {
        }

        public virtual SystemData GetSystemData()
        {
            return new SystemData()
            {
                Description = Item.Config.IsLastLevel ? ConstStrings.IT_IS_LAST : ConstStrings.MERGE_TO_UPGRADE,
                ActionData = Action?.GetActionData(),
            };
        }

        protected void InvokeOnValueChanged()
        {
            OnValueChanged?.Invoke();
        }
    }
}