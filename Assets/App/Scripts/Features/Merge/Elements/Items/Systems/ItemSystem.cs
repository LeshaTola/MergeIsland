namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public abstract class ItemSystem
    {
        public Item Item { get; set; }
        
        public abstract void Execute();

        public virtual void Import(ItemSystem original)
        {
        }
    }
}