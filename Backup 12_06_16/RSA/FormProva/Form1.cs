using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using LibRSA.RSACD;
using LibRSA;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace FormSteganografia
{
    public partial class Form1 : MetroForm
    {
        

        MetroTabControl tabSessione = new MetroTabControl();
        MetroTabControl tabICD = new MetroTabControl();

        TabPage tabIniziale = new TabPage();
        Label lblSeparatore1 = new Label();
        Label lblSeparatore2 = new Label();
        Label lblSeparatore3 = new Label();
        Label titolo = new Label();
        Label descrizione1 = new Label();
        Label descrizione2 = new Label();
        Label copyright = new Label();
        Label banner = new Label();
        Label checkDBConnection = new Label();
        MetroButton reConnect = new MetroButton();

        MetroButton btnNuovaSessione = new MetroButton();
        MetroButton btnCancellaS = new MetroButton();

        MetroForm formSessione;
        MetroTextBox txtNomeSessione;
        MetroButton btnCrea;
        MetroButton btnAnnulla;
        MetroRadioButton radioStandard;
        MetroRadioButton radioCustom;
        MetroCheckBox checkPubblicaDB;
        MetroLabel lblEmail;
        TextBox txtEmail;
        ListBox lstBoxSessioni;
        ListView lstvBoxSessioni;

        MetroTextBox txtCercaSessione;
        MetroTextBox txtCercaUtente;
        MetroTextBox txtCercaPubblica;
        MetroTextBox txtCercaMail;
        MetroLabel lblCercaSessione;
        MetroLabel lblCercaUtente;
        MetroLabel lblCercaPubblica;
        MetroLabel lblCercaMail;

        Sessione addS;
        SessioneCollection sCollection = new SessioneCollection();
        SessioneProvider sProvider = new SessioneProvider();

        GeneraChiavi gc;

        StreamWriter writeConfig;
        StreamReader readConfig;

        string connessioneDB;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TabPage sess = new TabPage();
            TabPage tabInfo = new TabPage();
            TabPage tabCripta = new TabPage();
            TabPage tabDecripta = new TabPage();

            btnNuovaSessione.Size = new Size(789, 32);
            btnCancellaS.Size = new Size(789, 32);
            btnNuovaSessione.Location = new Point(-1, 68);
            btnCancellaS.Location = new Point(-1, 100);
            btnNuovaSessione.Text = "Nuova Sessione";
            btnCancellaS.Text = "ChiudiSessione";

            btnNuovaSessione.Click += BtnNuovaSessione_Click;
            btnCancellaS.Click += BtnCancellaS_Click;

            Controls.Add(btnNuovaSessione);
            Controls.Add(btnCancellaS);

            this.Text = "Steganografia";
            this.CenterToScreen();
            this.MaximizeBox = false;

            tabSessione.Size = new Size(this.Width, this.Height);
            tabSessione.Location = new Point(0, 130);
            tabSessione.SelectedIndexChanged += TabSessione_SelectedIndexChanged;
            tabIniziale.Text = "Pagina Iniziale";
            tabSessione.Controls.Add(tabIniziale);
            Controls.Add(tabSessione);
            tabICD.Size = new Size(this.Width, this.Height);

            titolo.Text = "Benvenuto!";
            titolo.AutoSize = true;
            titolo.Location = new Point(20, 20);
            titolo.Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 20);

            lblSeparatore1.Text = "______________________________________________________________________________________________________________________";
            lblSeparatore1.AutoSize = true;
            lblSeparatore1.Location = new Point(30, 60);

            descrizione1.Text = "Inizia subito creando una nuova Sessione!\r\nInserire messaggi segreti all'interno di un immagine non è mai stato così semplice.";
            descrizione1.Location = new Point(40, 90);
            descrizione1.AutoSize = true;

            lblSeparatore2.Text = "______________________________________________________________________________________________________________________";
            lblSeparatore2.AutoSize = true;
            lblSeparatore2.Location = new Point(30, 120);

            descrizione2.Text = "Collegati al Database del software per condividere le Sessioni da te create attraverso la funzione 'Pubblica nel Database'!\r\nComunica con altri utenti usufruendo delle Sessioni da loro pubblicate.";
            descrizione2.Location = new Point(40, 150);
            descrizione2.AutoSize = true;

            connessioneDB = sProvider.TryConnection();
            checkDBConnection.Text = String.Format("Connessione al Database: " + connessioneDB);
            checkDBConnection.AutoSize = true;
            checkDBConnection.Location = new Point(40, 200);

            reConnect.Text = "Connetti";
            reConnect.AutoSize = true;
            reConnect.Location = new Point(40, 220);
            reConnect.Size = new Size(165, reConnect.Height);
            reConnect.Click += ReConnect_Click;

            lblSeparatore3.Text = "______________________________________________________________________________________________________________________";
            lblSeparatore3.AutoSize = true;
            lblSeparatore3.Location = new Point(30, 410);

            copyright.Text = "Copyright \u00A9 2016 Steganografia, Marco Iacocca & Matteo Sperti";
            copyright.AutoSize = true;
            copyright.UseMnemonic = false;
            copyright.Location = new Point(40, 440);

            tabIniziale.Controls.Add(titolo);
            tabIniziale.Controls.Add(checkDBConnection);
            tabIniziale.Controls.Add(reConnect);
            tabIniziale.Controls.Add(lblSeparatore1);
            tabIniziale.Controls.Add(descrizione1);
            tabIniziale.Controls.Add(lblSeparatore2);
            tabIniziale.Controls.Add(descrizione2);
            tabIniziale.Controls.Add(lblSeparatore3);
            tabIniziale.Controls.Add(copyright);

            readConfig = new StreamReader("config.cfg");

            string s;
            while ((s = readConfig.ReadLine()) != null)
            {
                string[] splitted = new string[8];
                splitted = s.Split('|');

                addS = new Sessione(splitted[0], splitted[1], splitted[3], splitted[2], splitted[4], splitted[5]);
                sCollection.Add(addS);
            }

            readConfig.Dispose();

            for (int i = 0; i < sCollection.Count; i++)
            {
                if (sCollection[i].DString == "")
                    sCollection[i].AggiungiSessione(tabSessione, this, false);
                else
                    sCollection[i].AggiungiSessione(tabSessione, this, true);
            }

            btnCancellaS.Visible = false;

            if (connessioneDB == "Connesso")
                reConnect.Enabled = false;
            else
                reConnect.Enabled = true;

        }

        private void BtnCancellaS_Click(object sender, EventArgs e)
        {
            DialogResult msgBox = MessageBox.Show("Vuoi chiudere permanentemente la sessione? I dati relativi ad essa saranno persi.", "Chiudi Sessione", MessageBoxButtons.YesNo);
            if (msgBox == DialogResult.Yes)
            {
                for (int i = 0; i < sCollection.Count; i++)
                {
                    if (tabSessione.SelectedTab.Text == sCollection[i].NomeSessione + " (Standard)")
                    {
                        tabSessione.SelectedTab.Dispose();

                        if (connessioneDB == "Connesso")
                            sProvider.DeleteEntity(sCollection[i].NString);

                        sCollection.RemoveAt(i);
                    }
                    else if (tabSessione.SelectedTab.Text == sCollection[i].NomeSessione + " (Database)")
                    {
                        tabSessione.SelectedTab.Dispose();
                        sCollection.RemoveAt(i);
                    }
                }
            }
        }

        private void BtnNuovaSessione_Click(object sender, EventArgs e)
        {
            #region FormSessione

            formSessione = new MetroForm();
            txtNomeSessione = new MetroTextBox();
            btnCrea = new MetroButton();
            btnAnnulla = new MetroButton();
            radioStandard = new MetroRadioButton();
            radioCustom = new MetroRadioButton();
            checkPubblicaDB = new MetroCheckBox();
            lstBoxSessioni = new ListBox();
            lstvBoxSessioni = new ListView();
            txtCercaSessione = new MetroTextBox();
            lblCercaSessione = new MetroLabel();
            txtCercaUtente = new MetroTextBox();
            lblCercaUtente = new MetroLabel();
            txtCercaPubblica = new MetroTextBox();
            lblCercaPubblica = new MetroLabel();
            lblEmail = new MetroLabel();
            txtEmail = new TextBox();
            txtCercaMail = new MetroTextBox();
            lblCercaMail = new MetroLabel();

            checkPubblicaDB.CheckedChanged += CheckPubblicaDB_CheckedChanged;

            formSessione.StartPosition = FormStartPosition.CenterScreen;

            formSessione.Size = new Size(500, 400);
            formSessione.MaximizeBox = false;

            formSessione.MinimizeBox = false;
            formSessione.Resizable = false;

            formSessione.Text = "Nuova Sessione";
            radioStandard.Text = "Sessione Standard";
            radioCustom.Text = "Sessione da Database";
            radioStandard.AutoSize = true;
            radioCustom.AutoSize = true;
            radioStandard.Checked = true;

            radioCustom.Top = 70;
            radioStandard.Top = 70;
            radioCustom.Left = 270;
            radioStandard.Left = 90;

            radioStandard.CheckedChanged += RadioStandard_CheckedChanged;
            radioCustom.CheckedChanged += RadioCustom_CheckedChanged;

            formSessione.Controls.Add(radioStandard);
            formSessione.Controls.Add(radioCustom);
            formSessione.Controls.Add(txtNomeSessione);
            formSessione.Controls.Add(btnCrea);
            formSessione.Controls.Add(btnAnnulla);
            formSessione.Controls.Add(checkPubblicaDB);
            formSessione.Controls.Add(lstvBoxSessioni);
            formSessione.Controls.Add(txtCercaSessione);
            formSessione.Controls.Add(lblCercaSessione);
            formSessione.Controls.Add(txtCercaUtente);
            formSessione.Controls.Add(lblCercaUtente);
            formSessione.Controls.Add(txtCercaPubblica);
            formSessione.Controls.Add(lblCercaPubblica);
            formSessione.Controls.Add(lblEmail);
            formSessione.Controls.Add(txtEmail);
            formSessione.Controls.Add(lblCercaMail);
            formSessione.Controls.Add(txtCercaMail);

            txtNomeSessione.Focus();
            txtNomeSessione.Size = new Size(200, txtNomeSessione.Height);
            txtNomeSessione.Top = 150;
            txtNomeSessione.Left = 150;
            txtNomeSessione.Text = "Inserire nome sessione";

            lstvBoxSessioni.Visible = false;
            lstvBoxSessioni.Scrollable = true;
            lstvBoxSessioni.Alignment = ListViewAlignment.Left;

            ColumnHeader colHead = new ColumnHeader();

            colHead = new ColumnHeader();
            colHead.Text = "Nome Sessione";
            colHead.Width = -2;
            lstvBoxSessioni.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Utente";
            colHead.Width = 60;
            lstvBoxSessioni.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Chiave Pubblica (E, N)";
            colHead.Width = 120;
            lstvBoxSessioni.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "e-mail";
            colHead.Width = 120;

            lstvBoxSessioni.Columns.Add(colHead);
            lstvBoxSessioni.FullRowSelect = true;

            for (int i = 0; i < sProvider.GetEntityList().Count; i++)
            {
                ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                lstItem.SubItems.Add(keyPubb);
                lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                lstvBoxSessioni.Items.Add(lstItem);
            }

            lblCercaSessione.Visible = false;
            txtCercaSessione.Visible = false;
            lblCercaUtente.Visible = false;
            txtCercaUtente.Visible = false;
            lblCercaPubblica.Visible = false;
            txtCercaPubblica.Visible = false;
            lblCercaMail.Visible = false;
            txtCercaMail.Visible = false;

            lstvBoxSessioni.Size = new Size(285, 240);
            lstvBoxSessioni.Top = 100;
            lstvBoxSessioni.Left = 20;
            lstvBoxSessioni.View = View.Details;

            lblCercaSessione.Text = "Ricerca Nome Sessione";
            lblCercaSessione.Top = 115;
            lblCercaSessione.Left = 316;
            lblCercaSessione.AutoSize = true;

            txtCercaSessione.Text = "Cerca...";
            txtCercaSessione.Top = 135;
            txtCercaSessione.Left = 320;
            txtCercaSessione.Width = 130;
            txtCercaSessione.TextChanged += CercaSessione_TextChanged;

            lblCercaUtente.Text = "Ricerca Utente";
            lblCercaUtente.Top = 165;
            lblCercaUtente.Left = 316;
            lblCercaUtente.AutoSize = true;

            txtCercaUtente.Text = "Cerca...";
            txtCercaUtente.Top = 185;
            txtCercaUtente.Left = 320;
            txtCercaUtente.Width = 130;
            txtCercaUtente.TextChanged += TxtCercaUtente_TextChanged;

            lblCercaPubblica.Text = "Ricerca Chiave N";
            lblCercaPubblica.Top = 215;
            lblCercaPubblica.Left = 316;
            lblCercaPubblica.AutoSize = true;

            txtCercaPubblica.Text = "Cerca...";
            txtCercaPubblica.Top = 235;
            txtCercaPubblica.Left = 320;
            txtCercaPubblica.Width = 130;
            txtCercaPubblica.TextChanged += TxtCercaPubblica_TextChanged;

            lblCercaMail.Text = "Ricerca e-mail";
            lblCercaMail.Top = 265;
            lblCercaMail.Left = 316;
            lblCercaMail.AutoSize = true;

            txtCercaMail.Text = "Cerca...";
            txtCercaMail.Top = 285;
            txtCercaMail.Left = 320;
            txtCercaMail.Width = 130;
            txtCercaMail.TextChanged += TxtCercaMail_TextChanged;

            checkPubblicaDB.Checked = false;
            checkPubblicaDB.Top = 180;
            checkPubblicaDB.Left = 150;
            checkPubblicaDB.Text = "Pubblica nel Database";
            checkPubblicaDB.AutoSize = true;

            lblEmail.Visible = false;
            lblEmail.AutoSize = true;
            lblEmail.Text = "e-mail:";
            txtEmail.Visible = false;
            lblEmail.Top = 230;
            lblEmail.Left = 80;
            txtEmail.Top = 230;
            txtEmail.Left = 130;
            txtEmail.Width = 250;
            txtEmail.Text = "esempio@gmail.com";

            btnCrea.Top = 360;
            btnCrea.Left = 90;
            btnAnnulla.Top = 360;
            btnAnnulla.Left = 330;
            btnCrea.Text = "Crea";
            btnAnnulla.Text = "Annulla";

            formSessione.Show();

            if (checkDBConnection.Text == "Connessione al Database: Assente")
            {
                radioCustom.Enabled = false;
                checkPubblicaDB.Checked = false;
                checkPubblicaDB.Enabled = false;


                MessageBox.Show("La connessione al database è assente o è stata persa. Controllare lo stato della propria rete.",
                    "Connessione al database fallita", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                connessioneDB = "Assente";
                checkDBConnection.Text = String.Format("Connessione al Database: " + connessioneDB);

            }
            else if (checkDBConnection.Text == "Connessione al Database: Connesso")
            {
                if(sProvider.TryConnection() == "Assente")
                {
                    MessageBox.Show("La connessione al database è assente o è stata persa. Controllare lo stato della propria rete.",
                        "Connessione al database fallita", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkDBConnection.Text = String.Format("Connessione al Database: " + "Assente");
                    reConnect.Enabled = true;
                }

                else{
                    radioCustom.Enabled = true;
                    checkPubblicaDB.Checked = false;
                    checkPubblicaDB.Enabled = true;
                }
            }

            btnCrea.Click += _crea_Click;
            btnAnnulla.Click += _annulla_Click;

            #endregion

        }

       
        private void CheckPubblicaDB_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPubblicaDB.Checked)
            {
                lblEmail.Visible = true;
                txtEmail.Visible = true;
            }
            else
            {
                lblEmail.Visible = false;
                txtEmail.Visible = false;
            }
        }

        private void ReConnect_Click(object sender, EventArgs e)
        {
            checkDBConnection.Text = String.Format("Connessione al Database: ?");

            DateTime Tthen = DateTime.Now;
            do
            {
                Application.DoEvents();
            } while (Tthen.AddSeconds(1) > DateTime.Now);

            checkDBConnection.Text = String.Format("Connessione al Database: " + sProvider.TryConnection());

            if (checkDBConnection.Text == "Connessione al Database: Connesso")
                reConnect.Enabled = false;
        }

        private void TabSessione_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSessione.SelectedTab.Text == "Pagina Iniziale")
                btnCancellaS.Visible = false;
            else
                btnCancellaS.Visible = true;
        }

        private void TxtCercaMail_TextChanged(object sender, EventArgs e)
        {
            if (lstvBoxSessioni.Items.Count == 0 || txtCercaSessione.Text == "")
            {
                lstvBoxSessioni.Items.Clear();
                for (int i = 0; i < sProvider.GetEntityList().Count; i++)
                {
                    ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                    string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                    lstItem.SubItems.Add(keyPubb);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                    lstvBoxSessioni.Items.Add(lstItem);
                }
            }

            if (txtCercaMail.Text != "")
            {
                lstvBoxSessioni.Items.Clear();

                for (int i = 0; i < sProvider.GetEntityList().Count; i++)
                {
                    ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                    string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                    lstItem.SubItems.Add(keyPubb);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                    lstvBoxSessioni.Items.Add(lstItem);
                }

                for (int i = lstvBoxSessioni.Items.Count - 1; i >= 0; i--)
                {
                    var item = lstvBoxSessioni.Items[i];
                    if (item.SubItems[3].Text.ToLower().Contains(txtCercaMail.Text.ToLower()))
                    {

                    }
                    else
                        lstvBoxSessioni.Items.Remove(item);
                }
                if (lstvBoxSessioni.SelectedItems.Count == 1)
                    lstvBoxSessioni.Focus();
            }
        }

        private void TxtCercaPubblica_TextChanged(object sender, EventArgs e)
        {
            if (lstvBoxSessioni.Items.Count == 0 || txtCercaSessione.Text == "")
            {
                lstvBoxSessioni.Items.Clear();
                for (int i = 0; i < sProvider.GetEntityList().Count; i++)
                {
                    ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                    string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                    lstItem.SubItems.Add(keyPubb);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                    lstvBoxSessioni.Items.Add(lstItem);
                }
            }

            if (txtCercaPubblica.Text != "")
            {
                lstvBoxSessioni.Items.Clear();

                for (int i = 0; i < sProvider.GetEntityList().Count; i++)
                {
                    ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                    string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                    lstItem.SubItems.Add(keyPubb);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                    lstvBoxSessioni.Items.Add(lstItem);
                }

                for (int i = lstvBoxSessioni.Items.Count - 1; i >= 0; i--)
                {
                    var item = lstvBoxSessioni.Items[i];
                    if (item.SubItems[2].Text.ToLower().Contains(txtCercaPubblica.Text.ToLower()))
                    {

                    }
                    else
                        lstvBoxSessioni.Items.Remove(item);
                }
                if (lstvBoxSessioni.SelectedItems.Count == 1)
                    lstvBoxSessioni.Focus();
            }
        }

        private void TxtCercaUtente_TextChanged(object sender, EventArgs e)
        {
            if (lstvBoxSessioni.Items.Count == 0 || txtCercaSessione.Text == "")
            {
                lstvBoxSessioni.Items.Clear();
                for (int i = 0; i < sProvider.GetEntityList().Count; i++)
                {
                    ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                    string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                    lstItem.SubItems.Add(keyPubb);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                    lstvBoxSessioni.Items.Add(lstItem);
                }
            }

            if (txtCercaUtente.Text != "")
            {
                lstvBoxSessioni.Items.Clear();

                for (int i = 0; i < sProvider.GetEntityList().Count; i++)
                {
                    ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                    string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                    lstItem.SubItems.Add(keyPubb);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                    lstvBoxSessioni.Items.Add(lstItem);
                }

                for (int i = lstvBoxSessioni.Items.Count - 1; i >= 0; i--)
                {
                    var item = lstvBoxSessioni.Items[i];
                    if (item.SubItems[1].Text.ToLower().Contains(txtCercaUtente.Text.ToLower()))
                    {

                    }
                    else
                        lstvBoxSessioni.Items.Remove(item);
                }
                if (lstvBoxSessioni.SelectedItems.Count == 1)
                    lstvBoxSessioni.Focus();
            }
        }

        private void CercaSessione_TextChanged(object sender, EventArgs e)
        {
            if (lstvBoxSessioni.Items.Count == 0 || txtCercaSessione.Text == "")
            {
                lstvBoxSessioni.Items.Clear();
                for (int i = 0; i < sProvider.GetEntityList().Count; i++)
                {
                    ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                    string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                    lstItem.SubItems.Add(keyPubb);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                    lstvBoxSessioni.Items.Add(lstItem);
                }
            }

            if (txtCercaSessione.Text != "")
            {
                lstvBoxSessioni.Items.Clear();

                for (int i = 0; i < sProvider.GetEntityList().Count; i++)
                {
                    ListViewItem lstItem = new ListViewItem(sProvider.GetEntityList()[i].NomeSessione);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Utente);
                    string keyPubb = sProvider.GetEntityList()[i].EString + "," + sProvider.GetEntityList()[i].NString;
                    lstItem.SubItems.Add(keyPubb);
                    lstItem.SubItems.Add(sProvider.GetEntityList()[i].Email);

                    lstvBoxSessioni.Items.Add(lstItem);
                }

                for (int i = lstvBoxSessioni.Items.Count - 1; i >= 0; i--)
                {
                    var item = lstvBoxSessioni.Items[i];
                    if (item.Text.ToLower().Contains(txtCercaSessione.Text.ToLower()))
                    {

                    }
                    else 
                       lstvBoxSessioni.Items.Remove(item);
                }
                if (lstvBoxSessioni.SelectedItems.Count == 1)
                    lstvBoxSessioni.Focus();
            }
        }

        private void RadioCustom_CheckedChanged(object sender, EventArgs e)
        {
            checkPubblicaDB.Visible = false;
            txtNomeSessione.Visible = false;
            lstvBoxSessioni.Visible = true;
            lblCercaSessione.Visible = true;
            txtCercaSessione.Visible = true;
            lblCercaUtente.Visible = true;
            txtCercaUtente.Visible = true;
            lblCercaPubblica.Visible = true;
            txtCercaPubblica.Visible = true;
            lblCercaMail.Visible = true;
            txtCercaMail.Visible = true;
            txtCercaSessione.Text = "Cerca...";
            txtCercaUtente.Text = "Cerca...";
            txtCercaPubblica.Text = "Cerca...";
            lblEmail.Visible = false;
            txtEmail.Visible = false;

        }

        private void RadioStandard_CheckedChanged(object sender, EventArgs e)
        {
            checkPubblicaDB.Visible = true;
            txtNomeSessione.Visible = true;
            lstvBoxSessioni.Visible = false;
            lblCercaSessione.Visible = false;
            txtCercaSessione.Visible = false;
            lblCercaUtente.Visible = false;
            txtCercaUtente.Visible = false;
            lblCercaPubblica.Visible = false;
            txtCercaPubblica.Visible = false;
            lblCercaMail.Visible = false;
            txtCercaMail.Visible = false;
            if (checkPubblicaDB.Checked)
            {
                lblEmail.Visible = true;
                txtEmail.Visible = true;
            }
        }

        private void _annulla_Click(object sender, EventArgs e)
        {
            formSessione.Close();
        }

        private void _crea_Click(object sender, EventArgs e)
        {
            if (radioStandard.Checked)
            {
                if (!checkPubblicaDB.Checked)
                    txtEmail.Text = "_null_";

                gc = new GeneraChiavi();
                addS = new Sessione(txtNomeSessione.Text, gc, txtEmail.Text);

                sCollection.Add(addS);

                if (checkPubblicaDB.Checked)
                    sProvider.AddEntity(addS);

                addS.AggiungiSessione(tabSessione, this, true);

                formSessione.Close();
                btnCancellaS.Enabled = true;

                writeConfig = new StreamWriter("config.cfg", true);
                writeConfig.WriteLine(addS.ToString());
                writeConfig.Dispose();
            }
            else if (radioCustom.Checked)
            {
                List<Sessione> listSessione = sProvider.GetEntityList();
                try
                {
                    string pubb = lstvBoxSessioni.SelectedItems[0].SubItems[2].Text;
                    string[] split = new string[2];
                    split = pubb.Split(',');

                    for (int i = 0; i < listSessione.Count; i++)
                    {
                        if (listSessione[i].NString == split[1])
                        {
                            listSessione[i].AggiungiSessione(tabSessione, this, false);
                            sCollection.Add(listSessione[i]);
                        }

                    }

                    formSessione.Close();
                    btnCancellaS.Enabled = true;

                    writeConfig = new StreamWriter("config.cfg", true);
                    writeConfig.WriteLine(sCollection[0].ToString());
                    writeConfig.Dispose();
                }
                catch (Exception ex)
                {
                    if (lstvBoxSessioni.SelectedItems.Count == 0)
                    {
                        DialogResult msgBox = MessageBox.Show("Errore","Seleziona una Sessione Valida!");
                    }
                    else
                    {
                        DialogResult msgBox = MessageBox.Show("Connessione Assente","Errore di connessione al database. Riconnettersi e riprovare.");
                        writeConfig.Dispose();
                        checkDBConnection.Text = String.Format("Connessione al Database: " + "Assente");
                        reConnect.Enabled = true;
                    }
                    
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeConfig = new StreamWriter("config.cfg");
            for (int i = 0; i < sCollection.Count; i++)
                writeConfig.WriteLine(sCollection[i].ToString());

            writeConfig.Dispose();
        }
    }
}

