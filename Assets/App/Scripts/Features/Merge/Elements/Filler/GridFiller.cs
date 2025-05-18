using App.Scripts.Features.Merge.Factory;
using Zenject;

namespace App.Scripts.Features.Merge.Elements.Filler
{
    public class GridFiller : IInitializable
    {
        private readonly GridFillConfig _config;
        private readonly ItemFactory _itemFactory;
        private readonly Grid _grid;

        public GridFiller(GridFillConfig config,
            ItemFactory itemFactory,
            Grid grid)
        {
            _config = config;
            _itemFactory = itemFactory;
            _grid = grid;
        }

        public void Initialize()
        {
            FillGrid();
        }

        public void FillGrid()
        {
            _itemFactory.HideAll();
            foreach (var value in _config.Filling)
            {
                var item = _itemFactory.GetItem(value.ItemConfig,value.IsInWeb);
             
                var slot = _grid.GetSlot(value.Position);
                slot.DropItem(item);
                item.MoveToParentImmediate();
            }
        }
    }
}