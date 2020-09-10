namespace EpsilonEngine
{
    public sealed class SideInfo
    {
        public bool top = false;
        public bool bottom = false;
        public bool left = false;
        public bool right = false;

        private SideInfo() { }
        public static SideInfo Create()
        {
            SideInfo output = new SideInfo();
            output.top = false;
            output.bottom = false;
            output.left = false;
            output.right = false;
            return output;
        }
        public static SideInfo Create(bool value)
        {
            SideInfo output = new SideInfo();
            output.top = value;
            output.bottom = value;
            output.left = value;
            output.right = value;
            return output;
        }
        public static SideInfo Create(bool top, bool bottom, bool left, bool right)
        {
            SideInfo output = new SideInfo();
            output.top = top;
            output.bottom = bottom;
            output.left = left;
            output.right = right;
            return output;
        }
        public SideInfo Clone()
        {
            SideInfo output = new SideInfo();
            output.top = top;
            output.bottom = bottom;
            output.left = left;
            output.right = right;
            return output;
        }
    }
}
