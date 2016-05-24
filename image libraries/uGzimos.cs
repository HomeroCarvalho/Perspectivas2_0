using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MATRIZES;
using Utils;
using rotaciona;
using controlsRotacao;
namespace Gzimo
{
    public class Gzimo
    {
        /// <summary>
        /// tipos de Gzimos.
        /// </summary>
        public enum tipoGzimo { GzimoCruz, GzimoEixos }
        /// <summary>
        /// eixo X 3D. Utilizado para rotações 3D.
        /// </summary>
        public vetor3 eixoXGzimo { get; set; }
        /// <summary>
        /// eixo Y 3D. Utilizado para rotações 3D.
        /// </summary>
        public vetor3 eixoYGzimo { get; set; }
        /// <summary>
        /// eixo Z 3D. Utilizado para rotações 3D.
        /// </summary>
        public vetor3 eixoZGzimo { get; set; }
        /// <summary>
        /// eixo X aparente 2D. É extraído com o auxílio da visão humana sobre a imagem a ser processada.
        /// </summary>
        public vetor2 eixoX { get; set; }
        /// <summary>
        /// guarda as dimensões do Gzimo.
        /// </summary>
        public Size szNovo;
        /// <summary>
        /// guarda a posicao relativa ao control container.
        /// </summary>
        public PointF  posicao;

             
        /// <summary>
        /// construtor de um Gzimo genérico.
        /// </summary>
        /// <param name="szFormatoPadronizadoSaida">dimensões do Gzimo.</param>
        /// <param name="localizacao">localização em relação ao control container do Gzimo.</param>
        public Gzimo(Size szFormatoPadronizadoSaida, PointF localizacao)
        {
            this.szNovo = szFormatoPadronizadoSaida;
            this.posicao = localizacao;
        } // Gzimo()
        /// <summary>
        /// método virtual, deve ser substituído. Se não for substituído, será 
        /// lançado uma exception.
        /// </summary>
        /// <param name="g">disposito gráfico para desenho do Gzimo.</param>
        /// <param name="cor">cor do Gzimo.</param>
        public virtual void drawGzimo(Graphics g, Color cor, Matriz3DparaImagem2D.transformacaoPerspectiva perspectiva)
        {

            throw new Exception("Método não implementado!");
        } // void drawGzimo()

    } // class Gzimo


    /// <summary>
    /// Gzimo em formato de eixos tridimensional.
    /// </summary>
    public class GzimoEixos : Gzimo
    {
        /// <summary>
        /// cordenadas para desenho do Gzimo.
        /// </summary>
        public List<vetor3> cordsGzimoEixos = new List<vetor3>();
        /// <summary>
        /// índice das cordenadas para desenho do Gzimo.
        /// </summary>
        private List<int[]> indicesPontosCordsGzimoEixos = new List<int[]>();
        /// <summary>
        /// pontos para desenho dos planos meio transparentes, que auxiliarão na visualização para rotação.
        /// </summary>
        private List<PointF[]> planosXYZ = new List<PointF[]>();
        /// <summary>
        /// fator de divisão para cálculo das cordenadas 2D da tela a partir de cordenadas 3D.
        /// </summary>
        private double fatorPerspectiva;
        /// <summary>
        /// dimensões do Gzimo utilizam este size.
        /// </summary>
        public double sizeLine;
        /// <summary>
        /// centro do Gzimo.
        /// </summary>
        private vetor3 centro = null;
        /// <summary>
        /// lista de controles do Gzimo.
        /// </summary>
        public ComboBox cmbBxControlesGzimoEixos;
        /// <summary>
        /// plano currente para rotação esférica de planos.
        /// </summary>
        public vetor3.planoRotacao planoRotacaoCurrente;
        /// <summary>
        /// determina se a rotação será esférica de planos (=true) ou não (=false).
        /// </summary>
        public bool isRotacaoPlano = false;
        /// <summary>
        /// eixo X de legenda.
        /// </summary>
        public vetor3 eixoXLegenda;
        /// <summary>
        /// eixo Y de legenda.
        /// </summary>
        public vetor3 eixoYLegenda;
        /// <summary>
        /// eixo Z de legenda.
        /// </summary>
        public vetor3 eixoZLegenda;
       /// <summary>
        /// fonte associada aos textos de saída do gzimo.
        /// </summary>
        public Font fonteGzimo;
        /// <summary>
        /// tamanho dos eixos do gzimo.
        /// </summary>
        public double szLadoGzimo;
        /// <summary>
        /// ponteiro de função para o método que calcula a perspectiva (isométrica ou geométrica).
        /// </summary>
        private Matriz3DparaImagem2D.transformacaoPerspectiva perspectiva;

        /// <summary>
        /// localização relativa (forma um conjunto de coordenadas com os vetores do gzimo eixo),
        /// dos planos transparentes da legenda.
        /// /// </summary>
        private PointF locTransparentPlanesAndLegends = new PointF(10, 20);

        private bool showPlanoXY = true;
        private bool showPlanoXZ = true;
        private bool showPlanoYZ = true;

        public Button btnPlanoXY;
        public Button btnPlanoXZ;
        public Button btnPlanoYZ;

        public Boolean bPlanoXY = false;
        public Boolean bPlanoXZ = false;
        public Boolean bPlanoYZ= false;

        private Control containerPai;
        /// <summary>
        /// constroi este tipo de Gzimo (eixos).
        /// </summary>
        /// <param name="novasDims">dimensões da imagem de saída.</param>
        /// <param name="location">posicionamento do Gzimo frente ao control.</param>
        /// <param name="_fatorPerspectiva">fator de divisão da cordenada aparente na perspectiva isométrica.</param>
        /// <param name="sizeLinha">tamanho da grossura da linha.</param>
        /// <param name="sizeLadoGzimo">dimensões do gzimo.</param>
        /// <param name="pai">Control parent do Gzimo.</param>
        /// <param name="funcaoPerspectiva">ponteiro de função para cálculo da perspectiva.</param>
        /// <param name="dimsImage">dimensões da imagem de entrada.</param>
        public GzimoEixos(
            Size novasDims,
            PointF location,
            double _fatorPerspectiva, 
            double sizeLinha,
            double sizeLadoGzimo,
            Control pai,
            Matriz3DparaImagem2D.transformacaoPerspectiva funcaoPerspectiva,
            vetor2 dimsImage):base(novasDims, location)
        {
            // inicializa a fonte para ser escrito letras de identificação, como uma fonte do tipo padrão,
            this.fonteGzimo = new Font(FontFamily.Families[0].Name, 15);

            this.locTransparentPlanesAndLegends = new PointF(this.locTransparentPlanesAndLegends.X+(float)dimsImage.X,
                                                             this.locTransparentPlanesAndLegends.Y + (float)dimsImage.Y+10);
            
            // guarda o ponteiro para a função de transformação de pontos 3D 
            // para pontos 2D, utilizando a perspectiva isométrica ou geomé-
            // trica.
            this.perspectiva = funcaoPerspectiva;
            // set a posição do gzimo ante ao control pai.
            this.posicao = new PointF(location.X, location.Y);
             // seta a dimensão dos lados do Gzimo eixos.
            this.szLadoGzimo = sizeLadoGzimo;
            // seta a largura das linhas de desenho dos lados do Gzimo eixos.
            this.sizeLine = sizeLinha;
            // seta a propriedade de perspectiva (fator de divisão da cordenada de profundidade).
            this.fatorPerspectiva = _fatorPerspectiva;
            // inicializa o menu para controle da funções do Gzimo eixos.
            //***************************************************************************************************
            //  INICIA A COMBO BOX PARA CONTROLES DE ROTAÇÃO, FIXAÇÃO E REDIMENSIONAMENTO DO GZIMO CURRENTE.
            cmbBxControlesGzimoEixos = new ComboBox();
            // seta algumas propriedades do controle do Gzimo eixos.
            cmbBxControlesGzimoEixos.Size = new Size(200, 50);
            cmbBxControlesGzimoEixos.Visible = true;
            // localização física em relação ao contro pai, da list box de controles do gzimo.
            cmbBxControlesGzimoEixos.Location = new Point(5, (int)this.locTransparentPlanesAndLegends.Y + 45 + 20 + 40);
            // adiciona as opções de controle.
            cmbBxControlesGzimoEixos.Items.Add("fixar/desafixar posição do Gzimo");
            cmbBxControlesGzimoEixos.Items.Add("+/- ângulo da base XZ");
            cmbBxControlesGzimoEixos.Items.Add("+/- ângulo da base YZ");
            cmbBxControlesGzimoEixos.Items.Add("+/- ângulo da base XY");
            cmbBxControlesGzimoEixos.Items.Add("+/- ângulo base cords. cilíndricas");
            cmbBxControlesGzimoEixos.Items.Add("+/- ângulo profundidade cords. cilíndricas");
            cmbBxControlesGzimoEixos.Items.Add("+/- as dimensões do Gzimo");
            // adciona o menu de controle para o control pai.
            pai.Controls.Add(cmbBxControlesGzimoEixos);

            // prepara a imagem do botão de acionamento do plano XY.
            btnPlanoXY = new Button();
            btnPlanoXY.Size = new Size(45, 45);
            btnPlanoXY.Image = new Bitmap(45, 45);
            Graphics grphs1 = Graphics.FromImage(btnPlanoXY.Image);
            Color corQ1 = Color.FromArgb(35, Color.Red);
            grphs1.FillRectangle(new SolidBrush(corQ1), new RectangleF(0.0F, 0.0F, 45.0F, 45.0F));
            // desenha as letras dentro dos retângulo colorido
            grphs1.DrawString("XY", this.fonteGzimo, new SolidBrush(corQ1), new PointF(13.0F, 10.0F));
            btnPlanoXY.Location = new Point((int)this.locTransparentPlanesAndLegends.X + 45 * 0 + 0 * 3,
                                            (int)this.locTransparentPlanesAndLegends.Y + 40);

            // prepara a imagem do botão de acionamento do plano XZ.
            btnPlanoXZ = new Button();
            btnPlanoXZ.Size = new Size(45, 45);
            btnPlanoXZ.Image = new Bitmap(45, 45);
            Graphics grphs2 = Graphics.FromImage(btnPlanoXZ.Image);
            corQ1 = Color.FromArgb(35, Color.Green);
            grphs2.FillRectangle(new SolidBrush(corQ1), new RectangleF(0.0F, 0.0F, 45.0F, 45.0F));
            // desenha as letras dentro dos retângulo colorido
            grphs2.DrawString("XZ", this.fonteGzimo, new SolidBrush(corQ1), new PointF(13.0F, 10.0F));
            btnPlanoXZ.Location = new Point((int)this.locTransparentPlanesAndLegends.X + 45 * 1 + 1 * 3,
                                            (int)this.locTransparentPlanesAndLegends.Y + 40);


            // prepara a imagem do botão de acionamento do plano YZ.
            btnPlanoYZ = new Button();
            btnPlanoYZ.Size = new Size(45, 45);
            btnPlanoYZ.Image = new Bitmap(45, 45);
            Graphics grphs3 = Graphics.FromImage(btnPlanoYZ.Image);
            corQ1 = Color.FromArgb(35, Color.Blue);
            grphs3.FillRectangle(new SolidBrush(corQ1), new RectangleF(0.0F, 0.0F, 45.0F, 45.0F));
            // desenha as letras dentro dos retângulo colorido
            grphs3.DrawString("YZ", this.fonteGzimo, new SolidBrush(corQ1), new PointF(13.0F, 10.0F));
            btnPlanoYZ.Location = new Point((int)this.locTransparentPlanesAndLegends.X + 45 * 2 + 2 * 3,
                                            (int)this.locTransparentPlanesAndLegends.Y + 40);


            btnPlanoXY.Click += btnPlanoXY_Click;
            btnPlanoXZ.Click += btnPlanoXZ_Click;
            btnPlanoYZ.Click += btnPlanoYZ_Click;



            pai.Controls.Add(btnPlanoXY);
            pai.Controls.Add(btnPlanoXZ);
            pai.Controls.Add(btnPlanoYZ);

            ToolTip toolPlanoXY = new ToolTip();
            ToolTip toolPlanoXZ = new ToolTip();
            ToolTip toolPlanoYZ = new ToolTip();

            toolPlanoXY.SetToolTip(this.btnPlanoXY, "Clique para abilitar a rotação apenas o plano XY do cursor");
            toolPlanoXZ.SetToolTip(this.btnPlanoXZ, "Clique para abilitar a rotação apenas o plano XZ do cursor");
            toolPlanoYZ.SetToolTip(this.btnPlanoYZ, "Clique paa habilitar a rotação apenaso plano YZ' do cursor");

            this.containerPai = pai;

            //*********************************************************************************************************
            // constrói o Gzimo eixos.
            this.constroiGzimo();
            
        } // GzimoEixos()


        /// <summary>
        /// trata do evento para mostrar/esconder o plano YZ sobre o gzimo.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        void btnPlanoYZ_Click(object sender, EventArgs e)
        {
            this.bPlanoYZ = !this.bPlanoYZ;
            this.showPlanoYZ = !this.showPlanoYZ;
            this.showPlanoXY = false;
            this.showPlanoXZ = false;
            this.containerPai.Refresh();
            
        } // btnPlanoYZ_Click()

        /// <summary>
        /// trata do evento para mostrar/esconder o plano XZ sobre o gzimo.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        void btnPlanoXZ_Click(object sender, EventArgs e)
        {
            this.bPlanoXZ = !this.bPlanoXZ;
            this.showPlanoXZ = !this.showPlanoXZ;
            this.showPlanoXY = false;
            this.showPlanoYZ = false;
            this.containerPai.Refresh();
        } // btnPlanoXZ_CLick()

        /// <summary>
        /// trata do evento para mostrar/esconder o plano XY sobre o gzimo.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        void btnPlanoXY_Click(object sender, EventArgs e)
        {
            this.bPlanoXY = !this.bPlanoXY;
            this.showPlanoXY = !this.showPlanoXY;
            this.showPlanoXZ = false;
            this.showPlanoXY = false;
            this.containerPai.Refresh();
        } // btnPlanoXY_Click()

        /// <summary>
        /// desenha uma linha reta, componente do gzimo eixos.
        /// </summary>
        /// <param name="g">dispositivo gráfico da imagem do gzimo eixos.</param>
        /// <param name="v1">vetor 3D inicial.</param>
        /// <param name="v2">vetor 3D final.</param>
        /// <param name="pincel">Caneta para o desenho da linha, determina a cor da linha.</param>
        private void drawLinePerspective(Graphics g,
                                        vetor3 v1, vetor3 v2,
                                         Pen pincel)
        {
            vetor2 v2_i = perspectiva(v1, fatorPerspectiva);
            vetor2 v2_f = perspectiva(v2, fatorPerspectiva);
            // desenha a linha currente do Gzimo eixos.      
            g.DrawLine(pincel,
                (float)posicao.X + (float)v2_i.X,
                (float)posicao.Y + (float)v2_i.Y,
                (float)posicao.X + (float)v2_f.X,
                (float)posicao.Y + (float)v2_f.Y);
        } // drawLinePerspective()


        /// <summary>
        /// constrói os eixos que formarão o eixos tridimensional necessário
        /// para mensurar as rotações que corrigirão a  posição  do  objeto
        /// 3D alvo.
        /// </summary>
        /// <returns></returns>
        public void constroiGzimo()
        {
            // inicializa os eixos da base ortonormal.
            this.eixoXGzimo = new vetor3(1.0, 0.0, 0.0);
            this.eixoYGzimo = new vetor3(0.0, 1.0, 0.0);
            this.eixoZGzimo = new vetor3(0.0, 0.0, 1.0);

            // cálculos utilizados para as cordenadas do Gzimo eixos.
            this.eixoXGzimo = this.szLadoGzimo * this.eixoXGzimo;
            this.eixoYGzimo = this.szLadoGzimo * this.eixoYGzimo;
            this.eixoZGzimo = this.szLadoGzimo * this.eixoZGzimo;
            
            if (this.cordsGzimoEixos == null)
                this.cordsGzimoEixos = new List<vetor3>();
            // constrói as cordenadas do Gzimo eixos.
            this.cordsGzimoEixos.Clear();
            this.cordsGzimoEixos.Add(new vetor3(0.0, 0.0, 0.0));
            this.cordsGzimoEixos.Add(this.eixoXGzimo);
            this.cordsGzimoEixos.Add(this.eixoYGzimo);
            this.cordsGzimoEixos.Add(this.eixoXGzimo + this.eixoYGzimo);
            this.cordsGzimoEixos.Add(this.eixoZGzimo);
            this.cordsGzimoEixos.Add(this.eixoXGzimo + this.eixoZGzimo);
            this.cordsGzimoEixos.Add(this.eixoYGzimo + this.eixoZGzimo);
            this.cordsGzimoEixos.Add(this.eixoXGzimo + this.eixoYGzimo + this.eixoZGzimo);
            // eixos de rotação e de base ortonormal.
            this.eixoXGzimo = new vetor3(1.0, 0.0, 0.0);
            this.eixoYGzimo = new vetor3(0.0, 1.0, 0.0);
            this.eixoZGzimo = new vetor3(0.0, 0.0, 1.0);
            // fixa os eixos de legenda.
            this.eixoXLegenda = new vetor3(this.eixoXGzimo);
            this.eixoYLegenda = new vetor3(this.eixoYGzimo);
            this.eixoZLegenda = new vetor3(this.eixoZGzimo);
            // redimensiona os eixos de legenda.
            this.eixoXLegenda = 45.0 * this.eixoXLegenda;
            this.eixoYLegenda = 45.0 * this.eixoYLegenda;
            this.eixoZLegenda = 45.0 * this.eixoZLegenda;
            this.calculaCentroEAtualiza();

            this.indicesPontosCordsGzimoEixos.Add(new int[] { 1, 0});
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 2, 0 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 1, 3 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 2, 3 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 4, 6 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 1, 5 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 4, 6 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 4, 5 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 5, 7 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 6, 7 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 2, 6 });
            this.indicesPontosCordsGzimoEixos.Add(new int[] { 3, 7 });
        } // constroieixos

        /// <summary>
        /// calcula o centro, e retira o centro de todos
        /// os pontos que formam o GzimoEixos currente.
        /// </summary>
        public void calculaCentroEAtualiza()
        {
            int x;
            this.centro = new vetor3(0.0, 0.0, 0.0);
            for (x = 0; x<this.cordsGzimoEixos.Count; x++)
            {

                this.centro.X += this.cordsGzimoEixos[x].X;
                this.centro.Y += this.cordsGzimoEixos[x].Y;
                this.centro.Z += this.cordsGzimoEixos[x].Z;
            } // for x
            this.centro.X /= (double)this.cordsGzimoEixos.Count;
            this.centro.Y /= (double)this.cordsGzimoEixos.Count;
            this.centro.Z /= (double)this.cordsGzimoEixos.Count;
            for (x = 0; x < this.cordsGzimoEixos.Count; x++)
                this.cordsGzimoEixos[x] = this.cordsGzimoEixos[x] - centro;
        } // calculaCentro

        /// <summary>
        /// Descentraliza o Gzimo, somando seu centro de massa. 
        /// </summary>
        private void calculaCentroEDescentraliza()
        {
            if (this.centro == null)
                this.calculaCentroEAtualiza();
            int x;
            for (x = 0; x < this.cordsGzimoEixos.Count; x++)
                this.cordsGzimoEixos[x] = this.cordsGzimoEixos[x] + centro;
        } // void descentraliza()

        /// <summary>
        /// rotaciona o Gzimo eixos em um determinado plano (XZ,YZ ou XY).
        /// </summary>
        /// <param name="omega">ângulo para rotacionar o plano parâmetro, deve estar em graus.</param>
        /// <param name="pln">plano para rotacionar.</param>
        public void rotacionaGzimoEixos(double omega, vetor3.planoRotacao pln, vetor3.tipoAngulo typeAngle)
        {

            double omegaX, omegaY, omegaZ;
            omegaX = 0.0;
            omegaY = 0.0;
            omegaZ = 0.0;
            switch (pln)
            {
                case vetor3.planoRotacao.XY:
                    omegaX = 0.0;
                    omegaY = 0.0;
                    omegaZ = omega;
                    break;
                case vetor3.planoRotacao.XZ:
                    omegaX = 0.0;
                    omegaY = omega;
                    omegaZ = 0.0;
                    break;
                case vetor3.planoRotacao.YZ:
                    omegaX = omega;
                    omegaY = 0.0;
                    omegaZ = 0.0;
                    break;
            } // switch
            if (typeAngle == vetor3.tipoAngulo.Relativo)
            {
                // rotaciona os eixos.
                this.eixoXGzimo.rotacionaVetor(omegaY, omegaX, omegaZ);
                this.eixoYGzimo.rotacionaVetor(omegaY, omegaX, omegaZ);
                this.eixoZGzimo.rotacionaVetor(omegaY, omegaX, omegaZ);
                this.eixoXGzimo.normaliza();
                this.eixoYGzimo.normaliza();
                this.eixoZGzimo.normaliza();
            } // if typeAngle
            if (typeAngle == vetor3.tipoAngulo.Absoluto)
            {
                // rotaciona os eixos.
                this.eixoXGzimo.rotacionaVetorAnguloAbsoluto(omegaZ, omegaX, omegaY);
                this.eixoYGzimo.rotacionaVetorAnguloAbsoluto(omegaZ, omegaX, omegaY);
                this.eixoZGzimo.rotacionaVetorAnguloAbsoluto(omegaZ, omegaX, omegaY);
                this.eixoXGzimo.normaliza();
                this.eixoYGzimo.normaliza();
                this.eixoZGzimo.normaliza();
           
            } // if typeAngle
        }// rotacionaGzimmoeixos()

        /// <summary>
        /// modifica os raios de todos os pontos do gzimo eixos.
        /// </summary>
        /// <param name="nRaio"></param>
        public void modificaRaioGzimoEixos(double nRaio)
        {
            int x;
            for (x = 0; x < this.cordsGzimoEixos.Count; x++)
            {
                this.cordsGzimoEixos[x].modificaRaio(nRaio);
            } // for x
        } // modificaRaioGzimo()

        /// <summary>
        /// converte um vetor 2D em um PointF.
        /// </summary>
        /// <param name="v">vetor 2D a ser convertido.</param>
        /// <returns>retorna um PointF com as cordenadas do vetor 2D.</returns>
        /// <param name="offsetPos">offset de deslocamento a ser colocado, útil quando a vetor 2D faz uma base ortonormal.</param>
        private PointF vetor2ToPointF(vetor2 v,PointF offsetPos)
        {
            return new PointF((float)(v.X+offsetPos.X), (float)(v.Y+offsetPos.Y));
        } // vetor2ToPointF()
              
        /// <summary>
        /// desenha num dispositivo gráfico o Gzimo na forma de um eixos,
        /// para auxiliar as rotações.
        /// </summary>
        /// <param name="g">dispositivo gráfico para desenho.</param>
        /// <param name="cor">cor do Gzimo.</param>
        /// <param name="perspectiva">ponteiro para função de cálculo da perspectiva.</param>
        public override void drawGzimo(Graphics g, Color cor, Matriz3DparaImagem2D.transformacaoPerspectiva perspectiva)
        {
            
            // desenho ds plano XZ no gzimo.
            if (this.showPlanoXZ)
                this.drawATransparentPlane(new Point((int)this.locTransparentPlanesAndLegends.X + 45 * 0 + 0 * 3,
                                                     (int)this.locTransparentPlanesAndLegends.Y), g,
                                                     Color.FromArgb(35, Color.Green),
                                                     new vetor2(this.posicao.X, this.posicao.Y),
                                                     6, 2, 3, 7);
            // desenho do plano YZ no gzimo.
            if (this.showPlanoYZ)
                this.drawATransparentPlane(new PointF((int)this.locTransparentPlanesAndLegends.X + 45 * 1 + 1 * 3,
                                                      (int)this.locTransparentPlanesAndLegends.Y), g,
                                                      Color.FromArgb(35, Color.Blue),
                                                      new vetor2(this.posicao.X, this.posicao.Y),
                                                       6, 2, 0, 4);
            // desenho do plano XY no gzimo.
            if (this.showPlanoXY)
                this.drawATransparentPlane(new PointF((int)this.locTransparentPlanesAndLegends.X + 45 * 2 + 2 * 3,
                                                      (int)this.locTransparentPlanesAndLegends.Y), g,
                                                      Color.FromArgb(35, Color.Red),
                                                      new vetor2(this.posicao.X, this.posicao.Y),
                                                      2, 3, 1, 0);
            
            // desenha a legenda de orientaçâo do gzimo.
            this.drawLegend(g, new vetor2(this.locTransparentPlanesAndLegends.X - 45-20,
                                          this.locTransparentPlanesAndLegends.Y + 45+20),
                            new vetor3(0.0, 0.0, 0.0));

            Pen pincel = new Pen(cor, (float)this.sizeLine);
            for (int x = 0; x < this.indicesPontosCordsGzimoEixos.Count; x++)
            {
                // destaca o ponto que representa o eixo Z.
                if ((indicesPontosCordsGzimoEixos[x][0] == 0) || (indicesPontosCordsGzimoEixos[x][1] == 0))
                    pincel.Color = Color.Blue;
                else
                {
                    // destaca os pontos que se ligam até a origem dos eixos do Gzimo eixos.
                    if ((this.indicesPontosCordsGzimoEixos[x][0] == 6) || (this.indicesPontosCordsGzimoEixos[x][1] == 6))
                    {
                        if ((indicesPontosCordsGzimoEixos[x][0] == 4) || (indicesPontosCordsGzimoEixos[x][1] == 4))
                            pincel.Color = Color.Red;
                        if ((indicesPontosCordsGzimoEixos[x][0] == 7) || (indicesPontosCordsGzimoEixos[x][1] == 7))
                            pincel.Color = Color.Green;
                        if ((indicesPontosCordsGzimoEixos[x][0] == 2) || (indicesPontosCordsGzimoEixos[x][1] == 2))
                            pincel.Color = Color.Blue;
                    } // if indicesPontosCordsGzimoEixos[x][0]==0
                    else
                    {
                        // partes do eixos que não são eixos
                        pincel.Color = cor;
                    } // else
                } // else
                if (((this.indicesPontosCordsGzimoEixos[x][0] == 6) || (this.indicesPontosCordsGzimoEixos[x][1] == 6)) &&
                    ((this.indicesPontosCordsGzimoEixos[x][0] == 4) || (this.indicesPontosCordsGzimoEixos[x][1] == 4) ||
                     (this.indicesPontosCordsGzimoEixos[x][0] == 7) || (this.indicesPontosCordsGzimoEixos[x][1] == 7) ||
                     (this.indicesPontosCordsGzimoEixos[x][0] == 2) || (this.indicesPontosCordsGzimoEixos[x][1] == 2))) 
                {
                    // inicializa os vetores para desenho.
                    vetor3 v3_0 = new vetor3(this.cordsGzimoEixos[indicesPontosCordsGzimoEixos[x][0]]);
                    vetor3 v3_1 = new vetor3(this.cordsGzimoEixos[indicesPontosCordsGzimoEixos[x][1]]);
                    // multiplica os vetores para desenho pela base ortonormal currente.
                    v3_0.multiplicaPorUmaBase(this.eixoXGzimo, this.eixoYGzimo, this.eixoZGzimo);
                    v3_1.multiplicaPorUmaBase(this.eixoXGzimo, this.eixoYGzimo, this.eixoZGzimo);
                    // desenha a linha entre os vetores para desenho.
                    this.drawLinePerspective(g, v3_0, v3_1, pincel);
                } // if 
                
            } // for x
            pincel.Dispose();
        } // void DrawGzimo().
        /// <summary>
        /// desenha uma legenda de orientação dos eixos cartesianos.
        /// desenha também retângulos coloridos de acordo com cada plano e letras indicando o nome do plano (XZ,YZ, ou XY).
        /// </summary>
        /// <param name="g">dispositivo gráfico para desenho da legenda.</param>
        /// <param name="locLegenda">posição da legenda, em relação ao Control pai.</param>
        /// <param name="vOrigemLegenda">origem dos eixos legenda.</param>
        private void drawLegend(Graphics g, vetor2 locLegenda, vetor3 vOrigemLegenda)
        {
            Pen pincel = new Pen(Color.Black, 2.5F);
            vetor2 v2ini = vetor3.transformacaoPerspectivaIsometrica(vOrigemLegenda, fatorPerspectiva);
            vetor2 v2fini = vetor3.transformacaoPerspectivaIsometrica(eixoXLegenda, fatorPerspectiva);
            
            pincel.Color = Color.Green;
            g.DrawLine(pincel,
                (float)locLegenda.X + (float)v2ini.X,
                (float)locLegenda.Y - (float)v2ini.Y,
                (float)locLegenda.X + (float)v2fini.X,
                (float)locLegenda.Y - (float)v2fini.Y);
            g.DrawString("X", this.fonteGzimo, new SolidBrush(Color.Green), new PointF((float)locLegenda.X + (float)v2fini.X + 5,
                                                                                       (float)locLegenda.Y - (float)v2fini.Y));
            v2fini = vetor3.transformacaoPerspectivaIsometrica(eixoYLegenda, fatorPerspectiva);
            pincel.Color = Color.Red;
            g.DrawLine(pincel,
                (float)locLegenda.X + (float)v2ini.X,
                (float)locLegenda.Y - (float)v2ini.Y,
                (float)locLegenda.X + (float)v2fini.X,
                (float)locLegenda.Y - (float)v2fini.Y);
            g.DrawString("Y", this.fonteGzimo, new SolidBrush(Color.Red), new PointF((float)locLegenda.X + (float)v2fini.X,
                                                                                     (float)locLegenda.Y - (float)v2fini.Y - 5));

            v2fini = vetor3.transformacaoPerspectivaIsometrica(eixoZLegenda, fatorPerspectiva);
            pincel.Color = Color.Blue;
            g.DrawLine(pincel,
                (float)locLegenda.X + (float)v2ini.X,
                (float)locLegenda.Y - (float)v2ini.Y,
                (float)locLegenda.X + (float)v2fini.X,
                (float)locLegenda.Y - (float)v2fini.Y);
            g.DrawString("Z", this.fonteGzimo, new SolidBrush(Color.Blue), new PointF((float)locLegenda.X + (float)v2fini.X + 6,
                                                                                      (float)locLegenda.Y - (float)v2fini.Y - 6));

            
        }// drawLegend()

        /// <summary>
        /// desenha no dispositivo gráfico do gzimo eixos, um plano quase
        /// transparente, útil para visualização qual plano é qual.
        /// </summary>
        /// <param name="g">dispositivo gráfico para desenho.</param>
        /// <param name="corQ1">cor do plano. Deve ser bem transparente.</param>
        /// <param name="numerosIndicesEixoeixos">índices do eixos gráfico, que determina o plano.</param>
        public void drawATransparentPlane(PointF location, Graphics g, Color corQ1,
            vetor2 locOffset,params int[] numeroIndicesEixoeixos)
        {
            if (numeroIndicesEixoeixos.Length != 0)
            {
                List<PointF> pontosPlano = new List<PointF>();
                for (int x = 0; x < numeroIndicesEixoeixos.Length; x++)
                {
                    vetor3 v = this.cordsGzimoEixos[numeroIndicesEixoeixos[x]].Clone();
                    v.multiplicaPorUmaBase(this.eixoXGzimo, this.eixoYGzimo, this.eixoZGzimo);
                    pontosPlano.Add(vetor2ToPointF(this.perspectiva(
                                                                v,
                                                                this.fatorPerspectiva),
                                                                new PointF((float)locOffset.X, (float)locOffset.Y)));
                } // for x
                GraphicsPath path = new GraphicsPath(pontosPlano.ToArray(), new byte[]{
                                                                (byte)PathPointType.Line,
                                                                (byte)PathPointType.Line,
                                                                (byte)PathPointType.Line,
                                                                (byte)PathPointType.Line});
                Region rg = new Region(path);
                g.FillRegion(new SolidBrush(corQ1), rg);
            } // if numeroIndicesEixoeixos.Length!=0
        } // drawATransparentPlane()
    } // class GzimoEixos

    /// <summary>
    /// classe utilizada para ordenação de vetores [vetor3], calculando
    /// a distância do vetor até a origem dos eixos.
    /// </summary>
    class comparerVetor3 : IComparer<vetor3>
    {
        int IComparer<vetor3>.Compare(vetor3 v1, vetor3 v2)
        {
            double m1 = v1.modulo();
            double m2 = v2.modulo();
            if (m1 < m2)
                return  -1;
            if (m1 > m2)
                return +1;
            return 0;
        } // Compare()
    } // comparerVetor3
    /// <summary>
    /// Gzimo 2D em formato de cruz.
    /// </summary>
    class GzimoCruz: Gzimo
    {
        double xi, yi, xm1, ym1, xm2, ym2, xf, yf;
        private Bitmap cenaDeAtuacao = null;
     
        public GzimoCruz(Size novaDims, PointF _posicao,Bitmap cena)
            : base(novaDims, _posicao)
        {
            this.szNovo = novaDims;
            this.constroiGzimo();
            this.cenaDeAtuacao= cena;
        } // GzimoCruz()

        /// <summary>
        /// constroi a imagem do Gzimo em cruz.
        /// </summary>
        /// <param name="rect">dimensões e localização do Gzimo em Cruz.</param>
        /// <param name="cor">cor de frente da imagem do Gzimo.</param>
        private void constroiGzimo()
        {
            RectangleF rect = new RectangleF(posicao.X, posicao.Y, szNovo.Width, szNovo.Height);

            //  A TRAVE VERTICAL.
            xi = 0 + rect.Width / 2;
            yi = 0;

            xm1 = 0 + rect.Width / 2;
            ym1 = rect.Height;

            // A TRAVE HORIZONTAL.
            xm2 = 0;
            ym2 = 0 + rect.Height / 2;

            xf = 0 + rect.Width;
            yf = rect.Height / 2;
         
        } // constroi Gzimo.

        
        /// <summary>
        /// desenha Gzimo em cruz.
        /// </summary>
        /// <param name="g">dispositivo de imagem a ser utilizado.</param>
        /// <param name="cor">cor do Gzimo.</param>
        public override void drawGzimo(Graphics g, Color cor, Matriz3DparaImagem2D.transformacaoPerspectiva perspectiva)
        {
            //___________________________________________________________________________________
            // calcula os pontos que serão utilizados no desenho do Gzimo.
            PointF ini1 = new PointF((float)(xi + posicao.X - szNovo.Width / 2),
                                    (float)(yi + posicao.Y - szNovo.Height / 2));
            PointF fini1 = new PointF((float)(posicao.X + xm1 - szNovo.Width / 2),
                                   (float)( posicao.Y + ym1 - szNovo.Height / 2));
            PointF ini2 = new PointF((float)(xm2 + posicao.X - szNovo.Width / 2),
                                    (float)(ym2 + posicao.Y - szNovo.Height / 2));
            PointF fini2 = new PointF((float)(posicao.X + xf - szNovo.Width / 2),
                                     (float)(posicao.Y + yf - szNovo.Height / 2));
            //_______________________________________________________________________________________
           
            // seleciona um pincel com cor [cor] e grossura do pincel 3.5F
            Pen pincel = new Pen(cor, 3.5F);
           
            // desenha o Gzimo.
            g.DrawLine(pincel,ini1,fini1);
            g.DrawLine(pincel,ini2, fini2);
        } // drawGzimo()
    } // class GzimoCruz
} // namespace Gzimo.
