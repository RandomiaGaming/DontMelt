namespace DontMelt
{
    public class Stage_Player : Item
    {
        public static Stage_Player Instance;

        private static Stage_Data CurrentStageData = new Stage_Data();
        private StageGenerator SG;

        [HideInInspector] public Vector2 CheckPointPos;
        public bool OnOffIsOn = true;
        public bool Paused = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (CurrentStageData == null)
            {
                SceneManager.LoadScene("TitleScreen");
                return;
            }

            CheckPointPos = CurrentStageData.Player_Pos + new Vector2(0.5f, 0.5f);
            Regenerate();
        }

        private void Regenerate()
        {
            SG = GetComponent<StageGenerator>();
            SG.CurrentStageData = CurrentStageData.Clone();
            SG.Regenerate();
        }

        private void Update()
        {
            if (Paused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void Level_Failed()
        {
            Regenerate();
        }

        public void Level_Completed()
        {
            //PlayerUI.Instance.OnStageCompleted();
        }

        public Stage_Data GetStageData()
        {
            return CurrentStageData.Clone();
        }

        public static void PlayStage(Stage_Data Stage)
        {
            Stage.Clean();
            CurrentStageData = Stage.Clone();
            SceneManager.LoadScene("StagePlayer");
        }
    }
}