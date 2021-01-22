using Newtonsoft.Json;
using System.Collections.Generic;
namespace DontMelt
{
    public sealed class DontMeltGame
    {
        public StagePlayer stagePlayer { get; private set; } = null;
        private bool requestingToQuit = false;
        public bool paused = false;
        public DontMeltGame()
        {
            StageData stageData = JsonConvert.DeserializeObject<StageData>(AssetHelper.LoadAsset<TextAsset>("THE BEGINING.json").data);
            //stageData.data = new List<TileData>() { new TileData("player", Point.Zero), new TileData("ground", new Point(0, -1)), new TileData("lava", new Point(1, 0)) };
            stagePlayer = new StagePlayer(stageData);
        }
        public TickReturnPacket Tick(TickInputPacket packet)
        {
            if (stagePlayer is null)
            {
                return new TickReturnPacket(new List<System.Exception>(), null, null, requestingToQuit);
            }
            else
            {
                stagePlayer.Tick(packet.inputPacket);
                Texture frame = stagePlayer.Render();;
                return new TickReturnPacket(null, frame, new AudioClip(48000, new byte[0]), requestingToQuit);
            }
        }
        public void Quit()
        {
            requestingToQuit = true;
        }
    }
}