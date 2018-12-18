using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class Specialty
    {
        private int _id;
        private string _name;


        public Specialty(string name, int id = 0)
        {
            _name = name;
            _id = id;
        }


        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }


        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@Name);";
            MySqlParameter specialtyName = new MySqlParameter();
            specialtyName.ParameterName = "@Name";
            specialtyName.Value = this._name;
            cmd.Parameters.Add(specialtyName);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }



        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialties = new List<Specialty> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int SpecialtyId = rdr.GetInt32(0);
                string SpecialtyName = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(SpecialtyName, SpecialtyId);
                allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }



        public static Specialty Find(int specialtyId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@specialtyId);";
            MySqlParameter specialty_id = new MySqlParameter();
            specialty_id.ParameterName = "@specialtyId";
            specialty_id.Value = specialtyId;
            cmd.Parameters.Add(specialty_id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            string SpecialtyName = "";
            int SpecialtyId = 0;
            while (rdr.Read())
            {
                SpecialtyId = rdr.GetInt32(0);
                SpecialtyName = rdr.GetString(1);

            }
            Specialty foundSpecialty = new Specialty(SpecialtyName, SpecialtyId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundSpecialty;
        }

        public List<Stylist> GetStylists()
        {
            List<Stylist> specialtyStylists = new List<Stylist>();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialties
                JOIN stylists_specialties on (specialties.id = stylists_specialties.specialty_id)
                JOIN stylists ON (stylists_specialties.stylist_id = stylists.stylist_id)
                WHERE specialties.id = @SpecialtyId;";
            cmd.Parameters.AddWithValue("@SpecialtyId", this._id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                Stylist newStylist = new Stylist(stylistName, stylistId);
                specialtyStylists.Add(newStylist);
                Console.WriteLine(newStylist.GetName());
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return specialtyStylists;
        }

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (specialty_id, stylist_id) VALUES (@StylistId, @StylistId);";
            MySqlParameter specialty_id = new MySqlParameter();
            specialty_id.ParameterName = "@StylistId";
            specialty_id.Value = _id;
            cmd.Parameters.Add(specialty_id);
            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@StylistId";
            stylist_id.Value = newStylist.GetId();
            cmd.Parameters.Add(stylist_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void Delete(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties WHERE id = (@thisId);";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty)otherSpecialty;
                bool idEquality = this.GetId() == newSpecialty.GetId();
                bool nameEquality = this.GetName() == newSpecialty.GetName();

                return (idEquality && nameEquality);
            }
        }

        // public static List<Copy> GetByAuthorTitle(string bookTitle, string authorName)
        // {
        //     List<Copy> foundCopies = new List<Copy> { };
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT * FROM copies WHERE (name, author) = (@bookTitle, @authorName);";
        //     MySqlParameter book_title = new MySqlParameter();
        //     book_title.ParameterName = "@bookTitle";
        //     book_title.Value = bookTitle;
        //     cmd.Parameters.Add(book_title);
        //     MySqlParameter author_name = new MySqlParameter();
        //     author_name.ParameterName = "@authorName";
        //     author_name.Value = authorName;
        //     cmd.Parameters.Add(author_name);
        //     var rdr = cmd.ExecuteReader() as MySqlDataReader;

        //     while (rdr.Read())
        //     {
        //         int CopyId = rdr.GetInt32(0);
        //         string CopyName = rdr.GetString(1);
        //         string CopyAuthor = rdr.GetString(2);
        //         bool CopyCheckedOut = rdr.GetBoolean(3);
        //         Copy newCopy = new Copy(CopyName, CopyAuthor, CopyCheckedOut, CopyId);
        //         foundCopies.Add(newCopy);
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return foundCopies;
        // }

        // public static List<Copy> GetAvailable()
        // {
        //     List<Copy> availableCopies = new List<Copy> { };
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"SELECT * FROM copies WHERE checked_out = 0;";
        //     var rdr = cmd.ExecuteReader() as MySqlDataReader;

        //     while (rdr.Read())
        //     {
        //         int CopyId = rdr.GetInt32(0);
        //         string CopyName = rdr.GetString(1);
        //         string CopyAuthor = rdr.GetString(2);
        //         bool CopyCheckedOut = rdr.GetBoolean(3);
        //         Copy newCopy = new Copy(CopyName, CopyAuthor, CopyCheckedOut, CopyId);
        //         availableCopies.Add(newCopy);
        //     }
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return availableCopies;
        // }





        //         public void Edit(bool newCheckoutStatus)
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"UPDATE copies SET checked_out = @newCheckoutStatus WHERE id = @searchId;";
        //     MySqlParameter searchId = new MySqlParameter();
        //     searchId.ParameterName = "@searchId";
        //     searchId.Value = _id;
        //     cmd.Parameters.Add(searchId);
        //     MySqlParameter checkoutStatus = new MySqlParameter();
        //     checkoutStatus.ParameterName = "@newCheckoutStatus";
        //     checkoutStatus.Value = newCheckoutStatus;
        //     cmd.Parameters.Add(checkoutStatus);
        //     cmd.ExecuteNonQuery();
        //     _checkedOut = newCheckoutStatus;
        //     conn.Close();
        //     if (conn != null)
        //         {
        //             conn.Dispose();
        //         }
        // }

    }
}