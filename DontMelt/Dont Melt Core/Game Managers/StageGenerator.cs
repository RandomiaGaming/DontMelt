using System.Collections.Generic;
namespace DontMelt
{
    public class StageGenerator : Item
    {
        public static StageGenerator Instance;

        public Stage_Data CurrentStageData = null;
        private Stage_Data CurrentlyLoadedStageData = null;

        public List<GameObject> ItemPrefabs;
        public GameObject PlayerPrefab;
        public GameObject SnowflakePrefab;
        public GameObject GoalgatePrefab;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        public Vector2Int WorldToGrid(Vector2 WorldPoint)
        {
            return new Vector2Int((int)WorldPoint.x, (int)WorldPoint.y);
        }

        public Vector2 GridToWorld(Vector2Int GridPoint)
        {
            return (Vector3)(Vector3Int)GridPoint;
        }

        private void Update()
        {
            if (CurrentStageData != null)
            {
                if (CurrentlyLoadedStageData == null)
                {
                    Regenerate();
                }
                else if (!CurrentStageData.Equals(CurrentlyLoadedStageData))
                {
                    Regenerate();
                }
            }
        }

        public void Regenerate()
        {
            CurrentStageData.Clean();
            CurrentlyLoadedStageData = CurrentStageData.Clone();
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            Instantiate(PlayerPrefab, transform).transform.position =
                (Vector3Int)CurrentStageData.Player_Pos + new Vector3(0.5f, 0.5f, 0f);

            Instantiate(GoalgatePrefab, transform).transform.position =
                (Vector3Int)CurrentStageData.Goal_Pos + new Vector3(0.5f, 0.5f, 0f);

            foreach (Tile_Data dat in CurrentStageData.Tile_Data)
            {
                foreach (GameObject Item in ItemPrefabs)
                {
                    if (dat.Item_Name == Item.name)
                    {
                        Instantiate(Item, transform).transform.position =
                            (Vector3Int)dat.Position + new Vector3(0.5f, 0.5f, 0f);
                        break;
                    }
                }
            }
        }
    }
}