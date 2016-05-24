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
        public vetor3[, ,] matrix;

        public bool isMatrizJaGerada = false;
        /// <summary>
        /// média de vetores 3D da matrix. utilizada para retirar/adicionar o centro de massa da matrix 3D que mapeará a imagem 3D para rotacionar.
        /// </summary>
        private vetor3 media = new vetor3(0.0, 0.0, 0.0);

        public delegate  vetor2 transformacaoPerspectiva(vetor3 v, double fatorPerspectiva);

        private double xmin, ymin;
        private vetor3 v3;
        private vetor2 v2_0;

        /// <summary>
        /// constroi e rotaciona uma lista de vetores 3D a partir de uma imagem e vetor3 eixos determinados pelo aplicativo, e alguns parâmetros.
        /// </summary>
        /// <param name="eixoX">eixo X 3D calculado com ferramentas como o gzimo eixos.</param>
        /// <param name="eixoY">eixo Y 3D calculado com ferramentas como o gzimo eixos.</param>
        /// <param name="eixoZ">eixo Z 3D calculado com ferramentas como o gzimo eixos.</param>
        /// <param name="cena">imagem a ser processada.</param>
        /// <param name="profundidadeObjeto">profundidade imaginada pelo usuário para a imagem 3D a ser rotacionada.</param>
        /// <param name="anguloXYEmGraus">ângulo de rotação do plano XY.</param>
        /// <param name="anguloYZEmGraus">ângulo de rotação do plano YZ.</param>
        /// <param name="anguloXZEmGraus">ângulo de rotação do plano XZ.</param>
        /// <param name="msgErro">mensagens de erro durante o processamento da imagem para matriz rotacionada.</param>
        /// <param name="isAnguloAbsoluto">se [true], os ângulos não são incrementos, mas marcam posições absolutas.</param>
        /// <param name="isUsaEixoZCalculado">se [true], o eixo Z é calculado pelo produto vetorial entre os 
        ///                                   eixos X e Y, e o ângulo entre esses eixos.</param>
        /// <param name="funcPerspecitva">função de transformação para o efeito de perspectiva (transformação de dados 3D para geometria 2D.</param>
        /// <param name="fatorPerspectiva">utiliza o fator de perspectiva para incrementar o método de transformação de perspectiva.</param>
        ///<returns>retorna a imagem rotacionada.</returns>
        public Bitmap rotacionaMatriz3D(
            vetor3 eixoX,
            vetor3 eixoY,
            vetor3 eixoZ,
            Bitmap cena,
            double profundidadeObjeto,
            double anguloXYEmGraus,
            double anguloYZEmGraus,
            double anguloXZEmGraus,
            ref string msgErro,
            bool isAnguloAbsoluto,
            bool isUsaEixoZCalculado,
            transformacaoPerspectiva funcPerspectiva,
            double fatorPerspectiva)
        {
            vetor3 eixo_i = new vetor3(eixoX);
            vetor3 eixo_j = new vetor3(eixoY);
            vetor3 eixo_k = new vetor3(eixoZ);
            msgErro = "";
            if (msgErro != "")
                return null;
            if (isUsaEixoZCalculado)
            {
                // calcula o eixo Z a partir do produto vetorial e do ângulo entre os eixos X e Y.É um grande avanço!
                double anguloXY = Math.Acos(1 / ((eixoX.modulo() * eixoY.modulo())) * (eixoX * eixoY));
                // faz o produto vetorial e divide pelos modulos dos eixos X e Y.
                eixoZ = (Math.Sin(anguloXY) / (eixoX.modulo() * eixoY.modulo())) * (eixoX & eixoY);
            } // if isUsaEixoZCalculado

            // normaliza os três eixos.
            eixo_i.normaliza();
            eixo_j.normaliza();
            eixo_k.normaliza();
            // inicia a imagem a ser rotacionada.
            Bitmap cenaRotacionada = new Bitmap(6 * cena.Width, 6 * cena.Height);
            if (isAnguloAbsoluto)
            {
                // calcula os ângulos iniciais, que são retirados para formar um ângulo absoluto com os ângulos parâmetros.
                double anguloXYEixoXinicial = angulos.toGraus(eixo_i.encontraAnguloOmega(vetor3.planoRotacao.XY));
                double anguloXZEixoXinicial = angulos.toGraus(eixo_i.encontraAnguloOmega(vetor3.planoRotacao.XZ));
                double anguloYZEixoXInicial = angulos.toGraus(eixo_i.encontraAnguloOmega(vetor3.planoRotacao.YZ));

                double anguloXYEixoYinicial = angulos.toGraus(eixo_j.encontraAnguloOmega(vetor3.planoRotacao.XY));
                double anguloXZEixoYinicial = angulos.toGraus(eixo_j.encontraAnguloOmega(vetor3.planoRotacao.XZ));
                double anguloYZEixoYInicial = angulos.toGraus(eixo_j.encontraAnguloOmega(vetor3.planoRotacao.YZ));

                double anguloXYEixoZinicial = angulos.toGraus(eixo_k.encontraAnguloOmega(vetor3.planoRotacao.XY));
                double anguloXZEixoZinicial = angulos.toGraus(eixo_k.encontraAnguloOmega(vetor3.planoRotacao.XZ));
                double anguloYZEixoZInicial = angulos.toGraus(eixo_k.encontraAnguloOmega(vetor3.planoRotacao.YZ));

                // rotaciona os eixos cartesianos com acrescimos de ângulos, que também são ângulos absolutos.
                eixo_i.rotacionaVetorAnguloAbsoluto(anguloXYEmGraus + anguloXYEixoXinicial, -anguloYZEmGraus + anguloYZEixoXInicial, -anguloXZEmGraus + anguloXZEixoXinicial);
                eixo_j.rotacionaVetorAnguloAbsoluto(anguloXYEmGraus + anguloXYEixoYinicial, -anguloYZEmGraus + anguloYZEixoYInicial, -anguloXZEmGraus + anguloXZEixoYinicial);
                eixo_k.rotacionaVetorAnguloAbsoluto(anguloXYEmGraus + anguloXYEixoZinicial, -anguloYZEmGraus + anguloYZEixoZInicial, -anguloXZEmGraus + anguloXZEixoZinicial);                // normaliza os três eixos.
                eixo_i.normaliza();
                eixo_j.normaliza();
                eixo_k.normaliza();
                if (!this.isMatrizJaGerada)
                {
                    this.iniciaMatriz3D(
                            eixoX,
                            eixoY,
                            eixoZ,
                            cena,
                            profundidadeObjeto,
                            fatorPerspectiva,
                            funcPerspectiva,
                            ref msgErro);
                } // if !isMatrizJaGerada
            
            } // if isAnguloAbsoluto
            else
            {   // cálculo ângulo relativo.
                // rotaciona os eixos-base, portanto uma rotação com acréscimos de ângulos.
                eixo_i.rotacionaVetor(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);
                eixo_j.rotacionaVetor(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);
                eixo_k.rotacionaVetor(anguloXZEmGraus, anguloYZEmGraus, anguloXYEmGraus);

                if (!this.isMatrizJaGerada)
                {
                    this.iniciaMatriz3D(
                            eixo_i,
                            eixo_j,
                            eixo_k,
                            cena,
                            profundidadeObjeto,
                            fatorPerspectiva,
                            funcPerspectiva,
                            ref msgErro);
                } // if !isMatrizJaGerada
                if (msgErro != "")
                    return null;
            } // else
            return (this.calcImagemComMatrix3D(
                eixo_i,
                eixo_j,
                eixo_k,
                funcPerspectiva,
                fatorPerspectiva, 
                cenaRotacionada,cena,
                profundidadeObjeto, 
                ref msgErro));

        } // rotacionaMatriz3D()

        /// <summary>
        /// constrói a matriz 3D associado à imagem 2D de entrada.
        /// </summary>
        /// <param name="eixoX">eixo X, imaginado com o gzimo eixos.</param>
        /// <param name="eixoY">eixo Y, imaginado com o gzimo eixos.</param>
        /// <param name="eixoZ">eixo Z, imaginado com o gzimo eixos.</param>
        /// <param name="isUsaEixoZCalculado">calcula o eixo Z, se [true];</param>
        /// <param name="profundidadeObjeto">cumprimento no eixo Z da matriz 3D.</param>
        /// <param name="fatorPerspectiva">parâmetro de cálculo da perspectiva (isométrica ou geométrica).</param>
        /// <param name="funcPerspectiva">método a ser utilizada na perspectiva (isométrica ou geométrica).</param>
        /// <param name="msgErro">string guardando mensagens de erro geradas pelo método.</param>
        public void iniciaMatriz3D(
            vetor3 eixoX,
            vetor3 eixoY,
            vetor3 eixoZ,
            Bitmap cena,
            double profundidadeObjeto,
            double fatorPerspectiva,
            Matriz3DparaImagem2D.transformacaoPerspectiva funcPerspectiva,
            ref string msgErro)
        {
            this.isMatrizJaGerada = true;
            int x, y, z;
            vetor2 v2_0 = new vetor2(0.0, 0.0);
            try
            {
                Rectangle rectOldBordas = new Rectangle(0, 0, cena.Width, cena.Height);
                this.matrix = new vetor3[cena.Width, cena.Height, (int)profundidadeObjeto];
                xmin = 100000.0;
                ymin = 100000.0;
                // calcula o ponto mínimo (xmin,ymin)
                for (x = 0; x < matrix.GetLength(0); x++)
                    for (y = 0; y < matrix.GetLength(1); y++)
                        for (z = 0; z < matrix.GetLength(2); z++)
                        {
                            // inicia o ponto 3D.
                            matrix[x, y, z] = new vetor3(x, y, z);
                            vetor3 v3_0 = new vetor3(matrix[x, y, z]);
                            v3_0.multiplicaPorUmaBase(eixoX, eixoY, eixoZ);
                            v2_0 = funcPerspectiva(v3_0, fatorPerspectiva);
                            if (v2_0.X < xmin)
                                xmin = v2_0.X;
                            if (v2_0.Y < ymin)
                                ymin = v2_0.Y;
                        } // for z
                // faz a ida, calculando o ponto 3D e através da perspectiva neste ponto, calcula a cor da imagem neste ponto 3D.
                for (x = 0; x < matrix.GetLength(0); x++)
                    for (y = 0; y < matrix.GetLength(1); y++)
                        for (z = 0; z < matrix.GetLength(2); z++)
                        {

                            vetor3 v3_0 = new vetor3(matrix[x, y, z]);
                            v3_0.multiplicaPorUmaBase(eixoX, eixoY, eixoZ);
                            // calcula a perspectiva sobe o ponto 3D.
                            v2_0 = funcPerspectiva(v3_0, fatorPerspectiva);
                            if (((v2_0.X - xmin) < cena.Width) && 
                                ((v2_0.Y - ymin) < cena.Height) &&
                                ((v2_0.X-xmin)>=0) &&
                                ((v2_0.Y-ymin)>=0))
                            {
                                Color cor = cena.GetPixel((int)(v2_0.X - xmin), (int)(v2_0.Y - ymin));
                                matrix[x, y, z].cor = cor;
                            } // if
                           
                        } // for z

            } // try
            catch (Exception ex)
            {
                msgErro = "Erro ao iniciar o objeto 3D associado à imagem de entrada. Mensagem de erro: " + ex.Message;
            } // catch

        } // iniciaMatriz3D()

        public Bitmap calcImagemComMatrix3D(vetor3 eixoX, vetor3 eixoY, vetor3 eixoZ,
            Matriz3DparaImagem2D.transformacaoPerspectiva funcPerspectiva,
            double fatorPerspectiva, Bitmap cenaRotacionada, Bitmap cena, double profundidadeObjeto, ref string msgErro)
        {
            try
            {
                Bitmap cenaSaida = null;
                int x, y, z;
                v2_0 = null;
                if (!this.isMatrizJaGerada)
                    this.iniciaMatriz3D(eixoX, eixoY, eixoZ, cena, profundidadeObjeto, fatorPerspectiva, funcPerspectiva, ref msgErro);
                // malha principal, mapeia a matriz rotacionada e seta as cores das coordenadas calculadas.
                for (z = 0; z < this.matrix.GetLength(2); z++)
                    for (y = 0; y < this.matrix.GetLength(1); y++)
                        for (x = 0; x < this.matrix.GetLength(0); x++)
                        {
                            v3 = new vetor3(this.matrix[x, y, z]);
                            v3.multiplicaPorUmaBase(eixoX, eixoY, eixoZ);
                            v2_0 = funcPerspectiva(v3, fatorPerspectiva);
                            if (((int)(v2_0.X - xmin) < cena.Width) &&
                                ((int)(v2_0.Y - ymin) < cena.Height) &&
                                ((v2_0.X - xmin) >= 0) &&
                                ((v2_0.Y - ymin) >= 0)) 
                                // seta a cor na coordenada 2D perspectiva isométrica calculada a partir do ponto 3D matrix[x,y,z].
                                cenaRotacionada.SetPixel((int)(v2_0.X - this.xmin), (int)(v2_0.Y - this.ymin), this.matrix[x, y, z].cor);
                           } // for x
                msgErro = "";
                cenaSaida = Utils.UtilsImage.recortaImagem(cenaRotacionada);
                return cenaSaida;
            } // try
            catch (Exception ex)
            {
                msgErro = "Erro no processamento da imagem, em sua inicialização. Mensagem de Erro: " + ex.Message;
                return null;
            } // catch
        } // calcImagemComMatrix3D()

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
                        if (matrix[x, y, z] != null)
                            // calcula a soma do vetores para depois mensurar a média final.
                            media = media + matrix[x, y, z];
            double n = matrix.GetLength(0) * matrix.GetLength(1)*matrix.GetLength(2);
            // calcla o produto entre um numero [1/n] e o vetor 3D [media], para calcular a média final.
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

    } // class Matriz3SDparaImagem2D
} // namespace rotaciona
