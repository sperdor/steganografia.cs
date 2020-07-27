using System;
using EASendMail;
using System.Windows.Forms;

namespace LibRSA.RSACD
{
    public class eMail
    {
        SmtpMail _oMail = new SmtpMail("TryIt");
        SmtpClient _oSmtp = new SmtpClient();
        SmtpServer _oServer = new SmtpServer("smtp.gmail.com");

        public eMail(string eMailMittente, string eMailDestinatario, string eMailOggetto, string eMailCorpo, string pathAllegato)
        {
            _oMail.From = eMailMittente;
            _oMail.To = eMailDestinatario;
            _oMail.Subject = eMailOggetto;
            _oMail.TextBody = eMailCorpo;

            try
            {
                _oMail.AddAttachment(pathAllegato);
            }catch(Exception ex)
            {
                MessageBox.Show("Selezionare l'Allegato", "Errore di Invio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _oServer.Port = 587;
            _oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            _oServer.User = "prj.steganografia@gmail.com";
            _oServer.Password = "PrjctStegano2016";
        }

        public void Invia()
        {
            try
            {
                this._oSmtp.SendMail(this._oServer, this._oMail);
                MessageBox.Show("L'e-mail è stata inviata con successo!", "Successo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string messaggio = string.Format("Si è verificato un errore nell'invio della eMail. Controllare la propria connessione o che l'indirizzo e-mail sia valido. Errore: 'q{0}'", ex.Message);
                MessageBox.Show(messaggio, "Errore di Invio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
