namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public abstract class ItemSystem
    {
        public Item Item { get; set; }
        
        public virtual void Start(){}
        public virtual void Stop(){}
        
        public virtual void Execute(){}

        public virtual void Import(ItemSystem original)
        {
        }
    }
}