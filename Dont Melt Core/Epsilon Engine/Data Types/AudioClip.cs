using System;
namespace EpsilonEngine
{
    public sealed class AudioClip
    {
        public int sampleRate = 48000;
        private byte[] data = new byte[0];
        private AudioClip() { }
        public static AudioClip Create(byte[] data, int sampleRate)
        {
            if (sampleRate <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (data == null || data.Length < 0)
            {
                throw new ArgumentException();
            }
            AudioClip output = new AudioClip();
            output.data = (byte[])data.Clone();
            output.sampleRate = sampleRate;
            return output;
        }
        public static AudioClip Create()
        {
            AudioClip output = new AudioClip();
            output.data = new byte[0];
            output.sampleRate = 48000;
            return output;
        }
        public void SetData(byte[] newData)
        {
            data = (byte[])newData.Clone();
        }
        public byte[] GetData()
        {
            return (byte[])data.Clone();
        }
        public AudioClip Clone()
        {
            AudioClip output = new AudioClip();
            output.data = (byte[])data.Clone();
            output.sampleRate = sampleRate;
            return output;
        }
    }
}
