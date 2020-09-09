namespace DontMelt
{
    public sealed class OutputPacket
    {
        public Texture FrameTexture { get; private set; }
        public bool RequestExit { get; private set; }
        private OutputPacket() { }
        public static OutputPacket Create()
        {
            OutputPacket Output = new OutputPacket();
            Output.FrameTexture = Texture.Create(0, 0);
            Output.RequestExit = false;
            return Output;
        }
        public static OutputPacket Create(Texture frame, bool requestExit)
        {
            OutputPacket Output = new OutputPacket();
            Output.FrameTexture = frame.Clone();
            Output.RequestExit = requestExit;
            return Output;
        }
        public OutputPacket Clone()
        {
            OutputPacket Output = new OutputPacket();
            Output.FrameTexture = FrameTexture.Clone();
            Output.RequestExit = RequestExit;
            return Output;
        }
    }
}
