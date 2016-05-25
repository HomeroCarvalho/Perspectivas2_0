using System.Windows.Forms;

namespace rotacao3DparaFiguras2D
{
    partial class windowPerspectivas: Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnStrpMenuPrincipal = new System.Windows.Forms.MenuStrip();
            this.arquivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarImagemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarImagemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblAlturaImaginada = new System.Windows.Forms.Label();
            this.tabPageAnimacoes = new System.Windows.Forms.TabPage();
            this.btnLimparListaDeImaensPage1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGuardarRotacaoInicialFinal = new System.Windows.Forms.Button();
            this.btnCarregarRotacoes = new System.Windows.Forms.Button();
            this.btnSalvarRotacoes = new System.Windows.Forms.Button();
            this.cmbBxPlanoDeRotacao = new System.Windows.Forms.ComboBox();
            this.lblNumeroDeImagens = new System.Windows.Forms.Label();
            this.txtBxNumeroDeImagens = new System.Windows.Forms.TextBox();
            this.txtBxRotacaoFinal = new System.Windows.Forms.TextBox();
            this.txtBxRotacaoInicial = new System.Windows.Forms.TextBox();
            this.txtBxRotacoesGuardadas = new System.Windows.Forms.TextBox();
            this.btnGeraImagens = new System.Windows.Forms.Button();
            this.lblStatusMsgsPageRotacoes = new System.Windows.Forms.Label();
            this.lblRotacionarDeAte = new System.Windows.Forms.Label();
            this.lblRotacaoFinal = new System.Windows.Forms.Label();
            this.lblRotacaoInicial = new System.Windows.Forms.Label();
            this.tabPageRotacoes = new System.Windows.Forms.TabPage();
            this.grpBxPainelPrincipal = new System.Windows.Forms.GroupBox();
            this.lblTipoPerspectiva = new System.Windows.Forms.Label();
            this.btnGerarObjeto3D = new System.Windows.Forms.Button();
            this.grpBxAngulosEProfundidade = new System.Windows.Forms.GroupBox();
            this.lblTipoDeAngulo = new System.Windows.Forms.Label();
            this.cmbBxTipoDeAngulo = new System.Windows.Forms.ComboBox();
            this.txtBxAlturaImaginada = new System.Windows.Forms.TextBox();
            this.txtBxAnguloRotacaoPlanoXY = new System.Windows.Forms.TextBox();
            this.lblAnguloBaseRotacaoXY = new System.Windows.Forms.Label();
            this.txtBxAnguloRotaçãoPlanoYZ = new System.Windows.Forms.TextBox();
            this.lblAnguloBaseRotacaoYZ = new System.Windows.Forms.Label();
            this.txtBxAnguloRotacaoPlanoXZ = new System.Windows.Forms.TextBox();
            this.lblAnguloBaseRotacaoXZ = new System.Windows.Forms.Label();
            this.btnRotacionar = new System.Windows.Forms.Button();
            this.grpBasesOrtonormaisGuardadas = new System.Windows.Forms.GroupBox();
            this.lblNomearUmaBaseOrtonomal = new System.Windows.Forms.Label();
            this.txtBxNomearUmaBaseOrtonormal = new System.Windows.Forms.TextBox();
            this.btnGuardarBaseOrtonormal = new System.Windows.Forms.Button();
            this.btnDeletarBaseOrtonormal = new System.Windows.Forms.Button();
            this.btnCarregarUmaBaseOrtonormal = new System.Windows.Forms.Button();
            this.cmbBxBasesGuardadas = new System.Windows.Forms.ComboBox();
            this.cmbBxTipoPerspectiva = new System.Windows.Forms.ComboBox();
            this.chkBxRotacaoRelativa = new System.Windows.Forms.CheckBox();
            this.lblMsgStatus = new System.Windows.Forms.Label();
            this.TabControlFuncoesDoAplicativo = new System.Windows.Forms.TabControl();
            this.toolTipLblAltura = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipBtnCarregar = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipcmbBxPlanoDeRotacao = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipLblNumeroDeImagens = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipTxtNumeroDeImagens = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipTxtBxRotacaoFinal = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipTxtBxRotacaoInicial = new System.Windows.Forms.ToolTip(this.components);
            this.tooltipTxtBxRotacoesGuardada = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipBtnGeraImagens = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipBtnSalvaRotcoes = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipLblStatusMsg = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipbtnguardar = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTiplblRotacionarDeAte = new System.Windows.Forms.ToolTip(this.components);
            this.toolTiplblRotacaoFinal = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipLblRotacaoInicial = new System.Windows.Forms.ToolTip(this.components);
            this.toollblTipoPerspectiva = new System.Windows.Forms.ToolTip(this.components);
            this.toolbtnGerarObjeto3D = new System.Windows.Forms.ToolTip(this.components);
            this.toolgrpBxAngulosEProfundidade = new System.Windows.Forms.ToolTip(this.components);
            this.toollblTipoDeAngulo = new System.Windows.Forms.ToolTip(this.components);
            this.toolcmbxTipoDeAngulo = new System.Windows.Forms.ToolTip(this.components);
            this.toolTxtBxAlturaImaginada = new System.Windows.Forms.ToolTip(this.components);
            this.tooltxtBxAnguloRotacaoXY = new System.Windows.Forms.ToolTip(this.components);
            this.toollblAnguloBaseRotacaoXY = new System.Windows.Forms.ToolTip(this.components);
            this.tooltxtAnguloRotacaoYZ = new System.Windows.Forms.ToolTip(this.components);
            this.toollblAnguloBaseRotacaoYZ = new System.Windows.Forms.ToolTip(this.components);
            this.tooltxtBxAnguloRotacaoPlanoXZ = new System.Windows.Forms.ToolTip(this.components);
            this.toollblAnguloBaseRotacaoXZ = new System.Windows.Forms.ToolTip(this.components);
            this.toolBtnRotacionar = new System.Windows.Forms.ToolTip(this.components);
            this.toolTxtBxNomearUmaBaseOrtonormal = new System.Windows.Forms.ToolTip(this.components);
            this.toolBtnGuardarBaseOrtonormal = new System.Windows.Forms.ToolTip(this.components);
            this.toolbtnDeletatBaseOrtonormal = new System.Windows.Forms.ToolTip(this.components);
            this.toolbtnCarregarUmaBaseOrtonormal = new System.Windows.Forms.ToolTip(this.components);
            this.toolcmbxTipoPerspectiva = new System.Windows.Forms.ToolTip(this.components);
            this.toolchkBxRotacaoRelativa = new System.Windows.Forms.ToolTip(this.components);
            this.toollblMsgSTatus = new System.Windows.Forms.ToolTip(this.components);
            this.btnLimparImagensGeradasPage0 = new System.Windows.Forms.Button();
            this.mnStrpMenuPrincipal.SuspendLayout();
            this.tabPageAnimacoes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageRotacoes.SuspendLayout();
            this.grpBxPainelPrincipal.SuspendLayout();
            this.grpBxAngulosEProfundidade.SuspendLayout();
            this.grpBasesOrtonormaisGuardadas.SuspendLayout();
            this.TabControlFuncoesDoAplicativo.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnStrpMenuPrincipal
            // 
            this.mnStrpMenuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivosToolStripMenuItem});
            this.mnStrpMenuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnStrpMenuPrincipal.Name = "mnStrpMenuPrincipal";
            this.mnStrpMenuPrincipal.Size = new System.Drawing.Size(1288, 24);
            this.mnStrpMenuPrincipal.TabIndex = 52;
            this.mnStrpMenuPrincipal.Text = "Menu Principal";
            // 
            // arquivosToolStripMenuItem
            // 
            this.arquivosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carregarImagemToolStripMenuItem,
            this.salvarImagemToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.arquivosToolStripMenuItem.Name = "arquivosToolStripMenuItem";
            this.arquivosToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.arquivosToolStripMenuItem.Text = "Arquivos";
            this.arquivosToolStripMenuItem.ToolTipText = "carregue,salve arquivos, sair do aplicativo";
            // 
            // carregarImagemToolStripMenuItem
            // 
            this.carregarImagemToolStripMenuItem.Name = "carregarImagemToolStripMenuItem";
            this.carregarImagemToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.carregarImagemToolStripMenuItem.Text = "Carregar Imagem";
            this.carregarImagemToolStripMenuItem.ToolTipText = "carregue para a memória um arquivo de imagem";
            this.carregarImagemToolStripMenuItem.Click += new System.EventHandler(this.carregarImagemToolStripMenuItem_Click);
            // 
            // salvarImagemToolStripMenuItem
            // 
            this.salvarImagemToolStripMenuItem.Name = "salvarImagemToolStripMenuItem";
            this.salvarImagemToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.salvarImagemToolStripMenuItem.Text = "Salvar Imagem";
            this.salvarImagemToolStripMenuItem.ToolTipText = "salve para disco a última imagem gerada.";
            this.salvarImagemToolStripMenuItem.Click += new System.EventHandler(this.salvarImagemToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.ToolTipText = "sair do aplicativo";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // lblAlturaImaginada
            // 
            this.lblAlturaImaginada.AutoSize = true;
            this.lblAlturaImaginada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlturaImaginada.Location = new System.Drawing.Point(3, 203);
            this.lblAlturaImaginada.Name = "lblAlturaImaginada";
            this.lblAlturaImaginada.Size = new System.Drawing.Size(211, 13);
            this.lblAlturaImaginada.TabIndex = 57;
            this.lblAlturaImaginada.Text = "Profundidade imaginada da imagem:";
            this.toolTipLblAltura.SetToolTip(this.lblAlturaImaginada, "Este é o cumprimento da componente z, que falta em objetos 2D");
            // 
            // tabPageAnimacoes
            // 
            this.tabPageAnimacoes.Controls.Add(this.btnLimparListaDeImaensPage1);
            this.tabPageAnimacoes.Controls.Add(this.groupBox1);
            this.tabPageAnimacoes.Controls.Add(this.cmbBxPlanoDeRotacao);
            this.tabPageAnimacoes.Controls.Add(this.lblNumeroDeImagens);
            this.tabPageAnimacoes.Controls.Add(this.txtBxNumeroDeImagens);
            this.tabPageAnimacoes.Controls.Add(this.txtBxRotacaoFinal);
            this.tabPageAnimacoes.Controls.Add(this.txtBxRotacaoInicial);
            this.tabPageAnimacoes.Controls.Add(this.txtBxRotacoesGuardadas);
            this.tabPageAnimacoes.Controls.Add(this.btnGeraImagens);
            this.tabPageAnimacoes.Controls.Add(this.lblStatusMsgsPageRotacoes);
            this.tabPageAnimacoes.Controls.Add(this.lblRotacionarDeAte);
            this.tabPageAnimacoes.Controls.Add(this.lblRotacaoFinal);
            this.tabPageAnimacoes.Controls.Add(this.lblRotacaoInicial);
            this.tabPageAnimacoes.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnimacoes.Name = "tabPageAnimacoes";
            this.tabPageAnimacoes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnimacoes.Size = new System.Drawing.Size(596, 538);
            this.tabPageAnimacoes.TabIndex = 1;
            this.tabPageAnimacoes.Text = "Animações";
            this.tabPageAnimacoes.UseVisualStyleBackColor = true;
            // 
            // btnLimparListaDeImaensPage1
            // 
            this.btnLimparListaDeImaensPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimparListaDeImaensPage1.Location = new System.Drawing.Point(27, 333);
            this.btnLimparListaDeImaensPage1.Name = "btnLimparListaDeImaensPage1";
            this.btnLimparListaDeImaensPage1.Size = new System.Drawing.Size(365, 34);
            this.btnLimparListaDeImaensPage1.TabIndex = 12;
            this.btnLimparListaDeImaensPage1.Text = "limpar lista de Imagens";
            this.btnLimparListaDeImaensPage1.UseVisualStyleBackColor = true;
            this.btnLimparListaDeImaensPage1.Click += new System.EventHandler(this.btnLimparListaDeImagensPage1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGuardarRotacaoInicialFinal);
            this.groupBox1.Controls.Add(this.btnCarregarRotacoes);
            this.groupBox1.Controls.Add(this.btnSalvarRotacoes);
            this.groupBox1.Location = new System.Drawing.Point(27, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 92);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "gerenciamento de rotações";
            // 
            // btnGuardarRotacaoInicialFinal
            // 
            this.btnGuardarRotacaoInicialFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarRotacaoInicialFinal.Location = new System.Drawing.Point(6, 19);
            this.btnGuardarRotacaoInicialFinal.Name = "btnGuardarRotacaoInicialFinal";
            this.btnGuardarRotacaoInicialFinal.Size = new System.Drawing.Size(171, 35);
            this.btnGuardarRotacaoInicialFinal.TabIndex = 3;
            this.btnGuardarRotacaoInicialFinal.Text = "guardar rotação";
            this.toolTipbtnguardar.SetToolTip(this.btnGuardarRotacaoInicialFinal, "guarda em arquivo a faixa de rotações do script de rotação");
            this.btnGuardarRotacaoInicialFinal.UseVisualStyleBackColor = true;
            this.btnGuardarRotacaoInicialFinal.Click += new System.EventHandler(this.btnGuardarRotacaoInicialFinal_Click);
            // 
            // btnCarregarRotacoes
            // 
            this.btnCarregarRotacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarregarRotacoes.Location = new System.Drawing.Point(178, 19);
            this.btnCarregarRotacoes.Name = "btnCarregarRotacoes";
            this.btnCarregarRotacoes.Size = new System.Drawing.Size(187, 32);
            this.btnCarregarRotacoes.TabIndex = 10;
            this.btnCarregarRotacoes.Text = "carregar rotações";
            this.toolTipBtnCarregar.SetToolTip(this.btnCarregarRotacoes, "carregue um script de rotações previamente salvas");
            this.btnCarregarRotacoes.UseVisualStyleBackColor = true;
            this.btnCarregarRotacoes.Click += new System.EventHandler(this.btnCarregarRotacoes_Click);
            // 
            // btnSalvarRotacoes
            // 
            this.btnSalvarRotacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarRotacoes.Location = new System.Drawing.Point(6, 57);
            this.btnSalvarRotacoes.Name = "btnSalvarRotacoes";
            this.btnSalvarRotacoes.Size = new System.Drawing.Size(171, 29);
            this.btnSalvarRotacoes.TabIndex = 5;
            this.btnSalvarRotacoes.Text = "salvar rotações";
            this.toolTipBtnSalvaRotcoes.SetToolTip(this.btnSalvarRotacoes, "Salva as imagens de rotação geradas pelo script currente de rotação");
            this.btnSalvarRotacoes.UseVisualStyleBackColor = true;
            this.btnSalvarRotacoes.Click += new System.EventHandler(this.btnSalvarRotacoes_Click);
            // 
            // cmbBxPlanoDeRotacao
            // 
            this.cmbBxPlanoDeRotacao.FormattingEnabled = true;
            this.cmbBxPlanoDeRotacao.Items.AddRange(new object[] {
            "plano XY",
            "plano XZ",
            "plano YZ"});
            this.cmbBxPlanoDeRotacao.Location = new System.Drawing.Point(133, 57);
            this.cmbBxPlanoDeRotacao.Name = "cmbBxPlanoDeRotacao";
            this.cmbBxPlanoDeRotacao.Size = new System.Drawing.Size(165, 21);
            this.cmbBxPlanoDeRotacao.TabIndex = 9;
            this.toolTipcmbBxPlanoDeRotacao.SetToolTip(this.cmbBxPlanoDeRotacao, "escolha um plano cartesiano para rotacionar");
            // 
            // lblNumeroDeImagens
            // 
            this.lblNumeroDeImagens.AutoSize = true;
            this.lblNumeroDeImagens.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroDeImagens.Location = new System.Drawing.Point(24, 148);
            this.lblNumeroDeImagens.Name = "lblNumeroDeImagens";
            this.lblNumeroDeImagens.Size = new System.Drawing.Size(151, 16);
            this.lblNumeroDeImagens.TabIndex = 8;
            this.lblNumeroDeImagens.Text = "Número de Imagens:";
            this.toolTipLblNumeroDeImagens.SetToolTip(this.lblNumeroDeImagens, "este é o número de imagens a ser gerada pelo script");
            // 
            // txtBxNumeroDeImagens
            // 
            this.txtBxNumeroDeImagens.Location = new System.Drawing.Point(195, 148);
            this.txtBxNumeroDeImagens.Name = "txtBxNumeroDeImagens";
            this.txtBxNumeroDeImagens.Size = new System.Drawing.Size(129, 20);
            this.txtBxNumeroDeImagens.TabIndex = 7;
            this.toolTipTxtNumeroDeImagens.SetToolTip(this.txtBxNumeroDeImagens, "entre com o número de imagens a ser gerada pelo script de rotações");
            this.txtBxNumeroDeImagens.Validated += new System.EventHandler(this.txtBxNumeroDeImagens_Validated);
            // 
            // txtBxRotacaoFinal
            // 
            this.txtBxRotacaoFinal.Location = new System.Drawing.Point(251, 91);
            this.txtBxRotacaoFinal.Name = "txtBxRotacaoFinal";
            this.txtBxRotacaoFinal.Size = new System.Drawing.Size(130, 20);
            this.txtBxRotacaoFinal.TabIndex = 1;
            this.toolTipTxtBxRotacaoFinal.SetToolTip(this.txtBxRotacaoFinal, "entre com o valor final de rotação de plano, para a currente linha do script de r" +
        "otações");
            this.txtBxRotacaoFinal.Validated += new System.EventHandler(this.txtBxRotacaoFinal_Validated);
            // 
            // txtBxRotacaoInicial
            // 
            this.txtBxRotacaoInicial.Location = new System.Drawing.Point(69, 91);
            this.txtBxRotacaoInicial.Name = "txtBxRotacaoInicial";
            this.txtBxRotacaoInicial.Size = new System.Drawing.Size(129, 20);
            this.txtBxRotacaoInicial.TabIndex = 0;
            this.toolTipTxtBxRotacaoInicial.SetToolTip(this.txtBxRotacaoInicial, "entre com o valor inicial de rotação de plano, para a currente linha do script de" +
        " rotações");
            this.txtBxRotacaoInicial.Validated += new System.EventHandler(this.txtBxRotacaoInicial_Validated);
            // 
            // txtBxRotacoesGuardadas
            // 
            this.txtBxRotacoesGuardadas.Location = new System.Drawing.Point(409, 71);
            this.txtBxRotacoesGuardadas.Multiline = true;
            this.txtBxRotacoesGuardadas.Name = "txtBxRotacoesGuardadas";
            this.txtBxRotacoesGuardadas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBxRotacoesGuardadas.Size = new System.Drawing.Size(206, 236);
            this.txtBxRotacoesGuardadas.TabIndex = 0;
            this.tooltipTxtBxRotacoesGuardada.SetToolTip(this.txtBxRotacoesGuardadas, "estes são os scripts de rotação salvos em arquivo");
            this.txtBxRotacoesGuardadas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtBxRotacoesGuardadas_MouseDoubleClick);
            // 
            // btnGeraImagens
            // 
            this.btnGeraImagens.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeraImagens.Location = new System.Drawing.Point(27, 297);
            this.btnGeraImagens.Name = "btnGeraImagens";
            this.btnGeraImagens.Size = new System.Drawing.Size(364, 30);
            this.btnGeraImagens.TabIndex = 6;
            this.btnGeraImagens.Text = "Gerar Imagens rotacionadas";
            this.toolTipBtnGeraImagens.SetToolTip(this.btnGeraImagens, "processa o script de rotação, gerando as images rotacionas");
            this.btnGeraImagens.UseVisualStyleBackColor = true;
            this.btnGeraImagens.Click += new System.EventHandler(this.btnGeraImagens_Click);
            // 
            // lblStatusMsgsPageRotacoes
            // 
            this.lblStatusMsgsPageRotacoes.AutoSize = true;
            this.lblStatusMsgsPageRotacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMsgsPageRotacoes.Location = new System.Drawing.Point(24, 500);
            this.lblStatusMsgsPageRotacoes.Name = "lblStatusMsgsPageRotacoes";
            this.lblStatusMsgsPageRotacoes.Size = new System.Drawing.Size(96, 16);
            this.lblStatusMsgsPageRotacoes.TabIndex = 4;
            this.lblStatusMsgsPageRotacoes.Text = "Mensagens: ";
            this.toolTipLblStatusMsg.SetToolTip(this.lblStatusMsgsPageRotacoes, "Mostra Mensagens geradas em cada operação no aplictivo.");
            // 
            // lblRotacionarDeAte
            // 
            this.lblRotacionarDeAte.AutoSize = true;
            this.lblRotacionarDeAte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotacionarDeAte.Location = new System.Drawing.Point(24, 58);
            this.lblRotacionarDeAte.Name = "lblRotacionarDeAte";
            this.lblRotacionarDeAte.Size = new System.Drawing.Size(88, 16);
            this.lblRotacionarDeAte.TabIndex = 2;
            this.lblRotacionarDeAte.Text = "Rotacionar:";
            this.ToolTiplblRotacionarDeAte.SetToolTip(this.lblRotacionarDeAte, "o campo ao lado guarda em arquivo o script de rotações.");
            // 
            // lblRotacaoFinal
            // 
            this.lblRotacaoFinal.AutoSize = true;
            this.lblRotacaoFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotacaoFinal.Location = new System.Drawing.Point(204, 95);
            this.lblRotacaoFinal.Name = "lblRotacaoFinal";
            this.lblRotacaoFinal.Size = new System.Drawing.Size(31, 16);
            this.lblRotacaoFinal.TabIndex = 2;
            this.lblRotacaoFinal.Text = "Até";
            this.toolTiplblRotacaoFinal.SetToolTip(this.lblRotacaoFinal, "o campo ao lado define a rotação final (vai até esta), para a faixa de rotações c" +
        "urrente");
            // 
            // lblRotacaoInicial
            // 
            this.lblRotacaoInicial.AutoSize = true;
            this.lblRotacaoInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotacaoInicial.Location = new System.Drawing.Point(24, 95);
            this.lblRotacaoInicial.Name = "lblRotacaoInicial";
            this.lblRotacaoInicial.Size = new System.Drawing.Size(28, 16);
            this.lblRotacaoInicial.TabIndex = 1;
            this.lblRotacaoInicial.Text = "De";
            this.toolTipLblRotacaoInicial.SetToolTip(this.lblRotacaoInicial, "o campo ao lado define a rotação inicial (inicia com esta) para a faixa de rotaçõ" +
        "es currente");
            // 
            // tabPageRotacoes
            // 
            this.tabPageRotacoes.Controls.Add(this.btnLimparImagensGeradasPage0);
            this.tabPageRotacoes.Controls.Add(this.grpBxPainelPrincipal);
            this.tabPageRotacoes.Controls.Add(this.lblMsgStatus);
            this.tabPageRotacoes.Location = new System.Drawing.Point(4, 22);
            this.tabPageRotacoes.Name = "tabPageRotacoes";
            this.tabPageRotacoes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRotacoes.Size = new System.Drawing.Size(596, 716);
            this.tabPageRotacoes.TabIndex = 0;
            this.tabPageRotacoes.Text = "Rotações";
            this.tabPageRotacoes.UseVisualStyleBackColor = true;
            // 
            // grpBxPainelPrincipal
            // 
            this.grpBxPainelPrincipal.Controls.Add(this.lblTipoPerspectiva);
            this.grpBxPainelPrincipal.Controls.Add(this.btnGerarObjeto3D);
            this.grpBxPainelPrincipal.Controls.Add(this.grpBxAngulosEProfundidade);
            this.grpBxPainelPrincipal.Controls.Add(this.btnRotacionar);
            this.grpBxPainelPrincipal.Controls.Add(this.grpBasesOrtonormaisGuardadas);
            this.grpBxPainelPrincipal.Controls.Add(this.cmbBxTipoPerspectiva);
            this.grpBxPainelPrincipal.Controls.Add(this.chkBxRotacaoRelativa);
            this.grpBxPainelPrincipal.Location = new System.Drawing.Point(92, 7);
            this.grpBxPainelPrincipal.Name = "grpBxPainelPrincipal";
            this.grpBxPainelPrincipal.Size = new System.Drawing.Size(465, 506);
            this.grpBxPainelPrincipal.TabIndex = 58;
            this.grpBxPainelPrincipal.TabStop = false;
            this.grpBxPainelPrincipal.Text = "Painel Principal";
            // 
            // lblTipoPerspectiva
            // 
            this.lblTipoPerspectiva.AutoSize = true;
            this.lblTipoPerspectiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPerspectiva.Location = new System.Drawing.Point(19, 31);
            this.lblTipoPerspectiva.Name = "lblTipoPerspectiva";
            this.lblTipoPerspectiva.Size = new System.Drawing.Size(171, 20);
            this.lblTipoPerspectiva.TabIndex = 51;
            this.lblTipoPerspectiva.Text = "Tipo de Perspectiva:";
            this.toollblTipoPerspectiva.SetToolTip(this.lblTipoPerspectiva, "o campo abaixo modifica o tipo de perspectiva: isométrica ou geométrica");
            // 
            // btnGerarObjeto3D
            // 
            this.btnGerarObjeto3D.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarObjeto3D.Location = new System.Drawing.Point(23, 141);
            this.btnGerarObjeto3D.Name = "btnGerarObjeto3D";
            this.btnGerarObjeto3D.Size = new System.Drawing.Size(148, 35);
            this.btnGerarObjeto3D.TabIndex = 6;
            this.btnGerarObjeto3D.Text = "Gerar Objeto 3D";
            this.toolbtnGerarObjeto3D.SetToolTip(this.btnGerarObjeto3D, "gera antecipadamente o objeto 3D");
            this.btnGerarObjeto3D.UseVisualStyleBackColor = true;
            this.btnGerarObjeto3D.Click += new System.EventHandler(this.btnGerarObjeto3D_Click_1);
            // 
            // grpBxAngulosEProfundidade
            // 
            this.grpBxAngulosEProfundidade.Controls.Add(this.lblTipoDeAngulo);
            this.grpBxAngulosEProfundidade.Controls.Add(this.cmbBxTipoDeAngulo);
            this.grpBxAngulosEProfundidade.Controls.Add(this.txtBxAlturaImaginada);
            this.grpBxAngulosEProfundidade.Controls.Add(this.lblAlturaImaginada);
            this.grpBxAngulosEProfundidade.Controls.Add(this.txtBxAnguloRotacaoPlanoXY);
            this.grpBxAngulosEProfundidade.Controls.Add(this.lblAnguloBaseRotacaoXY);
            this.grpBxAngulosEProfundidade.Controls.Add(this.txtBxAnguloRotaçãoPlanoYZ);
            this.grpBxAngulosEProfundidade.Controls.Add(this.lblAnguloBaseRotacaoYZ);
            this.grpBxAngulosEProfundidade.Controls.Add(this.txtBxAnguloRotacaoPlanoXZ);
            this.grpBxAngulosEProfundidade.Controls.Add(this.lblAnguloBaseRotacaoXZ);
            this.grpBxAngulosEProfundidade.Location = new System.Drawing.Point(23, 254);
            this.grpBxAngulosEProfundidade.Name = "grpBxAngulosEProfundidade";
            this.grpBxAngulosEProfundidade.Size = new System.Drawing.Size(413, 252);
            this.grpBxAngulosEProfundidade.TabIndex = 46;
            this.grpBxAngulosEProfundidade.TabStop = false;
            this.grpBxAngulosEProfundidade.Text = "ângulos e profundidade";
            this.toolgrpBxAngulosEProfundidade.SetToolTip(this.grpBxAngulosEProfundidade, "Entre aqui com os ângulos dos três planos e a profundida (eixo Z). São campos obr" +
        "igatórios para rotacionar.");
            // 
            // lblTipoDeAngulo
            // 
            this.lblTipoDeAngulo.AutoSize = true;
            this.lblTipoDeAngulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoDeAngulo.Location = new System.Drawing.Point(6, 30);
            this.lblTipoDeAngulo.Name = "lblTipoDeAngulo";
            this.lblTipoDeAngulo.Size = new System.Drawing.Size(127, 20);
            this.lblTipoDeAngulo.TabIndex = 16;
            this.lblTipoDeAngulo.Text = "Tipo de ângulo";
            this.toollblTipoDeAngulo.SetToolTip(this.lblTipoDeAngulo, "Modifique no campo abaixo o tipo de ângulo (ângulo da imagem, ou ângulo do cursor" +
        "-desenho gzimo");
            // 
            // cmbBxTipoDeAngulo
            // 
            this.cmbBxTipoDeAngulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBxTipoDeAngulo.FormattingEnabled = true;
            this.cmbBxTipoDeAngulo.Items.AddRange(new object[] {
            "ângulos de rotação imagem",
            "ângulos de rotação gzimo"});
            this.cmbBxTipoDeAngulo.Location = new System.Drawing.Point(148, 26);
            this.cmbBxTipoDeAngulo.Name = "cmbBxTipoDeAngulo";
            this.cmbBxTipoDeAngulo.Size = new System.Drawing.Size(259, 24);
            this.cmbBxTipoDeAngulo.TabIndex = 12;
            this.toolcmbxTipoDeAngulo.SetToolTip(this.cmbBxTipoDeAngulo, "modifique o tipo de ângulo (para a rotação da imagem, ou para a rotação do cursor" +
        " gzimo");
            // 
            // txtBxAlturaImaginada
            // 
            this.txtBxAlturaImaginada.Location = new System.Drawing.Point(220, 200);
            this.txtBxAlturaImaginada.Name = "txtBxAlturaImaginada";
            this.txtBxAlturaImaginada.Size = new System.Drawing.Size(137, 20);
            this.txtBxAlturaImaginada.TabIndex = 4;
            this.toolTxtBxAlturaImaginada.SetToolTip(this.txtBxAlturaImaginada, "defina a profundidade da imagem (o tamanho no eixo Z, que não tem no mundo 2D)");
            this.txtBxAlturaImaginada.Validated += new System.EventHandler(this.txtBxAlturaImaginada_Validated);
            // 
            // txtBxAnguloRotacaoPlanoXY
            // 
            this.txtBxAnguloRotacaoPlanoXY.Location = new System.Drawing.Point(213, 148);
            this.txtBxAnguloRotacaoPlanoXY.Name = "txtBxAnguloRotacaoPlanoXY";
            this.txtBxAnguloRotacaoPlanoXY.Size = new System.Drawing.Size(194, 20);
            this.txtBxAnguloRotacaoPlanoXY.TabIndex = 3;
            this.tooltxtBxAnguloRotacaoXY.SetToolTip(this.txtBxAnguloRotacaoPlanoXY, "defina o ângulo de rotação para o plano XY");
            this.txtBxAnguloRotacaoPlanoXY.TextChanged += new System.EventHandler(this.txtBxAnguloRotacaoPlanoXY_TextChanged_1);
            // 
            // lblAnguloBaseRotacaoXY
            // 
            this.lblAnguloBaseRotacaoXY.AutoSize = true;
            this.lblAnguloBaseRotacaoXY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnguloBaseRotacaoXY.Location = new System.Drawing.Point(3, 149);
            this.lblAnguloBaseRotacaoXY.Name = "lblAnguloBaseRotacaoXY";
            this.lblAnguloBaseRotacaoXY.Size = new System.Drawing.Size(174, 15);
            this.lblAnguloBaseRotacaoXY.TabIndex = 14;
            this.lblAnguloBaseRotacaoXY.Text = "Ângulo Rotação Plano XY:";
            this.toollblAnguloBaseRotacaoXY.SetToolTip(this.lblAnguloBaseRotacaoXY, "o campo ao lado define o ângulo de rotação para o plano XY");
            // 
            // txtBxAnguloRotaçãoPlanoYZ
            // 
            this.txtBxAnguloRotaçãoPlanoYZ.Location = new System.Drawing.Point(201, 105);
            this.txtBxAnguloRotaçãoPlanoYZ.Name = "txtBxAnguloRotaçãoPlanoYZ";
            this.txtBxAnguloRotaçãoPlanoYZ.Size = new System.Drawing.Size(191, 20);
            this.txtBxAnguloRotaçãoPlanoYZ.TabIndex = 2;
            this.tooltxtAnguloRotacaoYZ.SetToolTip(this.txtBxAnguloRotaçãoPlanoYZ, "defina o ângulo de rotação para o plano YZ");
            this.txtBxAnguloRotaçãoPlanoYZ.TextChanged += new System.EventHandler(this.txtBxAnguloRotaçãoPlanoYZ_TextChanged_1);
            // 
            // lblAnguloBaseRotacaoYZ
            // 
            this.lblAnguloBaseRotacaoYZ.AutoSize = true;
            this.lblAnguloBaseRotacaoYZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnguloBaseRotacaoYZ.Location = new System.Drawing.Point(3, 106);
            this.lblAnguloBaseRotacaoYZ.Name = "lblAnguloBaseRotacaoYZ";
            this.lblAnguloBaseRotacaoYZ.Size = new System.Drawing.Size(173, 15);
            this.lblAnguloBaseRotacaoYZ.TabIndex = 9;
            this.lblAnguloBaseRotacaoYZ.Text = "Ângulo Rotação Plano YZ:";
            this.toollblAnguloBaseRotacaoYZ.SetToolTip(this.lblAnguloBaseRotacaoYZ, "o campo ao lado define o ângulo de rotação para o plano YZ");
            // 
            // txtBxAnguloRotacaoPlanoXZ
            // 
            this.txtBxAnguloRotacaoPlanoXZ.Location = new System.Drawing.Point(201, 66);
            this.txtBxAnguloRotacaoPlanoXZ.Name = "txtBxAnguloRotacaoPlanoXZ";
            this.txtBxAnguloRotacaoPlanoXZ.Size = new System.Drawing.Size(183, 20);
            this.txtBxAnguloRotacaoPlanoXZ.TabIndex = 1;
            this.tooltxtBxAnguloRotacaoPlanoXZ.SetToolTip(this.txtBxAnguloRotacaoPlanoXZ, "defina o ângulo de rotação para o plano XZ");
            this.txtBxAnguloRotacaoPlanoXZ.TextChanged += new System.EventHandler(this.txtBxAnguloRotacaoPlanoXZ_TextChanged_1);
            // 
            // lblAnguloBaseRotacaoXZ
            // 
            this.lblAnguloBaseRotacaoXZ.AutoSize = true;
            this.lblAnguloBaseRotacaoXZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnguloBaseRotacaoXZ.Location = new System.Drawing.Point(3, 71);
            this.lblAnguloBaseRotacaoXZ.Name = "lblAnguloBaseRotacaoXZ";
            this.lblAnguloBaseRotacaoXZ.Size = new System.Drawing.Size(178, 15);
            this.lblAnguloBaseRotacaoXZ.TabIndex = 7;
            this.lblAnguloBaseRotacaoXZ.Text = "Ângulo Rotação Plano XZ: ";
            this.toollblAnguloBaseRotacaoXZ.SetToolTip(this.lblAnguloBaseRotacaoXZ, "o campo ao lado define o ângulo de rotação para o plano XZ");
            // 
            // btnRotacionar
            // 
            this.btnRotacionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotacionar.Location = new System.Drawing.Point(23, 182);
            this.btnRotacionar.Name = "btnRotacionar";
            this.btnRotacionar.Size = new System.Drawing.Size(148, 39);
            this.btnRotacionar.TabIndex = 5;
            this.btnRotacionar.Text = "Rotacionar";
            this.toolBtnRotacionar.SetToolTip(this.btnRotacionar, "rotaciona a imagem, com os ângulos dos 3 planos e a profundidade imaginda para a " +
        "nova imagem");
            this.btnRotacionar.UseVisualStyleBackColor = true;
            this.btnRotacionar.Click += new System.EventHandler(this.btnRotacionar_Click_1);
            // 
            // grpBasesOrtonormaisGuardadas
            // 
            this.grpBasesOrtonormaisGuardadas.Controls.Add(this.lblNomearUmaBaseOrtonomal);
            this.grpBasesOrtonormaisGuardadas.Controls.Add(this.txtBxNomearUmaBaseOrtonormal);
            this.grpBasesOrtonormaisGuardadas.Controls.Add(this.btnGuardarBaseOrtonormal);
            this.grpBasesOrtonormaisGuardadas.Controls.Add(this.btnDeletarBaseOrtonormal);
            this.grpBasesOrtonormaisGuardadas.Controls.Add(this.btnCarregarUmaBaseOrtonormal);
            this.grpBasesOrtonormaisGuardadas.Controls.Add(this.cmbBxBasesGuardadas);
            this.grpBasesOrtonormaisGuardadas.Location = new System.Drawing.Point(224, 31);
            this.grpBasesOrtonormaisGuardadas.Name = "grpBasesOrtonormaisGuardadas";
            this.grpBasesOrtonormaisGuardadas.Size = new System.Drawing.Size(217, 217);
            this.grpBasesOrtonormaisGuardadas.TabIndex = 45;
            this.grpBasesOrtonormaisGuardadas.TabStop = false;
            this.grpBasesOrtonormaisGuardadas.Text = "bases ortonormais guardadas";
            // 
            // lblNomearUmaBaseOrtonomal
            // 
            this.lblNomearUmaBaseOrtonomal.AutoSize = true;
            this.lblNomearUmaBaseOrtonomal.Location = new System.Drawing.Point(9, 68);
            this.lblNomearUmaBaseOrtonomal.Name = "lblNomearUmaBaseOrtonomal";
            this.lblNomearUmaBaseOrtonomal.Size = new System.Drawing.Size(143, 13);
            this.lblNomearUmaBaseOrtonomal.TabIndex = 50;
            this.lblNomearUmaBaseOrtonomal.Text = "nomear uma base ortonormal";
            // 
            // txtBxNomearUmaBaseOrtonormal
            // 
            this.txtBxNomearUmaBaseOrtonormal.Location = new System.Drawing.Point(12, 84);
            this.txtBxNomearUmaBaseOrtonormal.Name = "txtBxNomearUmaBaseOrtonormal";
            this.txtBxNomearUmaBaseOrtonormal.Size = new System.Drawing.Size(199, 20);
            this.txtBxNomearUmaBaseOrtonormal.TabIndex = 8;
            this.toolTxtBxNomearUmaBaseOrtonormal.SetToolTip(this.txtBxNomearUmaBaseOrtonormal, " coloque um nome para a base ortonormal constituida pelos eixos do cursor gzimo.");
            // 
            // btnGuardarBaseOrtonormal
            // 
            this.btnGuardarBaseOrtonormal.Location = new System.Drawing.Point(12, 177);
            this.btnGuardarBaseOrtonormal.Name = "btnGuardarBaseOrtonormal";
            this.btnGuardarBaseOrtonormal.Size = new System.Drawing.Size(200, 23);
            this.btnGuardarBaseOrtonormal.TabIndex = 11;
            this.btnGuardarBaseOrtonormal.Text = "guardar base ortonormal";
            this.toolBtnGuardarBaseOrtonormal.SetToolTip(this.btnGuardarBaseOrtonormal, "guarde em arquivo a base definida pelos três eixos do cursor de planos, também ch" +
        "amado de gzimo");
            this.btnGuardarBaseOrtonormal.UseVisualStyleBackColor = true;
            this.btnGuardarBaseOrtonormal.Click += new System.EventHandler(this.btnGuardarBaseOrtonormal_Click_1);
            // 
            // btnDeletarBaseOrtonormal
            // 
            this.btnDeletarBaseOrtonormal.Location = new System.Drawing.Point(12, 148);
            this.btnDeletarBaseOrtonormal.Name = "btnDeletarBaseOrtonormal";
            this.btnDeletarBaseOrtonormal.Size = new System.Drawing.Size(199, 23);
            this.btnDeletarBaseOrtonormal.TabIndex = 10;
            this.btnDeletarBaseOrtonormal.Text = "deletar base ortonormal";
            this.toolbtnDeletatBaseOrtonormal.SetToolTip(this.btnDeletarBaseOrtonormal, "remova definitivamente a base selecionada, mas não associada ao  cursor gzimo.");
            this.btnDeletarBaseOrtonormal.UseVisualStyleBackColor = true;
            this.btnDeletarBaseOrtonormal.Click += new System.EventHandler(this.btnDeletarBaseOrtonormal_Click_1);
            // 
            // btnCarregarUmaBaseOrtonormal
            // 
            this.btnCarregarUmaBaseOrtonormal.Location = new System.Drawing.Point(12, 119);
            this.btnCarregarUmaBaseOrtonormal.Name = "btnCarregarUmaBaseOrtonormal";
            this.btnCarregarUmaBaseOrtonormal.Size = new System.Drawing.Size(199, 23);
            this.btnCarregarUmaBaseOrtonormal.TabIndex = 9;
            this.btnCarregarUmaBaseOrtonormal.Text = "carregar base para o Gzimo";
            this.toolbtnCarregarUmaBaseOrtonormal.SetToolTip(this.btnCarregarUmaBaseOrtonormal, "transfere a base selecionada para o cursor gzimo.");
            this.btnCarregarUmaBaseOrtonormal.UseVisualStyleBackColor = true;
            this.btnCarregarUmaBaseOrtonormal.Click += new System.EventHandler(this.btnCarregarUmaBaseOrtonormal_Click_1);
            // 
            // cmbBxBasesGuardadas
            // 
            this.cmbBxBasesGuardadas.FormattingEnabled = true;
            this.cmbBxBasesGuardadas.Location = new System.Drawing.Point(10, 13);
            this.cmbBxBasesGuardadas.Name = "cmbBxBasesGuardadas";
            this.cmbBxBasesGuardadas.Size = new System.Drawing.Size(202, 21);
            this.cmbBxBasesGuardadas.TabIndex = 14;
            // 
            // cmbBxTipoPerspectiva
            // 
            this.cmbBxTipoPerspectiva.FormattingEnabled = true;
            this.cmbBxTipoPerspectiva.Items.AddRange(new object[] {
            "Perspectiva Isométrica",
            "Perspectiva Geométrica"});
            this.cmbBxTipoPerspectiva.Location = new System.Drawing.Point(23, 69);
            this.cmbBxTipoPerspectiva.Name = "cmbBxTipoPerspectiva";
            this.cmbBxTipoPerspectiva.Size = new System.Drawing.Size(195, 21);
            this.cmbBxTipoPerspectiva.TabIndex = 13;
            this.toolcmbxTipoPerspectiva.SetToolTip(this.cmbBxTipoPerspectiva, "modifique o tipo de perspectiva para desenhos da imagem e cursor (isométrica: des" +
        "enhos; geométrica: realidade");
            // 
            // chkBxRotacaoRelativa
            // 
            this.chkBxRotacaoRelativa.AutoSize = true;
            this.chkBxRotacaoRelativa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxRotacaoRelativa.Location = new System.Drawing.Point(23, 113);
            this.chkBxRotacaoRelativa.Name = "chkBxRotacaoRelativa";
            this.chkBxRotacaoRelativa.Size = new System.Drawing.Size(195, 19);
            this.chkBxRotacaoRelativa.TabIndex = 6;
            this.chkBxRotacaoRelativa.Text = "uso De Rotação Absoluta?";
            this.toolchkBxRotacaoRelativa.SetToolTip(this.chkBxRotacaoRelativa, "quando marcado, faz uma rotação em que os ângulos iniciais do cursor gzimo também" +
        ", uma rotação sem adicionar ângulos, mas sim definir os ângulos");
            this.chkBxRotacaoRelativa.UseVisualStyleBackColor = true;
            this.chkBxRotacaoRelativa.CheckedChanged += new System.EventHandler(this.chkBxRotacaoRelativa_CheckedChanged_1);
            // 
            // lblMsgStatus
            // 
            this.lblMsgStatus.AutoSize = true;
            this.lblMsgStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsgStatus.Location = new System.Drawing.Point(89, 516);
            this.lblMsgStatus.Name = "lblMsgStatus";
            this.lblMsgStatus.Size = new System.Drawing.Size(96, 16);
            this.lblMsgStatus.TabIndex = 53;
            this.lblMsgStatus.Text = "Mensagens: ";
            this.toollblMsgSTatus.SetToolTip(this.lblMsgStatus, "Leia as diversas mensagens do proccessamento do aplicativo.");
            // 
            // TabControlFuncoesDoAplicativo
            // 
            this.TabControlFuncoesDoAplicativo.Controls.Add(this.tabPageRotacoes);
            this.TabControlFuncoesDoAplicativo.Controls.Add(this.tabPageAnimacoes);
            this.TabControlFuncoesDoAplicativo.Location = new System.Drawing.Point(701, 20);
            this.TabControlFuncoesDoAplicativo.Name = "TabControlFuncoesDoAplicativo";
            this.TabControlFuncoesDoAplicativo.SelectedIndex = 0;
            this.TabControlFuncoesDoAplicativo.Size = new System.Drawing.Size(604, 742);
            this.TabControlFuncoesDoAplicativo.TabIndex = 54;
            // 
            // btnLimparImagensGeradasPage0
            // 
            this.btnLimparImagensGeradasPage0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimparImagensGeradasPage0.Location = new System.Drawing.Point(92, 561);
            this.btnLimparImagensGeradasPage0.Name = "btnLimparImagensGeradasPage0";
            this.btnLimparImagensGeradasPage0.Size = new System.Drawing.Size(465, 33);
            this.btnLimparImagensGeradasPage0.TabIndex = 59;
            this.btnLimparImagensGeradasPage0.Text = "limpar lista de imagens geradas";
            this.btnLimparImagensGeradasPage0.UseVisualStyleBackColor = true;
            this.btnLimparImagensGeradasPage0.Click += new System.EventHandler(this.btnLimparImagensGeradasPage0_Click);
            // 
            // windowPerspectivas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 681);
            this.Controls.Add(this.TabControlFuncoesDoAplicativo);
            this.Controls.Add(this.mnStrpMenuPrincipal);
            this.MainMenuStrip = this.mnStrpMenuPrincipal;
            this.Name = "windowPerspectivas";
            this.Text = "Correção de Perspectivas vr 2.0";
            this.mnStrpMenuPrincipal.ResumeLayout(false);
            this.mnStrpMenuPrincipal.PerformLayout();
            this.tabPageAnimacoes.ResumeLayout(false);
            this.tabPageAnimacoes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPageRotacoes.ResumeLayout(false);
            this.tabPageRotacoes.PerformLayout();
            this.grpBxPainelPrincipal.ResumeLayout(false);
            this.grpBxPainelPrincipal.PerformLayout();
            this.grpBxAngulosEProfundidade.ResumeLayout(false);
            this.grpBxAngulosEProfundidade.PerformLayout();
            this.grpBasesOrtonormaisGuardadas.ResumeLayout(false);
            this.grpBasesOrtonormaisGuardadas.PerformLayout();
            this.TabControlFuncoesDoAplicativo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList lstImgImagensRotacionar3D;
        private System.Windows.Forms.MenuStrip mnStrpMenuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem arquivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem carregarImagemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarImagemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.Label lblAlturaImaginada;
        private System.Windows.Forms.TabPage tabPageAnimacoes;
        private System.Windows.Forms.Button btnCarregarRotacoes;
        private System.Windows.Forms.ComboBox cmbBxPlanoDeRotacao;
        private System.Windows.Forms.Label lblNumeroDeImagens;
        private System.Windows.Forms.TextBox txtBxNumeroDeImagens;
        private System.Windows.Forms.TextBox txtBxRotacaoFinal;
        private System.Windows.Forms.TextBox txtBxRotacaoInicial;
        private System.Windows.Forms.TextBox txtBxRotacoesGuardadas;
        private System.Windows.Forms.Button btnGeraImagens;
        private System.Windows.Forms.Button btnSalvarRotacoes;
        private System.Windows.Forms.Label lblStatusMsgsPageRotacoes;
        private System.Windows.Forms.Button btnGuardarRotacaoInicialFinal;
        private System.Windows.Forms.Label lblRotacionarDeAte;
        private System.Windows.Forms.Label lblRotacaoFinal;
        private System.Windows.Forms.Label lblRotacaoInicial;
        private System.Windows.Forms.TabPage tabPageRotacoes;
        private System.Windows.Forms.GroupBox grpBxPainelPrincipal;
        private System.Windows.Forms.Label lblTipoPerspectiva;
        private System.Windows.Forms.Button btnGerarObjeto3D;
        private System.Windows.Forms.GroupBox grpBxAngulosEProfundidade;
        private System.Windows.Forms.Label lblTipoDeAngulo;
        private System.Windows.Forms.ComboBox cmbBxTipoDeAngulo;
        private System.Windows.Forms.TextBox txtBxAnguloRotacaoPlanoXY;
        private System.Windows.Forms.Label lblAnguloBaseRotacaoXY;
        private System.Windows.Forms.TextBox txtBxAnguloRotaçãoPlanoYZ;
        private System.Windows.Forms.Label lblAnguloBaseRotacaoYZ;
        private System.Windows.Forms.TextBox txtBxAnguloRotacaoPlanoXZ;
        private System.Windows.Forms.Label lblAnguloBaseRotacaoXZ;
        private System.Windows.Forms.Button btnRotacionar;
        private System.Windows.Forms.GroupBox grpBasesOrtonormaisGuardadas;
        private System.Windows.Forms.Label lblNomearUmaBaseOrtonomal;
        private System.Windows.Forms.TextBox txtBxNomearUmaBaseOrtonormal;
        private System.Windows.Forms.Button btnGuardarBaseOrtonormal;
        private System.Windows.Forms.Button btnDeletarBaseOrtonormal;
        private System.Windows.Forms.Button btnCarregarUmaBaseOrtonormal;
        private System.Windows.Forms.ComboBox cmbBxBasesGuardadas;
        private System.Windows.Forms.ComboBox cmbBxTipoPerspectiva;
        private System.Windows.Forms.CheckBox chkBxRotacaoRelativa;
        private System.Windows.Forms.Label lblMsgStatus;
        private System.Windows.Forms.TabControl TabControlFuncoesDoAplicativo;
        private System.Windows.Forms.TextBox txtBxAlturaImaginada;
        private ToolTip toolTipLblAltura;
        private Button btnLimparListaDeImaensPage1;
        private GroupBox groupBox1;
        private ToolTip toolTipbtnguardar;
        private ToolTip toolTipBtnCarregar;
        private ToolTip toolTipBtnSalvaRotcoes;
        private ToolTip toolTipcmbBxPlanoDeRotacao;
        private ToolTip toolTipLblNumeroDeImagens;
        private ToolTip toolTipTxtNumeroDeImagens;
        private ToolTip toolTipTxtBxRotacaoFinal;
        private ToolTip toolTipTxtBxRotacaoInicial;
        private ToolTip tooltipTxtBxRotacoesGuardada;
        private ToolTip toolTipBtnGeraImagens;
        private ToolTip toolTipLblStatusMsg;
        private ToolTip ToolTiplblRotacionarDeAte;
        private ToolTip toolTiplblRotacaoFinal;
        private ToolTip toolTipLblRotacaoInicial;
        private ToolTip toollblTipoPerspectiva;
        private ToolTip toolbtnGerarObjeto3D;
        private ToolTip toollblTipoDeAngulo;
        private ToolTip toolcmbxTipoDeAngulo;
        private ToolTip toolTxtBxAlturaImaginada;
        private ToolTip tooltxtBxAnguloRotacaoXY;
        private ToolTip toollblAnguloBaseRotacaoXY;
        private ToolTip tooltxtAnguloRotacaoYZ;
        private ToolTip toollblAnguloBaseRotacaoYZ;
        private ToolTip tooltxtBxAnguloRotacaoPlanoXZ;
        private ToolTip toollblAnguloBaseRotacaoXZ;
        private ToolTip toolgrpBxAngulosEProfundidade;
        private ToolTip toolBtnRotacionar;
        private ToolTip toolTxtBxNomearUmaBaseOrtonormal;
        private ToolTip toolBtnGuardarBaseOrtonormal;
        private ToolTip toolbtnDeletatBaseOrtonormal;
        private ToolTip toolbtnCarregarUmaBaseOrtonormal;
        private ToolTip toolcmbxTipoPerspectiva;
        private ToolTip toolchkBxRotacaoRelativa;
        private ToolTip toollblMsgSTatus;
        private Button btnLimparImagensGeradasPage0;
    }
}