//UNSAFE
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;
using System.Text;

namespace Epsilon
{
    public static class AssetHelper
    {
        private static AssetBase[] assets = null;
        public static void LoadAssets()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] assetPaths = assembly.GetManifestResourceNames();
            assets = new AssetBase[assetPaths.Length];
            for (int i = 0; i < assetPaths.Length; i++)
            {
                string assetExtention = AssetBase.GetFileExtension(assetPaths[i]).ToLower();
                Stream assetStream = assembly.GetManifestResourceStream(assetPaths[i]);
                if (assetExtention == "png" || assetExtention == "jpg" || assetExtention == "jpge")
                {
                    assets[i] = new TextureAsset(assetPaths[i], assetStream);
                }
                else if (assetExtention == "txt" || assetExtention == "text" || assetExtention == "json")
                {
                    assets[i] = new TextAsset(assetPaths[i], assetStream);
                }
                else
                {
                    assets[i] = new BianaryAsset(assetPaths[i], assetStream);
                }
            }
        }

        public static string[] GetAssetPaths()
        {
            List<string> outputList = new List<string>();
            foreach (AssetBase asset in assets)
            {
                outputList.Add(asset.path);
            }
            return outputList.ToArray();
        }
        public static string[] GetAssetNames()
        {
            List<string> outputList = new List<string>();
            foreach (AssetBase asset in assets)
            {
                outputList.Add(asset.GetFileName());
            }
            return outputList.ToArray();
        }

        public static AssetBase[] GetAssets()
        {
            List<AssetBase> outputList = new List<AssetBase>(assets);
            return outputList.ToArray();
        }
        public static AssetBase GetAsset(string name)
        {
            foreach (AssetBase asset in assets)
            {
                if (asset.GetFileName() == name.ToLower())
                {
                    return asset;
                }
            }
            return null;
        }
        public static BianaryAsset GetBianaryAsset(string name)
        {
            foreach (AssetBase asset in assets)
            {
                if (asset.GetFileName() == name.ToLower())
                {
                    if (asset.GetType().IsAssignableFrom(typeof(BianaryAsset)))
                    {
                        return (BianaryAsset)asset;
                    }
                    else
                    {
                        throw new Exception($"Asset at {asset.path} is not a binary asset.");
                    }
                }
            }
            return null;
        }
        public static TextAsset GetTextAsset(string name)
        {
            foreach (AssetBase asset in assets)
            {
                if (asset.GetFileName() == name.ToLower())
                {
                    if (asset.GetType().IsAssignableFrom(typeof(TextAsset)))
                    {
                        return (TextAsset)asset;
                    }
                    else
                    {
                        throw new Exception($"Asset at {asset.path} is not a text asset.");
                    }
                }
            }
            return null;
        }
        public static TextureAsset GetTextureAsset(string name)
        {
            foreach (AssetBase asset in assets)
            {
                if (asset.GetFileName() == name.ToLower())
                {
                    if (asset.GetType().IsAssignableFrom(typeof(TextureAsset)))
                    {
                        return (TextureAsset)asset;
                    }
                    else
                    {
                        throw new Exception($"Asset at {asset.path} is not a texture asset.");
                    }
                }
            }
            return null;
        }

        public static Texture TextureFromStream(Stream sourceStream)
        {
            System.Drawing.Image loadedImage = System.Drawing.Image.FromStream(sourceStream);
            System.Drawing.Bitmap loadedBitMap = new System.Drawing.Bitmap(loadedImage);
            Texture output = new Texture(loadedBitMap.Width, loadedBitMap.Height);
            for (int x = 0; x < loadedBitMap.Width; x++)
            {
                for (int y = 0; y < loadedBitMap.Height; y++)
                {
                    System.Drawing.Color systemColor = loadedBitMap.GetPixel(x, loadedBitMap.Height - y - 1);
                    output.SetPixel(x, y, new Color(systemColor.R, systemColor.G, systemColor.B, systemColor.A));
                }
            }
            return output;
        }
        public static string StringFromStream(Stream sourceStream)
        {
            return Encoding.ASCII.GetString(BytesFromStream(sourceStream));
        }
        public static byte[] BytesFromStream(Stream sourceStream)
        {
            byte[] output = new byte[sourceStream.Length];
            sourceStream.Read(output, 0, (int)sourceStream.Length);
            return output;
        }
    }
}
