using EpsilonEngine;
namespace DontMelt
{
    public sealed class Ground : Component
    {
        private int offset = 0;
        private double timer = 0;
        private double animationSpeed = 32;
        private Texture groundBase;

        public Ground(GameObject parent)
        {
        }

        public override void Initialize()
        {
            groundBase = parent.graphic.Clone();
        }
        public override void Update()
        {
            timer += packet.deltaTime.TotalSeconds * animationSpeed;
            while (timer > 1)
            {
                timer--;
                offset++;
            }
            while (offset > 64)
            {
                offset -= 64;
            }
            Texture frame = groundBase.Clone();
            for (int y = 0; y < frame.height; y++)
            {
                for (int x = 0; x < frame.width; x++)
                {
                    int grad1 = ((offset + y + parent.position.y) + (x + parent.position.x)) / 2;
                    while (grad1 >= 32)
                    {
                        grad1 -= 32;
                    }
                    double grad2 = grad1 / 32.0;
                    if (grad2 < 0.5)
                    {
                        if (frame.GetPixelUnsafe(x, y) == Color.Pink)
                        {
                            frame.SetPixelUnsafe(x, y, GradientHelper.Gradient(grad2 * 2, new Color(255, 100, 0, 255), new Color(255, 200, 0)));
                        }
                    }
                    else
                    {
                        if (frame.GetPixelUnsafe(x, y) == Color.Pink)
                        {
                            frame.SetPixelUnsafe(x, y, GradientHelper.Gradient((grad2 * 2) - 1, new Color(255, 200, 0, 255), new Color(255, 100, 0)));
                        }
                    }
                }
            }
            parent.graphic = frame;
        }
        public static new Ground Create(GameObject parent)
        {
            Ground output = new Ground();
            output.parent = parent;
            return output;
        }
    }
}