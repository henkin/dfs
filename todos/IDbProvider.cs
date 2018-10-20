using LiteDB;

namespace Todos
{
    public interface IDbProvider 
    {
        LiteDatabase Db { get; }
    }
}