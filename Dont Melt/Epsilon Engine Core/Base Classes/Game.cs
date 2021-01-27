using System.Collections.Generic;
using System;
namespace EpsilonEngine
{
    public sealed class Game
    {
        private List<SceneBase> scenes = new List<SceneBase>();
        private List<int> scenesToUnload = new List<int>();
        private List<SceneBase> scenesToLoad = new List<SceneBase>();

        private List<GameManager> gameManagers = new List<GameManager>();
        private List<int> gameManagersToRemove = new List<int>();
        private List<GameManager> gameManagersToAdd = new List<GameManager>();

        public Point viewPortRect = new Point(256, 144);
        public bool requestingToQuit { get; private set; } = false;
        public InputPacket packetBuffer { get; private set; } = null;
        public void Initialize(InitializationPacket packet)
        {
            Cleanup();
        }
        public void Tick(InputPacket packet)
        {
            packetBuffer = packet;

            foreach (GameManager gameManager in gameManagers)
            {
                gameManager.Update();
            }

            foreach (SceneBase scene in scenes)
            {
                scene.Update();
            }

            Cleanup();
        }
        public Texture Render()
        {
            if (scenes is null || scenes.Count == 0)
            {
                return null;
            }
            else if (scenes.Count == 1)
            {
                Texture output = scenes[0].Render();
                if (output is null || output.width != viewPortRect.x || output.height != viewPortRect.y)
                {
                    return null;
                }
                else
                {
                    return output;
                }
            }
            else
            {
                Texture output = new Texture(viewPortRect.x, viewPortRect.y);
                foreach (SceneBase scene in scenes)
                {
                    Texture sceneRender = scene.Render();
                    if (sceneRender is not null && sceneRender.width == viewPortRect.x && sceneRender.height == viewPortRect.y)
                    {
                        TextureHelper.Blitz(sceneRender, output, Point.Zero);
                    }
                }
                return output;
            }
        }
        public void RequestToQuit()
        {
            requestingToQuit = true;
        }
        private void Cleanup()
        {
            if (gameManagersToRemove is null)
            {
                gameManagersToRemove = new List<int>();
            }
            gameManagersToRemove.Sort();
            foreach (int componentID in gameManagersToRemove)
            {
                gameManagers.RemoveAt(componentID);
            }
            gameManagersToRemove = new List<int>();

            foreach (GameManager gameManagerToAdd in gameManagersToAdd)
            {
                gameManagers.Add(gameManagerToAdd);
            }
            foreach (GameManager gameManagerToAdd in gameManagersToAdd)
            {
                gameManagerToAdd.Initialize();
            }
            gameManagersToAdd = new List<GameManager>();



            if (scenesToUnload is null)
            {
                scenesToUnload = new List<int>();
            }
            scenesToUnload.Sort();
            foreach (int sceneID in scenesToUnload)
            {
                scenes.RemoveAt(sceneID);
            }
            scenesToUnload = new List<int>();

            foreach (SceneBase sceneToLoad in scenesToLoad)
            {
                scenes.Add(sceneToLoad);
            }
            foreach (SceneBase sceneToLoad in scenesToLoad)
            {
                sceneToLoad.Initialize();
            }
            scenesToLoad = new List<SceneBase>();

            foreach (SceneBase scene in scenes)
            {
                scene.Cleanup();
            }
        }
        #region Game Manager Management Methods
        public GameManager GetGameManager(int index)
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return null;
            }
            if (index < 0 || index >= gameManagers.Count)
            {
                throw new ArgumentException();
            }
            return gameManagers[index];
        }
        public GameManager GetGameManager(Type targetType)
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return null;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(targetType))
                {
                    return gameManagers[i];
                }
            }
            return null;
        }
        public T GetGameManager<T>() where T : GameManager
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return null;
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)gameManagers[i];
                }
            }
            return null;
        }
        public List<GameManager> GetGameManagers()
        {
            return new List<GameManager>(gameManagers);
        }
        public List<GameManager> GetGameManagers(Type targetType)
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return null;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            List<GameManager> output = new List<GameManager>();
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(targetType))
                {
                    output.Add(gameManagers[i]);
                }
            }
            return output;
        }
        public List<T> GetGameManagers<T>() where T : GameManager
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return null;
            }
            List<T> output = new List<T>();
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)gameManagers[i]);
                }
            }
            return output;
        }
        public int GetGameManagerCount()
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return 0;
            }
            return gameManagers.Count;
        }
        public void RemoveGameManager(int index)
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return;
            }
            if (index < 0 || index >= gameManagers.Count)
            {
                throw new ArgumentException();
            }
            if (gameManagersToRemove is null)
            {
                gameManagersToRemove = new List<int>();
            }
            gameManagersToRemove.Add(index);
        }
        public void RemoveGameManager(GameManager targetGameManager)
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return;
            }
            if (targetGameManager is null)
            {
                throw new NullReferenceException();
            }
            if (gameManagersToRemove is null)
            {
                gameManagersToRemove = new List<int>();
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i] == targetGameManager)
                {
                    gameManagersToRemove.Add(i);
                }
            }
        }
        public void RemoveGameManagers(Type targetType)
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            if (gameManagersToRemove is null)
            {
                gameManagersToRemove = new List<int>();
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(targetType))
                {
                    gameManagersToRemove.Add(i);
                }
            }
        }
        public void RemoveGameManagers<T>() where T : GameManager
        {
            if (gameManagers is null)
            {
                gameManagers = new List<GameManager>();
                return;
            }
            if (gameManagersToRemove is null)
            {
                gameManagersToRemove = new List<int>();
            }
            for (int i = 0; i < gameManagers.Count; i++)
            {
                if (gameManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    gameManagersToRemove.Add(i);
                }
            }
        }
        public void AddGameManager(GameManager newGameManager)
        {
            if (gameManagersToAdd is null)
            {
                gameManagersToAdd = new List<GameManager>();
            }
            if (newGameManager is null)
            {
                throw new NullReferenceException();
            }
            if (newGameManager.game != this)
            {
                throw new ArgumentException();
            }
            gameManagersToAdd.Add(newGameManager);
        }
        #endregion
        #region Scene Management Methods
        public SceneBase GetScene(int index)
        {
            if (scenes is null)
            {
                scenes = new List<SceneBase>();
                return null;
            }
            if (index < 0 || index >= scenes.Count)
            {
                throw new ArgumentException();
            }
            return scenes[index];
        }
        public List<SceneBase> GetScenes()
        {
            return new List<SceneBase>(scenes);
        }
        public int GetSceneCount()
        {
            if (scenes is null)
            {
                scenes = new List<SceneBase>();
                return 0;
            }
            return scenes.Count;
        }
        public void RemoveScene(int index)
        {
            if (scenes is null)
            {
                scenes = new List<SceneBase>();
                return;
            }
            if (index < 0 || index >= scenes.Count)
            {
                throw new ArgumentException();
            }
            if (scenesToUnload is null)
            {
                scenesToUnload = new List<int>();
            }
            scenesToUnload.Add(index);
        }
        public void RemoveScene(SceneBase targetScene)
        {
            if (scenes is null)
            {
                scenes = new List<SceneBase>();
                return;
            }
            if (targetScene is null)
            {
                throw new NullReferenceException();
            }
            if (scenesToUnload is null)
            {
                scenesToUnload = new List<int>();
            }
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i] == targetScene)
                {
                    scenesToUnload.Add(i);
                }
            }
        }
        public void AddScene(SceneBase newScene)
        {
            if (scenesToLoad is null)
            {
                scenesToLoad = new List<SceneBase>();
            }
            if (newScene is null)
            {
                throw new NullReferenceException();
            }
            scenesToLoad.Add(newScene);
        }
        #endregion
    }
}