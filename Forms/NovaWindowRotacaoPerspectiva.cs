using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MATRIZES;
using Gzimo;
using rotaciona;

namespace rotacao3DparaFiguras2D
{
    public partial class windowPerspectivas : Form
    {
        /// <summary>
        /// guarda a imagem de entrada currente.
        /// </summary>
        public Bitmap cena = null;


        /// <summary>
        /// guarda a imagem aplicada com eixos rotacionados, da imagem de entrada currente.
        /// </summary>
        public Bitmap cenaProcessada = null;
        
        /// <summary>
        /// control que mostra a imagem currente, e processa os eixos relativos.
        /// </summary>
        private controlsRotacao.usrCtrBxDeterminaEixos ctrViewRotacao;

       
        /// <summary>
        /// ângulo em graus, rotação plano XZ, ângulo de Euler.
        /// </summary>
        private double anguloRotacaoPlanoXZ = 0.0;

        /// <summary>
        /// ângulo em graus, rotação plano YZ, ângulo de Euler.
        /// </summary>
        private double anguloRotacaoPlanoYZ = 0.0;

        /// <summary>
        /// ângulo em graus, rotação plano XY, ângulo de Euler.
        /// </summary>
        private double anguloRotacaoPlanoXY = 0.0;

        /// <summary>
        /// profundidade imaginada pelo usuário para a imagem 3D.
        /// </summary>
        private double profundidadeImaginada = 0.0;

        /// <summary>
        /// fator modificativo que entra nos cálculos de perspectiva (isométrica ou geométrica).
        /// </summary>
        private double fatorPerspectiva = 2.0;
        /// <summary>
        /// path do arquivo de bases ortonormais.
        /// </summary>
        private string fileNameBasesOrtonormais = "basesOrtonormais.txt";

        /// <summary>
        /// guarda a matriz de rotação 3D para imagens 2D (o core do aplicativo).
        /// </summary>
        private Matriz3DparaImagem2D mtx3Dto2D = null;

        /// <summary>
        /// localização da imagem que sofrerá rotações.
        /// </summary>
        private Point locImagemSendoTrabalhada = new Point(0, 100);

        /// <summary>
        /// as rotações são com ângulos relativos ou absolutos?
        /// </summary>
        private vetor3.tipoAngulo typeAngle = vetor3.tipoAngulo.Relativo;
        //*******************************************************************************************************************
        private Size szImgPadrao= new Size(100,100);


        private double rotacaoInicialCurrente = 0.0;
        private double rotacaoFinalCurrente = 0.0;
        private int numeroDeImagensCurrente = 0;

        // lista de informações para faixas de rotação,utilizada na 2a. aba do frame principal.
        private List<infoAnimacaoRotacao> infoAnimacoes = new List<infoAnimacaoRotacao>();
        

        //*********************************************************************************************************************
        /// <summary>
        /// construtor básico.
        /// </summary>
        public windowPerspectivas()
        {
            InitializeComponent();
            // coloca o cursor.default para o cursor currente do formulário.
            this.Cursor = Cursors.Default;
            this.lstImgImagensRotacionar3D = new ImageList();
            this.lstImgImagensRotacionar3D.ImageSize = this.szImgPadrao;
            this.typeAngle= vetor3.tipoAngulo.Relativo;
            this.cmbBxTipoPerspectiva.SelectedIndex = 0;
            this.cmbBxTipoDeAngulo.SelectedIndex = 0;
            this.Refresh();
        } //windowPerspectivas()

        //***********************************************************************************************************************************************
        //  TRATADORES DE EVENTOS PARA A PAGE ANIMAÇÕES
        //***********************************************************************************************************************************************
        
        /// <summary>
        /// salva a última imagem 2D gerada, e rotacionada.
        /// </summary>
        /// <param name="sender">parâmetro do evento.</param>
        /// <param name="e">parâmetro do evento.</param>
        private void salvarImagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // cria um novo dialog de salvar um arquivo.
            SaveFileDialog svFlDlg = new SaveFileDialog();
            // filtro para arquivos desejáveis.
            svFlDlg.Filter = "PNG Images|*.png|Bitmap (BMP)|*.bmp|JPEG Images|*.jpg|GIF Images|*.gif";
            try
            {
                if (svFlDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // salva a última imagem da listagem de imagens geradas.
                    String nomeBaseFile = svFlDlg.FileName.Split(new String[] { "." },StringSplitOptions.RemoveEmptyEntries)[0];
                    String tipoImagem = svFlDlg.FileName.Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries)[1];
                    for (int x = 0; x < this.lstImgImagensRotacionar3D.Images.Count; x++)
                    {
                        // salva todas imagens da lista de imagens geradas pelo aplicativo.
                        this.lstImgImagensRotacionar3D.Images[this.lstImgImagensRotacionar3D.Images.Count - 1].Save(
                             Path.GetFullPath(nomeBaseFile+x+"."+tipoImagem));
                    } // for x
                    this.mostraMensagem("imagem salva com sucesso.");
                } // if svFlDlg.ShowDialog()
            } // try
            catch
            {
                this.mostraMensagem("Erro no método de salvar a Imagem.Imagem não foi salva.");
            } // catch

        } // validaNumero()
        /// <summary>
        /// carrega uma imagem em perspectiva, é a imagem 2D a ser associada à matriz 3D de rotação.
        /// </summary>
        /// <param name="sender">parâmetro do evento.</param>
        /// <param name="e">parâmetro do evento.</param>
        private void carregarImagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // cria um dialogo de carregar um arquivo.
            OpenFileDialog opFlDlg = new OpenFileDialog();
            // filtro para arquivos desejáveis.
            opFlDlg.Filter = "PNG Images|*.png|Bitmap (BMP)|*.bmp|JPEG Images|*.JPEG|GIF Images|*.gif";
            if (opFlDlg.ShowDialog() == DialogResult.OK)
                try
                {
                    {
                        // abre o arquivo de imagem, escolhida pelo usuario
                        this.cena = new Bitmap(Bitmap.FromFile(opFlDlg.FileName),this.szImgPadrao);
                        // instancia o control de visualizacao de image e processamento.
                        if (this.ctrViewRotacao != null) this.ctrViewRotacao.Dispose();
                        bool perspectivaIsometrica = true;
                        // constroi um control para visualização da rotação 2D da figura.
                        // este control permite a entrada do ângulo da rotação mais  al-
                        // gumas outras opções.
                        this.ctrViewRotacao = new controlsRotacao.usrCtrBxDeterminaEixos(this.cena,
                                                                         new PointF(0, 50),
                                                                         this, new Size(30, 30),
                                                                         this.cena.Size,
                                                                         this.fatorPerspectiva,
                                                                         perspectivaIsometrica);

                        // inicializa o gzimo eixos (não há opção para o gzimo cruz).
                        this.ctrViewRotacao.gzimosCurrentes = new GzimoEixos[1];
                        this.ctrViewRotacao.inicializaGzimoEixos();

                        // prepara a matriz de mapeamento.
                        this.mtx3Dto2D = null;
                        // inicializa a combo box de bases guardadas.
                        this.cmbBxBasesGuardadas.Items.Clear();

                        // limpa a lista das imagens geradas pelo aplicativo.
                        this.lstImgImagensRotacionar3D.Images.Clear();

                        // inicializa as combo box de tipo de perspectiva e tipo de ângulo.
                        this.cmbBxTipoPerspectiva.SelectedIndex = 0;
                        this.cmbBxTipoDeAngulo.SelectedIndex = 0;

                        // adiciona o control ao formulário.
                        this.Controls.Add(this.ctrViewRotacao);
                        this.ctrViewRotacao.lstBasesGuardadas = new List<vetor3[]>();
                        // carrega as bases ortonormais do arquivo.
                        this.carregaBasesOrtonormais();
                        // redesenha o formulário, desta vez com o control [this.ctrViewRotacao] adicionado.
                        this.Refresh();
                        this.mostraMensagem("Imagem carregada com sucesso.");
                    } // if opFlDlg.ShowDialog
                } // try
                catch (Exception ex)
                {
                    this.mostraMensagem("Erro no carregamento da imagem. Mensagem Erro: " + ex.Message);
                } // catch
        } // carregarImagemToolStripMenuItem_Click() 

        /// <summary>
        /// redesenha o control.
        /// </summary>
        /// <param name="e">parâmetro de evento.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // desenha os controls dentro da window.
            foreach (Control c in this.Controls)
                c.Refresh();
            // desenha a lista de imagens resultantes, já processadas.
            if (lstImgImagensRotacionar3D != null)
                for (int x = 0; x < lstImgImagensRotacionar3D.Images.Count; x++)
                {
                    e.Graphics.DrawImage(this.lstImgImagensRotacionar3D.Images[x],
                              new PointF(x * lstImgImagensRotacionar3D.Images[x].Width,
                                         this.Height - lstImgImagensRotacionar3D.Images[x].Height - 50));
                } // for x
        } // void OnPaint()

        /// <summary>
        /// rotaciona com o Gzimo Eixos.
        /// </summary>
        private void rotacionaImagemComGzimoEixos()
        {
            try
            {
                if (cmbBxTipoDeAngulo.SelectedIndex == 1)
                {

                    this.ctrViewRotacao.gzimosCurrentes[0].eixoXGzimo.rotacionaVetor(
                        this.anguloRotacaoPlanoXZ,
                        this.anguloRotacaoPlanoYZ,
                        this.anguloRotacaoPlanoXY);

                    this.ctrViewRotacao.gzimosCurrentes[0].eixoYGzimo.rotacionaVetor(
                                this.anguloRotacaoPlanoXZ,
                                this.anguloRotacaoPlanoYZ,
                                this.anguloRotacaoPlanoXY);

                    this.ctrViewRotacao.gzimosCurrentes[0].eixoZGzimo.rotacionaVetor(
                        this.anguloRotacaoPlanoXZ,
                        this.anguloRotacaoPlanoYZ,
                        this.anguloRotacaoPlanoXY);
                  
                    this.mostraMensagem("gzimo eixo rotacionado com sucesso.");
                    this.ctrViewRotacao.Refresh();
                } // if cmbBxTipoDeAngulo.SelectedIndex==1
                if (cmbBxTipoDeAngulo.SelectedIndex == 0)
                {
                    try
                    {
                        string msgErro = "";
                        
                        Bitmap imagemProcessada = null;
                            this.mtx3Dto2D = new Matriz3DparaImagem2D();
                            Matriz3DparaImagem2D.transformacaoPerspectiva perspectiva = vetor3.transformacaoPerspectivaIsometrica;
                            if (this.cmbBxTipoPerspectiva.SelectedIndex == 0)
                                perspectiva = vetor3.transformacaoPerspectivaIsometrica;
                            if (this.cmbBxTipoPerspectiva.SelectedIndex == 1)
                             perspectiva = vetor3.transformacaoPerspectivaGeometrica;
                            // constroi uma imagem processada utilizando ângulos de rotação em coordenadas esféricas.
                            imagemProcessada = this.mtx3Dto2D.rotacionaMatriz3D(
                              this.ctrViewRotacao.gzimosCurrentes[0].eixoXGzimo,
                              this.ctrViewRotacao.gzimosCurrentes[0].eixoYGzimo,
                              this.ctrViewRotacao.gzimosCurrentes[0].eixoZGzimo,
                              cena,
                              this.profundidadeImaginada,
                              this.anguloRotacaoPlanoXY,
                              this.anguloRotacaoPlanoYZ,
                              this.anguloRotacaoPlanoXZ,
                              ref msgErro, (typeAngle == vetor3.tipoAngulo.Absoluto),
                              false,
                              perspectiva,
                              this.fatorPerspectiva);
                            this.mostraMensagem("imagem rotacionada com sucesso.");
              
                        // caso haja algum erro no processamento, mostra uma mensagem ao usuário.
                        if (msgErro != "")
                            this.mostraMensagem(msgErro);
                        else
                        {
                            // dispõe no vídeo a imagem processada.
                            this.lstImgImagensRotacionar3D.Images.Add(imagemProcessada);
                            this.Refresh();
                        } // else
                    } // try
                    catch (Exception ex)
                    {
                        this.mostraMensagem("Erro na rotação da imagem.Msg: " + ex.Message);
                    } // catch
                } // if cmbBxTipoDeAngulo.SelectedIndex==0
            } // try
            catch (Exception ex)
            {
                this.mostraMensagem("Erro na rotação da imagem. Msg: " + ex.Message);
            } // catch
        } // rotacionaGzimoEixosO()


        /// <summary>
        /// sai do aplicativo, gravando antes as bases ortonormais.
        /// </summary>
        /// <param name="sender">parâmetro do evento.</param>
        /// <param name="e">parâmetro do evento.</param>
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gravarBasesOrtonormais();
            System.Environment.Exit(0);
        } // sairToolStripMenuItem_Click()

        /// <summary>
        /// grava no arquivo as bases ortonormais.
        /// </summary>
        private void gravarBasesOrtonormais()
        {
            FileStream fl = null;
            TextWriter flstrm = null;

            try
            {
                fl = new FileStream(Path.GetFullPath(this.fileNameBasesOrtonormais), FileMode.Create);
                fl.Close();
                flstrm = new StreamWriter(this.fileNameBasesOrtonormais);
                for (int x = 0; x < ctrViewRotacao.lstBasesGuardadas.Count; x++)
                {
                    flstrm.WriteLine(this.cmbBxBasesGuardadas.Items[x].ToString());

                    this.gravaVetor3D(ctrViewRotacao.lstBasesGuardadas[x][0], flstrm);
                    this.gravaVetor3D(ctrViewRotacao.lstBasesGuardadas[x][1], flstrm);
                    this.gravaVetor3D(ctrViewRotacao.lstBasesGuardadas[x][2], flstrm);
                } // for x
                this.mostraMensagem("bases ortonormais guardadas no arquivo com sucesso.");
                flstrm.Close();

            } // try
            catch (Exception ex)
            {
                this.mostraMensagem("Erro ao salvar bases Ortonormais." + ex.Message);
                flstrm.Close();
            } // catch
            
        } // gravarBasesOrtonormais()

        public void gravaVetor3D(vetor3 v, TextWriter flStmWtr)
        {
            flStmWtr.WriteLine(v.X);
            flStmWtr.WriteLine(v.Y);
            flStmWtr.WriteLine(v.Z);
        } // gravaVetor3D()

        /// <summary>
        /// carrega do arquivo as bases ortonormais.
        /// </summary>
        private void carregaBasesOrtonormais()
        {
            try
            {
                StreamReader flstrm = new StreamReader(Path.GetFullPath(this.fileNameBasesOrtonormais));
                if (!flstrm.EndOfStream)
                {
                    int x = 0;
                    while (!flstrm.EndOfStream)
                    {
                        string nomeBaseOrtonormal = flstrm.ReadLine();

                        ctrViewRotacao.lstBasesGuardadas.Add(new vetor3[3]);
                        ctrViewRotacao.lstBasesGuardadas[x][0] = new vetor3();
                        ctrViewRotacao.lstBasesGuardadas[x][1] = new vetor3();
                        ctrViewRotacao.lstBasesGuardadas[x][2] = new vetor3();
                        this.carregaVetor3D(ref ctrViewRotacao.lstBasesGuardadas[x][0], flstrm);
                        this.carregaVetor3D(ref ctrViewRotacao.lstBasesGuardadas[x][1], flstrm);
                        this.carregaVetor3D(ref ctrViewRotacao.lstBasesGuardadas[x][2], flstrm);

                        this.cmbBxBasesGuardadas.Items.Add(nomeBaseOrtonormal);
                        x++;
                    } // while !flstrm.EndOfStream
                } // if !flstrm.EndOfStream
                flstrm.Close();
                this.mostraMensagem("base ortonormais recuperadas do arquivo com sucesso.");                        
            } // try
            catch
            {
                File.Create(this.fileNameBasesOrtonormais);
                mostraMensagem("Arquivo de bases ortonormais criado.");
                this.carregaBasesOrtonormais();
            } // catch

        } // carregaBasesOrtonormais()

        private void carregaVetor3D(ref vetor3 v, TextReader flTxtRdr)
        {
            v.X = double.Parse(flTxtRdr.ReadLine());
            v.Y = double.Parse(flTxtRdr.ReadLine());
            v.Z = double.Parse(flTxtRdr.ReadLine());
        } // carregaVetor3D()

        /// <summary>
        /// carrega uma Base Ortonormal para o Gzimo eixos.
        /// </summary>
        private void carregaUmaBaseortonormal()
        {
            try
            {
                // obtém o índice da base ortormal a ser carregada para o gzimo eixos.
                int index= cmbBxBasesGuardadas.SelectedIndex;
                // muda a base ortonormal currente do gzimo eixos.
                ctrViewRotacao.gzimosCurrentes[0].eixoXGzimo = ctrViewRotacao.lstBasesGuardadas[index][0];
                ctrViewRotacao.gzimosCurrentes[0].eixoYGzimo = ctrViewRotacao.lstBasesGuardadas[index][1];
                ctrViewRotacao.gzimosCurrentes[0].eixoZGzimo = ctrViewRotacao.lstBasesGuardadas[index][2];
                this.mostraMensagem("base ortonormal carregada com sucesso.");
                ctrViewRotacao.Refresh();
            } // try
            catch (Exception ex)
            {
                mostraMensagem("Erro ao carregar base ortonormal. Mensagem de Erro: " + ex.Message);
            } // catch
        } // carregaUmaBaseortonormal()

        /// <summary>
        /// mostra mensagens de status para todas as pages do aplicativo.
        /// </summary>
        /// <param name="msg"></param>
        private void mostraMensagem(string msg)
        {
            if (this.TabControlFuncoesDoAplicativo.SelectedIndex == 0)
                this.lblMsgStatus.Text = msg;
            if (this.TabControlFuncoesDoAplicativo.SelectedIndex == 1)
                this.lblStatusMsgsPageRotacoes.Text = msg;
        } // mostraMensagem()


        /// <summary>
        /// valida a conversão de um valor string para um double [valor], mostra mensagem de erro se for invalidado.
        /// </summary>
        /// <param name="msg">mensagem de erro a ser mostrada.</param>
        /// <param name="valorNome">valor string a ser validado.</param>
        /// <param name="valor">double de retorno.</param>
        /// <returns>[true] se a validação for bem sucedida, [false] se não.</returns>
        private bool validaNumero(string msg, string valorNome, ref double valor)
        {
            try
            {
                valor = double.Parse(valorNome);
                return true;
            } // try
            catch (Exception ex)
            {
                this.mostraMensagem(msg + " Mensagem de Erro: " + ex.Message);
                return false;
            } // catch
        } // validaNumero()

        /// <summary>
        /// mostra mensagens de validações de ângulos
        /// </summary>
        private bool msgs()
        {

            if (this.txtBxAnguloRotacaoPlanoXZ.Text == "")
            {
                this.mostraMensagem("Entre com um ângulo de rotação para o plano de base da imagem.");
                return false;
            } // if
            if (this.txtBxAnguloRotaçãoPlanoYZ.Text == "")
            {
                this.mostraMensagem("Entre com um ângulo de rotação para o eixo de altura da imagem.");
                return false;
            }// if
            if (!validaNumero("Entre com um valor válido para o ângulo do plano XY", this.txtBxAnguloRotacaoPlanoXY.Text, ref this.anguloRotacaoPlanoXY))
                return false;
            if (!validaNumero("Entre com um valor válido para o ângulo do plano YZ", this.txtBxAnguloRotacaoPlanoXY.Text, ref this.anguloRotacaoPlanoYZ))
                return false;
            if (!validaNumero("Entre com um valor válido para o ângulo do plano XZ", this.txtBxAnguloRotacaoPlanoXY.Text, ref this.anguloRotacaoPlanoXZ))
                return false;
            return true;
        }
        //***********************************************************************************************************************************************
        //  TRATADORES DE EVENTOS PARA A PAGE ANIMAÇÕES
        //***********************************************************************************************************************************************

        /// <summary>
        /// valida a entrada do início das rotações.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void txtBxRotacaoInicial_Validated(object sender, EventArgs e)
        {
            if (!this.validaNumero("Erro na entrada de dados da rotação inicial. ", this.txtBxRotacaoInicial.Text, ref rotacaoInicialCurrente))
                rotacaoInicialCurrente = (-10000.0);
        } // txtBxRotacaoInicial_Validated()

        /// <summary>
        /// valida a entrada do fim das rotações.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void txtBxRotacaoFinal_Validated(object sender, EventArgs e)
        {
            if (!this.validaNumero("Erro na entrada de dados da rotação final. ", this.txtBxRotacaoFinal.Text, ref rotacaoFinalCurrente))
                rotacaoFinalCurrente = (-10000.0);
        } // txtBxRotacaoFinal_Validated()

        /// <summary>
        /// guarda a faixa de rotação na lista de rotações.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnGuardarRotacaoInicialFinal_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.numeroDeImagensCurrente < 0)
                {
                    this.mostraMensagem("Entre com um número de imagens a serem geradas válido");
                    return;
                }

                if (this.rotacaoInicialCurrente > this.rotacaoFinalCurrente)
                {
                    this.mostraMensagem("rotação de início é maior que rotação de fim!");
                    return;
                } // if 

                if (this.rotacaoInicialCurrente < -9999)
                {
                    this.mostraMensagem("Ainda há erros na entrada de dados.");
                    return;
                } // if
                if (this.rotacaoFinalCurrente < -9999)
                {
                    this.mostraMensagem("Ainda há erros na entrada de dados.");
                    return;
                } // if
                int planoDeRotacaoCurrente = cmbBxPlanoDeRotacao.SelectedIndex;
                if ((planoDeRotacaoCurrente < 0) || (planoDeRotacaoCurrente > 2))
                {
                    this.mostraMensagem("Entre com um plano de rotação.");
                    return;
                } // if 
                double[] faixaRotacoes = new double[2];
                // guarda a faixa de rotações
                faixaRotacoes[0] = this.rotacaoInicialCurrente;
                faixaRotacoes[1] = this.rotacaoFinalCurrente;

                Matriz3DparaImagem2D.transformacaoPerspectiva _perspectiva;
                _perspectiva= vetor3.transformacaoPerspectivaIsometrica;
                if (cmbBxTipoPerspectiva.SelectedIndex==0)
                    _perspectiva=vetor3.transformacaoPerspectivaIsometrica;
                if (cmbBxTipoPerspectiva.SelectedIndex==1)
                    _perspectiva=vetor3.transformacaoPerspectivaGeometrica;

                infoAnimacaoRotacao.planosRotacao planoRot;
                switch (planoDeRotacaoCurrente)
                {
                    case (0):
                        planoRot = infoAnimacaoRotacao.planosRotacao.XY;
                        break;
                    case (1):
                        planoRot = infoAnimacaoRotacao.planosRotacao.XZ;
                        break;
                    case (2):
                        planoRot = infoAnimacaoRotacao.planosRotacao.YZ;
                        break;
                    default:
                        planoRot = infoAnimacaoRotacao.planosRotacao.XY;
                        break;
                }
                this.infoAnimacoes.Add(new infoAnimacaoRotacao(faixaRotacoes[0], faixaRotacoes[1],
                                                               numeroDeImagensCurrente,
                                                               planoRot,
                                                               this.cena,
                                                               80.0,
                                                               2.0,
                                                               _perspectiva,
                                                               this.typeAngle == vetor3.tipoAngulo.Absoluto));
              
                // guarda a faixa de rotações no componente visual.
                List<string> lstTmpRotacoesGuardadas = new List<string>();
                lstTmpRotacoesGuardadas = this.txtBxRotacoesGuardadas.Lines.ToList<string>();
                lstTmpRotacoesGuardadas.Add(this.infoAnimacoes[this.infoAnimacoes.Count-1].ToString());
                // redimensiona as linhas do componente visual.
                this.txtBxRotacoesGuardadas.Lines = lstTmpRotacoesGuardadas.ToArray<string>();
                // redesenha o componente visual.
                this.txtBxRotacoesGuardadas.Refresh();
                this.mostraMensagem("faixa de rotações guardadas com sucesso.");
            } // try
            catch (Exception ex)
            {
                this.mostraMensagem("Erro no processamento dos dados. Mensagem de erro: " + ex.Message);
            } // catch
        } // btnGuardarRotacaoInicialFinal_Click()

        /// <summary>
        /// trata do duplo clicar no text box que guarda as faixas de rotações, 
        /// removendo a faixa de rotações que esteja selecionada no text box.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void txtBxRotacoesGuardadas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           List<string> lstTmpRotacoesGuardadas= new List<string>();
            // remove o texto selecionado do componente visual textBox.
           lstTmpRotacoesGuardadas.Remove(txtBxRotacoesGuardadas.SelectedText);
           // redimensiona o componente visual textBox.
           this.txtBxRotacoesGuardadas.Lines = lstTmpRotacoesGuardadas.ToArray<string>();
           // redesenha o componente visual.
           this.mostraMensagem("faixa de rotações removida.");
           this.txtBxRotacoesGuardadas.Refresh();
        }

        /// <summary>
        /// trata do evento de salvar planejamento de rotações.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnSalvarRotacoes_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svFlDlg = new SaveFileDialog();
                svFlDlg.Filter = "*.txt|rotation file";
                if (svFlDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream strm = new FileStream(Path.GetFullPath(svFlDlg.FileName), FileMode.Create, FileAccess.Write);
                    StreamWriter strmWrtr = new StreamWriter(strm);
                    for (int x = 0; x < this.infoAnimacoes.Count; x++) 
                    {
                        strmWrtr.WriteLine(this.infoAnimacoes[x].limiteMinInfo);
                        strmWrtr.WriteLine(this.infoAnimacoes[x].limiteMaxInfo);
                        strmWrtr.WriteLine(this.infoAnimacoes[x].numeroDeImagens);
                        strmWrtr.WriteLine(this.infoAnimacoes[x].planoRotacao);
                    } // for x
                    strmWrtr.Close();
                    strm.Close();
                    this.mostraMensagem("Arquivo de rotações salvo.");
                } // if svFlDlg.ShowDialog()
            }  // try
            catch (Exception ex)
            {
                this.mostraMensagem("Erro na gravação do arquivo de rotações. Mensagem de erros: "+ex.Message);
            } // catch
        } // btnSalvarRotacoes_Click()

        /// <summary>
        /// carrega de um arquivo um planejamento de rotações.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnCarregarRotacoes_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnFlDlg = new OpenFileDialog();
            opnFlDlg.Filter = "*.txt|rotation file";
            if (opnFlDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream strm = new FileStream(Path.GetFullPath(opnFlDlg.FileName), FileMode.Create, FileAccess.Write);
                StreamReader strmRdtr = new StreamReader(strm);
                Matriz3DparaImagem2D.transformacaoPerspectiva _perspectiva;
                _perspectiva = vetor3.transformacaoPerspectivaIsometrica;
                if (cmbBxTipoPerspectiva.SelectedIndex == 0)
                    _perspectiva = vetor3.transformacaoPerspectivaIsometrica;
                if (cmbBxTipoPerspectiva.SelectedIndex == 1)
                    _perspectiva = vetor3.transformacaoPerspectivaGeometrica;

                while (!strmRdtr.EndOfStream)
                {
                    double[] _limitesRotacao = new double[2];
                    
                    // carrega do arquivo os dados da infoAnimacao.
                    _limitesRotacao[0] = double.Parse(strmRdtr.ReadLine());
                    _limitesRotacao[1] = double.Parse(strmRdtr.ReadLine());
                    int _numeroDeImagens = int.Parse(strmRdtr.ReadLine());
                    int _planoDeRotacao = int.Parse(strmRdtr.ReadLine());
                    infoAnimacaoRotacao.planosRotacao planoRot;
                    switch (_planoDeRotacao)
                    {
                        case 0:
                            planoRot = infoAnimacaoRotacao.planosRotacao.XY;
                            break;
                        case 1:
                            planoRot = infoAnimacaoRotacao.planosRotacao.XZ;
                            break;
                        case 2:
                            planoRot = infoAnimacaoRotacao.planosRotacao.YZ;
                            break;
                        default:
                            planoRot = infoAnimacaoRotacao.planosRotacao.XY;
                            break;
                    } // switch _planoDeRotacao

                    // inicia uma infoAnimacao.
                    infoAnimacaoRotacao infoAnim = new infoAnimacaoRotacao(
                                                            _limitesRotacao[0],
                                                            _limitesRotacao[1],
                                                             _numeroDeImagens,
                                                             planoRot,
                                                             this.cena,
                                                              80.0,
                                                              2.0,
                                                              _perspectiva,
                                                              this.typeAngle == vetor3.tipoAngulo.Absoluto);

                    // adiciona a infoAnim à lista de infoAnimacao.
                    this.infoAnimacoes.Add(infoAnim);

                    // guarda o nome do plano de rotação, conforme o índice.
                    List<string> strPlanoDeRotacao = new List<string>() { "XY", "XZ", "YZ" };

                    // guarda a faixa de rotações no componente visual.
                    List<string> lstTmpRotacoesGuardadas = new List<string>();
                    lstTmpRotacoesGuardadas = this.txtBxRotacoesGuardadas.Lines.ToList<string>();
                    // guarda a recém carregada [infoAnimacoes] na lista temporária de faixa de rotações.
                    lstTmpRotacoesGuardadas.Add(this.infoAnimacoes[this.infoAnimacoes.Count-1].ToString());
                    // redimensiona as linhas do componente visual.
                    this.txtBxRotacoesGuardadas.Lines = lstTmpRotacoesGuardadas.ToArray<string>();
                    this.mostraMensagem("faixa de rotações guardada com sucesso.");
                } // while()
                strmRdtr.Close();
                strm.Close();
                this.mostraMensagem("Arquivo de rotações carregado.");
            } // if
        } // txtRotacoesGuardadas_MouseDoubleClick()

        /// <summary>
        /// gera as imagens rotacionadas.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnGeraImagens_Click(object sender, EventArgs e)
        {
            if ((this.infoAnimacoes==null) || (this.infoAnimacoes.Count==0))
            {
                MessageBox.Show("Não há faixa de rotação para ser aplicada.");
                return;
            }
            infoAnimacaoRotacao lastInfo = this.infoAnimacoes[this.infoAnimacoes.Count - 1];
            //gera as imagens a partir da última faixa de rotação.
            List<Bitmap> lstImgs=lastInfo.geraImagens(new vetor3(1.0, 0.0, 0.0), new vetor3(0.0, 1.0, 0.0), new vetor3(0.0, 0.0,1.0));
            // adiciona as imagens geradas à lista a ser mostrada na tela.
            this.lstImgImagensRotacionar3D.Images.AddRange(lstImgs.ToArray());
            // redimensiona para o tamanho padrão.
            this.lstImgImagensRotacionar3D.ImageSize = this.szImgPadrao;
            // redesenha o frame da window currente.    
            this.Refresh();


        } // btnGeraImagens_Click()

        /// <summary>
        /// trata do evento de validar a entra do número de imagens.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void txtBxNumeroDeImagens_Validated(object sender, EventArgs e)
        {
            double dbNumeroDeImagens=0.0;
            if (!this.validaNumero(
                "Entre com um número de imagens por faixa de rotação válido",txtBxNumeroDeImagens.Text,
                ref dbNumeroDeImagens))
                return;
            this.numeroDeImagensCurrente = (int)dbNumeroDeImagens;
        }// txtBxNumeroDeImagens_Validated()

        /// <summary>
        /// trata do evento de guardar para o banco de dados de bases ortonormais, a base ortonormal
        /// formada pelo gzimo eixos.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnGuardarBaseOrtonormal_Click_1(object sender, EventArgs e)
        {
            int x;
            // verifica se a base ortonormal currente não tenha um nome, dado na caixa de texto.
            // se não tiver um nome, pede para escrever um nome e retorna.
            if (txtBxNomearUmaBaseOrtonormal.Text == "")
            {
                this.mostraMensagem("Entre com um nome para a base ortonormal a ser guardada.");
                return;
            }
            // verifica se a base ortonormal currente não tenha nome igual a outras bases guardadas anteriormente.
            // se tiver, emite um alarme, informando e pedindo um novo nome para a base ortonormal currente a ser
            // aguardada.
            for (x = 0; x < this.ctrViewRotacao.lstBasesGuardadas.Count; x++)
            {
                if (this.ctrViewRotacao.lstBasesGuardadas[x].Equals(txtBxNomearUmaBaseOrtonormal.Text))
                {
                    this.mostraMensagem("Entre com um nome diferente das demais bases ortonormais.");
                    return;
                } // if lstBasesGuardadas[x].Equals()
            } // for x
            // guarda o nome da base ortonormal currente.
            this.cmbBxBasesGuardadas.Items.Add(txtBxNomearUmaBaseOrtonormal.Text);
            // guarda os eixos da base ortonormal currente.
            this.ctrViewRotacao.lstBasesGuardadas.Add(new vetor3[] {
                this.ctrViewRotacao.gzimosCurrentes[0].eixoXGzimo,
                this.ctrViewRotacao.gzimosCurrentes[0].eixoYGzimo,
                this.ctrViewRotacao.gzimosCurrentes[0].eixoZGzimo});
            this.mostraMensagem("base ortonormal currente salva com sucesso.");
            // atualiza o arquivo de base ortonormais, acrescentando a base ortonormal currente.
            this.gravarBasesOrtonormais();
        } // btnGuardarBaseOrtonormal_Click_1()
        

        /// <summary>
        /// deleta a base ortonormal selecionada da base de dados ortonormais.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnDeletarBaseOrtonormal_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbBxBasesGuardadas.SelectedIndex >= 0)
                {
                    this.ctrViewRotacao.lstBasesGuardadas.RemoveAt(this.cmbBxBasesGuardadas.SelectedIndex);
                    this.cmbBxBasesGuardadas.Items.RemoveAt(this.cmbBxBasesGuardadas.SelectedIndex);
                    this.gravarBasesOrtonormais();
                    this.mostraMensagem("base ortonormal selecionada deletada com sucesso.");
                    this.Refresh();
                } // if
                else
                {
                    this.mostraMensagem("Não há base ortonormal para ser removida.");
                } // else
            } // try
            catch (Exception ex)
            {
                this.mostraMensagem("Erro ao remover base ortonormal da lista. Mensagem de Erro: " + ex.Message);
            } // catch
        
        }
        /// <summary>
        /// trata do evento de carregar para o gzimo eixos a base ortnormal selecionada.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnCarregarUmaBaseOrtonormal_Click_1(object sender, EventArgs e)
        {
            this.carregaUmaBaseortonormal();
        }

        /// <summary>
        /// trata do evento de rotacionar a imagem com a base do gzimo eixos.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnRotacionar_Click_1(object sender, EventArgs e)
        {
            this.rotacionaImagemComGzimoEixos();
        }

        /// <summary>
        /// trata do evento de inicializar a imagem de matriz 3D (isso impede de gerar a imagem
        /// a cada rotação).
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnGerarObjeto3D_Click_1(object sender, EventArgs e)
        {
            string msgErro = "";
            this.mtx3Dto2D = new Matriz3DparaImagem2D();
            if (this.cmbBxTipoPerspectiva.SelectedIndex == 0)
                this.mtx3Dto2D.iniciaMatriz3D(
                                    ctrViewRotacao.gzimosCurrentes[0].eixoXGzimo,
                                    ctrViewRotacao.gzimosCurrentes[0].eixoYGzimo,
                                    ctrViewRotacao.gzimosCurrentes[0].eixoZGzimo,
                                    this.cena,
                                    this.profundidadeImaginada,
                                    this.fatorPerspectiva,
                                    vetor3.transformacaoPerspectivaIsometrica,
                                    ref msgErro);
            if (this.cmbBxTipoPerspectiva.SelectedIndex == 1)
                this.mtx3Dto2D.iniciaMatriz3D(
                                    ctrViewRotacao.gzimosCurrentes[0].eixoXGzimo,
                                    ctrViewRotacao.gzimosCurrentes[0].eixoYGzimo,
                                    ctrViewRotacao.gzimosCurrentes[0].eixoZGzimo,
                                    this.cena,
                                    this.profundidadeImaginada,
                                    this.fatorPerspectiva,
                                    vetor3.transformacaoPerspectivaGeometrica,
                                    ref msgErro);
            if (msgErro != "")
            {
                this.mostraMensagem(msgErro);
                this.mtx3Dto2D = null;
                return;
            } // if msgErro
            this.mtx3Dto2D.isMatrizJaGerada = true;
            this.mostraMensagem("Objeto 3D de mapeamento gerado com sucesso.");
        }
        /// <summary>
        /// trata do evento de determinar se a rotação é com ângulos relativos ou absolutos.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void chkBxRotacaoRelativa_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkBxRotacaoRelativa.Checked)
            {
                typeAngle = vetor3.tipoAngulo.Absoluto;
                ctrViewRotacao.typeAngleForRotationGzimo = vetor3.tipoAngulo.Absoluto;
                this.mostraMensagem("Ângulos absolutos selecionados com sucesso.");
            } // if 
            else
            {
                typeAngle = vetor3.tipoAngulo.Relativo;
                ctrViewRotacao.typeAngleForRotationGzimo = vetor3.tipoAngulo.Relativo;
                this.mostraMensagem("Ângulos relativos selecionados com sucesso.");
            } // else
        } // chkBxRotacaoRelativa_CheckedChaanged_1()

        /// <summary>
        /// trata do evento de validar o ângulo de rotação do plano XZ.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void txtBxAnguloRotacaoPlanoXZ_TextChanged_1(object sender, EventArgs e)
        {
            this.validaNumero("Erro, entre com um número válido para o ângulo XZ.", 
                              txtBxAnguloRotacaoPlanoXZ.Text,
                              ref this.anguloRotacaoPlanoXZ);
        }

        /// <summary>
        /// trata do evento de validar o ângulo de rotação do plano YZ.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void txtBxAnguloRotaçãoPlanoYZ_TextChanged_1(object sender, EventArgs e)
        {
            this.validaNumero("Erro, entre com um número válido para o ângulo YZ.", txtBxAnguloRotaçãoPlanoYZ.Text,
                          ref this.anguloRotacaoPlanoYZ);
        }

        /// <summary>
        /// trata do evento de validar o ângulo de rotação do plano XY.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void txtBxAnguloRotacaoPlanoXY_TextChanged_1(object sender, EventArgs e)
        {
            this.validaNumero("Erro, entre com um número válido para o ângulo XY.", txtBxAnguloRotacaoPlanoXY.Text,
                      ref this.anguloRotacaoPlanoXY);
        }
        
        /// <summary>
        /// trata do evento de validar o profundidade imaginada pelo usuária da matriz 3D associada à imagem.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxAlturaImaginada_Validated(object sender, EventArgs e)
        {
            this.validaNumero("Erro, entre com um número válido para a profundidade imaginada.", txtBxAlturaImaginada.Text,
                        ref this.profundidadeImaginada);
        } // txtBxAlturaImaginada_Validated()

        /// <summary>
        /// limpa a lista de imagens já geradas pelo aplicativo, na TabPage-0.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnLimparListaDeImagensPage1_Click(object sender, EventArgs e)
        {
            this.lstImgImagensRotacionar3D.Images.Clear();
            this.Refresh();
        } // btnLimparListaDeImagensPage1_Click()


        /// <summary>
        /// limpa a lista de imagens já geradas pelo aplicativo, na TabPage-1.
        /// </summary>
        /// <param name="sender">parâmetro de evento.</param>
        /// <param name="e">parâmetro de evento.</param>
        private void btnLimparImagensGeradasPage0_Click(object sender, EventArgs e)
        {
            this.lstImgImagensRotacionar3D.Images.Clear();
            this.Refresh();
        } // btnLimparListaDeImagensPage0_Click()


    } // class windowPerspectivas
    /// <summary>
    /// guarda as informações para uma faixa de rotação para animação.
    /// </summary>
    class infoAnimacaoRotacao
    {
        public enum planosRotacao{XY,YZ,XZ };
        public double limiteMinInfo { get; set; }
        public double limiteMaxInfo { get; set; }
        public int numeroDeImagens { get; set; }
        public planosRotacao planoRotacao { get; set; }
        public Bitmap imagem { get; set; }
        public double profundidade { get; set; }
        public double fatorPerspectiva { get; set; }
        public bool isAnguloAbsoluto { get; set; }
        public Matriz3DparaImagem2D.transformacaoPerspectiva perspectiva { get; set; }
        /// <summary>
        /// construtor.
        /// </summary>
        /// <param name="limiteMin">limite mínimo para rotacionar a animação.</param>
        /// <param name="limiteMax">limite máximo para rotacionar a animação.</param>
        /// <param name="numImgs">número de imagens a serem geradas no limite da animação.</param>
        /// <param name="pl">plano de rotação para esta informação de animação-rotação.</param>
        public infoAnimacaoRotacao(double limiteMin, double limiteMax, int numImgs, planosRotacao pl,
            Bitmap _imagem, double _profundidadeImagem, double _fatorPerspectiva,
            Matriz3DparaImagem2D.transformacaoPerspectiva _perspectiva, bool angulosAbsolutos)
        {
            this.limiteMinInfo = limiteMin;
            this.limiteMaxInfo = limiteMax;
            this.numeroDeImagens = numImgs;
            this.planoRotacao = pl;
            this.imagem = _imagem;
            this.profundidade = _profundidadeImagem;
            this.fatorPerspectiva = _fatorPerspectiva;
            this.perspectiva = _perspectiva;
            this.isAnguloAbsoluto = angulosAbsolutos;
        } // infoAnumacaoRotacao()

        public override string ToString()
        {
            // guarda o nome do plano de rotação, conforme o índice.
             string[] strPlanoRotacao= new string[]{"XY","XZ","YZ"};
            string planoRot = "";
            switch (this.planoRotacao)
            {
                case (planosRotacao.XY):
                    planoRot = "XY";
                    break;
                case (planosRotacao.XZ):
                    planoRot = "XZ";
                    break;
                case (planosRotacao.YZ):
                    planoRot="YZ";
                    break;
                default:
                    planoRot = "";
                    break;
            } // switch planoRotacao

            // inicializa uma string contendo os dados da infoAnimacao.
            string str = "ini: " + this.limiteMinInfo.ToString("N1") + 
                       " fini: " + this.limiteMaxInfo.ToString("N1") +
                       ",  " + this.numeroDeImagens + " imagens "+
                       "( plano de rotação " + planoRot + ").";
            return str;
        } // string ToString()

        public List<Bitmap> geraImagens(vetor3 eixoX,vetor3 eixoY, vetor3 eixoZ)
        {
            List<Bitmap> imagens = new List<Bitmap>();
            Matriz3DparaImagem2D matrizImagem = new Matriz3DparaImagem2D();
            string msgErro="";
            matrizImagem.iniciaMatriz3D(
               eixoX,
               eixoY,
               eixoZ,
               this.imagem,
               this.profundidade,
               this.fatorPerspectiva, 
               this.perspectiva,
               ref msgErro);

            if (msgErro != "")
                return null;
            double incAngulo = (this.limiteMaxInfo - this.limiteMinInfo) / ((double)this.numeroDeImagens);
            double omegaX = 0.0;
            double omegaY = 0.0;
            double omegaZ = 0.0;
            if (this.planoRotacao == planosRotacao.XY)
                omegaZ = incAngulo;
            if (this.planoRotacao == planosRotacao.XZ)
                omegaY = incAngulo;
            if (this.planoRotacao == planosRotacao.YZ)
                omegaX = incAngulo;
            for (int n = 0; n < numeroDeImagens; n++)
            {
                msgErro="";
                imagens.Add(matrizImagem.rotacionaMatriz3D(eixoX,
                                                           eixoY,
                                                           eixoZ,
                                                           this.imagem,
                                                           (int)this.profundidade,
                                                           omegaZ,
                                                           omegaX,
                                                           omegaY,
                                                           ref msgErro,
                                                           this.isAnguloAbsoluto,
                                                           false,
                                                           this.perspectiva,
                                                           this.fatorPerspectiva));
                if (msgErro != "")
                    return null;
            } // for n
            return imagens;
        } // geraImagens()
    } // class infoAnimacaoRotacao

}// namespace