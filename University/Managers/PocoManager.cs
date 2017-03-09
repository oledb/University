using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public abstract class PocoManager<T> where T : class
    {
        private IContextFactory factory;
        internal PocoManager(IContextFactory contextFactory)
        {
            factory = contextFactory;
        }

        protected abstract void createObject(T obj, UniversityContext context);
        protected abstract IEnumerable<T> readObject(Func<T, bool> isValid,
            UniversityContext context);
        protected abstract void updateObject(T obj, UniversityContext context);
        protected abstract void deleteObject(T obj, UniversityContext context);

        public void Create(T obj)
        {
            using (var context = factory.Create())
            {
                createObject(obj, context);
                context.SaveChanges();
            }
        }

        public List<T> Read()
        {
            using (var context = factory.Create())
            {
                return readObject(o => true, context).ToList();
            }
        }

        public List<T> Read(Func<T, bool> isValid)
        {
            using (var context = factory.Create())
                return readObject(isValid, context).ToList();
        }

        public T ReadSingle(Func<T, bool> isValid)
        {
            using (var context = factory.Create())
                return readObject(isValid, context).SingleOrDefault();
        }

        public void Update(T obj)
        {
            using (var context = factory.Create())
            {
                updateObject(obj, context);
                context.SaveChanges();
            }
        }

        public void Delete(T obj)
        {
            using (var context = factory.Create())
            {
                deleteObject(obj, context);
                context.SaveChanges();
            }
        }
    }
}
