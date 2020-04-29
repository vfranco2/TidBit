using System.Collections.Generic;
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
            var dbtotable = _database.Table<Article>().ToListAsync();
            return dbtotable;
        }

        public Task<Article> GetArticleAsync(int id)
        {
            return _database.Table<Article>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveArticleAsync(Article article)
        {
            if (article.Id != 0)
            {
                return _database.UpdateAsync(article);
            }
            else
            {
                return _database.InsertAsync(article);
            }
        }

        public Task<int> DeleteArticleAsync(Article article)
        {
            return _database.DeleteAsync(article);
        }
    }
}