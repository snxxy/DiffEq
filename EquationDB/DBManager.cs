using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationDB
{
    public sealed class DBManager
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

        public int Count()
        {
            using (Context context = new Context())
            {
                return context.Equation.Count();
            }
        }

        public int CountByType(int type)
        {
            using (Context context = new Context())
            {
                return context.Equation.Where(a => a.Type == type).Count();
            }
        }

        public void AddToDB(Equation equation)
        {
            Console.WriteLine($"Adding {equation.Eq} to DB");
            bool flag = false;
            var a = Count();
            using (Context context = new Context())
            {
                for (int i = 1; i <= a; i++)
                {
                    if (GetEquationById(i) == null)
                    {
                        equation.Id = i;
                        context.Equation.Add(equation);
                        //context.Entry(equation).State = EntityState.Added;
                        context.SaveChanges();
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    if (GetLatestId() == null)
                    {
                        equation.Id = 1;
                    }
                    else
                    {
                        equation.Id = GetLatestId() + 1;
                        context.Equation.Add(equation);
                        //context.Entry(equation).State = EntityState.Added;
                        context.SaveChanges();
                    }
                }
            }
            Console.WriteLine(Count());
        }

        public void AddToDB(IEnumerable<Equation> equations)
        {
            using (Context context = new Context())
            {
                foreach (var item in equations)
                {
                    AddToDB(item);
                }
            }
        }

        public int? GetLatestId()
        {
            using (Context context = new Context())
            {
                if (!context.Equation.Any())
                {
                    return null;
                }
                return context.Equation.OrderByDescending(a => a.Id).FirstOrDefault().Id;
            }
        }

        public void DeleteFromDB(int id)
        {
            using (Context context = new Context())
            {
                Equation equation = new Equation() { Id = id };
                context.Equation.Attach(equation);
                context.Equation.Remove(equation);
                context.SaveChanges();
            }
        }
        public Equation GetEquationById(int id)
        {
            if (GetLatestId() == null)
            {
                return null;
            }
            using (Context context = new Context())
            {
                return context.Equation.FirstOrDefault(a => a.Id == id);
            }
        }

        public List<Equation> GetEquationList(int count, int type)
        {
            List<Equation> result = new List<Equation>();
            Equation eq = new Equation();
            Random rand = new Random();

            int countType = CountByType(type);
            using (Context context = new Context())
            {
                var eqs = context.Equation.Where(a => a.Type == type).Take(countType);
                foreach (var equat in eqs)
                {
                    result.Add(equat);
                }

                int toDelete = countType - count;
                for (int i = 0; i < toDelete; i++)
                {
                    result.RemoveAt(rand.Next(0, result.Count() - 1));
                }
            }
            return result;
        }
    }
}
