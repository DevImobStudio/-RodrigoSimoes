using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Imobiliaria.Services
{
    public class DataBaseAsync
    {
        public SQLiteAsyncConnection database { get; }

        public DataBaseAsync()
        {
            this.database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "imobrodrigosimoes.db"));

        }

        public DataBaseAsync(SQLiteAsyncConnection database)
        {
            this.database = database;
        }
    }
}
