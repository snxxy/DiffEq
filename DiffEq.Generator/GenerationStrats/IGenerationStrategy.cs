namespace DiffEq.Generator.Strats
{
    interface IGenerationStrategy
    {
        string ExecuteTreeAlgorithm(string variable, int difficulty);
    }
}
