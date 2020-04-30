using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SQLite;
using TidBit.Models;

namespace TidBit.Data
{
    public class FavArticleDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public FavArticleDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Article>().Wait();
        }

        public Task<List<Article>> GetArticlesAsync()
        {
            return _database.Table<Article>().ToListAsync();
        }

        public Task<Article> GetArticleAsync(int id)
        {
            return _database.Table<Article>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveArticleAsync(Article article)
        {
            return _database.InsertAsync(article);
        }

        public Task<int> DeleteArticleAsync(Article article)
        {
            return _database.DeleteAsync(article);
        }
    }
}