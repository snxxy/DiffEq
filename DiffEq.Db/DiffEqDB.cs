using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffEq.DB
{
    public sealed class DiffEqDB
    {
        //private static readonly object singleLock = new object();

        //private static DBManager instance = null;
        //private Context context = new Context();
        //public static DBManager Instance
        //{
        //    get
        //    {
        //        lock (singleLock)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new DBManager();
        //            }
        //            return instance;
        //        }
        //    }
        //}

        public async Task<int> GetCount()
        {
            using (var context = new Context())
            {
                var result = await context.Equation.CountAsync();
                return result;
            }
        }

        public async Task<int> CountByType(int type)
        {
            using (var context = new Context())
            {
                var result = await context.Equation.Where(a => a.Type == type).CountAsync();
                return result;
            }
        }

        public async Task<IEnumerable<int>> GetEquationCounts()
        {
            var result = new List<int>();
            using (var context = new Context())
            {
                result = await context.Equation.GroupBy(a => new { a.Type }).Select(b => new { type = b.Key.Type, count = b.Count() }).Select(c => c.count).ToListAsync();
            }
            return result;
        }

        public async Task AddToDB(Equation equation)
        {
            Console.WriteLine($"Adding {equation.Eq} to DB");
            var flag = false;
            var a = await GetCount();
            using (var context = new Context())
            {
                for (int i = 1; i <= a; i++)
                {
                    if (GetEquationById(i) == null)
                    {
                        equation.Id = i;
                        await context.Equation.AddAsync(equation);
                        //context.Entry(equation).State = EntityState.Added;
                        await context.SaveChangesAsync();
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    if (await GetLatestId() == null)
                    {
                        equation.Id = 1;
                    }
                    else
                    {
                        equation.Id = await GetLatestId() + 1;
                        await context.Equation.AddAsync(equation);
                        //context.Entry(equation).State = EntityState.Added;
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task AddToDB(IEnumerable<Equation> equations)
        {
            foreach (var item in equations)
            {
                    await AddToDB(item);
            }
        }

        public async Task<int?> GetLatestId()
        {
            using (var context = new Context())
            {
                if (!await context.Equation.AnyAsync())
                {
                    return null;
                }
                var result = await context.Equation.OrderByDescending(a => a.Id)
                                                   .FirstOrDefaultAsync();
                return result.Id;
            }
        }

        public void DeleteFromDB(int id)
        {
            using (var context = new Context())
            {
                var equation = new Equation() { Id = id };
                context.Equation.Attach(equation);
                context.Equation.Remove(equation);
                context.SaveChanges();
            }
        }
        public async Task<Equation> GetEquationById(int id)
        {
            if (await GetLatestId() == null)
            {
                return null;
            }
            using (var context = new Context())
            {
                var result = await context.Equation.FirstOrDefaultAsync(a => a.Id == id);
                return result;
            }
        }

        public async Task<IEnumerable<Equation>> GetEquationList(int count, int type)
        {
            var result = new List<Equation>();
            var eq = new Equation();
            var rand = new Random();

            var countType = await CountByType(type);
            using (var context = new Context())
            {
                var eqs = context.Equation.Where(a => a.Type == type).Take(countType);
                foreach (var equat in eqs)
                {
                    result.Add(equat);
                }

                var toDelete = countType - count;
                for (int i = 0; i < toDelete; i++)
                {
                    result.RemoveAt(rand.Next(0, result.Count() - 1));
                }
            }
            return result;
        }
    }
}
