namespace EpsilonEngine
{
    public sealed class OutputPacket
    {
        public Texture frameTexture { get; private set; }
        public bool requestExit { get; private set; }
        private OutputPacket() { }
        public static OutputPacket Create(Texture frame, bool requestExit)
        {
            OutputPacket Output = new OutputPacket();
            Output.frameTexture = frame.Clone();
            Output.requestExit = requestExit;
            return Output;
        }
        public OutputPacket Clone()
        {
            OutputPacket Output = new OutputPacket();
            Output.frameTexture = frameTexture.Clone();
            Output.requestExit = requestExit;
            return Output;
        }
    }
}
