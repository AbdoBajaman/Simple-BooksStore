namespace BooksStore.Models.ReposteryPattern
{
    // Design pattern Repostry
    public interface IBookStoreRepostery<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);


        void Create(TEntity entity);
        void Update(int Id,TEntity entity);
        void Delete(int id);


    }
}
