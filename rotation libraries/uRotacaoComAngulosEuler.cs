using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATRIZES;
using System.Drawing;
namespace rotaciona
{
        /// <summary>
        /// rotaciona os eixos 3D (extraídos com base num eixo X aparente),
        /// utilizando três ângulos Euler.
        /// </summary>
        public class rotacionaImagemComAngulosEuler
        {
            /// <summary>
            /// matriz de transição dos eixos Extraidos para os eixos de Origem.
            /// </summary>
            private Matriz mtzTransformacao = new Matriz(3, 3);
            /// <summary>
            /// eixo X 3D extraido do eixo X aparente.
            /// </summary>
            public vetor3 eixoX { get; set; }
            /// <summary>
            /// eixo Y 3D extraído do eixo X aparente, rotacionado 90 graus.
            /// </summary>
            public vetor3 eixoY { get; set; }
            /// <summary>
            /// eixo Z 3D, calculado como produto vetorial dos eixos 3D X e Y.
            /// </summary>
            public vetor3 eixoZ { get; set; }
            /// <summary>
            /// variável utilizado no cálculo da perspectiva isométrica.
            /// </summary>
            private double szPerspectiva = 2.0F;

            // CÁLCULOS:
            // EixosEntrada*mt[3,3]=EixosOrigem.
            // mt[3,3]=MatrizInversa(EixosEntrada)*EixosOrigem.
            // como EixosOrigem=I3, mt[3,3]=MatrizInversa(EixosEntrada).
            // EixosOrigemRotacionados=rotaciona(EixosOrigem).
            // EixosSaida=EixosOrigemRotacionados*MatrizInversa(mt[3,3].


            /// <summary>
            /// construtor. Constroi os eixos 3D e rotaciona estes eixos com os
            /// três angulo Euler de entrada.
            /// </summary>
            /// <param name="cena">imagem cujos eixos serão aplicados.</param>
            /// <param name="eixoXAparente">representa o eixo X aparente, retirado da imagem de entrada.</param>
            /// <param name="anguloX">ângulo X Euler.</param>
            /// <param name="anguloY">ângulo Y Euler.</param>
            /// <param name="anguloZ">ângulo Z Euler.</param>
            /// <param name="dimCells">dimensões da imagem final, já processada.</param>
            public Bitmap rotacionaComAngulosEuler(Bitmap cena, vetor2 eixoXAparente,
                                                   double anguloX, double anguloY, double anguloZ, Size dimCells)
            {
                // inicializa o eixo X com o parâmetro de entrada [eixoXAparente].
                vetor3 eixoX = new vetor3(eixoXAparente.X, eixoXAparente.Y, 0.0);

                // calcula o eixo Y como rotação de 90.0F graus do eixo X.
                vetor2 ey = angulos.rotacionaVetor(90.0F, eixoXAparente);

                // guarda o eixo Y num objeto [vetor3].
                vetor3 eixoY = new vetor3(ey.X, ey.Y, 0.0);

                // inicializa o eixo Z, como produto vetorial do eixo X e eixo Y.
                vetor3 eixoZ = new vetor3();

                // calcula o produto vetorial com o eixo X e o eixo Y.
                eixoZ.X = eixoX.Y * eixoY.Z - eixoX.Z * eixoY.Y;
                eixoZ.Y = eixoX.Z * eixoY.X - eixoX.X * eixoY.Z;
                eixoZ.Z = eixoX.X * eixoY.Y - eixoX.Y * eixoY.X;

                // faz a rotação nos três eixos.
                vetor3[] eixosTransformados = rotacaoEuler(anguloX, anguloY, anguloZ, eixoX, eixoY, eixoZ);

                // calcula o eixo X aparente através da perspectiva isométrica dos três eixos.
                vetor2 eixoX2D = new vetor2((eixosTransformados[0].X + eixosTransformados[1].X + eixosTransformados[2].X) +
                                              (eixosTransformados[0].Z + eixosTransformados[1].Z + eixosTransformados[2].Z) / this.szPerspectiva,
                                              (eixosTransformados[0].Y + eixosTransformados[1].Y + eixosTransformados[2].Y) +
                                              (eixosTransformados[0].Z + eixosTransformados[1].Z + eixosTransformados[2].Z) / this.szPerspectiva);

                // o angulo de rotação é zero porque a rotação foi feita no eixo X Aparente [eixoX2D].
                return (rotaciona.rotacionaImagemComUmEixo2D(cena, 0.0F, eixoX2D, dimCells));

            } // void entradaEixosAparente()

            /// <summary>
            /// rotaciona os eixos 3D, extraidos da imagem, com três angulos de rotação.
            /// </summary>
            /// <param name="anguloX">rotaciona o plano YZ, em radianos.</param>
            /// <param name="anguloY">rotaciona o plano XZ, em radianos.</param>
            /// <param name="anguloZ">rotaciona o plano XY, em radianos.</param>
            /// <param name="ex">eixo X a ser rotacionado.</param>
            /// <param name="ey">eixo Y a ser rotacionado.</param>
            /// <param name="ez">eixo Z a ser rotacionado.</param>
            public static vetor3[] rotacaoEuler(double anguloX, double anguloY, double anguloZ, vetor3 ex, vetor3 ey, vetor3 ez)
            {
                // guarda numa só matriz os eixos X,Y e Z retirados da Imagem
                Matriz mtzEixosEntrada = new Matriz(3, 3);
                mtzEixosEntrada.setElemento(0, 0, ex.X);
                mtzEixosEntrada.setElemento(1, 0, ex.Y);
                mtzEixosEntrada.setElemento(2, 0, ex.Z);
                mtzEixosEntrada.setElemento(0, 1, ey.X);
                mtzEixosEntrada.setElemento(1, 1, ey.Y);
                mtzEixosEntrada.setElemento(2, 1, ey.Z);
                mtzEixosEntrada.setElemento(0, 2, ez.X);
                mtzEixosEntrada.setElemento(1, 2, ez.Y);
                mtzEixosEntrada.setElemento(2, 2, ez.Z);


                // faz o calculo da matriz de transformação.
                Matriz mtzTransformacao = Matriz.MatrizInversa(mtzEixosEntrada);


                // inicializa os eixos Origem.
                vetor3 eixoXOrigem = new vetor3(1.0F, 0.0F, 0.0F);
                vetor3 eixoYOrigem = new vetor3(0.0F, 1.0F, 0.0F);
                vetor3 eixoZOrigem = new vetor3(0.0F, 0.0F, 1.0F);

                double anguloX_Z = Math.Acos(eixoXOrigem.Z / vetor3.raio(eixoXOrigem));
                double anguloY_Z = Math.Acos(eixoYOrigem.Z / vetor3.raio(eixoYOrigem));
                double anguloZ_Z = Math.Acos(eixoZOrigem.Z / vetor3.raio(eixoZOrigem));

                // rotaciona o plano YZ, em cordenadas de ângulo absoluto.
                eixoYOrigem = angulos.rotacionaVetorComAnguloAbsoluto(anguloX, anguloY_Z, eixoYOrigem);
                eixoZOrigem = angulos.rotacionaVetorComAnguloAbsoluto(anguloX, anguloZ_Z, eixoZOrigem);

                // rotaciona o plano XZ.
                eixoXOrigem =angulos.rotacionaVetorComAnguloAbsoluto(anguloY, anguloX_Z, eixoXOrigem);
                // a próxima rotação é feita sobre a rotação anterior no [eixoZOrigem].         
                eixoZOrigem.rotacionaVetor(anguloY, anguloZ_Z);

                // rotaciona o plano XY.
                // a próxima rotação é feita sobre a rotação anterior no [eixoXOrigem].
                eixoXOrigem.rotacionaVetor(anguloZ, anguloX_Z);
                // a próxima rotação é feita sobre a rotação anterior no [eixoYOrigem].
                eixoYOrigem.rotacionaVetor(anguloZ, anguloY_Z);


                // guarda nesta matriz os angulos de Origem rotacionados.
                Matriz mtEixosOrigemRotacionados = new Matriz(3, 3);
                mtEixosOrigemRotacionados.setElemento(0, 0, eixoXOrigem.X);
                mtEixosOrigemRotacionados.setElemento(1, 0, eixoXOrigem.Y);
                mtEixosOrigemRotacionados.setElemento(2, 0, eixoXOrigem.Z);
                mtEixosOrigemRotacionados.setElemento(0, 1, eixoYOrigem.X);
                mtEixosOrigemRotacionados.setElemento(1, 1, eixoYOrigem.Y);
                mtEixosOrigemRotacionados.setElemento(2, 1, eixoYOrigem.Z);
                mtEixosOrigemRotacionados.setElemento(0, 2, eixoZOrigem.X);
                mtEixosOrigemRotacionados.setElemento(1, 2, eixoZOrigem.Y);
                mtEixosOrigemRotacionados.setElemento(2, 2, eixoZOrigem.Z);

                // calcula a matriz que guarda os eixos de Origem rotacionados.
                Matriz mtEixosSaida = mtEixosOrigemRotacionados * mtzTransformacao;

                // atribui para os eixos de entrada os eixos rotacionados com ângulos Euler.
                ex = new vetor3(mtEixosSaida.getElemento(0, 0), mtEixosSaida.getElemento(1, 0), mtEixosSaida.getElemento(2, 0));
                ey = new vetor3(mtEixosSaida.getElemento(0, 1), mtEixosSaida.getElemento(1, 1), mtEixosSaida.getElemento(2, 1));
                ez = new vetor3(mtEixosSaida.getElemento(0, 2), mtEixosSaida.getElemento(1, 2), mtEixosSaida.getElemento(2, 2));

                return (new vetor3[] { ex, ey, ez });

            } // rotacionaComAngulosEuler()

        } // class rotacionaComAngulosEuler

}
