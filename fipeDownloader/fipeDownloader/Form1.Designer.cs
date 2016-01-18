namespace FipeDownloader
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnDownload = new System.Windows.Forms.Button();
            this.cmbMarcas = new System.Windows.Forms.ComboBox();
            this.lblMarcas = new System.Windows.Forms.Label();
            this.cmbModelos = new System.Windows.Forms.ComboBox();
            this.lblModelos = new System.Windows.Forms.Label();
            this.cmbAnos = new System.Windows.Forms.ComboBox();
            this.lblAnos = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.staStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.groDetalhes = new System.Windows.Forms.GroupBox();
            this.chkIncluirDet = new System.Windows.Forms.CheckBox();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.txtCodFipe = new System.Windows.Forms.TextBox();
            this.txtMesRef = new System.Windows.Forms.TextBox();
            this.lblPreco = new System.Windows.Forms.Label();
            this.lblCodigoFipe = new System.Windows.Forms.Label();
            this.lblMes = new System.Windows.Forms.Label();
            this.lblTipos = new System.Windows.Forms.Label();
            this.cmbTipos = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.groDetalhes.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(177, 305);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(76, 25);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // cmbMarcas
            // 
            this.cmbMarcas.FormattingEnabled = true;
            this.cmbMarcas.Location = new System.Drawing.Point(50, 47);
            this.cmbMarcas.Name = "cmbMarcas";
            this.cmbMarcas.Size = new System.Drawing.Size(376, 21);
            this.cmbMarcas.TabIndex = 1;
            this.cmbMarcas.SelectedIndexChanged += new System.EventHandler(this.cmbMarcas_SelectedIndexChanged);
            // 
            // lblMarcas
            // 
            this.lblMarcas.AutoSize = true;
            this.lblMarcas.Location = new System.Drawing.Point(7, 50);
            this.lblMarcas.Name = "lblMarcas";
            this.lblMarcas.Size = new System.Drawing.Size(42, 13);
            this.lblMarcas.TabIndex = 2;
            this.lblMarcas.Text = "Marcas";
            // 
            // cmbModelos
            // 
            this.cmbModelos.Enabled = false;
            this.cmbModelos.FormattingEnabled = true;
            this.cmbModelos.Location = new System.Drawing.Point(50, 83);
            this.cmbModelos.Name = "cmbModelos";
            this.cmbModelos.Size = new System.Drawing.Size(376, 21);
            this.cmbModelos.TabIndex = 3;
            this.cmbModelos.SelectedIndexChanged += new System.EventHandler(this.cmbModelos_SelectedIndexChanged);
            // 
            // lblModelos
            // 
            this.lblModelos.AutoSize = true;
            this.lblModelos.Location = new System.Drawing.Point(2, 86);
            this.lblModelos.Name = "lblModelos";
            this.lblModelos.Size = new System.Drawing.Size(47, 13);
            this.lblModelos.TabIndex = 4;
            this.lblModelos.Text = "Modelos";
            // 
            // cmbAnos
            // 
            this.cmbAnos.Enabled = false;
            this.cmbAnos.FormattingEnabled = true;
            this.cmbAnos.Location = new System.Drawing.Point(50, 119);
            this.cmbAnos.Name = "cmbAnos";
            this.cmbAnos.Size = new System.Drawing.Size(376, 21);
            this.cmbAnos.TabIndex = 5;
            this.cmbAnos.SelectedIndexChanged += new System.EventHandler(this.cmbAnos_SelectedIndexChanged);
            // 
            // lblAnos
            // 
            this.lblAnos.AutoSize = true;
            this.lblAnos.Location = new System.Drawing.Point(18, 122);
            this.lblAnos.Name = "lblAnos";
            this.lblAnos.Size = new System.Drawing.Size(31, 13);
            this.lblAnos.TabIndex = 6;
            this.lblAnos.Text = "Anos";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staStatusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(431, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // staStatusBar
            // 
            this.staStatusBar.Name = "staStatusBar";
            this.staStatusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // groDetalhes
            // 
            this.groDetalhes.Controls.Add(this.chkIncluirDet);
            this.groDetalhes.Controls.Add(this.txtPreco);
            this.groDetalhes.Controls.Add(this.txtCodFipe);
            this.groDetalhes.Controls.Add(this.txtMesRef);
            this.groDetalhes.Controls.Add(this.lblPreco);
            this.groDetalhes.Controls.Add(this.lblCodigoFipe);
            this.groDetalhes.Controls.Add(this.lblMes);
            this.groDetalhes.Location = new System.Drawing.Point(5, 162);
            this.groDetalhes.Name = "groDetalhes";
            this.groDetalhes.Size = new System.Drawing.Size(420, 137);
            this.groDetalhes.TabIndex = 15;
            this.groDetalhes.TabStop = false;
            this.groDetalhes.Text = "Detalhes";
            // 
            // chkIncluirDet
            // 
            this.chkIncluirDet.AutoSize = true;
            this.chkIncluirDet.Location = new System.Drawing.Point(253, 104);
            this.chkIncluirDet.Name = "chkIncluirDet";
            this.chkIncluirDet.Size = new System.Drawing.Size(161, 17);
            this.chkIncluirDet.TabIndex = 20;
            this.chkIncluirDet.Text = "Incluir detalhes no download";
            this.chkIncluirDet.UseVisualStyleBackColor = true;
            // 
            // txtPreco
            // 
            this.txtPreco.Location = new System.Drawing.Point(103, 72);
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.ReadOnly = true;
            this.txtPreco.Size = new System.Drawing.Size(311, 20);
            this.txtPreco.TabIndex = 19;
            // 
            // txtCodFipe
            // 
            this.txtCodFipe.Location = new System.Drawing.Point(103, 46);
            this.txtCodFipe.Name = "txtCodFipe";
            this.txtCodFipe.ReadOnly = true;
            this.txtCodFipe.Size = new System.Drawing.Size(311, 20);
            this.txtCodFipe.TabIndex = 18;
            // 
            // txtMesRef
            // 
            this.txtMesRef.Location = new System.Drawing.Point(103, 20);
            this.txtMesRef.Name = "txtMesRef";
            this.txtMesRef.ReadOnly = true;
            this.txtMesRef.Size = new System.Drawing.Size(311, 20);
            this.txtMesRef.TabIndex = 17;
            // 
            // lblPreco
            // 
            this.lblPreco.AutoSize = true;
            this.lblPreco.Location = new System.Drawing.Point(28, 75);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(66, 13);
            this.lblPreco.TabIndex = 16;
            this.lblPreco.Text = "Preço médio";
            // 
            // lblCodigoFipe
            // 
            this.lblCodigoFipe.AutoSize = true;
            this.lblCodigoFipe.Location = new System.Drawing.Point(31, 49);
            this.lblCodigoFipe.Name = "lblCodigoFipe";
            this.lblCodigoFipe.Size = new System.Drawing.Size(63, 13);
            this.lblCodigoFipe.TabIndex = 15;
            this.lblCodigoFipe.Text = "Código Fipe";
            // 
            // lblMes
            // 
            this.lblMes.AutoSize = true;
            this.lblMes.Location = new System.Drawing.Point(2, 23);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(92, 13);
            this.lblMes.TabIndex = 14;
            this.lblMes.Text = "Mês de referencia";
            // 
            // lblTipos
            // 
            this.lblTipos.AutoSize = true;
            this.lblTipos.Location = new System.Drawing.Point(16, 15);
            this.lblTipos.Name = "lblTipos";
            this.lblTipos.Size = new System.Drawing.Size(33, 13);
            this.lblTipos.TabIndex = 17;
            this.lblTipos.Text = "Tipos";
            // 
            // cmbTipos
            // 
            this.cmbTipos.FormattingEnabled = true;
            this.cmbTipos.Location = new System.Drawing.Point(50, 12);
            this.cmbTipos.Name = "cmbTipos";
            this.cmbTipos.Size = new System.Drawing.Size(376, 21);
            this.cmbTipos.TabIndex = 16;
            this.cmbTipos.SelectedIndexChanged += new System.EventHandler(this.cmbTipos_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 359);
            this.Controls.Add(this.lblTipos);
            this.Controls.Add(this.cmbTipos);
            this.Controls.Add(this.groDetalhes);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblAnos);
            this.Controls.Add(this.cmbAnos);
            this.Controls.Add(this.lblModelos);
            this.Controls.Add(this.cmbModelos);
            this.Controls.Add(this.lblMarcas);
            this.Controls.Add(this.cmbMarcas);
            this.Controls.Add(this.btnDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fipe Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groDetalhes.ResumeLayout(false);
            this.groDetalhes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.ComboBox cmbMarcas;
        private System.Windows.Forms.Label lblMarcas;
        private System.Windows.Forms.ComboBox cmbModelos;
        private System.Windows.Forms.Label lblModelos;
        private System.Windows.Forms.ComboBox cmbAnos;
        private System.Windows.Forms.Label lblAnos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel staStatusBar;
        private System.Windows.Forms.GroupBox groDetalhes;
        private System.Windows.Forms.CheckBox chkIncluirDet;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.TextBox txtCodFipe;
        private System.Windows.Forms.TextBox txtMesRef;
        private System.Windows.Forms.Label lblPreco;
        private System.Windows.Forms.Label lblCodigoFipe;
        private System.Windows.Forms.Label lblMes;
        private System.Windows.Forms.Label lblTipos;
        private System.Windows.Forms.ComboBox cmbTipos;
    }
}