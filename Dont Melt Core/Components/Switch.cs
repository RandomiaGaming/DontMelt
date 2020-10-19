namespace DontMelt
{
    public class OnOffSwitch : Component
    {
        public bool IsOnSwitch = false;

        private RG_Collider RGC;
        private SpriteRenderer SR;
        public Sprite Pressed;
        public Sprite Unpressed;

        // Start is called before the first frame update
        void Start()
        {
            SR = GetComponent<SpriteRenderer>();
            RGC = GetComponent<RG_Collider>();
        }

        private void Update()
        {
            if (Stage_Player.Instance != null)
            {
                if (IsOnSwitch == Stage_Player.Instance.OnOffIsOn)
                {
                    SR.sprite = Pressed;
                }
                else
                {
                    SR.sprite = Unpressed;
                }

                foreach (RG_Trigger_Overlap collision in RGC.Get_Trigger_Overlaps())
                {
                    if (collision.Other_GameObject.tag == "Player" ||
                        collision.Other_GameObject.layer == LayerMask.NameToLayer("Spiny"))
                    {
                        Stage_Player.Instance.OnOffIsOn = IsOnSwitch;
                    }
                }
            }
            else
            {
                SR.sprite = Unpressed;
            }
        }
    }
}