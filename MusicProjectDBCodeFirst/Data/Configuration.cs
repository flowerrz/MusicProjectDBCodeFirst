using System;
using System.Collections.Generic;
using System.Text;

namespace MusicProjectDBCodeFirst.Data
{
    public static class Configuration
    {
        static string connectionString = "Server=.\\SQLEXPRESS; Database=MusicProjectDB; Integrated Security=True;";

        static public string ConnectionString { get { return connectionString; } }
    }
}
