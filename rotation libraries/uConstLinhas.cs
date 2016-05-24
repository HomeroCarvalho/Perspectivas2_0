using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MATRIZES;
namespace rotaciona
{
    /// *******************************************************************************************************************************
    /// *ATENÇÃO:                                                                                                                     * 
    /// 1- FIQUE DE OLHO NO MÉTODO RecortaImagem, pois os limites podem estar invertidos.                                             *
    /// 2- CUIDADO COM A PROFUNDIDADE IMAGINA PASSADA PARA A CLASSE, FOI UMA DETERMINAÇÃO ARBITRÁRIA. O IDEAL SERIA CALCULAR A PROFUN-
    /// DIDADE AUTOMATICAMENTE.                                                                                                       *
    ///                                                                                                                               *
    //  *******************************************************************************************************************************
    /// <summary>
    /// constroi uma imagem 3D e a rotaciona, a partir de uma imagem 2D original e ângulos em graus de coordenadas esféricas (planos XY,
    /// XZ,YZ).
    /// </summary>
    public class Matriz3DparaImagem2D
    {
        /// <summary>
        /// matriz que guarda os vetores 3D que foram mapeados com a ajuda da sucessão de retângulos que formarão a imagem 3D para rotacionar.
        /// </summary>
        public vetor3[,,] matrix;
        
        /// <summary>
        /// média de vetores 3D da matrix. utilizada para retirar/adicionar o centro de massa da matrix 3D que mapeará a imagem 3D para rotacionar.
        /// </summary>
        private vetor3 media = new vetor3(0.0, 0.0, 0.0);
        
        
        
        /// <summary>
        /// constroi uma lista de vetores 3D a partir de uma imagem em perspectiva isométrica, e alguns parâmetros.
        /// </summary>
        /// <param name="eixo2DX">vetor 2D representando o eixo X.</param>
        /// <param name="eixo2DZ">vetor 2D representando o eixo Z (profundidade)</param>
        /// <param name="cena">imagem a ser processada para gerar a lista de vetores 3D.</param>
        /// <param name="fatorPerspetivaIsometrica">fator de compactação para gerar as cordenadas 2D isométricas.</param>
        /// <param name="profundidadeImaginada">altura imaginária da imagem 3D.</param>
        /// <param name="anguloXYEmGraus">ângulo de rotação do plano XY.</param>
        /// <param name="anguloYZEmGraus">ângulo de rotação do plano YZ.</param>
        /// <param name="anguloXZEmGraus">ângulo de rotação do plano XZ.</param>
        /// <param name="msgErro">mensagens de erro que surgirem no processamento da imagem a ser rotacionada.</param>
        /// <returns></returns>
        public Bitmap rotacionaMatriz3D(vetor2 eixo2DX, vetor2 eixo2DY,
                                        Bitmap cena, double fatorPerspetivaIsometrica, 
                                        int profundidadeImaginada,
                                        double anguloXYEmGraus,
                                        double anguloYZEmGraus, 
                                        double anguloXZEmGraus,
                                        ref string msgErro)
        {
            try
            {
                this.matrix = new vetor3[cena.Width + 6, cena.Height + 6, profundidadeImaginada + 5];

                vetor3 eixoX = new vetor3(0.0, 0.0, 0.0);
                vetor3 eixoY = new vetor3(0.0, 0.0, 0.0);
                vetor3 eixoZ = new vetor3(0.0, 0.0, 0.0);
                int x, y, z;
                for (x = 0; x < matrix.GetLength(0); x++)
                    for (y = 0; y < matrix.GetLength(1); y++)
                        for (z = 0; z < matrix.GetLength(2); z++)
                        {
                            matrix[x, y, z] = new vetor3(x, y, z);
                            vetor2 v2 = matrix[x, y, z].transformacaoPerspecctivaIsometrica(fatorPerspetivaIsometrica);

                            Color cor = cena.GetPixel((int)v2.X, (int)v2.Y);
                            matrix[x, y, z].cor = cor;

                            if (v2.Equals(eixo2DX))
                                eixoX = new vetor3(matrix[x, y, z]);
                            if (v2.Equals(eixo2DY))
                                eixoY = new vetor3(matrix[x, y, z]);

                        } // for z
                eixoZ = eixoX & eixoY;
                eixoX.normaliza();
                eixoY.normaliza();
                eixoZ.normaliza();
                eixoX.rotacionaVetorComPlanos(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);
                eixoY.rotacionaVetorComPlanos(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);
                eixoZ.rotacionaVetorComPlanos(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);
                for (x = 0; x < matrix.GetLength(0); x++)
                    for (y = 0; y < matrix.GetLength(1); y++)
                        for (z = 0; z < matrix.GetLength(2); z++)
                            matrix[x, y, z].multiplicaPorUmaBase(eixoX, eixoY, eixoZ);
                vetor3 dims3 = new vetor3(cena.Width, cena.Height, profundidadeImaginada);
                vetor2 dims2 = dims3.transformacaoPerspecctivaIsometrica(fatorPerspetivaIsometrica);
                Bitmap cenaRotacionada = new Bitmap((int)dims2.X, (int)dims2.Y);
                for (x = 0; x < matrix.GetLength(0); x++)
                    for (y = 0; y < matrix.GetLength(1); y++)
                        for (z = 0; z < matrix.GetLength(2); z++)
                        {
                            vetor2 v2_0 = matrix[x, y, z].transformacaoPerspecctivaIsometrica(fatorPerspetivaIsometrica);
                            cenaRotacionada.SetPixel((int)v2_0.X, (int)v2_0.Y, matrix[x, y, z].cor);
                        } //for z
                return cenaRotacionada;
            } // try
            catch (Exception ex)
            {
                msgErro = "Erro no processamento da imagem a ser rotacionada. Motivo: " + ex.Message;
                return null;
            }
        } // constroiMatriz3D()

        /// <summary>
        /// constroi e rotaciona uma lista de vetores 3D a partir de uma imagem e vetor3 eixos determinados pelo aplicativo, e alguns parâmetros.
        /// </summary>
        /// <param name="eixoX">eixo X 3D calculado com ferramentas como o gzimo cubo.</param>
        /// <param name="eixoY">eixo Y 3D calculado com ferramentas como o gzimo cubo.</param>
        /// <param name="eixoZ">eixo Z 3D calculado com ferramentas como o gzimo cubo.</param>
        /// <param name="cena">imagem a ser processada.</param>
        /// <param name="fatorPerspetivaIsometrica">fator de divisão do eixo projetado para formar o eixo 2D.</param>
        /// <param name="profundidadeObjeto">profundidade imaginada pelo usuário para a imagem 3D a ser rotacionada.</param>
        /// <param name="anguloXYEmGraus">ângulo de rotação do plano XY.</param>
        /// <param name="anguloYZEmGraus">ângulo de rotação do plano YZ.</param>
        /// <param name="anguloXZEmGraus">ângulo de rotação do plano XZ.</param>
        /// <param name="msgErro">mensagens de erro durante o processamento da imagem para matriz rotacionada.</param>
        /// 
        public Bitmap rotacionaMatriz3D(vetor3 eixoX, vetor3 eixoY, vetor3 eixoZ,
            Bitmap cena,
            double fatorPerspetivaIsometrica,
            double profundidadeObjeto,
            double anguloXYEmGraus, double anguloYZEmGraus, double anguloXZEmGraus, 
            ref string msgErro)
        {
            try
            {
                int x, y, z;
                for (x = 0; x < matrix.GetLength(0); x++)
                    for (y = 0; y < matrix.GetLength(1); y++)
                        for (z = 0; z < matrix.GetLength(2); z++)
                        {
                            matrix[x, y, z] = new vetor3(x, y, z);
                            vetor2 v2 = matrix[x, y, z].transformacaoPerspecctivaIsometrica(fatorPerspetivaIsometrica);

                            Color cor = cena.GetPixel((int)v2.X, (int)v2.Y);
                            matrix[x, y, z].cor = cor;

                        } // for z
                eixoZ = eixoX & eixoY;
                eixoX.normaliza();
                eixoY.normaliza();
                eixoZ.normaliza();
                eixoX.rotacionaVetorComPlanos(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);
                eixoY.rotacionaVetorComPlanos(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);
                eixoZ.rotacionaVetorComPlanos(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);
                for (x = 0; x < matrix.GetLength(0); x++)
                    for (y = 0; y < matrix.GetLength(1); y++)
                        for (z = 0; z < matrix.GetLength(2); z++)
                            matrix[x, y, z].multiplicaPorUmaBase(eixoX, eixoY, eixoZ);
                vetor3 dims3 = new vetor3(cena.Width, cena.Height, profundidadeObjeto);
                vetor2 dims2 = dims3.transformacaoPerspecctivaIsometrica(fatorPerspetivaIsometrica);
                Bitmap cenaRotacionada = new Bitmap((int)dims2.X, (int)dims2.Y);
                Rectangle rectBordas = new Rectangle(0, 0, cenaRotacionada.Width, cenaRotacionada.Height);
                for (x = 0; x < matrix.GetLength(0); x++)
                    for (y = 0; y < matrix.GetLength(1); y++)
                        for (z = 0; z < matrix.GetLength(2); z++)
                        {
                            vetor2 v2_0 = matrix[x, y, z].transformacaoPerspecctivaIsometrica(fatorPerspetivaIsometrica);
                            if (this.validaVetor2(rectBordas, v2_0))
                                cenaRotacionada.SetPixel((int)v2_0.X, (int)v2_0.Y, matrix[x, y, z].cor);
                        } //for z
                return cenaRotacionada;
            } // try
            catch (Exception ex)
            {
                msgErro="Erro no processamento da imagem a ser rotacionada em 3D. Motivo: "+ex.Message;
                return null;
            } // catch
     } // constroiMatriz3D()

        /// <summary>
        /// verifica se o vetor 2D está contido no Rectângulo 2D parâmetro.
        /// </summary>
        /// <param name="rect">rectângulo parâmetro.</param>
        /// <param name="v">vetor 2D a ser verificado se está contido.</param>
        /// <returns>retorna [true] se o vetor está contido, retorna [false]
        /// se o vetor não está contido pelo retângulo.</returns>
        bool validaVetor2(Rectangle rect, vetor2 v)
        {
            return ((v.X >= rect.X) && (v.X < (rect.X + rect.Width)) && (v.Y >= rect.Y) && (v.Y < (rect.Y + rect.Height)));
        }
                
        /// <summary>
        /// retira de uma matriz de vetores [vetor3] o valor de seu centro de massa.
        /// Isto permite uam rotação centralizada.
        /// </summary>
        private void centralizaMatriz()
        {

            media = new vetor3(0.0, 0.0, 0.0);
            int x, y, z;
            for (x = 0; x < matrix.GetLength(0); x++)
                for (y = 0; y < matrix.GetLength(1); y++)
                    for (z = 0; z < matrix.GetLength(2); z++)
                        if (matrix[x, y,z] != null)
                        // calcula a soma do vetores para depois mensurar a média final.
                           media = media + matrix[x, y,z];
            double n = matrix.GetLength(0) * matrix.GetLength(1);
            // calcla o produto entre um numero [n] e o vetor 3D [media], para calcular a média final.
            media = (1 / n) * media;
            for (x = 0; x < matrix.GetLength(0); x++)
                for (y = 0; y < matrix.GetLength(1); y++) 
                    for (z = 0; z < matrix.GetLength(2); z++)
                        if (matrix[x, y, z] != null)
                            matrix[x, y, z] = matrix[x, y, z] - media;
        } // centralizaMatriz();

        /// <summary>
        /// soma a média dos vetores à matriz de vetores.
        /// </summary>
        private void descentralizaMatriz()
        {
            int x, y, z;
            for (x = 0; x < matrix.GetLength(0); x++)
                for (y = 0; y < matrix.GetLength(1); y++)
                    for (z = 0; z < matrix.GetLength(2); z++)
                        if (matrix[x, y, z] != null)
                            matrix[x, y, z] = matrix[x, y, z] + media;
         } // descentralizaMatriz()

    } // class constrLinhas
} // namespace rotaciona
