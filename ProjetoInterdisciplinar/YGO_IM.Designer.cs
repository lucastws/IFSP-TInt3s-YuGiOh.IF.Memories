namespace ProjetoInterdisciplinar
{
    partial class YGO_IM
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">verdade se for necessário descartar os recursos gerenciados; caso contrário, falso.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte do Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YGO_IM));
            this.lstCartas = new System.Windows.Forms.ListBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.dgvRanking = new System.Windows.Forms.DataGridView();
            this.btnRanking = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnCartas = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnJogar = new Bunifu.Framework.UI.BunifuTileButton();
            this.pctVerso = new System.Windows.Forms.PictureBox();
            this.pctCarta = new System.Windows.Forms.PictureBox();
            this.btnVoltar = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnLogar = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnCadastrar = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnSobre = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnSair = new Bunifu.Framework.UI.BunifuTileButton();
            this.btnAjuda = new Bunifu.Framework.UI.BunifuTileButton();
            this.lblSobre = new System.Windows.Forms.Label();
            this.btnLogout = new Bunifu.Framework.UI.BunifuTileButton();
            this.lblAjuda = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVerso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctCarta)).BeginInit();
            this.SuspendLayout();
            // 
            // lstCartas
            // 
            this.lstCartas.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            resources.ApplyResources(this.lstCartas, "lstCartas");
            this.lstCartas.BackColor = System.Drawing.Color.White;
            this.lstCartas.ForeColor = System.Drawing.Color.Black;
            this.lstCartas.FormattingEnabled = true;
            this.lstCartas.Name = "lstCartas";
            this.lstCartas.SelectedIndexChanged += new System.EventHandler(this.lstCartas_SelectedIndexChanged);
            // 
            // btnEnviar
            // 
            resources.ApplyResources(this.btnEnviar, "btnEnviar");
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // txtUsuario
            // 
            resources.ApplyResources(this.txtUsuario, "txtUsuario");
            this.txtUsuario.Name = "txtUsuario";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // dgvRanking
            // 
            this.dgvRanking.AllowUserToAddRows = false;
            this.dgvRanking.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgvRanking, "dgvRanking");
            this.dgvRanking.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRanking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRanking.Name = "dgvRanking";
            this.dgvRanking.ReadOnly = true;
            this.dgvRanking.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRanking_CellFormatting);
            // 
            // btnRanking
            // 
            this.btnRanking.AllowDrop = true;
            resources.ApplyResources(this.btnRanking, "btnRanking");
            this.btnRanking.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnRanking.BackColor = System.Drawing.Color.SeaGreen;
            this.btnRanking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnRanking.color = System.Drawing.Color.SeaGreen;
            this.btnRanking.colorActive = System.Drawing.Color.MediumSeaGreen;
            this.btnRanking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRanking.ForeColor = System.Drawing.Color.White;
            this.btnRanking.Image = ((System.Drawing.Image)(resources.GetObject("btnRanking.Image")));
            this.btnRanking.ImagePosition = 20;
            this.btnRanking.ImageZoom = 50;
            this.btnRanking.LabelPosition = 41;
            this.btnRanking.LabelText = "Ranking";
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
            // 
            // btnCartas
            // 
            this.btnCartas.AllowDrop = true;
            resources.ApplyResources(this.btnCartas, "btnCartas");
            this.btnCartas.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnCartas.BackColor = System.Drawing.Color.SeaGreen;
            this.btnCartas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnCartas.color = System.Drawing.Color.SeaGreen;
            this.btnCartas.colorActive = System.Drawing.Color.MediumSeaGreen;
            this.btnCartas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCartas.ForeColor = System.Drawing.Color.White;
            this.btnCartas.Image = ((System.Drawing.Image)(resources.GetObject("btnCartas.Image")));
            this.btnCartas.ImagePosition = 20;
            this.btnCartas.ImageZoom = 50;
            this.btnCartas.LabelPosition = 41;
            this.btnCartas.LabelText = "Cartas";
            this.btnCartas.Name = "btnCartas";
            this.btnCartas.Click += new System.EventHandler(this.btnCartas_Click);
            // 
            // btnJogar
            // 
            this.btnJogar.AllowDrop = true;
            resources.ApplyResources(this.btnJogar, "btnJogar");
            this.btnJogar.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnJogar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnJogar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnJogar.color = System.Drawing.Color.SeaGreen;
            this.btnJogar.colorActive = System.Drawing.Color.MediumSeaGreen;
            this.btnJogar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJogar.ForeColor = System.Drawing.Color.White;
            this.btnJogar.Image = ((System.Drawing.Image)(resources.GetObject("btnJogar.Image")));
            this.btnJogar.ImagePosition = 20;
            this.btnJogar.ImageZoom = 50;
            this.btnJogar.LabelPosition = 41;
            this.btnJogar.LabelText = "Jogar";
            this.btnJogar.Name = "btnJogar";
            this.btnJogar.Click += new System.EventHandler(this.btnJogar_Click);
            // 
            // pctVerso
            // 
            resources.ApplyResources(this.pctVerso, "pctVerso");
            this.pctVerso.Name = "pctVerso";
            this.pctVerso.TabStop = false;
            // 
            // pctCarta
            // 
            resources.ApplyResources(this.pctCarta, "pctCarta");
            this.pctCarta.Name = "pctCarta";
            this.pctCarta.TabStop = false;
            // 
            // btnVoltar
            // 
            this.btnVoltar.AllowDrop = true;
            resources.ApplyResources(this.btnVoltar, "btnVoltar");
            this.btnVoltar.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnVoltar.BackColor = System.Drawing.Color.Maroon;
            this.btnVoltar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnVoltar.color = System.Drawing.Color.Maroon;
            this.btnVoltar.colorActive = System.Drawing.Color.Red;
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.Image = ((System.Drawing.Image)(resources.GetObject("btnVoltar.Image")));
            this.btnVoltar.ImagePosition = 20;
            this.btnVoltar.ImageZoom = 50;
            this.btnVoltar.LabelPosition = 41;
            this.btnVoltar.LabelText = "Voltar";
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnLogar
            // 
            this.btnLogar.AllowDrop = true;
            resources.ApplyResources(this.btnLogar, "btnLogar");
            this.btnLogar.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnLogar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLogar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnLogar.color = System.Drawing.Color.DodgerBlue;
            this.btnLogar.colorActive = System.Drawing.Color.DeepSkyBlue;
            this.btnLogar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogar.ForeColor = System.Drawing.Color.White;
            this.btnLogar.Image = ((System.Drawing.Image)(resources.GetObject("btnLogar.Image")));
            this.btnLogar.ImagePosition = 20;
            this.btnLogar.ImageZoom = 50;
            this.btnLogar.LabelPosition = 41;
            this.btnLogar.LabelText = "Logar";
            this.btnLogar.Name = "btnLogar";
            this.btnLogar.Click += new System.EventHandler(this.btnLogar_Click);
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.AllowDrop = true;
            resources.ApplyResources(this.btnCadastrar, "btnCadastrar");
            this.btnCadastrar.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnCadastrar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCadastrar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnCadastrar.color = System.Drawing.Color.DodgerBlue;
            this.btnCadastrar.colorActive = System.Drawing.Color.DeepSkyBlue;
            this.btnCadastrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastrar.ForeColor = System.Drawing.Color.White;
            this.btnCadastrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCadastrar.Image")));
            this.btnCadastrar.ImagePosition = 20;
            this.btnCadastrar.ImageZoom = 50;
            this.btnCadastrar.LabelPosition = 41;
            this.btnCadastrar.LabelText = "Cadastrar";
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // btnSobre
            // 
            this.btnSobre.AllowDrop = true;
            this.btnSobre.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnSobre.BackColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.btnSobre, "btnSobre");
            this.btnSobre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnSobre.color = System.Drawing.Color.Gray;
            this.btnSobre.colorActive = System.Drawing.Color.LightGray;
            this.btnSobre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSobre.ForeColor = System.Drawing.Color.White;
            this.btnSobre.Image = ((System.Drawing.Image)(resources.GetObject("btnSobre.Image")));
            this.btnSobre.ImagePosition = 20;
            this.btnSobre.ImageZoom = 50;
            this.btnSobre.LabelPosition = 41;
            this.btnSobre.LabelText = "Sobre";
            this.btnSobre.Name = "btnSobre";
            this.btnSobre.Click += new System.EventHandler(this.btnSobre_Click);
            // 
            // btnSair
            // 
            this.btnSair.AllowDrop = true;
            resources.ApplyResources(this.btnSair, "btnSair");
            this.btnSair.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnSair.BackColor = System.Drawing.Color.Maroon;
            this.btnSair.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnSair.color = System.Drawing.Color.Maroon;
            this.btnSair.colorActive = System.Drawing.Color.Red;
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.ImagePosition = 20;
            this.btnSair.ImageZoom = 50;
            this.btnSair.LabelPosition = 41;
            this.btnSair.LabelText = "Sair";
            this.btnSair.Name = "btnSair";
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnAjuda
            // 
            this.btnAjuda.AllowDrop = true;
            resources.ApplyResources(this.btnAjuda, "btnAjuda");
            this.btnAjuda.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnAjuda.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAjuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnAjuda.color = System.Drawing.Color.SeaGreen;
            this.btnAjuda.colorActive = System.Drawing.Color.MediumSeaGreen;
            this.btnAjuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjuda.ForeColor = System.Drawing.Color.White;
            this.btnAjuda.Image = ((System.Drawing.Image)(resources.GetObject("btnAjuda.Image")));
            this.btnAjuda.ImagePosition = 20;
            this.btnAjuda.ImageZoom = 50;
            this.btnAjuda.LabelPosition = 41;
            this.btnAjuda.LabelText = "Ajuda";
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // lblSobre
            // 
            resources.ApplyResources(this.lblSobre, "lblSobre");
            this.lblSobre.BackColor = System.Drawing.Color.White;
            this.lblSobre.Name = "lblSobre";
            // 
            // btnLogout
            // 
            this.btnLogout.AllowDrop = true;
            resources.ApplyResources(this.btnLogout, "btnLogout");
            this.btnLogout.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.btnLogout.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLogout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnLogout.color = System.Drawing.Color.DodgerBlue;
            this.btnLogout.colorActive = System.Drawing.Color.DeepSkyBlue;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImagePosition = 20;
            this.btnLogout.ImageZoom = 50;
            this.btnLogout.LabelPosition = 41;
            this.btnLogout.LabelText = "Log Out";
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblAjuda
            // 
            resources.ApplyResources(this.lblAjuda, "lblAjuda");
            this.lblAjuda.BackColor = System.Drawing.Color.White;
            this.lblAjuda.Name = "lblAjuda";
            // 
            // YGO_IM
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lblAjuda);
            this.Controls.Add(this.btnAjuda);
            this.Controls.Add(this.btnLogar);
            this.Controls.Add(this.btnRanking);
            this.Controls.Add(this.lblSobre);
            this.Controls.Add(this.dgvRanking);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.btnSobre);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.btnCadastrar);
            this.Controls.Add(this.btnCartas);
            this.Controls.Add(this.lstCartas);
            this.Controls.Add(this.pctCarta);
            this.Controls.Add(this.pctVerso);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnJogar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnLogout);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "YGO_IM";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.YGO_IM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctVerso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctCarta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuTileButton btnCartas;
        private Bunifu.Framework.UI.BunifuTileButton btnJogar;
        private System.Windows.Forms.PictureBox pctVerso;
        private System.Windows.Forms.PictureBox pctCarta;
        private System.Windows.Forms.ListBox lstCartas;
        private Bunifu.Framework.UI.BunifuTileButton btnVoltar;
        private Bunifu.Framework.UI.BunifuTileButton btnLogar;
        private Bunifu.Framework.UI.BunifuTileButton btnCadastrar;
        private System.Windows.Forms.Button btnEnviar;
        private Bunifu.Framework.UI.BunifuTileButton btnSobre;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPassword;
        private Bunifu.Framework.UI.BunifuTileButton btnRanking;
        private System.Windows.Forms.DataGridView dgvRanking;
        private Bunifu.Framework.UI.BunifuTileButton btnSair;
        private Bunifu.Framework.UI.BunifuTileButton btnAjuda;
        private System.Windows.Forms.Label lblSobre;
        private Bunifu.Framework.UI.BunifuTileButton btnLogout;
        private System.Windows.Forms.Label lblAjuda;
    }
}

