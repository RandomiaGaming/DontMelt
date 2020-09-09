using System.Collections.Generic;

namespace DontMelt.Internal
{
    public static class DontMeltKernal
    {
        public static Point cameraPosition = Point.Create(0, 0);
        public static List<GameObject> loadedGameObjects = new List<GameObject>();
        public static OutputPacket Update(UpdatePacket Packet)
        {
            loadedGameObjects[0].position += Packet.inputPacket.keyDirection;
            for (int i = 0; i < loadedGameObjects.Count; i++)
            {
                loadedGameObjects[i].Update(Packet.Clone());
            }
            Texture Frame = Texture.Create(256, 144, Color.Create(255, 255, 100));
            for (int i = 0; i < loadedGameObjects.Count; i++)
            {
                Frame.BlitzClip(loadedGameObjects[i].sprite, loadedGameObjects[i].position - cameraPosition);
            }
            return OutputPacket.Create(Frame, false);
        }
        public static void Initialize()
        {
            GameObject TestOBJ1 = GameObject.Create();
            TestOBJ1.position = Point.Create(0, 0);
            TestOBJ1.sprite = Texture.Create(16, 16, Color.Create(0, 0, 0));
            loadedGameObjects.Add(TestOBJ1);
            GameObject TestOBJ2 = GameObject.Create();
            TestOBJ2.position = Point.Create(0, 32);
            TestOBJ2.sprite = Texture.Create(16, 16, Color.Create(255, 150, 255));
            loadedGameObjects.Add(TestOBJ2);
            GameObject TestOBJ3 = GameObject.Create();
            TestOBJ3.position = Point.Create(32, 32);
            TestOBJ3.sprite = Texture.Create(16, 16, Color.Create(255, 150, 255));
            loadedGameObjects.Add(TestOBJ3);
            GameObject TestOBJ4 = GameObject.Create();
            TestOBJ4.position = Point.Create(32, 0);
            TestOBJ4.sprite = Texture.Create(16, 16, Color.Create(255, 150, 255));
            loadedGameObjects.Add(TestOBJ4);
        }
    }
}