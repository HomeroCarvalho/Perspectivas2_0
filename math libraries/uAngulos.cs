using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MATRIZES;

namespace rotaciona
{
    public class angulos
    {
        public static double toRadianos(double anguloEmGraus)
        {
            return (anguloEmGraus * (double)Math.PI / 180.0F);
        }

        public static double toGraus(double anguloEmRadianos)
        {
            return (anguloEmRadianos * (180.0F / (double)Math.PI));
        }

        /// <summary>
        /// rotaciona um vetor 2D em um angulo determinado de incremento.
        /// </summary>
        /// <param name="anguloEmGraus">angulo em graus de incremento, utilizado na rotacao.</param>
        /// <param name="v">vetor 2D a ser rotacionado.</param>
        /// <returns>retorna um vetor2 rotacionado em um angulo de [anguloEmGraus].</returns>
        public static vetor2 rotacionaVetor(double anguloEmGraus, vetor2 v)
        {
            vetor2 vf = new vetor2(0.0F, 0.0F);
            double angle = (double)vetor3.toRadianos(anguloEmGraus);
            double cosAngle = (double)Math.Cos(angle);
            double sinAngle = (double)Math.Sin(angle);
            vf.X = v.X * cosAngle - v.Y * sinAngle;
            vf.Y = v.Y * cosAngle + v.X * sinAngle;
            return (vf);
        } // rotacionaVetor()

        public static MATRIZES.Matriz rotacionaVetor(double anguloEmGraus, MATRIZES.Matriz mtv)
        {
            vetor2 vInicial = new vetor2((double)mtv.getElemento(0, 0), (double)mtv.getElemento(0, 1));
            vetor2 vFinal = rotacionaVetor(anguloEmGraus, vInicial);
            MATRIZES.Matriz mtFinal = new MATRIZES.Matriz(1, 2);
            mtFinal.setElemento(0, 0, (double)vFinal.X);
            mtFinal.setElemento(0, 1, (double)vFinal.Y);
            return (mtFinal);
        }
        /// <summary>
        /// muda a direção do vetor 2D para um determinado [anguloEmGraus].
        /// </summary>
        /// <param name="anguloEmGraus">novo ângulo-direção do vetor 2D de entrada.</param>
        /// <param name="v">estrutura representando o vetor 2D.</param>
        /// <returns></returns>
        public static vetor2 rotacionaVetorComAnguloAbsoluto(double anguloEmGraus, vetor2 v)
        {
            vetor2 vf = new vetor2(0.0, 0.0);
            double angle = vetor3.toRadianos(anguloEmGraus);
            double raio = Math.Sqrt(v.X * v.X + v.Y * v.Y);
            vf.X = raio * Math.Cos(angle);
            vf.Y = raio * Math.Sin(angle);

            return vf;

        } // rotacionaVetorComAnguloAbsoluto()

        /// <summary>
        /// rotaciona o vetor 3D em um determinado ângulo em seu plano XY, e detêrminado ângulo em seu eixo Z.
        /// Tais ângulos são absolutos. A rotação é feita em coordenadas esféricas.
        /// ATENÇÃO: O PLANO XY ORIGINAL FOI TROCADO PELO PLANO XZ, POIS A COORDENADA Z É A COORDENADA DE PROFUNDIDADE NOS DESENHOS.
        /// </summary>
        /// <param name="anguloXYEmGraus">ângulo absoluto para o plano XY, em Graus.</param>
        /// <param name="anguloZEmGraus">ângulo absoluto para o eixo Z.</param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static vetor3 rotacionaVetorComAnguloAbsoluto(double anguloXYEmGraus, double anguloZEmGraus, vetor3 v)
        {
            vetor3 vf = new vetor3(0.0, 0.0, 0.0);
            double angleXY = vetor3.toRadianos((double)anguloXYEmGraus);
            double angleZ = vetor3.toRadianos((double)anguloZEmGraus);
            double raio = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            vf.X = raio * Math.Sin(angleZ) * Math.Cos(angleXY);
            vf.Z = raio * Math.Sin(angleZ) * Math.Sin(angleXY);
            vf.Y = raio * Math.Cos(angleZ);

            return vf;

        } // rotacionaVetorComAnguloAbsoluto()
        /// <summary>
        /// rotaciona o vetor com acréscimos de ângulos.A rotação é feita em coordenadas
        /// esféricas modificadas (eixo altura: Y, plano base: plano XZ).
        /// </summary>
        /// <param name="anguloXYEmGraus">acréscimo de ângulo no plano XY em Graus.</param>
        /// <param name="anguloZEmGraus">ácrescimo de ângulo no eixo Z.</param>
        /// <param name="v">vetor 3D a ser rotacionado.</param>
        /// <returns>retorna o vetor [v] rotacionado com acréscimos em graus.</returns>
        public static vetor3 rotacionaVetorComAnguloRelativo(double anguloXYEmGraus, double anguloZEmGraus, vetor3 v)
        {
            // calcula os ângulos iniciais, para somar aos ângulos parâmetros para um cálculo final de [rotacionaVetorComAnguloAbsoluto]
            double anguloZEmGrausInicial = v.encontraAnguloTeta();
            double anguloXYEmGRausInicial = v.encontraAnguloOmega();
            return rotacionaVetorComAnguloAbsoluto(anguloXYEmGraus + anguloXYEmGRausInicial, anguloZEmGraus + anguloZEmGrausInicial, v);
        } // rotacionaVetorComAnguloRelativo()

    } // class angulos

} // namespace
