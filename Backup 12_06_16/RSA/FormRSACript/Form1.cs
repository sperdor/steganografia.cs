using System;
using System.Drawing;
using System.Windows.Forms;
using LibRSA;

namespace FormRSACript
{
    public partial class Form1 : Form
    {
        TabControl tabSessione = new TabControl();
        TabControl tabICD = new TabControl();

        TabPage tabInfo = new TabPage();
        TabPage tabCripta = new TabPage();
        TabPage tabDecripta = new TabPage();

        Button btnCaricaCripta = new Button();
        Button btnCaricaDecripta = new Button();
        Button btnCripta = new Button();
        Button btnDecripta = new Button();
        Button btnProva = new Button();

        PictureBox imgToCript = new PictureBox();
        PictureBox imgToDecript = new PictureBox();

        TextBox txtToDecript = new TextBox();
        TextBox txtToCript = new TextBox();
        TextBox txtCriptaE = new TextBox();
        TextBox txtCriptaN = new TextBox();
        TextBox txtDecriptaD = new TextBox();
        TextBox txtDecriptaN = new TextBox();
        TextBox txtProva = new TextBox();

        string StrnToCript;
        Bitmap imgCrip;
        Bitmap imgDec;

        string fullPath;

        public Form1()
        {
            CreaForm();
            InitializeComponent();
        }
        public void CreaForm()
        {
            //GeneraChiavi n = new GeneraChiavi();

            this.Size = new Size(800, 450);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.CenterToScreen();


            Controls.Add(tabSessione);
            tabSessione.Size = new Size(this.Width, this.Height);
            tabSessione.Controls.Add(tabInfo);
            tabSessione.Controls.Add(tabCripta);
            tabSessione.Controls.Add(tabDecripta);
            tabCripta.Text = "Cripta";
            tabDecripta.Text = "Decripta";
            txtToCript.Text = "Inserire il messaggio segreto qui!";

            //Controls.Add(tabICD);
            //tabICD.Size = new Size(this.Width, this.Height);
            //tabICD.TabPages.Add(tabCripta);
            //tabICD.TabPages.Add(tabDecripta);
            //tabCripta.Text = "Cripta";
            //tabDecripta.Text = "Decripta";
            //txtToCript.Text = "Inserire il messaggio segreto qui!";


            #region Cripta

            tabCripta.Controls.Add(btnCaricaCripta);
            tabCripta.Controls.Add(imgToCript);
            tabCripta.Controls.Add(txtToCript);
            tabCripta.Controls.Add(btnCripta);
            tabCripta.Controls.Add(txtCriptaE);
            tabCripta.Controls.Add(txtCriptaN);

            btnCaricaCripta.Visible = true;
            btnCaricaCripta.Enabled = true;
            btnCaricaCripta.Location = new Point(380, 40);
            btnCaricaCripta.Size = new Size(65, 30);
            btnCaricaCripta.FlatStyle = FlatStyle.Flat;
            btnCaricaCripta.Click += new System.EventHandler(this.btnCaricaCripta_Click);
            btnCaricaCripta.Text = ("Carica");

            imgToCript.Visible = true;
            imgToCript.Enabled = true;
            imgToCript.Location = new Point(15, 40);
            imgToCript.Size = new Size(360, 240);
            imgToCript.BorderStyle = BorderStyle.Fixed3D;
            imgToCript.SizeMode = PictureBoxSizeMode.StretchImage;

            txtToCript.Visible = true;
            txtToCript.Enabled = false;
            txtToCript.Location = new Point(15, 290);
            txtToCript.Size = new Size(360, 60);
            txtToCript.BorderStyle = BorderStyle.Fixed3D;
            txtToCript.Multiline = true;

            btnCripta.Visible = true;
            btnCripta.Enabled = false;
            btnCripta.Location = new Point(380, 290);
            btnCripta.Size = new Size(65, 30);
            btnCripta.FlatStyle = FlatStyle.Flat;
            btnCripta.Click += new System.EventHandler(this.Cripta_Click);
            btnCripta.Text = ("Cripta");

            System.IO.StreamReader file1 = new System.IO.StreamReader(@"publicN.txt");
            string line1;
            System.IO.StreamReader file = new System.IO.StreamReader(@"publicE.txt");
            string line;

            while ((line = file.ReadLine()) != null)
                txtCriptaE.Text = line;

            while ((line1 = file1.ReadLine()) != null)
                txtCriptaN.Text = line1;

            txtCriptaE.Visible = true;
            txtCriptaE.Enabled = false;
            txtCriptaN.Enabled = false;
            txtCriptaE.Location = new Point(460, 40);
            txtCriptaE.Size = new Size(100, 60);

            txtCriptaN.Visible = true;
            txtCriptaN.Location = new Point(570, 40);
            txtCriptaN.Size = new Size(100, 60);

            #endregion


            #region Decripta

            tabDecripta.Controls.Add(imgToDecript);
            tabDecripta.Controls.Add(btnCaricaDecripta);
            tabDecripta.Controls.Add(btnDecripta);
            tabDecripta.Controls.Add(txtToDecript);
            tabDecripta.Controls.Add(txtDecriptaD);
            tabDecripta.Controls.Add(txtDecriptaN);

            btnCaricaDecripta.Visible = true;
            btnCaricaDecripta.Enabled = true;
            btnCaricaDecripta.Location = new Point(380, 40);
            btnCaricaDecripta.Size = new Size(65, 30);
            btnCaricaDecripta.FlatStyle = FlatStyle.Flat;
            btnCaricaDecripta.Click += new System.EventHandler(this.btnCaricaDecripta_Click);
            btnCaricaDecripta.Text = ("Carica");

            imgToDecript.Visible = true;
            imgToDecript.Enabled = true;
            imgToDecript.Location = new Point(15, 40);
            imgToDecript.Size = new Size(360, 240);
            imgToDecript.BorderStyle = BorderStyle.Fixed3D;
            imgToDecript.SizeMode = PictureBoxSizeMode.StretchImage;

            txtToDecript.Visible = true;
            txtToDecript.Enabled = false;
            txtToDecript.Location = new Point(15, 290);
            txtToDecript.Size = new Size(360, 60);
            txtToDecript.BorderStyle = BorderStyle.Fixed3D;
            txtToDecript.Multiline = true;
            txtToDecript.Enabled = false;

            btnDecripta.Visible = true;
            btnDecripta.Enabled = false;
            btnDecripta.Location = new Point(380, 290);
            btnDecripta.Size = new Size(65, 30);
            btnDecripta.FlatStyle = FlatStyle.Flat;
            btnDecripta.Click += new System.EventHandler(this.Decripta_Click);
            btnDecripta.Text = ("Decripta");

            System.IO.StreamReader file3 = new System.IO.StreamReader(@"privateN.txt");
            string line3;
            System.IO.StreamReader file2 = new System.IO.StreamReader(@"privateD.txt");
            string line2;

            while ((line2 = file2.ReadLine()) != null)
                txtDecriptaD.Text = line2;


            while ((line3 = file3.ReadLine()) != null)
                txtDecriptaN.Text = line3;

            txtDecriptaD.Visible = true;
            txtDecriptaD.Enabled = false;
            txtDecriptaN.Enabled = false;
            txtDecriptaD.Location = new Point(460, 40);
            txtDecriptaD.Size = new Size(100, 60);

            txtDecriptaN.Visible = true;
            txtDecriptaN.Location = new Point(570, 40);
            txtDecriptaN.Size = new Size(100, 60);

            #endregion

            txtToCript.TextChanged += TxtToCript_TextChanged;
        }

        private void TxtToCript_TextChanged(object sender, EventArgs e)
        {
            if (txtToCript.Text.Length == 0)
                btnCripta.Enabled = false;
            else
                btnCripta.Enabled = true;

        }

        public void Prova_Click(object sender, EventArgs e)
        {

        }


        public void Cripta_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "File Immagine (*.jpg)|*.jpg";

            while (sfd.ShowDialog() != DialogResult.OK)
            {

            }

            string path = System.IO.Path.GetDirectoryName(sfd.FileName);
            fullPath = sfd.FileName;

            Bitmap imgCriptata;
            long longE = Convert.ToInt64(txtCriptaE.Text);
            long longN = Convert.ToInt64(txtCriptaN.Text);

            BigInteger bigE = longE;
            BigInteger bigN = longN;

            StrnToCript = txtToCript.Text;

            CriptaDecripta cd1 = new CriptaDecripta(StrnToCript, bigE, bigN);
            imgCriptata = cd1.CriptaImmagine(imgCrip, fullPath);

            fullPath = null;
        }

        public void Decripta_Click(object sender, EventArgs e)
        {
            string testo;
            long longD = Convert.ToInt64(txtDecriptaD.Text);
            long longN = Convert.ToInt64(txtDecriptaN.Text);

            BigInteger bigD = longD;
            BigInteger bigN = longN;
            CriptaDecripta dc1 = new CriptaDecripta(longD, longN);
            testo = dc1.DecriptaImmagine(imgDec, fullPath);
            txtToDecript.Text = testo;

            fullPath = null;
        }

        //CaricaCripta
        public void btnCaricaCripta_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File Immagine (*.jpg)|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgCrip = new Bitmap(ofd.FileName);
                imgToCript.Image = imgCrip;
            }

            txtToCript.Enabled = true;
            txtToCript.Focus();
        }

        //CaricaDecripta
        public void btnCaricaDecripta_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File Immagine (*.jpg)|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgDec = new Bitmap(ofd.FileName);
                imgToDecript.Image = imgDec;
                fullPath = ofd.FileName;
            }

            btnDecripta.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
