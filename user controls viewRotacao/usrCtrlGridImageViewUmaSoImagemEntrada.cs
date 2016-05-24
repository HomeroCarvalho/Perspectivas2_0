using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows;
using MATRIZES;
using rotaciona;
namespace controlsRotacao
{
    /// <summary>
    /// classe que encapsula uma lista de imagens que foram processadas na  rotação,   lista
    /// esta criada a partir de uma só imagem inicial, rotacionada com incrementos de ângulo.
    /// A lista é desenhada na tela, para fins de comparação se a perspectiva empregada
    /// está aceitável. Utilizada para rotações 2D.
    /// Por exemplo, se temos a imagem de uma   starship,  e   queremos   rotacioná-la de X 
    /// incremento de ângulo, podemos utilizar este control para visualizar o resultado das
    /// rotações.
    /// </summary>
    public class GridImageViewUmaSoImagemEntrada: UserControl
    {
        /// <summary>
        /// lista de imagens redimensionadas para se encaixar no desenho deste control.
        /// </summary>
        public List<Bitmap> lstImagens = new List<Bitmap>();
        /// <summary>
        /// lista de imagens originais, sem ser redimensionadas para fins registro,
        /// pois no futuro essas imagens terão que ser exportadas.
        /// </summary>
        public List<Bitmap> lstOriginais = new List<Bitmap>();
        
        /// <summary>
        /// dá as dimensões do tabuleiro de desenho das imagens.
        /// </summary>
        public Size gradeTela;

        /// <summary>
        /// tamanho de cada imagem dentro do tabuleiro.
        /// </summary>
        public static Size szCellGrade;
        
        /// <summary>
        /// determina o quanto do angulo de incremento para formar as imagens na tela;
        /// </summary>
        public double incrementoAngulo;
        /// <summary>
        /// contro parente na hierarquia de controls.
        /// </summary>
        private Control controlPai;

        /// <summary>
        /// guarda o eixo X aparente a ser utilizada no calculo
        /// das imagens rotacionadas.
        /// </summary>
        private vetor2 eixoX;

        /// <summary>
        /// imagem de entrada, da qual será extraída as imagens rotacionadas.
        /// </summary>
        private Bitmap cenaOriginal;

        /// <summary>
        /// serve como um Buffer de Imagem a ser desenhada na tela.
        /// guarda todas imagens rotacionadas.
        /// </summary>
        private Bitmap cenaTela;
 
        
        public GridImageViewUmaSoImagemEntrada(Control pai,PointF location, Size dimCellsGrade, Size dimGrade,
                             vetor2 eixoXAparente, Bitmap cenainicial)
        {

            // guarda a imagem que vai ser rotacionada, formando a lista de imagens.
            this.cenaOriginal = cenainicial;

            // seta a localizacao deste control.
            this.Location = new Point((int)location.X,(int)location.Y);

            // carrega o eixo X aparente.
            this.eixoX = eixoXAparente;
            
            // dimensões de cada imagem na grade do tabuleiro.
            szCellGrade = dimCellsGrade;

            
            // calcula o incremento do angulo, o quanto varia para cada imagem na tela.
            this.incrementoAngulo = 360.0F / (gradeTela.Width * gradeTela.Height);

            // seta as dimensões da cela.
            this.gradeTela = dimGrade;

            // inicializa a imagem que guardará a lista de imagens;
            this.cenaTela = new Bitmap(gradeTela.Width * szCellGrade.Width,
                                       gradeTela.Height * szCellGrade.Height);

            
            // determina o tamanho deste control.
            this.Size = new Size(this.cenaTela.Width, this.cenaTela.Height);

            // autoscroll=true para garantir que a lista seja totalmente vista, 
            // mesmo que a tela seja insuficiente para desenhar todas as imagens.
            this.AutoScroll = true;
            // control pai, para fins registro perante a hierarquia.
            this.controlPai = pai;
            // adiciona este control ao controi pai.
            pai.Controls.Add(this);
            // seta para [true] a visibilidade deste control.
            this.Visible = true;
            // calcula as imagens rotacionadas, e adiciona-as na lista de imagens.
            this.calcListaImagens();
        } // listImageView 

        /// <summary>
        /// limpa as listas de imagens, e redesenha o control.
        /// </summary>
        public void clearListImageView()
        {
            // limpa as listas de imagens.
            this.lstImagens.Clear();
            this.lstOriginais.Clear();
            // redesenha o control.
            this.Refresh();
        } // clearListImageView()

        /// <summary>
        /// recalcula as dimensões do tabuleiro, e refaz
        /// o calculo das imagens da lista.
        /// </summary>
        /// <param name="newsize"></param>
        public void setaNovasDimensoesGrade(PointF newsize)
        {
            this.clearListImageView();
            gradeTela = new Size((int)newsize.X,(int)newsize.Y);
            this.calcListaImagens();
            this.Refresh();

        } // void setaNovaDimensoesGrade()

        /// <summary>
        /// calcula as imagens rotacionadas, e as adiciona na lista de imagens.
        /// </summary>
        public void calcListaImagens()
        {
            double angulo = 0.0F;
            // forma a lista de imagens rotacionadas.
            for (int index = 0; index < (gradeTela.Width * gradeTela.Height); index++)
            {
                Bitmap cenaRotacionada = rotaciona.rotaciona.rotacionaImagemComUmEixo2D(cenaOriginal, angulo, this.eixoX, szCellGrade);

                // parte importante, pois retira da imagem bordas com cores absolutamente transparentes.
                Bitmap cenaFinal = null;
                // recorta a imagem, retirando bordas com pontos de cores totalmente transparentes.
                cenaFinal = Utils.UtilsImage.recortaImagem(cenaRotacionada);
                this.lstImagens.Add(cenaFinal);
                angulo += this.incrementoAngulo;
            } // for index

            // calcula a imagem de saida, que guarda a lista de imagens.
            this.Draw();

        } // void calcListaImagens()
        
        /// <summary>
        /// desenha dentro da imagem [cenaDesenho] a lista de imagens rotacionadas.
        /// </summary>
        /// <param name="cenaDesenho">cena de desenho</param>
        /// <param name="lstImagensRotacionadas">lista de imagens a serem desenhadas</param>
        public void Draw()
        {
            Graphics e = Graphics.FromImage(this.cenaTela);
            // limpa a tela pertencente a este control.
            e.Clear(Color.FromArgb(255, 255, 255, 255));
            int contadorImagens = 0;
            // desenha cada imagem-item, da lista [lstImagens].
            for (int y = 0; y < gradeTela.Height; y++)
                for (int x = 0; x < gradeTela.Width; x++)
                {
                    if ((this.lstImagens != null) && (this.lstImagens[contadorImagens] != null))
                    {
                        PointF location = new PointF((x * szCellGrade.Width),
                                                    y * szCellGrade.Height);
                        // dispositivo de desenho é acionado aqui.
                        e.DrawImage(this.lstImagens[contadorImagens], location);
                        contadorImagens++;
                    } // if
                } // for x
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

            // desenha a imagem que guarda a lista de imagens
            if (this.cenaTela != null)
                e.Graphics.DrawImage(this.cenaTela, new PointF(0, 0));


        } // OnPaint()
    } // class ListImageView

} // namespace rotacao3DparaFigura2D
