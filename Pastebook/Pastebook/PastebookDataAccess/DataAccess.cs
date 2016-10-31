using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEntities;
using System.Data.Entity;
using System.Linq.Expressions;

namespace PastebookDataAccess
{
    public class DataAccess<T> where T : class
    {

        public List<T> GetAll()
        {
            List<T> list = new List<T>();

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                list = context.Set<T>().ToList<T>();
            }

            return list;
        }

        public List<T> GetSelected(Expression<Func<T, bool>> where)
        {
            List<T> list = new List<T>();

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                list = context.Set<T>().Where(where).ToList<T>();
            }

            return list;
        }

        public List<T> GetSelected(Expression<Func<T, bool>> where, params string[] navigations)
        {
            List<T> list = new List<T>();

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                var query = context.Set<T>().Where(where).AsQueryable();
                foreach (string navigation in navigations)
                {
                    query = query.Include(navigation);
                }
                list = query.ToList();
            }

            return list;
        }

        public T GetSingle(Expression<Func<T, bool>> where)
        {
            T item = null;

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                item = context.Set<T>().FirstOrDefault(where);
            }

            return item;
        }

        public T GetSingle(Expression<Func<T, bool>> where, params string[] navigations)
        {
            T item = null;

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                var query = context.Set<T>().Where(where).AsQueryable();
                foreach (string navigation in navigations)
                {
                    query = query.Include(navigation);
                }
                item = query.FirstOrDefault();
            }

            return item;
        }

        public bool Add(T item)
        {
            bool addSuccess = false;

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Added;
                int addItemSuccess = context.SaveChanges();
                addSuccess = (addItemSuccess != 0) ? true : false;
            }

            return addSuccess;
        }

        public T AddWithID(T item)
        {
            T itemWithID = null;

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Added;
                int addItemSuccess = context.SaveChanges();
                itemWithID = (addItemSuccess != 0) ? item : null;
            }

            return itemWithID;
        }

        public bool EditMany(List<T> items)
        {
            bool editSuccess = false;

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                int editItemsSuccess = context.SaveChanges();
                editSuccess = (editItemsSuccess != 0) ? true : false;
            }

            return editSuccess;
        }

        public bool Edit(T item)
        {
            bool editSuccess = false;

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                int editItemSuccess = context.SaveChanges();
                editSuccess = (editItemSuccess != 0) ? true : false;
            }

            return editSuccess;
        }

        public bool Delete(T item)
        {
            bool deleteSuccess = false;

            using (var context = new PASTEBOOK_LIZBETHEntities())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                int deleteItemSuccess = context.SaveChanges();
                deleteSuccess = (deleteItemSuccess != 0) ? true : false;
            }

            return deleteSuccess;
        }
    }
}
