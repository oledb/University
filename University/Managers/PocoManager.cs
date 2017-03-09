using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Managers
{
    public abstract class PocoManager<T> where T : class
    {
        private IContextFactory factory;
        internal PocoManager(IContextFactory contextFactory)
        {
            factory = contextFactory;
        }

        protected abstract void addObject(T obj, UniversityContext context);
        protected abstract IQueryable<T> getObject(Func<T, bool> isValid,
            UniversityContext context);
        protected abstract void removeObject(T obj, UniversityContext context);

        public void Add(T obj)
        {
            using (var context = factory.Create())
            {
                addObject(obj, context);
                context.SaveChanges();
            }
        }

        public List<T> Get()
        {
            using (var context = factory.Create())
            {
                return getObject(o => true, context).ToList();
            }
        }

        public List<T> Get(Func<T, bool> isValid)
        {
            using (var context = factory.Create())
                return getObject(isValid, context).ToList();
        }

        public T GetSingle(Func<T, bool> isValid)
        {
            using (var context = factory.Create())
                return getObject(isValid, context).SingleOrDefault();
        }
    }
}
