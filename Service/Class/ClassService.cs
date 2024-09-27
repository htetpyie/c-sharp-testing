using Database.SQLDbContextModels;

namespace Service.ClassService
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

            var batch = new TblBatch();
        }
    }
}
