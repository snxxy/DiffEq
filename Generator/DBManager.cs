using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Generator
{
    public sealed class DBManager
    {
        private static readonly object singleLock = new object();

        private static DBManager instance = null;
        private Context context = new Context();
        public static DBManager Instance
        {
            get
            {
                lock (singleLock)
                {
                    if (instance == null)
                    {
                        instance = new DBManager();
                    }
                    return instance;
                }
            }
        }

        public int Count()
        {
            return context.Equation.Count();
        }

        public int CountByType(int type)
        {
            return context.Equation.Where(a => a.Type == type).Count();
        }

        public void AddToDB(Equation equation)
        {
            Console.WriteLine($"Adding {equation.Eq} to DB");
            bool flag = false;
            var a = Count();
            for (int i = 1; i <= a; i++)
            {
                if (GetEquationById(i) == null)
                {
                    equation.Id = i;
                    context.Equation.Add(equation);
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
                    context.SaveChanges();
                }
            }
        }

        public void AddToDB(List<IEquation> equations)
        {
            for (int i = 0; i < equations.Count; i++)
            {
                Equation equation = new Equation() { Eq = equations[i].Eq, Latex = equations[i].LatexEq, Type = equations[i].Type, Solution = equations[i].Solution, SolutionLatex = equations[i].SolutionLatex};
                AddToDB(equation);
            }
        }

        public int? GetLatestId()
        {
            if (!context.Equation.Any())
            {
                return null;
            }
            return context.Equation.OrderByDescending(a => a.Id).FirstOrDefault().Id;
        }

        public void DeleteFromDB(int id)
        {
            Equation equation = new Equation() { Id = id };
            context.Equation.Attach(equation);
            context.Equation.Remove(equation);
            context.SaveChanges();
        }
        public Equation GetEquationById(int id)
        {
            if (GetLatestId() == null)
            {
                return null;
            }
            return context.Equation.FirstOrDefault(a => a.Id == id);
        }

        public List<Equation> GetEquationList(int count, int type)
        {
            List<Equation> forSort = new List<Equation>();
            List<Equation> result = new List<Equation>();
            Equation eq = new Equation();
            Random rand = new Random();

            int countType = CountByType(type);

            var eqs = context.Equation.Where(a => a.Type == type).Take(countType);
            foreach (var equat in eqs)
            {
                result.Add(equat);
            }

            int toDelete = countType - count;
            for (int i = 0; i < toDelete; i++)
            {
                result.RemoveAt(rand.Next(0, result.Count()-1));
            }

            return result;
        }
    }
}
