namespace EpsilonEngine
{
    public enum Side { Top, Bottom, Left, Right };
    public struct SideInfo
    {
        public bool top;
        public bool bottom;
        public bool left;
        public bool right;
        public static readonly SideInfo False = new SideInfo(false, false, false, false);
        public static readonly SideInfo True = new SideInfo(true, true, true, true);
        public SideInfo(bool top, bool bottom, bool left, bool right)
        {
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
        }
    }
}
