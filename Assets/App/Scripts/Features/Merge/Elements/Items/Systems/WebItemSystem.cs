using App.Scripts.Features.Energy.Providers;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
    }
}