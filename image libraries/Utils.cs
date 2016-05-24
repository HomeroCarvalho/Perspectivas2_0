using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATRIZES;

namespace Utils
{
   
    /// <summary>
    /// contém funcionalidade de processamento de imagens comuns a vários método por toda solução.
    /// </summary>
    class UtilsImage
    {
        /// <summary>
        /// Faz o recorte de uma região da figura sendo editada.
        /// </summary>
        /// <param name="cenaEntrada">imagem de entrada a ser processada.</param>
        /// <returns>retorna a imagem recortada.</returns>
        public static Bitmap recortaImagem(Bitmap cenaEntrada)
        {
            Bitmap cenaSaida=null;
            int minX = 100000;
            int minY = 100000;
            int maxX = -1;
            int maxY = -1;
            // tenta localizar os pontos mínimos e máximos, sobre além dos quais só haja pontos com ALPHA=0.
            for (int y = 0; y < cenaEntrada.Height; y++)
                for (int x = 0; x < cenaEntrada.Width; x++)
                {    // se a cor não for transparente, tenta processar os pontos máximos e mínimos.
                    ushort cA = cenaEntrada.GetPixel(x, y).A;
                    ushort cR = cenaEntrada.GetPixel(x, y).R;
                    ushort cG = cenaEntrada.GetPixel(x, y).G;
                    ushort cB = cenaEntrada.GetPixel(x, y).B;
                    if (cA > 0)
                    {
                        if (x < minX)
                            minX = x;
                        if (x > maxX)
                            maxX = x;
                        if (y < minY)
                            minY = y;
                        if (y > maxY)
                            maxY = y;
                    } //if cA>0
                } // for x
            if ((maxX > -1) && (maxY> - 1))
            {
                // esta variável representa a área de corte sobre a área original.
                RectangleF cutArea = new RectangleF(minX, minY, maxX - minX, maxY - minY);
                // esta variável representa as dimensões finais da imagem copiada.
                RectangleF destArea = new RectangleF(0, 0, maxX - minX, maxY - minY);

                // cria a imagem de saída com as dimensões da área a ser copiada.
                cenaSaida = new Bitmap((int)destArea.Width, (int)destArea.Height);
                // cria um dispositivo gráfico para conectar a imagem de saída ao processamento subsequente.
                Graphics g = Graphics.FromImage(cenaSaida);
                // desenha com o dispositivo gráfico sobre a imagem de saída,
                // recortando a área [cutArea] da imagem de entrada.
                g.DrawImage(cenaEntrada, destArea, cutArea, GraphicsUnit.Pixel);
              
            }
            // faz o retorno da imagem processada.
            return cenaSaida;
        } // RecortaImagem()
    } /// class UtilsImage
   
} // namespace Utils
