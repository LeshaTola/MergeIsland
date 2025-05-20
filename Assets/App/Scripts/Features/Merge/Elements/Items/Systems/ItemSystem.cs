using System;
using App.Scripts.Features.Merge.Screens;
using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public class ItemSystem
    {
        public event Action OnValueChanged;
        
        [SerializeField] protected ItemSystemAction Action;
        
        private Item _item;

        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                
                if (Action != null)
                {
                    Action.Item = _item;
                }
            }
        }

        public virtual void Start(){}
        public virtual void Stop(){}
        
        public virtual void Execute(){}

        public virtual void Import(ItemSystem original)
        {
            Action?.Import(original.Action);
        }

        public virtual SystemData GetSystemData()
        {
            return new SystemData()
            {
                Description = Item.Config.IsLastLevel ? "Is last level": "Merge for get next level",
                ActionData = Action?.GetActionData(),
            };
        }

        protected void InvokeOnValueChanged()
        {
            OnValueChanged?.Invoke();
        }
    }
}