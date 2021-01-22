using System;
using System.Collections.Generic;

namespace DontMelt
{
    public sealed class TileData
    {
        public readonly string itemName = "Ground";
        public readonly Point position = Point.Zero;
        public TileData(string itemName, Point position)
        {
            if(itemName is null)
            {
                throw new NullReferenceException();
            }
            this.itemName = itemName;
            this.position = position;
        }
    }
    public sealed class StageData
    {
        public List<TileData> data = new List<TileData>();
    }
}
