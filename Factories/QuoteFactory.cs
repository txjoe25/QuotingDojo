using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using QuotingDojo.Models;

namespace QuotingDojo.Factories
{
    public class QuoteFactory : IFactory<Quote>
    {
        private string connectionString;
        public QuoteFactory()
        {
            connectionString = "server=localhost;userId=root;port=3306;database=quotesexample;SslMode=None";
        }

        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Quote Item)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = "INSERT INTO quotes (QuoterName, QuoteContent, CreatedAt, UpdatedAt) VALUES (@QuoterName, @QuoteContent, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(Query, Item);
            }
        }

        public IEnumerable<Quote> FindAll()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = "SELECT * FROM quotes";
                dbConnection.Open();
                return dbConnection.Query<Quote>(Query);
            }
        }

        public Quote GetQuoteById(int Id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = $"SELECT * FROM quotes WHERE QuoteId = {Id} LIMIT 1";
                dbConnection.Open();
                return dbConnection.Query<Quote>(Query).First();
            }
        }

        public void UpdateQuote(Quote item)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = "UPDATE quotes SET QuoterName = @QuoterName, QuoteContent = @QuoteContent, Likes = @Likes WHERE QuoteId = @QuoteId";
                dbConnection.Open();
                dbConnection.Execute(Query, item);
            }
        }

        public void DeleteQuote(int Id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string Query = $"DELETE FROM quotes WHERE QuoteId = {Id}";
                dbConnection.Open();
                dbConnection.Execute(Query);
            }
        }
    }
}