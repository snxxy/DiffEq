
namespace Generator
{
    internal sealed class EqScrambler
    {
        private int maxVar = 0;
        public IEquation Scrambler(IEquation eq)
        {
            PyScriptsLoader psp = PyScriptsPool.Instance.GetObject();
            string solution = null;
            string solutionLatex = null;
            if (eq.Type == 1)
            {
                eq.LatexEq = psp.Solve(eq.Eq, out solution, out solutionLatex);
                eq.Solution = solution;
                eq.SolutionLatex = solutionLatex;
                PyScriptsPool.Instance.PutObject(psp);
                maxVar = 0;
                return eq;
            }
            else if (eq.Type == 2)
            {
                for (int i = 0; i < eq.Eq.Length - 1; i++)
                {
                    if (eq.Eq[i].Equals('*') && eq.Eq[i + 1].Equals('*'))
                    {
                        if (((int)(char.GetNumericValue(eq.Eq[i + 2])) > maxVar))
                        {
                            maxVar = (int)(char.GetNumericValue(eq.Eq[i + 2]));
                        }
                    }
                }
                var equals = eq.Eq.Split("=");
                var a = equals[0].Split(")/(");
                a[0] = a[0] + ")" ;
                a[1] = "(" + a[1];
                a[0] = psp.DoScrambling(a[0], maxVar);
                a[1] = psp.DoScrambling(a[1], maxVar);
                eq.Eq = "dydx * (" + a[1].Split("==")[0] + ")" +")" + "=" + a[0].Split("==")[0] + ")";
                PyScriptsPool.Instance.PutObject(psp);
                maxVar = 0;
                return eq;
            }
            return null;
        }
    }
}
