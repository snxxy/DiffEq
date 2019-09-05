using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace Generator
{
    class Generator
    {
        private Random ran = new Random();
        private List<IEquation> Cache = new List<IEquation>();

        public int ManageOrder(Dictionary<int, int> pairs, bool useDb = true)
        {
            foreach(var order in pairs)
            {
                for (int i = 1; i <= order.Value; i++)
                {
                    if (order.Key == 1)
                    {
                        Dispatch(new SeparableVariables());
                    }
                    else if (order.Key == 2)
                    {
                        Dispatch(new Homogeneous());
                    }
                    else
                    {
                        throw new NotImplementedException($"Equations of type {order.Key} are not supported");
                    }
                }
            }   
            if (useDb)
            {
                DBManager.Instance.AddToDB(Cache);
            }
            {
                foreach (var equation in Cache)
                {
                    Console.WriteLine("----------------");
                    Console.WriteLine(equation.Type);
                    Console.WriteLine(equation.Eq);
                    Console.WriteLine("----------------");
                }
            }
            return 0;
        }

        private void Dispatch(IEquation equation)
        {
            dynamic _equation = equation;
            dynamic genResult = Generate(_equation);
            Cache.Add(genResult);
        }

        private IEquation Generate(SeparableVariables sv)
        {
            RandomFunctionGenerator generator = new RandomFunctionGenerator(new SeparableTreeStrategy());
            EqScrambler es = new EqScrambler();
            sv.Eq = "(" + generator.Generate("x", ran.Next(4,12)) + ")" + "*" + "(" + generator.Generate("y", ran.Next(4, 12)) +
                ")" + "=" + "dydx";
            sv = (SeparableVariables)es.Scrambler(sv);
            return sv;
        }

        private IEquation Generate(Homogeneous hg)
        {
            RandomFunctionGenerator generator = new RandomFunctionGenerator(new HomogeneousTreeStrategy());
            EqScrambler es = new EqScrambler();
            hg.Eq = "(" + generator.Generate("y/x", (1 + 2 * ran.Next(3, 6))) + ")" + "/" + "(" + generator.Generate("y/x", (1 + 2 * ran.Next(3, 6))) + ")" + "=" + "dydx";
            hg = (Homogeneous)es.Scrambler(hg);
            return hg;
        }
    }
}
