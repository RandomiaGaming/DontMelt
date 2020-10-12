namespace DontMelt
{
    public class Checkpoint : Component
    {
        public Sprite Selected;
        private Sprite Normal;
        private SpriteRenderer sr;
        private RG_Collider RGC;

        private void Start()
        {
            RGC = GetComponent<RG_Collider>();
            sr = GetComponent<SpriteRenderer>();
            Normal = sr.sprite;
        }

        private void Update()
        {
            if (Stage_Player.Instance != null)
            {
                if (Stage_Player.Instance.CheckPointPos == (Vector2)transform.position)
                {
                    sr.sprite = Selected;
                }
                else
                {
                    sr.sprite = Normal;
                }

                if (!Stage_Player.Instance.Paused)
                {
                    foreach (RG_Trigger_Overlap overlap in RGC.Get_Trigger_Overlaps())
                    {
                        if (overlap.Other_GameObject.tag == "Player")
                        {
                            Stage_Player.Instance.CheckPointPos = transform.position;
                        }
                    }
                }
            }
        }
    }
}