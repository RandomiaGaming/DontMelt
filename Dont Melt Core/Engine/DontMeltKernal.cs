using System.Collections.Generic;

namespace DontMelt.Internal
{
    public static class DontMeltKernal
    {
        public static List<GameObject> LoadedGameObjects = new List<GameObject>();
        public static List<GameManager> LoadedGameManagers = new List<GameManager>();
        public static OutputPacket Update(InputPacket Packet)
        {
            for (int i = 0; i < LoadedGameObjects.Count; i++)
            {
                LoadedGameObjects[i].Update(Packet.Clone());
            }
            for (int i = 0; i < LoadedGameManagers.Count; i++)
            {
                LoadedGameManagers[i].Update(Packet.Clone());
            }
            Texture Frame = Texture.Create(256, 144, Color.Create(255, 255, 100));
            return OutputPacket.Create(Frame, false);
        }
        public static void Initialize()
        {

        }
    }
}