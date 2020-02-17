using System.Drawing;

namespace BLL
{
    public class Redimencion
    {
        public Redimencion()
        { }

        public Image RedimencionarImagen(Image ImagenOriginal, int Alto)
        {
            var Radio = (double)Alto / ImagenOriginal.Height;
            var NuevoAncho = (int)(ImagenOriginal.Width * Radio);
            var NuevoAlto = (int)(ImagenOriginal.Height * Radio);
            var NuevaIamgenRedimencionada = new Bitmap(NuevoAncho, NuevoAlto);
            var g = Graphics.FromImage(NuevaIamgenRedimencionada);
            g.DrawImage(ImagenOriginal, 0, 0, NuevoAncho, NuevoAlto); //Genera imagen a partir de los tamaños
            return NuevaIamgenRedimencionada;
        }
    }
}
