﻿using System;
using System.Data.SqlClient;

namespace PRG_4_PROJEK.Models
{
    public class Mahasiswa
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
            
        public Mahasiswa(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _connection = new SqlConnection(_connectionString);
        }

        public MahasiswaModel getDataByUsername(string rfid)
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();
            try
            {
                string query = "SELECT * from mahasiswa where rfid = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", rfid);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    mahasiswaModel.nim = reader["nim"].ToString();
                    mahasiswaModel.nama = reader["nama"].ToString();
                    mahasiswaModel.rfid = reader["rfid"].ToString();
                    mahasiswaModel.pin = reader["pin"].ToString();
                    mahasiswaModel.jk = reader["jenis_kelamin"].ToString();
                    mahasiswaModel.jp = Convert.ToInt32(reader["jam_plus"]);
                    mahasiswaModel.jm = Convert.ToInt32(reader["jam_minus"]);
                    mahasiswaModel.softskil = reader["softskil"].ToString();
                    mahasiswaModel.status = reader["status"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally 
            {
                _connection.Close();
            }
            return mahasiswaModel;
        }
        public MahasiswaModel getDataByPassword(string pin)
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();
            try
            {
                string query = "SELECT * FROM mahasiswa WHERE pin = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", pin);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    mahasiswaModel.nim = reader["nim"].ToString();
                    mahasiswaModel.nama = reader["nama"].ToString();
                    mahasiswaModel.rfid = reader["rfid"].ToString();
                    mahasiswaModel.pin = reader["pin"].ToString();
                    mahasiswaModel.jk = reader["jenis_kelamin"].ToString();
                    mahasiswaModel.jp = Convert.ToInt32(reader["jam_plus"]);
                    mahasiswaModel.jm = Convert.ToInt32(reader["jam_minus"]);
                    mahasiswaModel.softskil = reader["softskil"].ToString();
                    mahasiswaModel.status = reader["status"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                _connection.Close();
            }
            return mahasiswaModel;
        }

        public List<MahasiswaModel> getAllData()
        {
            List<MahasiswaModel> mahasiswaList = new List<MahasiswaModel>();
            try
            {
                string query = "select * from mahasiswa";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MahasiswaModel mahasiswa= new MahasiswaModel
                    {
                        nim = reader["nim"].ToString(),
                        nama = reader["nama"].ToString(),
                        rfid = reader["rfid"].ToString(),
                        pin = reader["pin"].ToString(),
                        jk = reader["jenis_kelamin"].ToString(),
                        jp = Convert.ToInt32(reader["jam_plus"]),
                        jm = Convert.ToInt32(reader["jam_minus"]),
                        softskil = reader["softskil"].ToString(),
                        status = reader["status"].ToString(),
                };
                    mahasiswaList.Add(mahasiswa);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mahasiswaList;
        }

        public MahasiswaModel getData(string id)
        {
            MahasiswaModel mahasiswaModel = new MahasiswaModel();
            try
            {
                string query = "select * from mahasiswa where nim = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                mahasiswaModel.nim = reader["nim"].ToString();
                mahasiswaModel.nama = reader["nama"].ToString();
                mahasiswaModel.rfid = reader["rfid"].ToString();
                mahasiswaModel.pin = reader["pin"].ToString();
                mahasiswaModel.jk = reader["jenis_kelamin"].ToString();
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mahasiswaModel;
        }

        public void insertData(MahasiswaModel mahasiswaModel)
        {
            try
            {
                string query = "insert into mahasiswa values(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", mahasiswaModel.nim);
                command.Parameters.AddWithValue("@p2", mahasiswaModel.nama);
                command.Parameters.AddWithValue("@p3", mahasiswaModel.rfid);
                command.Parameters.AddWithValue("@p4", mahasiswaModel.pin);
                command.Parameters.AddWithValue("@p5", mahasiswaModel.jk);
                command.Parameters.AddWithValue("@p6", mahasiswaModel.jp);
                command.Parameters.AddWithValue("@p7", mahasiswaModel.jm);
                command.Parameters.AddWithValue("@p8", mahasiswaModel.softskil);
                command.Parameters.AddWithValue("@p9", mahasiswaModel.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void updateData(MahasiswaModel mahasiswaModel)
        {
            try
            {
                string query = "update mahasiswa " +
                "set nama = @p2, " +
                "rfid = @p3, " +
                "jenis_kelamin = @p4, " +
                "pin = @p5, " +
                "status = @p6 " +
                "where nim = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", mahasiswaModel.nim);
                command.Parameters.AddWithValue("@p2", mahasiswaModel.nama);
                command.Parameters.AddWithValue("@p3", mahasiswaModel.rfid);
                command.Parameters.AddWithValue("@p4", mahasiswaModel.jk);
                command.Parameters.AddWithValue("@p5", mahasiswaModel.pin);
                command.Parameters.AddWithValue("@p6", mahasiswaModel.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("update : "+ex.Message);
            }
        }

        public void deleteData(string id)
        {
            try
            {
                string query = "DELETE FROM mahasiswa WHERE nim = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    } 
}