using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace LibRSA.RSACD
{
    public class SessioneProvider
    {
        string _statoConnessioneDB;

        public string StatoConnessioneDB
        {
            get { return _statoConnessioneDB; }
            set { _statoConnessioneDB = value; }
        }

        public string TryConnection()
        {
            //Locale

            //MySqlConnection connection;
            //string server = "127.0.0.1";
            //string database = "steganografia";
            //string uid = "root";
            //string password = "";
            //string connectionString;

            //connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            //connection = new MySqlConnection(connectionString);

            //Free MySQL Hosting

            MySqlConnection connection;
            string server = "sql8.freemysqlhosting.net";
            string database = "sql8126047";
            string uid = "sql8126047";
            string password = "3DmnBWeLlA";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                _statoConnessioneDB = "Connesso";
                connection.Close();
            }
            catch(Exception ex)
            {
                _statoConnessioneDB = "Assente";
            }

            return _statoConnessioneDB;

        }
        public void AddEntity(Sessione s)
        {
            //MySqlConnection connection;
            //string server = "127.0.0.1";
            //string database = "steganografia";
            //string uid = "root";
            //string password = "";
            //string connectionString;

            //connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            //connection = new MySqlConnection(connectionString);

            MySqlConnection connection;
            string server = "sql8.freemysqlhosting.net";
            string database = "sql8126047";
            string uid = "sql8126047";
            string password = "3DmnBWeLlA";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string nomeSessione = s.NomeSessione;
                string e = s.EString;
                string n = s.NString;
                string utente = s.Utente;
                string eMail = s.Email;
                string query = String.Format("INSERT INTO Sessioni (nomeSessione, e, n, utente, eMail) values('{0}','{1}','{2}','{3}', '{4}')", nomeSessione, e, n, utente, eMail);
                MySqlCommand myCommand = new MySqlCommand(query, connection);
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
            }catch(Exception ex)
            {

            }

            connection.Close();
        }

        public void DeleteEntity(string n)
        {
            //MySqlConnection connection;
            //string server = "127.0.0.1";
            //string database = "steganografia";
            //string uid = "root";
            //string password = "";
            //string connectionString;

            //connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            //connection = new MySqlConnection(connectionString);

            MySqlConnection connection;
            string server = "sql8.freemysqlhosting.net";
            string database = "sql8126047";
            string uid = "sql8126047";
            string password = "3DmnBWeLlA";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string query = String.Format("DELETE FROM Sessioni WHERE n = '{0}'", n);
                MySqlCommand myCommand = new MySqlCommand(query, connection);
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
            }catch(Exception ex)
            {

            }

            connection.Close();
        }

        public List<Sessione> GetEntityList()
        {
            //MySqlConnection connection;
            //string server = "127.0.0.1";
            //string database = "steganografia";
            //string uid = "root";
            //string password = "";
            //string connectionString;

            //connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            //database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            //connection = new MySqlConnection(connectionString);

            MySqlConnection connection;
            string server = "sql8.freemysqlhosting.net";
            string database = "sql8126047";
            string uid = "sql8126047";
            string password = "3DmnBWeLlA";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

            SessioneCollection sCollection = new SessioneCollection();

            try
            {
                connection.Open();

                string query = String.Format("SELECT * FROM Sessioni");
                MySqlCommand myCommand = new MySqlCommand(query, connection);

                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    string nomeSessione = myReader["nomeSessione"].ToString();
                    string e = myReader["e"].ToString();
                    string n = myReader["n"].ToString();
                    string utente = myReader["utente"].ToString();
                    string eMail = myReader["eMail"].ToString();
                    Sessione s = new Sessione(nomeSessione, e, n, utente, eMail);
                    sCollection.Add(s);
                }

                myReader.Close();
            }catch(Exception ex)
            {

            }

            connection.Close();

            return sCollection;
        }
    }
}
