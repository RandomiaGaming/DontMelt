using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class Scene : SceneBase
    {
        public Point cameraPosition = Point.Zero;

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> gameObjectsToRemove = new List<GameObject>();
        private List<GameObject> gameObjectsToAdd = new List<GameObject>();

        private List<SceneManager> sceneManagers = new List<SceneManager>();
        private List<int> sceneManagersToRemove = new List<int>();
        private List<SceneManager> sceneManagersToAdd = new List<SceneManager>();
        public Scene(Game game) : base(game)
        {

        }
        public override Texture Render()
        {
            Texture frame = new Texture(game.viewPortRect.x, game.viewPortRect.y, new Color(255, 255, 155, 255));

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.texture is not null)
                {
                    TextureHelper.Blitz(gameObject.texture, frame, new Point(gameObject.position.x - cameraPosition.x, gameObject.position.y - cameraPosition.y));
                }
            }

            return frame;
        }
        public override void Update()
        {
            foreach (SceneManager sceneManager in sceneManagers)
            {
                sceneManager.Update();
            }

            foreach (GameObject pixel2DGameObject in gameObjects)
            {
                pixel2DGameObject.Update();
            }
        }
        public override void Cleanup()
        {
            foreach (int sceneManagerID in sceneManagersToRemove)
            {
                sceneManagers.RemoveAt(sceneManagerID);
            }
            sceneManagersToRemove = new List<int>();

            foreach (SceneManager sceneManagerToLoad in sceneManagersToAdd)
            {
                sceneManagers.Add(sceneManagerToLoad);
            }
            foreach (SceneManager sceneManagerToLoad in sceneManagersToAdd)
            {
                sceneManagerToLoad.Initialize();
            }
            sceneManagersToAdd = new List<SceneManager>();

            foreach (SceneManager sceneManager in sceneManagers)
            {
                sceneManager.Cleanup();
            }

            foreach (GameObject gameObject in gameObjectsToRemove)
            {
                gameObjects.Remove(gameObject);
            }
            gameObjectsToRemove = new List<GameObject>();

            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                gameObjects.Add(gameObject);
                gameObject.Initialize();
            }
            gameObjectsToAdd = new List<GameObject>();

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Cleanup();
            }
        }
        public override void OnRemove()
        {
            gameObjectsToAdd = null;
            gameObjectsToRemove = null;
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.OnRemove();
            }
            gameObjects = null;

            sceneManagersToAdd = null;
            sceneManagersToRemove = null;
            foreach (SceneManager sceneManager in sceneManagers)
            {
                sceneManager.OnRemove();
            }
            sceneManagers = null;
        }
        #region GameObject Management Methods
        public GameObject GetGameObject(int index)
        {
            if (gameObjects is null)
            {
                gameObjects = new List<GameObject>();
                return null;
            }
            if (index < 0 || index >= gameObjects.Count)
            {
                throw new ArgumentException();
            }
            return gameObjects[index];
        }
        public List<GameObject> GetGameObjects()
        {
            return new List<GameObject>(gameObjects);
        }
        public int GetGameObjectCount()
        {
            if (gameObjects is null)
            {
                gameObjects = new List<GameObject>();
                return 0;
            }
            return gameObjects.Count;
        }
        public void DestroyGameObject(int index)
        {
            if (gameObjects is null)
            {
                gameObjects = new List<GameObject>();
                return;
            }
            if (index < 0 || index >= gameObjects.Count)
            {
                throw new ArgumentException();
            }
            if (gameObjectsToRemove is null)
            {
                gameObjectsToRemove = new List<GameObject>();
            }
            gameObjectsToRemove.Add(gameObjects[index]);
        }
        public void DestroyGameObject(GameObject target)
        {
            if (gameObjects is null)
            {
                gameObjects = new List<GameObject>();
                return;
            }
            if (target is null)
            {
                throw new NullReferenceException();
            }
            if (gameObjectsToRemove is null)
            {
                gameObjectsToRemove = new List<GameObject>();
            }
            gameObjectsToRemove.Add(target);
        }
        public void InstantiateGameObject(GameObject newGameObject)
        {
            if (gameObjectsToAdd is null)
            {
                gameObjectsToAdd = new List<GameObject>();
            }
            if (newGameObject is null)
            {
                throw new NullReferenceException();
            }
            gameObjectsToAdd.Add(newGameObject);
        }
        #endregion
        #region Scene Manager Management Methods
        public SceneManager GetSceneManager(int index)
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return null;
            }
            if (index < 0 || index >= sceneManagers.Count)
            {
                throw new ArgumentException();
            }
            return sceneManagers[index];
        }
        public SceneManager GetSceneManager(Type targetType)
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return null;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(targetType))
                {
                    return sceneManagers[i];
                }
            }
            return null;
        }
        public T GetSceneManager<T>() where T : SceneManager
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return null;
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)sceneManagers[i];
                }
            }
            return null;
        }
        public List<SceneManager> GetSceneManagers()
        {
            return new List<SceneManager>(sceneManagers);
        }
        public List<SceneManager> GetSceneManagers(Type targetType)
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return null;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            List<SceneManager> output = new List<SceneManager>();
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(targetType))
                {
                    output.Add(sceneManagers[i]);
                }
            }
            return output;
        }
        public List<T> GetSceneManagers<T>() where T : SceneManager
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return null;
            }
            List<T> output = new List<T>();
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)sceneManagers[i]);
                }
            }
            return output;
        }
        public int GetSceneManagerCount()
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return 0;
            }
            return sceneManagers.Count;
        }
        public void RemoveSceneManager(int index)
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return;
            }
            if (index < 0 || index >= sceneManagers.Count)
            {
                throw new ArgumentException();
            }
            if (sceneManagersToRemove is null)
            {
                sceneManagersToRemove = new List<int>();
            }
            sceneManagersToRemove.Add(index);
        }
        public void RemoveSceneManager(SceneManager targetSceneManager)
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return;
            }
            if (targetSceneManager is null)
            {
                throw new NullReferenceException();
            }
            if (sceneManagersToRemove is null)
            {
                sceneManagersToRemove = new List<int>();
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i] == targetSceneManager)
                {
                    sceneManagersToRemove.Add(i);
                }
            }
        }
        public void RemoveSceneManagers(Type targetType)
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            if (sceneManagersToRemove is null)
            {
                sceneManagersToRemove = new List<int>();
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(targetType))
                {
                    sceneManagersToRemove.Add(i);
                }
            }
        }
        public void RemoveSceneManagers<T>() where T : SceneManager
        {
            if (sceneManagers is null)
            {
                sceneManagers = new List<SceneManager>();
                return;
            }
            if (sceneManagersToRemove is null)
            {
                sceneManagersToRemove = new List<int>();
            }
            for (int i = 0; i < sceneManagers.Count; i++)
            {
                if (sceneManagers[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    sceneManagersToRemove.Add(i);
                }
            }
        }
        public void AddSceneManager(SceneManager newSceneManager)
        {
            if (sceneManagersToAdd is null)
            {
                sceneManagersToAdd = new List<SceneManager>();
            }
            if (newSceneManager is null)
            {
                throw new NullReferenceException();
            }
            sceneManagersToAdd.Add(newSceneManager);
        }
        #endregion
    }
}
