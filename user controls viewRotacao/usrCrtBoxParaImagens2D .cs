using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

using MATRIZES;
using Gzimo;
using Utils;
using rotaciona;

namespace controlsRotacao
{

    /// <summary>
    /// Control utilizado para retirar os eixos X e Y sobre
    /// uma determinada imagem,a partir da marcação de cinco pontos
    /// 1-o ponto de origem (pointOne),
    /// 2-o vetor do eixo X  (pointTwo-pointOne),
    /// 3-o vetor do eixo Y  (pointThree-pointOne).
    /// 4- o vetor do eixo Z (pointFour-pointOne);
    /// Estas informações são de vital importância de encontrar os eixos 2D aparentes, que emulam os eixos X,y e Z na tela.
    /// De mãos nestas informações, é possível rotacionar a figura 2D utilizando parâmetros isométricos 3D..
    /// Os eixos Y e Z 2D aparentes são opcionais, podem ser calculados posteriormente.
    /// </summary>
    public partial class usrCtrBxDeterminaEixos : UserControl
    {
        /// <summary>
        /// localização relativa ao control da imagem principal.
        /// </summary>
        private PointF locationImage = new PointF(20.0F, 20.0F);
        /// <summary>
        /// o cálculo é feito com a perspectiva isométrica ([true]) ou geométrica ([false]).
        /// </summary>
        private bool perspectivaIsometrica = true;
        /// <summary>
        /// ponteiro de função para qual perspectiva a ser aplicada.
        /// </summary>
        private Matriz3DparaImagem2D.transformacaoPerspectiva perspectiva;
        /// <summary>
        /// guarda a imagem a ser processada.
        /// </summary>
        private Bitmap cena;
        /// <summary>
        /// guarda tudo que será desenhado: a imagem a ser processada, os Gzimos, as linhas de vetores de eixo, a linha de movimentação.
        /// </summary>
        public Bitmap cenaInteira = null;
        /// <summary>
        /// posicao atualizada do cursor movido pelo mouse.
        /// </summary>
        public PointF posicaoCursor;
        
        /// <summary>
        /// vetor de Gzimos (4 Gzimos Cruz, 1 Gzimo eixos).
        /// </summary>
        public Gzimo.GzimoEixos[] gzimosCurrentes;
        /// <summary>
        /// gerador de números randômico para gerar aleatoriamente valores para determinar a cor currente para desenho, para
        /// enxergar melhor os gzimmos e linhas de vetores eixo.
        /// </summary>
        private Random aleatorizador = new Random(1000);
        /// <summary>
        /// raio do Gzimo eixos.
        /// </summary>
        private double raioGzimoEixos = 30;
        /// <summary>
        /// fator de divisão do eixo 3D projetado nos eixos 2D;
        /// </summary>
        private double fatorIso = 2.0;
        /// <summary>
        /// valor de incremento de cada mudança num valor de ângulo.
        /// </summary>
        private double valorIncrementoAngulo = 0.5;
        /// <summary>
        /// se [true] a posição do Gzimo eixos não pode ser modificada,
        /// se [false] o Gzimo pode se movimentar livremente.
        /// </summary>
        private bool fixaPosicao = false;
        /// <summary>
        /// guarda a lista de bases presentes no arquivo de bases e/ou control pai.
        /// </summary>
        public List<vetor3[]> lstBasesGuardadas;
        /// <summary>
        /// combo box que lista o nome de bases guardadas, e permite a seleção de uma base ortonormal.
        /// </summary>
        public ComboBox cmBxBasesGuardadas;

        /// <summary>
        /// guarda o control (uma window) que conterá, como container, o currente control.
        /// </summary>
        private Control controlContainer;

        /// <summary>
        /// tipo de ângulo para rotação para rotação dos vetores do gzimo eixos.
        /// </summary>
        public vetor3.tipoAngulo typeAngleForRotationGzimo = vetor3.tipoAngulo.Relativo;

        /// <summary>
        /// constutor.
        /// </summary>
        /// <param name="cena">Imagem para ser processada neste control (retirada dos eixos 2D).</param>
        /// <param name="location">Parâmetro de posição do canto superior esquerdo do control.</param>
        /// <param name="pai">control que servirá de container para o control currente.</param>
        ///<param name="tamanhoGzimo">determina as dimensões do Gzimo.</param>
        ///<param name="tamanhoImagens">estabelece o tamanho de cada imagem adicionada ao control</param>
        /// <param name="typeGzm">tipo de cursor especial que age sobre o control</param>
        /// <param name="extensionSize">número para aumento da área de atuação do control currente.</param>
        /// <param name="fatorIsometrico">fator de divisão do eixo 3D que é projetado nos eixos 2D.</param>
        /// <param name="tipoPerspectiva">tipo de perspectiva [true]: isométrica, [false]: geométrica.</param>
        public usrCtrBxDeterminaEixos(Bitmap _cena,
                                      PointF location,
                                      Control pai,
                                      Size tamanhoGzimo,
                                      Size tamanhoImagens,
                                      double fatorIsometrico,
                                      bool tipoPerspectiva)
        {

            if (tipoPerspectiva)
               this.perspectiva = vetor3.transformacaoPerspectivaIsometrica;
            else
               this.perspectiva = vetor3.transformacaoPerspectivaGeometrica;
            this.fatorIso = fatorIsometrico;
            this.perspectivaIsometrica = tipoPerspectiva;
            this.typeAngleForRotationGzimo = vetor3.tipoAngulo.Relativo;
            this.Location = new Point((int)location.X, (int)location.Y);
            this.cena = new Bitmap(_cena);
            this.lstBasesGuardadas = new List<vetor3[]>();
            this.cmBxBasesGuardadas = new ComboBox();
            posicaoCursor = new PointF(location.X, location.Y);
            Matriz3DparaImagem2D.transformacaoPerspectiva perspectiva=null;
            if (tipoPerspectiva)
                perspectiva= vetor3.transformacaoPerspectivaIsometrica;
            else
                perspectiva= vetor3.transformacaoPerspectivaGeometrica;
            this.gzimosCurrentes = new GzimoEixos[1];
            //________________________________________________________________________________________________________________________
            // incializa a imagem que guardará tudo para ser desenhado neste control.
            cenaInteira = new Bitmap((int)(this.cena.Width * 4.0), (int)(this.cena.Height * 4.0));
            //_________________________________________________________________________________________________________________________
      
            // seta o ponteiro de função de evento de girar a roda do mouse.
            this.MouseWheel += usrCtrBxDeterminaEixos_MouseWheel;
            // seta o ponteiro de função de eventos de clicar com o mouse sobre o control.
            this.MouseDown += usrCtrBxDeterminaEixos_MouseWheel;
            // adiciona o control à window pai.
            pai.Controls.Add(this);
            
            // calcula o tamanho do current control.
            this.Size = new Size(this.cenaInteira.Width + 20, this.cenaInteira.Height + 20);

            // seta o control container.
            this.controlContainer = pai;
            // redesenha o control.
            this.Refresh();
          
        }

        /// <summary>
        /// trata do evento de girar a roda do mouse.
        /// </summary>
        /// <param name="sender">evento de parâmetro.</param>
        /// <param name="e">evento de parâmetro.</param>
        void usrCtrBxDeterminaEixos_MouseWheel(object sender, MouseEventArgs e)
        {
            this.mouseWheel(e);    
        } // constructor usrCtrBxDeterminaEixos

        
        /// <summary>
        /// desenha uma linha quando o mouse move sobre o control,
        /// se o ponto (0,0) já foi definido.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //________________________________________________________________________________________
            // atualiza a variavel [posicaoCursor] para as coordenadas do mouse.
            posicaoCursor.X = e.X;
            posicaoCursor.Y = e.Y;
            //_________________________________________________________________________________________
            // atualiza a posição e desenha o Gzimo eixos, se este está selecionado.
            if ((this.gzimosCurrentes[0] != null) && (!fixaPosicao))
            {
                this.gzimosCurrentes[0].posicao = posicaoCursor;
                this.Refresh();
            } // if currenteGzimo==GzimoEixos
        } // void OnMouseMove()

    
        /// <summary>
        /// inicializa um novo Gzimo eixos.
        /// </summary>
        public void inicializaGzimoEixos()
        {
            this.gzimosCurrentes[0] = new GzimoEixos(
                                                    cenaInteira.Size, this.posicaoCursor, this.fatorIso,
                                                     4.0, 58.0, this, this.perspectiva,
                                                     new vetor2(this.cena.Width, this.cena.Height));

            this.gzimosCurrentes[0].cmbBxControlesGzimoEixos.SelectedIndexChanged += cmbBxControlesGzimoEixos_SelectedIndexChanged;
            this.Refresh();
            this.fixaPosicao = false;
            
        }

        /// <summary>
        /// muda o foco do control combo box para outro control, para não haver confusão do control receber o
        /// evento de mouse-roda como mudança de item, e não comando do gzimo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbBxControlesGzimoEixos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = this.gzimosCurrentes[0].btnPlanoXY;
        } // usrCtrBxDeterminaEixos()

                
        
        /// <summary>
        /// CONTROLES DO Gzimo eixos
        /// 1-Rotação Positiva da roda do mouse= aumenta a propriedade currente.
        /// 2-Rotação Negativa da roda do mouse= diminui a propriedade currente.
        ///     0- fixa/desfixa a posição do mouse (botões esquerdo e direito do mouse)
        ///     1- +/- o ângulo da base XZ do Gzimo.
        ///     2- +/- o ângulo da base YZ do Gzimo.
        ///     3- +/- o ângulo da base XY do Gzimo
        ///     4- +/- o ângulo de base cordenadas cilíndricas.
        ///     5- +/- o ângulo de profundidade cordenas cilíndricas.
        ///     6- +/- as dimensões do Gzimo.
        /// 3- Rotação por planos (XY, YZ e XZ).
        /// </summary>
        /// <param name="e">parâmetro do evento.</param>
        private void mouseWheel(MouseEventArgs e)
        {

            // cuida da lógica associada ao controle do Gzimo eixos.
            if (this.gzimosCurrentes[0] != null)
            {
                double incAngulo = this.incrementoDecrementoAnguloPelosBotoesDoMouse(e);
                if (this.gzimosCurrentes[0].bPlanoXY)
                {
                    this.gzimosCurrentes[0].rotacionaGzimoEixos(incAngulo, vetor3.planoRotacao.XY, typeAngleForRotationGzimo);
                    this.Refresh();
                }    //if bPlanoXY
                if (this.gzimosCurrentes[0].bPlanoXZ)
                {
                    this.gzimosCurrentes[0].rotacionaGzimoEixos(incAngulo, vetor3.planoRotacao.XZ, typeAngleForRotationGzimo);
                    this.Refresh();
                       
                }
                if (this.gzimosCurrentes[0].bPlanoYZ)
                {
                    this.gzimosCurrentes[0].rotacionaGzimoEixos(incAngulo, vetor3.planoRotacao.YZ, typeAngleForRotationGzimo);
                        
                }
                switch (this.gzimosCurrentes[0].cmbBxControlesGzimoEixos.SelectedIndex)
                {
                    case 0: // fixa/desfixa a posição do Gzimo.
                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            this.gzimosCurrentes[0].posicao = posicaoCursor;
                            this.fixaPosicao = true;
                        } // if e.Button
                        if (e.Button == System.Windows.Forms.MouseButtons.Right)
                            this.fixaPosicao = false;
                        this.gzimosCurrentes[0].isRotacaoPlano = false;
                        break;
                    case 1: // aumenta/diminui o ângulo da base XZ do Gzimo.
                        this.gzimosCurrentes[0].rotacionaGzimoEixos(incAngulo, vetor3.planoRotacao.XZ, typeAngleForRotationGzimo);
                        this.gzimosCurrentes[0].isRotacaoPlano = false;
                        this.Refresh();
                        break;
                    case 2: // aumenta/diminui o ângulo da base YZ do Gzimo.
                        this.gzimosCurrentes[0].rotacionaGzimoEixos(incAngulo, vetor3.planoRotacao.YZ, typeAngleForRotationGzimo);
                        this.gzimosCurrentes[0].isRotacaoPlano = false;
                        this.Refresh();
                        break;
                    case 3: // aumenta/diminui o ângulo da base XY
                        this.gzimosCurrentes[0].rotacionaGzimoEixos(incAngulo, vetor3.planoRotacao.XY, typeAngleForRotationGzimo);
                        this.gzimosCurrentes[0].isRotacaoPlano = false;
                        this.Refresh();
                        break;
                    case 4: // rotaciona com coordenadas cilíndricas.
                        incAngulo = angulos.toRadianos(incAngulo);
                        this.gzimosCurrentes[0].eixoXGzimo.rotacionaVetor(angulos.toRadianos(incAngulo), 0.0);
                        this.gzimosCurrentes[0].eixoYGzimo.rotacionaVetor(angulos.toRadianos(incAngulo), 0.0);
                        this.gzimosCurrentes[0].eixoZGzimo.rotacionaVetor(angulos.toRadianos(incAngulo), 0.0);
                        for (int x = 0; x < gzimosCurrentes[0].cordsGzimoEixos.Count; x++)
                            gzimosCurrentes[0].cordsGzimoEixos[x].rotacionaVetor(angulos.toRadianos(incAngulo), 0.0);
                        this.gzimosCurrentes[0].isRotacaoPlano = false;
                        this.Refresh();
                        break;
                    case 5: // rotaciona com coordenadas cilíndricas.
                        incAngulo = angulos.toRadianos(incAngulo);
                        this.gzimosCurrentes[0].eixoXGzimo.rotacionaVetor(0.0, angulos.toRadianos(incAngulo));
                        this.gzimosCurrentes[0].eixoYGzimo.rotacionaVetor(0.0, angulos.toRadianos(incAngulo));
                        this.gzimosCurrentes[0].eixoZGzimo.rotacionaVetor(0.0, angulos.toRadianos(incAngulo));
                        for (int x = 0; x < gzimosCurrentes[0].cordsGzimoEixos.Count; x++)
                            gzimosCurrentes[0].cordsGzimoEixos[x].rotacionaVetor(0.0, angulos.toRadianos(incAngulo));
                        this.Refresh();
                        this.gzimosCurrentes[0].isRotacaoPlano = false;
                        break;
                    case 6: // aumenta/diminui as dimensões do Gzimo.
                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                            this.raioGzimoEixos += 5.0;
                        if (e.Button == System.Windows.Forms.MouseButtons.Right)
                            this.raioGzimoEixos -= 5.0;
                        this.gzimosCurrentes[0].modificaRaioGzimoEixos(this.raioGzimoEixos);
                        this.Refresh();
                        this.gzimosCurrentes[0].isRotacaoPlano = false;
                        break;
                } // switch
            } // if currenteGzimo==GzimoEixos
        } // OnMouseDown()

        /// <summary>
        /// Desenha sobre este control: a imagem, e os pontos marcadores.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.cena != null)
            {

                foreach (Control c in this.Controls)
                    c.Refresh();
                if (this.gzimosCurrentes[0]!=null)
                {
                     
                    // desenha o Gzimo eixos    
                    this.gzimosCurrentes[0].drawGzimo(Graphics.FromImage(cenaInteira), Color.Black, this.perspectiva);
                 } //  if currenteGzimo=GzimoEixos
                if ((cena != null) && (cenaInteira!=null))
                {
                    Graphics g = e.Graphics;
                    g.DrawImage(cenaInteira, new Point(0, 0));
                    cenaInteira = new Bitmap(cenaInteira.Width, cenaInteira.Height);
                    PointF location = this.locationImage;
                    Graphics.FromImage(cenaInteira).DrawImage(cena, location);
                    } // if cena<>null
            } // if this.cena!=null
                
        }  // OnPaint()

        /// <summary>
        /// calcula o valor de retorno dos botões do mouse.
        /// </summary>
        /// <param name="e">parâmetro de evento.</param>
        /// <returns>retorna o valor de giros da roda do mouse.</returns>
        private double incrementoDecrementoAnguloPelosBotoesDoMouse(MouseEventArgs e)
        {
            double incAngulo = 0.0;
            if (e.Delta > 0)
            {
                incAngulo = valorIncrementoAngulo;
            } // if e.Delta>0
            if (e.Delta < 0)
            {
                incAngulo = -valorIncrementoAngulo;
            } // if e.Delta<0
            return incAngulo;
        } // incrementoDecrementoAnguloPelosBotoesDoMouse()

        /// <summary>
        /// desenha o gzimo eixos, de acordo com o tipo de perspectiva.
        /// </summary>
        private void desenhaGzimoEixos()
        {
            this.gzimosCurrentes[0].drawGzimo(Graphics.FromImage(cenaInteira), Color.Black, this.perspectiva);
          } // void desenhaGzimoEixos()

    } // class usrCrtlBoxParaImagem2D
}// namespace controlsRotacao
