using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using MATRIZES;
using rotaciona;

namespace controlsRotacao
{
    /// <summary>
    /// control de construção de uma lista  de  imagens,  que 
    /// sofrerão as mesmas rotações de ângulos Euler, e serão
    /// desenhadas na tela.
    /// Utilizada para imagens distintas, todas rotacionadas
    /// para um padrão de ângulos Euler  único  para   todas
    /// imagens.
    /// Por exemplo deste grid view,um conjunto de edifícios
    /// que precisa ser construído com o  mesmo  ângulo   de
    /// perspectiva isométrica.
    /// </summary>
    public class usrCtrlGridViewVariasImagensEntrada: UserControl
    {
        // dimensões de cada imagem na grade do tabuleiro.
        private Size szCellGrade;
        // dá as dimensões de celula-imagens do tabuleiro
        private Size gradeTela;
        // cena para visualizacao do Control.
        private Bitmap cenaTela;
        // lista de imagens processadas.
        private List<Bitmap> cenasOutPut;
        private List<Bitmap> cenasInput;
        private List<vetor2> eixos2DAparentes;
        private double anguloX;
        private double anguloY;
        private double anguloZ;
        private bool bGridConstruida;
        /// <summary>
        /// construtor.
        /// </summary>
        /// <param name="location">Posição do control em relação ao seu control pai.</param>
        /// <param name="pai">control que serve de container ao control currente.</param>
        /// <param name="eixosAparentes">lista de eixos X 2D aparentes, para cada imagem.</param>
        /// <param name="cenas">lista de imagens a serem vistas.</param>
        /// <param name="anguloXabsolutoEmGraus">ângulo Euler para o eixo X 3D.</param>
        /// <param name="anguloYabsolutoEmGraus">ângulo Euler para o eixo Y 3D.</param>
        /// <param name="anguloZabsolutoEmGraus">ângulo Euler para o eixo Z 3D.</param>
        /// <param name="dimCellsLista">dimensões de cada imagem calculada, dentro do control currente.</param>
        /// <param name="dimLista">dimensões do Grid de imagens.</param>
        public usrCtrlGridViewVariasImagensEntrada(PointF location, Control pai,
                                                   List<vetor2> eixosAparentes,
                                                   List<Bitmap> cenas,
                                                   double anguloXabsolutoEmGraus,
                                                   double anguloYabsolutoEmGraus,
                                                   double anguloZabsolutoEmGraus,
                                                   Size dimCellsLista,
                                                   int dimLista)
        {
            // ____________________________________________________________________________
            //  INCIALIZA PROPRIADES DO USER CONTROL.
            // seta a localizacao deste control.
            this.Location = new Point((int)location.X,(int)location.Y);
            // seta as dimensões da grade de tela.
            this.gradeTela = new Size(cenas.Count / dimLista, dimLista);
            // seta as dimensões de cada imagem-célula do grid view.
            this.szCellGrade = dimCellsLista;
            // determina o tamanho deste control.
            this.Size = new Size(this.gradeTela.Width * this.szCellGrade.Width, this.gradeTela.Height * this.szCellGrade.Height);
            // autoscroll=true para garantir que a lista seja totalmente vista, 
            // mesmo que a tela seja insuficiente para desenhar todas as imagens.
            this.AutoScroll = true;
            // control pai, para fins registro perante a hierarquia.
            this.Parent = pai;
            // adiciona este control ao controi pai.
            this.Parent.Controls.Add(this);
            // seta para [true] a visibilidade deste control.
            this.Visible = true;
            //__________________________________________________________________________________
            // INICIALIZA ALGUMAS PROPRIEDADES RESPONSÁVEIS PELO FUNCIONAMENTO DO CONTROL.
            this.cenasInput= cenas.ToList<Bitmap>();
            this.eixos2DAparentes=eixosAparentes.ToList<vetor2>();
            this.anguloX=anguloXabsolutoEmGraus;
            this.anguloY=anguloYabsolutoEmGraus;
            this.anguloZ=anguloZabsolutoEmGraus;
            this.bGridConstruida = false;
            //___________________________________________________________________________________
            // CONSTROI A LISTA E A DESENHA
            // chama o construtor de desenho deste user control. Este método também constroi as células-imagens do grid View.
            this.Draw();
        
        }
        /// <summary>
        /// cronstrutor que constroi uma lista de figuras 2d com eixos x, y e z em 3D dados em coordenadas
        /// de angulos absolutos.
        /// </summary>
        public void constroiGridView()
        {
            // inicializa a lista de cenas-células do grid view.
            cenasOutPut = new List<Bitmap>();

            rotacionaImagemComAngulosEuler rtAngEuler = new rotacionaImagemComAngulosEuler();

            // é o núcleo da lógica deste control. rotaciona uma imagem, de acordo com seu eixo 2D aparente,
            // com angulos Euler (que são por definição absolutos, quando rotacionam a imagem);.
            for (int x = 0; x < this.eixos2DAparentes.Count; x++)
            {
                cenasOutPut.Add(rtAngEuler.rotacionaComAngulosEuler(cenasInput[x], eixos2DAparentes[x],
                                                                    anguloX, anguloY, anguloZ, this.szCellGrade));

                // esta parte é importante, pois recorta a imagem, reduzindo o seu tamanho desnecessário.
                Bitmap cenaFinal = null;
                // recorta a imagem, retirando linhas e colunas que sejam bordas, 
                // enchidos completamente com cores totalmente transparentes.
                cenaFinal=Utils.UtilsImage.recortaImagem(cenasOutPut[cenasOutPut.Count - 1]);
                cenasOutPut[cenasOutPut.Count - 1] = cenaFinal;
            } // for x
        } // void constroiGridView()

        /// <summary>
        /// recalcula as dimensões do tabuleiro, e refaz
        /// o calculo das imagens da lista.
        /// </summary>
        /// <param name="newsize"></param>
        public void setaNovasDimensoesGrade(Size newsize)
        {
            gradeTela = newsize;
            this.bGridConstruida = false;
            this.Draw();
            this.Refresh();

        } // void setaNovaDimensoesGrade()


        /// <summary>
        /// desenha dentro da imagem [cenaTela] o grid de imagens rotacionadas.  Esta
        /// imagem será a saída para tela do grid contendo todas imagens rotacionadas.
        /// </summary>
        public void Draw()
        {
            this.constroiGridView();
            // inicializa a cena de desenho do grid inteiro.
            this.cenaTela = new Bitmap(this.gradeTela.Width * this.szCellGrade.Width, this.gradeTela.Height * this.szCellGrade.Height);
            // obtem um dispositivo de desenho da cena de desenho do grid inteiro.
            Graphics e = Graphics.FromImage(this.cenaTela);
            // limpa a tela pertencente a este control.
            e.Clear(Color.FromArgb(255, 255, 255, 255));
            int contadorImagens = 0;
            // desenha cada imagem-item, do grid view.
            for (int y = 0; y < gradeTela.Height; y++)
                for (int x = 0; x < gradeTela.Width; x++)
                {
                    if ((this.cenasOutPut != null) &&
                        (contadorImagens<this.cenasOutPut.Count) &&
                        (this.cenasOutPut[contadorImagens] != null))
                    {
                        PointF posicao = new PointF((x * szCellGrade.Width), y * szCellGrade.Height);
                        // dispositivo de desenho é acionado aqui.
                        e.DrawImage(new Bitmap(this.cenasOutPut[contadorImagens], szCellGrade), posicao);
                        contadorImagens++;
                    } // if
                } // for x
            this.bGridConstruida = true;
            this.Refresh();
        } // void Draw()

        /// <summary>
        /// sobrescreve o método OnPaint da classe base [UserControl],
        /// para desenhar a lista de imagens.
        /// </summary>
        /// <param name="e">parâmetro deste evento.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // chama a classe base para realizar o redesenho perante a classe herdeira (este control).
            base.OnPaint(e);
            // desenha a imagem que guarda o grid view de imagens
            if (this.bGridConstruida)
                e.Graphics.DrawImage(this.cenaTela, new PointF(0, 0));
        } // OnPaint()
  
    } // class ustrCtrlListManyImages

} // namespace rotacao3DparaFiguras2D
