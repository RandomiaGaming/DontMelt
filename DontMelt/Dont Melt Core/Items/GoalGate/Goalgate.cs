namespace DontMelt
{
    public class Goalgate : Component
    {
        private RG_Collider RGC;

        private void Start()
        {
            RGC = GetComponent<RG_Collider>();
        }

        private void Update()
        {
            if (Stage_Player.Instance != null && !Stage_Player.Instance.Paused)
            {
                foreach (RG_Trigger_Overlap overlap in RGC.Get_Trigger_Overlaps())
                {
                    if (overlap.Other_GameObject.tag == "Player")
                    {
                        Stage_Player.Instance.Level_Completed();
                    }
                }
            }
        }
    }
}