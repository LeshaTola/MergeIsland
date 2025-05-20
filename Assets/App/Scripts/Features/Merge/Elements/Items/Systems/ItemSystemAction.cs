using App.Scripts.Features.Merge.Screens;

namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public abstract class ItemSystemAction
    {
        public Item Item { get; set; }

        public virtual void Execute()
        {
            
        }
        
        public virtual void Import(ItemSystemAction original)
        {
        }

        
        public virtual ActionData GetActionData()
        {
            return null;
        } 
    }
}