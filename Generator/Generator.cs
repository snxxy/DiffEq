using System;
using System.Collections.Generic;

namespace Generator
{
    public class Generator
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
                    Console.WriteLine(equation.Equation);
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
            EquationManager equationManager = new EquationManager();
            sv.Equation = "(" + generator.Generate("x", ran.Next(4,12)) + ")" + "*" + "(" + generator.Generate("y", ran.Next(4, 12)) +
                ")" + "=" + "dydx";
            sv = (SeparableVariables)equationManager.SolveAndScramble(sv);
            return sv;
        }

        private IEquation Generate(Homogeneous hg)
        {
            RandomFunctionGenerator generator = new RandomFunctionGenerator(new HomogeneousTreeStrategy());
            EquationManager equationManager = new EquationManager();
            hg.Equation = "(" + generator.Generate("y/x", (1 + 2 * ran.Next(3, 6))) + ")" + "/" + "(" + generator.Generate("y/x", (1 + 2 * ran.Next(3, 6))) + ")" + "=" + "dydx";
            hg = (Homogeneous)equationManager.SolveAndScramble(hg);
            return hg;
        }
    }
}
