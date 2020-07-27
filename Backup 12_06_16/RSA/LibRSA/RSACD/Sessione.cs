using System.Windows.Forms;
using System.Drawing;
using System;
using MetroFramework.Controls;

namespace LibRSA.RSACD
{
    public class Sessione
    {
        string _nomeSessione;
        string _utente;

        string _dString;
        string _eString;
        string _nString;

        BigInteger _dBigI = 0;
        BigInteger _eBigI = 0;
        BigInteger _nBigI = 0;

        MetroTabControl _tabControl;
        MetroTabControl _tabICD;
        TabPage _tabNomeSessione;
        TabPage _tabInfo;
        TabPage _tabCripta;
        TabPage _tabDecripta;
        TabPage _tabEmail;

        MetroButton _btnCaricaCripta;
        MetroButton _btnCaricaDecripta;
        MetroButton _btnCripta;
        MetroButton _btnDecripta;

        Panel _pnlToCript;
        Panel _pnlToDecript;
        PictureBox _imgToCript;
        PictureBox _imgToDecript;

        TextBox _txtToDecript;
        TextBox _txtToCript;
        TextBox _txtCriptaE;
        TextBox _txtCriptaN;
        TextBox _txtDecriptaD;
        TextBox _txtDecriptaN;
        TextBox _txtDestinatario;

        MetroButton _btnInviaEmail;
        MetroButton _btnAllegato;

        Label _lblTo;
        Label _lblInfoAllegato;
        Label _lblInfoAllegatoPath;

        Label _lblNomeSessione;
        Label _lblChiavePrivata;
        Label _lblChiavePubblica;
        Label _lblProprietarioSessione;
        Label _lblEmail;

        string stringToCript;
        Bitmap _imgCrip;
        Bitmap _imgDec;

        string _eMail;

        string _fullPath;

        bool _encriptionError;

        public string NomeSessione
        {
            get { return _nomeSessione; }
            set { _nomeSessione = value; }
        }

        public string Utente
        {
            get { return _utente; }
            set { _utente = value; }
        }

        public string EString
        {
            get { return _eString; }
            set { _eString = value; }
        }

        public string NString
        {
            get { return _nString; }
            set { _nString = value; }
        }

        public string DString
        {
            get { return _dString; }
            set { _dString = value; }
        }

        public bool EncriptionError
        {
            get{ return _encriptionError;}
        }

        public string Email
        {
            get { return _eMail; }
            set { _eMail = value; }
        }

        public Sessione(string nomeSessione, GeneraChiavi gc)
        {
            this._nomeSessione = nomeSessione;
            this._dString = gc.D.ToString();
            this._eString = gc.E.ToString();
            this._nString = gc.N.ToString();
            this._utente = System.Environment.UserName;

            this._dBigI = gc.D;
            this._eBigI = gc.E;
            this._nBigI = gc.N;
        }

        public Sessione(string nomeSessione, GeneraChiavi gc, string eMail)
        {
            this._nomeSessione = nomeSessione;
            this._dString = gc.D.ToString();
            this._eString = gc.E.ToString();
            this._nString = gc.N.ToString();
            this._utente = System.Environment.UserName;

            this._dBigI = gc.D;
            this._eBigI = gc.E;
            this._nBigI = gc.N;
            this._eMail = eMail;
        }

        public Sessione(string nomeSessione, string d, string e, string n, string utente, string eMail)
        {
            this._nomeSessione = nomeSessione;
            this._dString = d;
            this._eString = e;
            this._nString = n;
            this._utente = utente;

            if (d != "")
                this._dBigI = Convert.ToInt64(d);

            this._eBigI = Convert.ToInt64(e);
            this._nBigI = Convert.ToInt64(n);
            this._eMail = eMail;
        }

        public Sessione(string nomeSessione, string e, string n, string utente, string eMail)
        {
            this._nomeSessione = nomeSessione;
            this._eString = e;
            this._nString = n;
            this._utente = utente;
            this._eMail = eMail;
        }

        public void AggiungiSessione(TabControl tabControl, Form form, bool standard)
        {
            _tabControl = new MetroTabControl();
            _tabICD = new MetroTabControl();

            _tabNomeSessione = new TabPage();
            _tabInfo = new TabPage();
            _tabCripta = new TabPage();
            _tabDecripta = new TabPage();
            _tabEmail = new TabPage();

            _btnCaricaCripta = new MetroButton();
            _btnCaricaDecripta = new MetroButton();
            _btnCripta = new MetroButton();
            _btnDecripta = new MetroButton();
            _btnAllegato = new MetroButton();
            _btnInviaEmail = new MetroButton();

            _pnlToCript = new Panel();
            _pnlToDecript = new Panel();
            _imgToCript = new PictureBox();
            _imgToDecript = new PictureBox();

            _txtToDecript = new TextBox();
            _txtToCript = new TextBox();
            _txtCriptaE = new TextBox();
            _txtCriptaN = new TextBox();
            _txtDecriptaD = new TextBox();
            _txtDecriptaN = new TextBox();
            _txtDestinatario = new TextBox();

            _lblNomeSessione = new Label();
            _lblChiavePrivata = new Label();
            _lblChiavePubblica = new Label();
            _lblProprietarioSessione = new Label();
            _lblEmail = new Label();
            _lblTo = new Label();
            _lblInfoAllegato = new Label();
            _lblInfoAllegatoPath = new Label();

            #region NuovaTabellaSession

            tabControl.Controls.Add(_tabNomeSessione);

            if (standard)
                _tabNomeSessione.Text = _nomeSessione + " (Standard)";
            else
                _tabNomeSessione.Text = _nomeSessione + " (Database)";

            _tabNomeSessione.Controls.Add(_tabICD);

            _tabICD.Size = new Size(tabControl.Width, tabControl.Height - 50);

            _tabICD.Controls.Add(_tabInfo);
            _tabICD.Controls.Add(_tabCripta);
            if (standard)
                _tabICD.Controls.Add(_tabDecripta);
            else
                _tabICD.Controls.Add(_tabEmail);

            _tabInfo.Text = "Info";
            _tabCripta.Text = "Cripta";
            _tabDecripta.Text = "Decripta";
            _tabEmail.Text = "Servizio e-mail";

            #endregion

            #region Info

            _lblNomeSessione.Text = "Nome Sessione: " + _nomeSessione;

            if (standard)
                _lblChiavePrivata.Text = "Chiave Privata: (" + _dString + ", " + _nString + ")";
            else
                _lblChiavePrivata.Text = "Chiave Privata: ( )";

            _lblChiavePubblica.Text = "Chiave Pubblica: (" + _eString + ", " + _nString + ")";
            _lblProprietarioSessione.Text = "Proprietario Sessione: " + _utente;

                      
            _lblEmail.Text = "e-mail: " + _eMail;

            _lblNomeSessione.AutoSize = true;
            _lblChiavePrivata.AutoSize = true;
            _lblChiavePubblica.AutoSize = true;
            _lblProprietarioSessione.AutoSize = true;
            _lblEmail.AutoSize = true;

            _lblNomeSessione.Location = new Point(20, 50);
            _lblChiavePrivata.Location = new Point(20, 80);
            _lblChiavePubblica.Location = new Point(20, 110);
            _lblProprietarioSessione.Location = new Point(20, 140);
            _lblEmail.Location = new Point(20, 170);

            _tabInfo.Controls.Add(_lblNomeSessione);
            _tabInfo.Controls.Add(_lblChiavePrivata);
            _tabInfo.Controls.Add(_lblChiavePubblica);
            _tabInfo.Controls.Add(_lblProprietarioSessione);          
            _tabInfo.Controls.Add(_lblEmail);

            #endregion

            #region Cripta

            _tabCripta.Controls.Add(_btnCaricaCripta);
            _tabCripta.Controls.Add(_pnlToCript);
            _tabCripta.Controls.Add(_imgToCript);
            _tabCripta.Controls.Add(_txtToCript);
            _tabCripta.Controls.Add(_btnCripta);

            _btnCaricaCripta.Visible = true;
            _btnCaricaCripta.Enabled = true;
            _btnCaricaCripta.Location = new Point(10, 10);
            _btnCaricaCripta.Click += new System.EventHandler(this.btnCaricaCripta_Click);
            _btnCaricaCripta.Text = "Carica";

            _pnlToCript.Location = new Point(100, 10);
            _pnlToCript.Size = new Size(tabControl.Width - 150, tabControl.Height - 300);
            _pnlToCript.AutoScroll = true;
            _pnlToCript.Controls.Add(_imgToCript);

            _imgToCript.Visible = true;
            _imgToCript.Enabled = true;
            _imgToCript.Location = new Point(100, 10);
            _imgToCript.Size = new Size(tabControl.Width - 150, tabControl.Height - 300);
            _imgToCript.SizeMode = PictureBoxSizeMode.AutoSize;

            _txtToCript.Visible = true;
            _txtToCript.Enabled = false;
            _txtToCript.Location = new Point(100, 375);
            _txtToCript.Size = new Size(tabControl.Width - 150, 60);
            _txtToCript.BorderStyle = BorderStyle.Fixed3D;
            _txtToCript.Multiline = true;
            _txtToCript.TextChanged += _txtToCript_TextChanged;

            _btnCripta.Visible = true;
            _btnCripta.Enabled = false;
            _btnCripta.Location = new Point(10, 375);
            _btnCripta.Click += new System.EventHandler(this.Cripta_Click);
            _btnCripta.Text = "Cripta";

            #endregion

            if (standard)
            {
                #region Decripta

                _tabDecripta.Controls.Add(_imgToDecript);
                _tabDecripta.Controls.Add(_pnlToDecript);
                _tabDecripta.Controls.Add(_btnCaricaDecripta);
                _tabDecripta.Controls.Add(_btnDecripta);
                _tabDecripta.Controls.Add(_txtToDecript);

                _btnCaricaDecripta.Visible = true;
                _btnCaricaDecripta.Enabled = true;
                _btnCaricaDecripta.Location = new Point(10, 10);
                _btnCaricaDecripta.Click += new System.EventHandler(this.btnCaricaDecripta_Click);
                _btnCaricaDecripta.Text = "Carica";

                _pnlToDecript.Location = new Point(100, 10);
                _pnlToDecript.Size = new Size(tabControl.Width - 150, tabControl.Height - 300);
                _pnlToDecript.AutoScroll = true;
                _pnlToDecript.Controls.Add(_imgToDecript);

                _imgToDecript.Visible = true;
                _imgToDecript.Enabled = true;
                _imgToDecript.Location = new Point(100, 10);
                _imgToDecript.Size = new Size(tabControl.Width - 150, tabControl.Height - 300);
                _imgToDecript.SizeMode = PictureBoxSizeMode.AutoSize;
                //_imgToDecript.BorderStyle = BorderStyle.Fixed3D;
                //_imgToDecript.SizeMode = PictureBoxSizeMode.StretchImage;

                _txtToDecript.Visible = true;
                _txtToDecript.Enabled = false;
                _txtToDecript.Location = new Point(100,375);
                _txtToDecript.Size = new Size(tabControl.Width - 150, 60);
                _txtToDecript.BorderStyle = BorderStyle.Fixed3D;
                _txtToDecript.Multiline = true;
                _txtToDecript.Enabled = false;

                _btnDecripta.Visible = true;
                _btnDecripta.Enabled = false;
                _btnDecripta.Location = new Point(10, 375);
                _btnDecripta.Click += new System.EventHandler(this.Decripta_Click);
                _btnDecripta.Text = "Decripta";

                #endregion
            }
            else
            {
                #region eMail

                _tabEmail.Controls.Add(_lblTo);
                _tabEmail.Controls.Add(_txtDestinatario);
                _tabEmail.Controls.Add(_btnAllegato);
                _tabEmail.Controls.Add(_btnInviaEmail);
                _tabEmail.Controls.Add(_lblInfoAllegato);
                _tabEmail.Controls.Add(_lblInfoAllegatoPath);

                _lblTo.Location = new Point(60, 78);
                _lblTo.AutoSize = true;
                _lblTo.Text = "Destinatario:";

                _txtDestinatario.Location = new Point(140, 75);
                _txtDestinatario.Width = 500;
                _txtDestinatario.Text = this._eMail;

                _btnAllegato.Location = new Point(650, 75);
                _btnAllegato.Size = new Size(20, 20);
                _btnAllegato.BackgroundImage = Image.FromFile("paperclip.png");
                _btnAllegato.BackgroundImageLayout = ImageLayout.Center;
                _btnAllegato.Click += _btnAllegato_Click;

                _btnInviaEmail.Location = new Point(62, 135);
                _btnInviaEmail.Width = 610;
                _btnInviaEmail.Text = "Invia e-mail";
                _btnInviaEmail.Click += _btnInviaEmail_Click;

                _lblInfoAllegato.Location = new Point(60, 110);
                _lblInfoAllegato.AutoSize = true;
                _lblInfoAllegato.Text = "Info Allegato: ";

                _lblInfoAllegatoPath.Location = new Point(138, 110);
                _lblInfoAllegatoPath.AutoSize = true;
                _lblInfoAllegatoPath.Text = "";

                #endregion
            }
        }

        private void _btnAllegato_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File Immagine (*.jpg)|*.jpg";
            string path = "";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _lblInfoAllegatoPath.Text = "";
                path = ofd.FileName;
            }

            _lblInfoAllegatoPath.Text = path;
        }

        private void _btnInviaEmail_Click(object sender, EventArgs e)
        {
            string mittente = "prj.steganografia@gmail.com";
            string destinatario = _txtDestinatario.Text;
            string soggetto = "Messaggio da: " + this.Utente + " per Sessione: " + this.NomeSessione;
            string corpo = "Buona decriptata da " + this.Utente + "!";
            string pathAllegato = _lblInfoAllegatoPath.Text;

            eMail mail = new eMail(mittente, destinatario, soggetto, corpo, pathAllegato);
            mail.Invia();
        }

        private void _txtToCript_TextChanged(object sender, EventArgs e)
        {
            if (_txtToCript.Text.Length == 0)
                _btnCripta.Enabled = false;
            else
                _btnCripta.Enabled = true;
        }

        private void Decripta_Click(object sender, EventArgs e)
        {
            string msgDecriptato;

            try
            {
                CriptaDecripta dc1 = new CriptaDecripta(_dBigI, _nBigI);
                msgDecriptato = dc1.DecriptaImmagine(_imgDec, _fullPath);
                _txtToDecript.Text = msgDecriptato;
                MessageBox.Show("Informazioni decriptate con successo!", "Successo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Si è verificato un errore in fase di decriptazione: verificare che la chiave sia quella corretta o che il file non sia danneggiato.",
                    "Errore di Decriptazione", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _encriptionError = true;
            }

            _fullPath = null;
        }

        private void Cripta_Click(object sender, EventArgs e)
        {
            Bitmap imgCriptata;
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "File Immagine (*.jpg)|*.jpg";
            while (sfd.ShowDialog() != DialogResult.OK)
            {

            }

            string path = System.IO.Path.GetDirectoryName(sfd.FileName);
            _fullPath = sfd.FileName;

            stringToCript = _txtToCript.Text;
            CriptaDecripta cd1 = new CriptaDecripta(stringToCript, _eBigI, _nBigI);
            imgCriptata = cd1.CriptaImmagine(_imgCrip, _fullPath);

            MessageBox.Show("Informazioni criptate con successo!", "Successo!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _fullPath = null;
        }

        private void btnCaricaDecripta_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File Immagine (*.jpg)|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imgDec = new Bitmap(ofd.FileName);
                _imgToDecript.Image = _imgDec;
                _fullPath = ofd.FileName;
            }

            _btnDecripta.Enabled = true;
        }

        private void btnCaricaCripta_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File Immagine (*.jpg)|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imgCrip = new Bitmap(ofd.FileName);
                _imgToCript.Image = _imgCrip;
            }

            _txtToCript.Enabled = true;
            _txtToCript.Focus();
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}",  _nomeSessione, _dString, _nString, _eString, _utente, _eMail);
        }
    }
}
