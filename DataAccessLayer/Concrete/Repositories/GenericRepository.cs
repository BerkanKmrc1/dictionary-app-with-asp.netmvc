using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        Context c = new Context();
        DbSet<T> _object;

        public GenericRepository()
        {
            _object = c.Set<T>(); 
        }
        public void Delete(T p)
        {
            var deletedEntity = c.Entry(p);
            deletedEntity.State = EntityState.Deleted;
            //_object.Remove(p);
            c.SaveChanges(); 
        }

        public T Get(Expression<Func<T, bool>> filter)//t türünde tanımladık yani entity türünde mesela category,heading olabilir
        {
            return _object.SingleOrDefault(filter);//Bir dizide veya liste de sadece bir değer döndüren entityframework linq metodu
        }

        public void Insert(T p)
        {
         //Artık crud işlemlerini entity state komutlarıyla yapıcaz entity framework komutlarını
         //açıklama satırına aldım 
            var addedEntity = c.Entry(p);
            addedEntity.State = EntityState.Added;
            //_object.Add(p);
            c.SaveChanges();
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T p)
        {
            var updatedEntity = c.Entry(p);
            updatedEntity.State = EntityState.Modified;//önceki projelerde controllerda parametreden gelen
            //degerleri oluşturduğumuz değere atıyoduk burda entity state yardımıyla ve generic repositorynin
            //de avantajıyla kod tekrarından kurtularak kolayca güncelleme işlemi yapabildik.
            c.SaveChanges();
        }
    }
}
