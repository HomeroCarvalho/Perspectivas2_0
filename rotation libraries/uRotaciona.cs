using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using MATRIZES;
namespace rotaciona
{
    public class rotaciona
    {
        
    
        /// <summary>
        /// rotaciona uma imagem nos eixos X e Y por um angulo,
        /// sendos os eixos retirados a partir de marcação numa imagem.
        /// </summary>
        /// <param name="cena">Imagem a ser rotacionada.</param>
        /// <param name="angle">angulo em graus para o qual os eixos X e Y serem transformados.</param>
        /// <param name="eixoX_2D">eixo X conseguido através de marcação na imagem.</param>
        /// <param name="eixoY_2D">eixo Y conseguido através de marcação na imagem.</param>
        /// <param name="dimCell">dimensões da imagem final, já processada.</param>
        /// <returns>retorna uma imagem rotacionada pelo angulo em graus [angle], aplicado em eixos X e Y,
        ///           transformados para (1,0) e (0,1).</returns>
        public static Bitmap rotacionaImagemComDoisEixos2D(Bitmap cena, double[] angle,
                                                           vetor2 eixoX_2D,
                                                           vetor2 eixoY_2D,
                                                            Size dimCell)
        {
            if (cena == null)
                throw new Exception("erro no metodo rotacionaImagemComUmEixo2D(), imagem currente e' nula");

            
            // inicializa o eixo X, a partir do eixoX_2D, que é um eixo X aparente, conseguido com marcação na imagem.
            vetor2 ex = new vetor2(eixoX_2D.X, eixoX_2D.Y);
            // inicializa o eixo Y, a partir do eixoY_2D, que é um eixo Y aparente, conseguido com marcação na imagem.
            vetor2 ey = new vetor2(eixoY_2D.X, eixoY_2D.Y);

            // normaliza o eixo X [ex].
            ex = vetor2.normaliza(ex);
            // normaliza o eixo Y [ey].
            ey = vetor2.normaliza(ey);
            
            // constrói o eixo X como uma matriz [1,2].
            MATRIZES.Matriz mtEixoX = new MATRIZES.Matriz(1, 2);
            mtEixoX.setElemento(0, 0, ex.X);
            mtEixoX.setElemento(0, 1, ex.Y);
            
            // constrói o eixo Y como uma matriz [1,2].
            MATRIZES.Matriz mtEixoY = new MATRIZES.Matriz(1, 2);
            mtEixoY.setElemento(0, 0, ey.X);
            mtEixoY.setElemento(0, 1, ey.Y);

            // constroi a matriz de transformação (dos eixos construídos para os eixos (1,0) e (0,1).
            MATRIZES.Matriz mtrzTransf = new MATRIZES.Matriz(2, 2);
            mtrzTransf.setElemento(0, 0, ex.X);
            mtrzTransf.setElemento(0, 1, ex.Y);
            mtrzTransf.setElemento(1, 0, ey.X);
            mtrzTransf.setElemento(1, 1, ey.Y);

            // calcula a matriz inversa de transformação.
            MATRIZES.Matriz mtrzTransfInversa = MATRIZES.Matriz.MatrizInversa(mtrzTransf);

            // multiplica a matriz do eixo X pela matriz inversa de transformação.
            MATRIZES.Matriz mtEixoXTransformado = mtEixoX * mtrzTransfInversa;
            MATRIZES.Matriz mtEixoYTransformado = mtEixoY * mtrzTransfInversa;

            // rotaciona o eixo X transformado, com o ângulo 0.
            mtEixoXTransformado = angulos.rotacionaVetor(angle[0], mtEixoXTransformado);
            // rotaciona o eixo Y transformado, com o ângulo 1.
            mtEixoYTransformado = angulos.rotacionaVetor(angle[1], mtEixoYTransformado);

            // mutliplica os eixos X e Y pela matriz de transformação, retornando para o universo 2D inicial.
            mtEixoXTransformado = mtEixoXTransformado * mtrzTransf;
            mtEixoYTransformado = mtEixoYTransformado * mtrzTransf;

            // converte a matriz do eixo X para um [vetor2].
            vetor2 eixoXFinal = new vetor2((double)mtEixoXTransformado.getElemento(0, 0),
                                           (double)mtEixoXTransformado.getElemento(0, 1));

            // converte a matriz do eixo Y para um [vetor2].
            vetor2 eixoYFinal = new vetor2((double)mtEixoYTransformado.getElemento(0, 0),
                                          (double)mtEixoYTransformado.getElemento(0, 1));

            // normaliza os eixos finais.
            eixoXFinal = vetor2.normaliza(eixoXFinal);
            eixoYFinal = vetor2.normaliza(eixoYFinal);

            // retorna uma Imagem construida com os eixos finais.
            return (new Bitmap(aplicaEixos(cena, eixoXFinal, eixoYFinal, dimCell), dimCell));
        } // rotacionaImagemComDoisEixos2D()


        /// <summary>
        /// aplica a imagem [cena] um eixo X e Y dados, sendo o eixo X 
        /// rotacionado por um ângulo [angle]; o eixo Y é calculado por este método.
        /// </summary>
        /// <param name="cena">imagem em processamento.</param>
        /// <param name="angle">ângulo em graus para o incremento no eixo X e no eixo Y.</param>
        /// <param name="eixoX_2D">eixo X calculado manualmente com marcação na imagem [cena].</param>
        /// <param name="dimCells">dimensões da imagem final, já processada.</param>
        /// <returns>retorna a imagem rotacionada.</returns>
        public static Bitmap rotacionaImagemComUmEixo2D(Bitmap cena, double angle, vetor2 eixoX_2D, Size dimCells)
        {
            if (cena == null)
                throw new Exception("erro no metodo rotacionaImagemComUmEixo2D(), imagem currente e' nula");
            double anguloInc = angle;
            // prepara os eixos X e Y.
            // O eixo Y é conseguido rotacionando o eixo X por 90 graus.
            // O eixo X é conseguido rotacionando o eixo Y por -90 graus.
           
            // inicializa o eixo X, a partir do eixoX_2D, que é um eixo X aparente, conseguido com marcação na imagem.
            vetor2 ex = new vetor2(eixoX_2D.X, eixoX_2D.Y);

            // eixo Y, que foi calculado neste método como uma rotação de 90 graus sobre o eixo X [ex].
            vetor2 ey = angulos.rotacionaVetor(90.0F, ex);
            
            // constrói o eixo X como uma matriz [1,2].
            MATRIZES.Matriz mtEixoX = new MATRIZES.Matriz(1, 2);
            mtEixoX.setElemento(0, 0, ex.X);
            mtEixoX.setElemento(0, 1, ex.Y);

            // constroi a matriz de transformação (dos eixos construídos para os eixos (1,0) e (0,1).
            MATRIZES.Matriz mtrzTransf = new MATRIZES.Matriz(2, 2);
            mtrzTransf.setElemento(0, 0, ex.X);
            mtrzTransf.setElemento(0, 1, ex.Y);
            mtrzTransf.setElemento(1, 0, ey.X);
            mtrzTransf.setElemento(1, 1, ey.Y);

            // calcula a matriz inversa de transformação.
            MATRIZES.Matriz mtrzTransfInversa = MATRIZES.Matriz.MatrizInversa(mtrzTransf);

            // multiplica a matriz do eixo X pela matriz inversa de transformação.
            MATRIZES.Matriz mtEixoXTransformado = mtEixoX * mtrzTransfInversa;
            
            // rotaciona o eixo X
            mtEixoXTransformado = angulos.rotacionaVetor(anguloInc, mtEixoXTransformado);
            // constroi o eixo Y como sendo uma rotação de 90.0F graus no eixo X.
            MATRIZES.Matriz mtEixoYTransformado = angulos.rotacionaVetor(90.0F, mtEixoXTransformado);
            
            // mutliplica os eixos X e Y pela matriz de transformação, retornando para o universo 2D inicial.
            mtEixoXTransformado = mtEixoXTransformado * mtrzTransf;
            mtEixoYTransformado = mtEixoYTransformado * mtrzTransf;

            // converte a matriz do eixo X para um [vetor2].
            vetor2 eixoXFinal = new vetor2((double)mtEixoXTransformado.getElemento(0, 0),
                                             (double)mtEixoXTransformado.getElemento(0, 1));

            // converte a matriz do eixo Y para um [vetor2].
            vetor2 eixoYFinal= new vetor2((double)mtEixoYTransformado.getElemento(0,0),
                                            (double)mtEixoYTransformado.getElemento(0,1));

            // normaliza os eixos finais.
            eixoXFinal = vetor2.normaliza(eixoXFinal);
            eixoYFinal = vetor2.normaliza(eixoYFinal);

            // retorna uma Imagem construida com os eixos finais.
            return (new Bitmap(aplicaEixos(cena, eixoXFinal, eixoYFinal,dimCells)));

        } // rotacionaImagemComUmEixo2D

        /// <summary>
        /// retorna uma Imagem aplicando os eixos X e Y em cada ponto.
        /// </summary>
        /// <param name="cena">Imagem a ser processionada.</param>
        /// <param name="ex">coordenadas do eixo X.</param>
        /// <param name="ey">coordenadas do eixo Y.</param>
        /// <param name="dimCells">dimensões da imagem final, já processada.</param>
        /// <returns></returns>
        protected static Bitmap aplicaEixos(Bitmap cena, vetor2 ex, vetor2 ey, Size dimCells)
        {
            if (cena == null)
                throw new Exception("erro no metodo [aplicaEixos()], imagem currente e' nula");
            double xf = 0.0F;
            double yf = 0.0F;
            Bitmap cenaFinal = new Bitmap(3 * cena.Width, 3 * cena.Height);
            double minX = +1000000000.0F;
            double minY = +1000000000.0F;
            double maxX = -1000000000.0F;
            double maxY = -1000000000.0F;
            int xx, yy;
            try
            {
                for (xx = 0; xx < cena.Width; xx++)
                    for (yy = 0; yy < cena.Height; yy++)
                    {
 
                        xf = (double)((xx) * ex.X + (yy) * ey.X);
                        yf = (double)((xx) * ex.Y + (yy) * ey.Y);
                        // calcula os pontos extremos da imagem, para fins de correção de
                        // coordenadas fora das dimensões da imagem final.
                        if (xf < minX)
                            minX = xf;
                        if (xf > maxX)
                            maxX = xf;
                        if (yf < minY)
                            minY = yf;
                        if (yf > maxY)
                            maxY = yf;
                    } // for yy
                cenaFinal = new Bitmap((int)((maxX - minX) + cena.Width / 2), (int)((maxY - minY) + cena.Height / 2));
                for (xx = 0; xx < cena.Width; xx++)
                    for (yy = 0; yy < cena.Height; yy++)
                    {
                        
                        xf = (double)((xx ) * ex.X + (yy ) * ey.X) - minX;
                        yf = (double)((xx ) * ex.Y + (yy ) * ey.Y) - minY;
                        cenaFinal.SetPixel((int)xf, (int)yf, cena.GetPixel(xx, yy));
                    }
                return (new Bitmap(cenaFinal, new Size(3*dimCells.Width,3*dimCells.Height)));
            }
            catch (Exception ex1)
            {
                MessageBox.Show( ex1.Message+
                                "  valor(X,Y): " + (int)xf + " : " + (int)yf +
                                "  dimensoes: (" + cenaFinal.Width + " , " + cenaFinal.Height);
                return null;
            } // catch
        } // aplicaEixos()
        
    } // public class rotaciona
    } // namespace rotacaoPerspectiva











