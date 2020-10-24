using System.Collections.Generic;
namespace Epsilon
{
    internal sealed class StageData
    {
        public string name = "";
        public Point playerPos = new Point(0, 0);
        public Point goalPos = new Point(5, 0);
        public List<TileData> tileData = new List<TileData>();
        public bool Equals(StageData OtherStage)
        {
            if (OtherStage == null)
            {
                return false;
            }
            else
            {
                if (playerPos != OtherStage.playerPos || tileData.Count != OtherStage.tileData.Count || goalPos != OtherStage.goalPos)
                {
                    return false;
                }
                for (int i = 0; i < tileData.Count; i++)
                {
                    if (tileData[i].itemName != OtherStage.tileData[i].itemName || tileData[i].position != OtherStage.tileData[i].position)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public StageData Clone()
        {
            StageData Clone = new StageData();
            Clone.name = name;
            Clone.playerPos = playerPos;
            Clone.goalPos = goalPos;
            Clone.tileData = new List<TileData>(tileData);
            return Clone;
        }

        public void Clean()
        {
            if (playerPos == goalPos)
            {
                playerPos = new Point(0, 0);
                goalPos = new Point(5, 1);
            }

            List<Point> OccupiedPositions = new List<Point>() { playerPos, goalPos };
            List<TileData> CleanedTileData = new List<TileData>();
            foreach (TileData dat in tileData)
            {
                bool Occupied = false;
                foreach (Point pos in OccupiedPositions)
                {
                    if (pos == dat.position)
                    {
                        Occupied = true;
                    }
                }

                if (!Occupied)
                {
                    OccupiedPositions.Add(dat.position);
                    CleanedTileData.Add(dat);
                }
            }

            tileData = new List<TileData>(CleanedTileData);
        }

        public void SetTile(Point position, string NewItem)
        {
            if (playerPos != position && goalPos != position)
            {
                DeleteTile(position);
                tileData.Add(new TileData(NewItem, position));
                Clean();
            }
        }

        public void DeleteTile(Point position)
        {
            for (int i = 0; i < tileData.Count; i++)
            {
                if (tileData[i].position == position)
                {
                    tileData.RemoveAt(i);
                    i--;
                }
            }

            Clean();
        }

        public string GetTile(Point position)
        {
            Clean();
            if (playerPos == position)
            {
                return "Player";
            }
            else if (goalPos == position)
            {
                return "GoalGate";
            }
            foreach (TileData dat in tileData)
            {
                if (dat.position == position)
                {
                    return dat.itemName;
                }
            }
            return "None";
        }
    }
    public struct TileData
    {
        public string itemName;
        public Point position;
        public TileData(string itemName, Point position)
        {
            this.itemName = itemName;
            this.position = position;
        }
    }
}