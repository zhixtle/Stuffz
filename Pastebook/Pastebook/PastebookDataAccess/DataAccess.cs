using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEntities;

namespace PastebookDataAccess
{
    public class DataAccess<T> where T : class
    {
        List<Exception> listOfExceptions = new List<Exception>();

        public List<T> GetAll()
        {
            List<T> list = new List<T>();
            try
            {
                using (var context = new PASTEBOOK_LIZBETHEntities())
                {
                    list = context.Set<T>().AsNoTracking().ToList<T>();
                }
            }
            catch (Exception ex)
            {
                listOfExceptions.Add(ex);
            }
            return list;
        }

        public List<T> GetSelected(Func<T, bool> where)
        {
            List<T> list = new List<T>();
            try
            {
                using (var context = new PASTEBOOK_LIZBETHEntities())
                {
                    list = context.Set<T>().AsNoTracking().Where(where).ToList<T>();
                }
            }
            catch (Exception ex)
            {
                listOfExceptions.Add(ex);
            }
            return list;
        }

        public T GetSingle(Func<T, bool> where)
        {
            T item = null;
            try
            {
                using (var context = new PASTEBOOK_LIZBETHEntities())
                {
                    item = context.Set<T>().AsNoTracking().FirstOrDefault(where);
                }
            }
            catch (Exception ex)
            {
                listOfExceptions.Add(ex);
            }
            return item;
        }

        public bool Add(T item)
        {
            bool addSuccess = false;
            try
            {
                using (var context = new PASTEBOOK_LIZBETHEntities())
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    int addItemSuccess = context.SaveChanges();
                    addSuccess = (addItemSuccess != 0) ? true : false;
                }
            }
            catch (Exception ex)
            {
                listOfExceptions.Add(ex);
            }
            return addSuccess;
        }

        public T AddWithID(T item)
        {
            T itemWithID = null;
            try
            {
                using (var context = new PASTEBOOK_LIZBETHEntities())
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    int addItemSuccess = context.SaveChanges();
                    itemWithID = (addItemSuccess != 0) ? item : null;
                }
            }
            catch (Exception ex)
            {
                listOfExceptions.Add(ex);
            }
            return itemWithID;
        }

        public bool EditMany(List<T> items)
        {
            bool editSuccess = false;
            try
            {
                using (var context = new PASTEBOOK_LIZBETHEntities())
                {
                    foreach (T item in items)
                    {
                        context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    int editItemsSuccess = context.SaveChanges();
                    editSuccess = (editItemsSuccess != 0) ? true : false;
                }
            }
            catch (Exception ex)
            {
                listOfExceptions.Add(ex);
            }
            return editSuccess;
        }

        public bool Edit(T item)
        {
            bool editSuccess = false;
            try
            {
                using (var context = new PASTEBOOK_LIZBETHEntities())
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    int editItemSuccess = context.SaveChanges();
                    editSuccess = (editItemSuccess != 0) ? true : false;
                }
            }
            catch (Exception ex)
            {
                listOfExceptions.Add(ex);
            }
            return editSuccess;
        }
    }
}
