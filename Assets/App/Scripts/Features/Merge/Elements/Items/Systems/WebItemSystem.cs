using App.Scripts.Features.Merge.Screens;

namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public class WebItemSystem : ItemSystem
    {
        public override void Start()
        {
            Item.Visual.WebSetActive(true);
            Item.IsBlocked = true;
        }

        public override void Stop()
        {
            Item.Visual.WebSetActive(false);
            Item.IsBlocked = false;
        }

        public override SystemData GetSystemData()
        {
            var data = base.GetSystemData();
            data.IsBlocked = true;
            return data;
        }
    }
}