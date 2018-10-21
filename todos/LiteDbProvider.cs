using LiteDB;

namespace Todos
{
    public class LiteDbProvider : IDbProvider
    {
        private static LiteDatabase _db;

        public LiteDatabase Db
        {
            get => _db;
            set => _db = value;
        }

        public LiteDbProvider()
        {
            Db = new LiteDatabase(@"Filename=Todos.db;Mode=Exclusive;Flush=true");
        }

        ~LiteDbProvider() 
        {
            Db.Dispose();
            Db = null;
        }
    }
}