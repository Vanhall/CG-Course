using System.Drawing;
using System.Drawing.Imaging;
using SharpGL;

namespace LR_2
{
    public class Texture
    {
        OpenGL gl;
        uint[] TexturePtr = new uint[1];    // Указатель на текстуру

        // Конструктор --------------------------------------------------------
        public Texture(OpenGL GL, string path)
        {
            gl = GL;
            Bitmap Texture = new Bitmap(path);
            Texture.RotateFlip(RotateFlipType.RotateNoneFlipY);
            var TexData = Texture.LockBits(new Rectangle(0, 0, Texture.Width, Texture.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.GenTextures(1, TexturePtr);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, TexturePtr[0]);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexImage2D(
                OpenGL.GL_TEXTURE_2D,
                0,
                OpenGL.GL_RGBA,
                Texture.Width,
                Texture.Height,
                0,
                OpenGL.GL_BGRA,
                OpenGL.GL_UNSIGNED_BYTE,
                TexData.Scan0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            gl.Disable(OpenGL.GL_TEXTURE_2D);

            Texture.UnlockBits(TexData);
            Texture.Dispose();
        }

        public void Bind()
        {
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, TexturePtr[0]);
        }

        public void Unbind()
        {
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }

        // Смена картинки -----------------------------------------------------
        public void ChangeImage(string path)
        {
            Bitmap Texture = new Bitmap(path);
            Texture.RotateFlip(RotateFlipType.RotateNoneFlipY);
            var TexData = Texture.LockBits(new Rectangle(0, 0, Texture.Width, Texture.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, TexturePtr[0]);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            gl.TexImage2D(
                OpenGL.GL_TEXTURE_2D,
                0,
                OpenGL.GL_RGBA,
                Texture.Width,
                Texture.Height,
                0,
                OpenGL.GL_BGRA,
                OpenGL.GL_UNSIGNED_BYTE,
                TexData.Scan0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            gl.Disable(OpenGL.GL_TEXTURE_2D);

            Texture.UnlockBits(TexData);
            Texture.Dispose();
        }
    }
}
