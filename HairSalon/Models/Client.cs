using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Client
    {
        private int _clientId;
        private string _clientName;
        private int _clientPhone;
        private int _stylistId;

        public Client (string clientName, int clientPhone, int stylistId, int clientId = 0)
        {
            _clientName = clientName;
            _clientPhone = clientPhone;
            _stylistId = stylistId;
            _clientId = clientId;
        }

        public string GetName()
        {
            return _clientName;
        }

        public void SetName(string newClientName)
        {
            _clientName = newClientName;
        }

        public int GetClientId()
        {
            return _clientId;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }

        public static List<Item> GetAll()
        {
            List<Client> allClients = new List<Client>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            var rdr = cmd.ExecuteReader() as MYSqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int clientPhone = rdr.GetInt10(2);
                int stylistId = rdr.GetInt32(3);
                Client newClient = new Client(clientName, clientPhone, stylistId, clientId);
                allClients.Add(newClient);
            }
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static Client Find(int clientId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE client_id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = clientId;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            int clientPhone = 0;
            int tempStylistId = 0;
            while(rdr.Read())
                {
                    clientId = rdr.GetInt32(0);
                    clientName = rdr.GetString(1);
                    clientPhone = rdr.GetInt10(2);
                    stylistId = rdr.GetInt32(3);
                }
            Client newClient = new Client(clientName, clientPhone, stylistId, clientId);
            conn.Close();
            if (conn != null)
                {
                    conn.Dispose();
                }
            return newClient;
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
                {
                    return false;
                }
            else
                {
                    Client newClient = (Client) otherClient;
                    bool idEquality = this.GetClientId() == newClient.GetClientId();
                    bool nameEquality = this.GetClientName() == newClient.GetClientName();
                    bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
                    return (idEquality && nameEquality && stylistEquality);
                }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (client_name, client_phone, stylist_id) VALUES (@name, @phone, @stylistId);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);
            MySqlParameter phone = new MySqlParameter();
            phone.ParameterName = "@phone";
            phone.Value = this._phone;
            cmd.Parameters.Add(phone);
            MySqlParameter phone = new MySqlParameter();
            stylistId.ParameterName = "@stylistId";
            stylistId.Value = this._stylistId;
            cmd.Parameters.Add(stylistId);
            cmd.ExecuteNonQuery();
            _clientId = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
                {
                    conn.Dispose();
                }
        }
    }
}
