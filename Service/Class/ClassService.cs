using Database.SQLDbContextModels;

namespace Service.Class
{
    public class ClassService
    {
        private readonly SQLAppDbContext _sqlDb;

        public ClassService(SQLAppDbContext sqlDb)
        {
            _sqlDb = sqlDb;
        }

        public async Task InsertStudent()
        {
            var classList = new List<TblClass>();
            using var transaction = await _sqlDb.Database.BeginTransactionAsync();
            foreach (var item in Enumerable.Range(1, 10))
            {
                var classItem = new TblClass
                {
                    Name = $"Class {item}"
                };
                classList.Add(classItem);
            }

            await _sqlDb.AddRangeAsync(classList);
            await _sqlDb.SaveChangesAsync();
            await transaction.RollbackAsync();
            var batch = new TblBatch();
        }
    }
}
