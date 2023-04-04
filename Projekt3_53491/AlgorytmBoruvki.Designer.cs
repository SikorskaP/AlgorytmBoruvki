namespace AlgorytmBoruvki
{
    partial class AlgorytmBoruvki
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
            this.DgvSzukanaMacierz = new System.Windows.Forms.DataGridView();
            this.LblTytuł = new System.Windows.Forms.Label();
            this.LblSzukanaMacierz = new System.Windows.Forms.Label();
            this.LblWynik = new System.Windows.Forms.Label();
            this.DgvWynik = new System.Windows.Forms.DataGridView();
            this.LblListaWierzchołków = new System.Windows.Forms.Label();
            this.TxtLiczbaWierzchołków = new System.Windows.Forms.TextBox();
            this.BtnStworzenieMacierzy = new System.Windows.Forms.Button();
            this.TxtListaSąsiedztwa = new System.Windows.Forms.TextBox();
            this.LblListaSąsiedztwa = new System.Windows.Forms.Label();
            this.BtnAlgorytmBoruvki = new System.Windows.Forms.Button();
            this.BtnLosoweWagi = new System.Windows.Forms.Button();
            this.ErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.LblOstrzeżenie = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSzukanaMacierz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvWynik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvSzukanaMacierz
            // 
            this.DgvSzukanaMacierz.AllowUserToAddRows = false;
            this.DgvSzukanaMacierz.AllowUserToDeleteRows = false;
            this.DgvSzukanaMacierz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvSzukanaMacierz.Location = new System.Drawing.Point(12, 81);
            this.DgvSzukanaMacierz.Name = "DgvSzukanaMacierz";
            this.DgvSzukanaMacierz.Size = new System.Drawing.Size(580, 290);
            this.DgvSzukanaMacierz.TabIndex = 0;
            this.DgvSzukanaMacierz.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSzukanaMacierz_CellValidated);
            this.DgvSzukanaMacierz.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSzukanaMacierz_CellValueChanged);
            // 
            // LblTytuł
            // 
            this.LblTytuł.AutoSize = true;
            this.LblTytuł.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTytuł.Location = new System.Drawing.Point(184, 9);
            this.LblTytuł.Name = "LblTytuł";
            this.LblTytuł.Size = new System.Drawing.Size(904, 25);
            this.LblTytuł.TabIndex = 1;
            this.LblTytuł.Text = "Znajdowanie minimalnego drzewa rozpinającego z zastosowaniem algorytmu Borůvki";
            // 
            // LblSzukanaMacierz
            // 
            this.LblSzukanaMacierz.AutoSize = true;
            this.LblSzukanaMacierz.Location = new System.Drawing.Point(12, 53);
            this.LblSzukanaMacierz.Name = "LblSzukanaMacierz";
            this.LblSzukanaMacierz.Size = new System.Drawing.Size(123, 18);
            this.LblSzukanaMacierz.TabIndex = 2;
            this.LblSzukanaMacierz.Text = "Szukana macierz";
            // 
            // LblWynik
            // 
            this.LblWynik.AutoSize = true;
            this.LblWynik.Location = new System.Drawing.Point(701, 53);
            this.LblWynik.Name = "LblWynik";
            this.LblWynik.Size = new System.Drawing.Size(49, 18);
            this.LblWynik.TabIndex = 3;
            this.LblWynik.Text = "Wynik";
            // 
            // DgvWynik
            // 
            this.DgvWynik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvWynik.Location = new System.Drawing.Point(704, 81);
            this.DgvWynik.Name = "DgvWynik";
            this.DgvWynik.ReadOnly = true;
            this.DgvWynik.Size = new System.Drawing.Size(580, 290);
            this.DgvWynik.TabIndex = 4;
            // 
            // LblListaWierzchołków
            // 
            this.LblListaWierzchołków.AutoSize = true;
            this.LblListaWierzchołków.Location = new System.Drawing.Point(38, 380);
            this.LblListaWierzchołków.Name = "LblListaWierzchołków";
            this.LblListaWierzchołków.Size = new System.Drawing.Size(146, 18);
            this.LblListaWierzchołków.TabIndex = 5;
            this.LblListaWierzchołków.Text = "Liczba wierzchołków";
            // 
            // TxtLiczbaWierzchołków
            // 
            this.TxtLiczbaWierzchołków.Location = new System.Drawing.Point(215, 377);
            this.TxtLiczbaWierzchołków.MaxLength = 2;
            this.TxtLiczbaWierzchołków.Name = "TxtLiczbaWierzchołków";
            this.TxtLiczbaWierzchołków.Size = new System.Drawing.Size(115, 24);
            this.TxtLiczbaWierzchołków.TabIndex = 6;
            this.TxtLiczbaWierzchołków.Text = "16";
            this.TxtLiczbaWierzchołków.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtLiczbaWierzchołków.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLiczbaWierzchołków_KeyPress);
            // 
            // BtnStworzenieMacierzy
            // 
            this.BtnStworzenieMacierzy.Location = new System.Drawing.Point(38, 411);
            this.BtnStworzenieMacierzy.Name = "BtnStworzenieMacierzy";
            this.BtnStworzenieMacierzy.Size = new System.Drawing.Size(292, 44);
            this.BtnStworzenieMacierzy.TabIndex = 7;
            this.BtnStworzenieMacierzy.Text = "Stworzenie macierzy o wybranej wielkości";
            this.BtnStworzenieMacierzy.UseVisualStyleBackColor = true;
            this.BtnStworzenieMacierzy.Click += new System.EventHandler(this.BtnStworzenieMacierzy_Click);
            // 
            // TxtListaSąsiedztwa
            // 
            this.TxtListaSąsiedztwa.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.TxtListaSąsiedztwa.Location = new System.Drawing.Point(376, 398);
            this.TxtListaSąsiedztwa.Multiline = true;
            this.TxtListaSąsiedztwa.Name = "TxtListaSąsiedztwa";
            this.TxtListaSąsiedztwa.ReadOnly = true;
            this.TxtListaSąsiedztwa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtListaSąsiedztwa.Size = new System.Drawing.Size(908, 273);
            this.TxtListaSąsiedztwa.TabIndex = 8;
            // 
            // LblListaSąsiedztwa
            // 
            this.LblListaSąsiedztwa.AutoSize = true;
            this.LblListaSąsiedztwa.Location = new System.Drawing.Point(373, 377);
            this.LblListaSąsiedztwa.Name = "LblListaSąsiedztwa";
            this.LblListaSąsiedztwa.Size = new System.Drawing.Size(121, 18);
            this.LblListaSąsiedztwa.TabIndex = 9;
            this.LblListaSąsiedztwa.Text = "Lista sąsiedztwa:";
            // 
            // BtnAlgorytmBoruvki
            // 
            this.BtnAlgorytmBoruvki.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAlgorytmBoruvki.Location = new System.Drawing.Point(38, 598);
            this.BtnAlgorytmBoruvki.Name = "BtnAlgorytmBoruvki";
            this.BtnAlgorytmBoruvki.Size = new System.Drawing.Size(292, 73);
            this.BtnAlgorytmBoruvki.TabIndex = 10;
            this.BtnAlgorytmBoruvki.Text = "Przeprowadzenie algorytmu Borůvki";
            this.BtnAlgorytmBoruvki.UseVisualStyleBackColor = true;
            this.BtnAlgorytmBoruvki.Click += new System.EventHandler(this.BtnAlgorytmBoruvki_Click);
            // 
            // BtnLosoweWagi
            // 
            this.BtnLosoweWagi.Location = new System.Drawing.Point(38, 473);
            this.BtnLosoweWagi.Name = "BtnLosoweWagi";
            this.BtnLosoweWagi.Size = new System.Drawing.Size(292, 44);
            this.BtnLosoweWagi.TabIndex = 11;
            this.BtnLosoweWagi.Text = "Wypełnienie macierzy losowymi wagami";
            this.BtnLosoweWagi.UseVisualStyleBackColor = true;
            this.BtnLosoweWagi.Click += new System.EventHandler(this.BtnLosoweWagi_Click);
            // 
            // ErrorProvider1
            // 
            this.ErrorProvider1.ContainerControl = this;
            // 
            // LblOstrzeżenie
            // 
            this.LblOstrzeżenie.Location = new System.Drawing.Point(588, 81);
            this.LblOstrzeżenie.Name = "LblOstrzeżenie";
            this.LblOstrzeżenie.Size = new System.Drawing.Size(122, 253);
            this.LblOstrzeżenie.TabIndex = 12;
            this.LblOstrzeżenie.Text = "Graf musi być nieskierowany, oraz nie może mieć krawędzi między elementem a nim s" +
    "amym";
            this.LblOstrzeżenie.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LblOstrzeżenie.Visible = false;
            // 
            // AlgorytmBoruvki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1296, 683);
            this.Controls.Add(this.BtnLosoweWagi);
            this.Controls.Add(this.BtnAlgorytmBoruvki);
            this.Controls.Add(this.LblListaSąsiedztwa);
            this.Controls.Add(this.TxtListaSąsiedztwa);
            this.Controls.Add(this.BtnStworzenieMacierzy);
            this.Controls.Add(this.TxtLiczbaWierzchołków);
            this.Controls.Add(this.LblListaWierzchołków);
            this.Controls.Add(this.DgvWynik);
            this.Controls.Add(this.LblWynik);
            this.Controls.Add(this.LblSzukanaMacierz);
            this.Controls.Add(this.LblTytuł);
            this.Controls.Add(this.DgvSzukanaMacierz);
            this.Controls.Add(this.LblOstrzeżenie);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1312, 723);
            this.MinimumSize = new System.Drawing.Size(1312, 721);
            this.Name = "AlgorytmBoruvki";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Algorytm Boruvki";
            ((System.ComponentModel.ISupportInitialize)(this.DgvSzukanaMacierz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvWynik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvSzukanaMacierz;
        private System.Windows.Forms.Label LblTytuł;
        private System.Windows.Forms.Label LblSzukanaMacierz;
        private System.Windows.Forms.Label LblWynik;
        private System.Windows.Forms.DataGridView DgvWynik;
        private System.Windows.Forms.Label LblListaWierzchołków;
        private System.Windows.Forms.TextBox TxtLiczbaWierzchołków;
        private System.Windows.Forms.Button BtnStworzenieMacierzy;
        private System.Windows.Forms.TextBox TxtListaSąsiedztwa;
        private System.Windows.Forms.Label LblListaSąsiedztwa;
        private System.Windows.Forms.Button BtnAlgorytmBoruvki;
        private System.Windows.Forms.Button BtnLosoweWagi;
        private System.Windows.Forms.ErrorProvider ErrorProvider1;
        private System.Windows.Forms.Label LblOstrzeżenie;
    }
}