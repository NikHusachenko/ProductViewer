using Dapper;
using ProductViewer.Common;
using ProductViewer.Database.Entities;
using System.Data;

namespace ProductViewer.DataAccess.Repositories
{
    public class ProductRepository : IGenericRepository<ProductEntity>
    {
        private readonly IDbConnection _connection;

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Count(string query, object obj)
            => await _connection.ExecuteScalarAsync<int>(query, obj);

        public async Task Create(ProductEntity entity)
        {
            string query = $"INSERT INTO {DbTables.PRODUCT_TABLE_NAME} ([Index], Title, Description, Price, Rate, Count, ImageName, ImageExtension) VALUES (@Index, @Title, @Description, @Price, @Rate, @Count, @ImageName, @ImageExtension); SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";
            long id = await _connection.ExecuteScalarAsync<long>(query, entity);
            entity.Id = id;
        }

        public async Task Delete(ProductEntity entity)
        {
            string query = $"DELETE FROM {DbTables.PRODUCT_TABLE_NAME} WHERE Id = @Id";
            await _connection.ExecuteAsync(query, new { Id = entity.Id });
        }

        public async Task<ProductEntity> Find(long id)
        {
            string query = $"SELECT * FROM {DbTables.PRODUCT_TABLE_NAME} WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<ProductEntity>(query, new { Id = id });
        }

        public async Task<ProductEntity> First(string query, object obj)
            => await _connection.QueryFirstOrDefaultAsync<ProductEntity>(query, obj);

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            string query = $"SELECT * FROM {DbTables.PRODUCT_TABLE_NAME}";
            return await _connection.QueryAsync<ProductEntity>(query);
        }

        public async Task<IEnumerable<ProductEntity>> GetAll(string query, object obj)
            => await _connection.QueryAsync<ProductEntity>(query, obj);

        public async Task<IEnumerable<ProductEntity>> GetChunk(int skip, int take)
        {
            string query = $"SELECT * FROM {DbTables.PRODUCT_TABLE_NAME} ORDER BY Id OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
            return await _connection.QueryAsync<ProductEntity>(query, new { skip = skip, take = take });
        }

        public async Task<int> Max(string query) 
            => await _connection.ExecuteScalarAsync<int>(query);

        public async Task Update(ProductEntity entity)
        {
            string query = $"UPDATE {DbTables.PRODUCT_TABLE_NAME} SET [Index] = @Index, Title = @Title, Description = @Description, Price = @Price, Rate = @Rate, Count = @Count, ImageName = @ImageName, ImageExtension = @ImageExtension WHERE Id = @Id";
            await _connection.ExecuteAsync(query, entity);
        }
    }
}