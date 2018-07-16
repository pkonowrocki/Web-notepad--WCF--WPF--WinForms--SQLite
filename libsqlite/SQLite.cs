using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace libsqlite
{
    public class SQLite
    {
        string Watcher_path;
        string Watcher_file;
        string path;
        SHA256 hash = SHA256.Create();
        public bool DBis()
        {
            return File.Exists(path);
        }
        public bool CreateDB()
        {
            
            try
            {
                SQLiteConnection.CreateFile(path);
                return true;
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Source + " " + e.Message);
            }
            return false;
        }
        public bool CreateUserTable()
        {
            bool f = false;
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;"))
            {
                m_dbConnection.Open();
                string sql = "CREATE TABLE Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, User TEXT)";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                f = true;
            }
            return f;
        }
        public bool AddNewUser(string imie)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {   
                cnn.Open();
                SQLiteCommand insertCommand = new SQLiteCommand("INSERT INTO Users (User) VALUES (@1)", cnn);
                insertCommand.Parameters.AddWithValue("@1", imie);
                return insertCommand.ExecuteNonQuery() == 1;
            }
        }
        public int GetUserId(string user)
        {
            using(cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand getCommand = new SQLiteCommand("SELECT Id FROM Users WHERE User = @0", cnn);
                getCommand.Parameters.AddWithValue("@0", user);
                using (SQLiteDataReader reader = getCommand.ExecuteReader())
                {
                    reader.Read();
                    if (reader.HasRows)
                        return reader.GetInt32(0);
                    else
                        return -1;
                }
            }
        }
        public int AddNewText(string imie)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand insertCommand = new SQLiteCommand("INSERT INTO Texts (UserId) VALUES (@1)", cnn);
                insertCommand.Parameters.AddWithValue("@1", GetUserId(imie));
                insertCommand.ExecuteNonQuery();
                return LastTextID(imie);
            }
        }
        public int LastTextID(string imie)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand getCommand = new SQLiteCommand("SELECT Id FROM Texts WHERE UserId = @0 ORDER BY Id DESC", cnn);
                getCommand.Parameters.AddWithValue("@0", GetUserId(imie));
                using (SQLiteDataReader reader = getCommand.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
            }
        }
        public int FirstTextID(string imie)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand getCommand = new SQLiteCommand("SELECT Id FROM Texts WHERE UserId = @0 ORDER BY Id", cnn);
                getCommand.Parameters.AddWithValue("@0", GetUserId(imie));
                using (SQLiteDataReader reader = getCommand.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
            }
        }
        public string FirstText(string imie)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand getCommand = new SQLiteCommand("SELECT Lines FROM Texts WHERE UserId = @0 ORDER BY Id", cnn);
                getCommand.Parameters.AddWithValue("@0", GetUserId(imie));
                using (SQLiteDataReader reader = getCommand.ExecuteReader())
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                        return reader.GetString(0);
                    else
                        return "";
                }
            }
        }
        public Dictionary<int,string> GetTexts(string imie)
        {
            Dictionary<int, string> a = new Dictionary<int, string>();
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand getCommand = new SQLiteCommand(@" SELECT Texts.Id, Texts.Lines 
                                                                FROM Texts INNER JOIN Users  
                                                                ON Texts.UserId=Users.Id 
                                                                WHERE Users.User = @0", cnn);
                
                getCommand.Parameters.AddWithValue("@0", imie);
                using (SQLiteDataReader reader = getCommand.ExecuteReader())
                {
                    while (reader.Read())
                        if (!reader.IsDBNull(1))
                            if (reader.GetString(1).Length > 13)
                                a.Add(reader.GetInt32(0), reader.GetString(1).Substring(0, 10) + "...");
                            else
                                a.Add(reader.GetInt32(0), reader.GetString(1));
                        else
                            a.Add(reader.GetInt32(0), null);
                }
                return a;
            }
        }
        public string GetText(int ID)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand getCommand = new SQLiteCommand("SELECT Lines FROM Texts WHERE Id=@1", cnn);
                getCommand.Parameters.AddWithValue("@1", ID);
                using (SQLiteDataReader reader = getCommand.ExecuteReader())
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                        return reader.GetString(0);
                    else
                        return "";
                }
                return "";
            }
        }
        public Dictionary<int, string> GetUsers()
        {
            Dictionary<int, string> a = new Dictionary<int, string>();
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand getCommand = new SQLiteCommand(@" SELECT Id, User 
                                                                FROM Users", cnn);

                
                using (SQLiteDataReader reader = getCommand.ExecuteReader())
                {
                    while (reader.Read())
                        if (!reader.IsDBNull(1))
                            a.Add(reader.GetInt32(0), reader.GetString(1));
                        else
                            a.Add(reader.GetInt32(0), null);
                }
                return a;
            }
        }
        public bool UpdateText(int ID, string tekst)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand updateCommand = new SQLiteCommand("UPDATE Texts SET Lines=@2 WHERE Id=@1", cnn);
                updateCommand.Parameters.AddWithValue("@1", ID);
                updateCommand.Parameters.AddWithValue("@2", tekst);
                return updateCommand.ExecuteNonQuery() == 1;
            }
        }
        public void CreateTextTable()
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;"))
            {
                m_dbConnection.Open();
                string sql = "CREATE TABLE Texts (Id INTEGER PRIMARY KEY AUTOINCREMENT, UserId INTEGER , Lines TEXT, FOREIGN KEY(UserId) REFERENCES Users(Id))";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
            }
        }
        public int TextsPerUser(string name)
        {
            
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand("Select Count(Texts.Id) From Texts Inner Join Users On Texts.UserId = Users.Id Where Users.User = @0", cnn);
                command.Parameters.AddWithValue("@0", name);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    if (reader.HasRows)
                    {
                        return reader.GetInt32(0);
                    }
                    else
                        return 0;
                    
                }
            }
        }
        public SQLite(string _path = "D:\\", string _db ="DB2.sqlite" )
        {
            path = _path+_db;
            Watcher_path = _path;
            Watcher_file = _db;
            connectionString = "Data Source = " + path + "; Version = 3; ";
        }
        public void Connect()
        {
            connectionString = "Data Source = "+path+"; Version = 3; ";
            cnn = new SQLiteConnection(connectionString);
            cnn.Open();
            cnn.Close();
        }
        string connectionString;
        SQLiteConnection cnn;
        public bool UserExist(string imie)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM Users WHERE User = @0", cnn);
                command.Parameters.AddWithValue("@0", imie);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return reader.HasRows;
                }
            }
        }
        FileSystemWatcher watcher = new FileSystemWatcher();
        public delegate void OnChangedDB(object source, FileSystemEventArgs e);
        public void SetWatcher(OnChangedDB actionOnDBChanged)
        {
            watcher.Path = Watcher_path;
            watcher.Filter = Watcher_file;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            watcher.Changed += new FileSystemEventHandler(actionOnDBChanged);
            watcher.EnableRaisingEvents = true;
        }
        public int TextLen(int TextId)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT LENGTH(Lines) FROM Texts WHERE Id = @0", cnn);
                command.Parameters.AddWithValue("@0", TextId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                        return reader.GetInt32(0);
                    else
                        return 0;
                }
            }
        }
        public int LinesNum(int TextId)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT LENGTH(Lines)-LENGTH(REPLACE(Lines,char(10),''))+1 FROM Texts WHERE Id = @0", cnn);
                command.Parameters.AddWithValue("@0", TextId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                        return reader.GetInt32(0);
                    else
                        return 0;
                }
            }
        }
        public async Task<int> LinesNumAsync(int TextId)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT LENGTH(Lines)-LENGTH(REPLACE(Lines,char(10),''))+1 FROM Texts WHERE Id = @0", cnn);
                command.Parameters.AddWithValue("@0", TextId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    await reader.ReadAsync();
                    if (!reader.IsDBNull(0))
                        return reader.GetInt32(0);
                    else
                        return 0;
                }
            }
        }
        public async Task<int> TextLenAsync(int TextId)
        {
            using (cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT LENGTH(Lines) FROM Texts WHERE Id = @0", cnn);
                command.Parameters.AddWithValue("@0", TextId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    await reader.ReadAsync();
                    if (!reader.IsDBNull(0))
                        return reader.GetInt32(0);
                    else
                        return 0;
                }
            }
        }

        string GetSHA256(SHA256 sha256, string input) 
        {
            byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input.ToCharArray()));
            StringBuilder sB = new StringBuilder();
            foreach(byte b in data)
            {
                sB.Append(b);
            }
            return sB.ToString();
        }
        bool VerifyMd5Hash(SHA256 sha256, string input, string hash)
        {
            string hashOfInput = GetSHA256(sha256, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }
    }
}
