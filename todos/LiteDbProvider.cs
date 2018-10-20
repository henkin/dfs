using LiteDB;

namespace Todos
{
    public class LiteDbProvider : IDbProvider {
        public LiteDatabase Db { get; private set; }

        public LiteDbProvider()
        {
            Db = new LiteDatabase(@"Filename=Todos.db;Mode=Exclusive");
        }

        ~LiteDbProvider() 
        {
            Db.Dispose();
            Db = null;
        }
    }
}