using DiffEq.Generator.BinaryTree;
using DiffEq.Generator.Misc;
using DiffEq.Generator.GenerationConfigs;
using System;

namespace DiffEq.Generator.Strats
{
    public class HomogeneousTreeStrategy : IGenerationStrategy
    {
        IGenerationConfig genCfg;
        public HomogeneousTreeStrategy()
        {
            genCfg = new HomogeneousTreeConfig();
        }

        public string ExecuteTreeAlgorithm(string variable, int difficulty)
        {
            var rngCustom = new RandomNumberGenerator();
            var rngDefault = new Random();
            var tree = new BinaryTree<string>(difficulty);
            var operatorsPossibleCount = (difficulty / 2) - 1;

            if (!genCfg.IsTrigonometryEnabled)
            {
                if (difficulty % 2 == 0)
                {
                    difficulty++;
                }
            }                 

            for (int i = 0; i < operatorsPossibleCount; i++)
            {
                var toAdd = genCfg.Operators[rngDefault.Next(0, genCfg.Operators.Count - 1)];
                tree.Add(toAdd);
            }

            tree.Add("*" + "(" + variable + ")");
            for (int i = 0; i < difficulty / 2; i++)
            {
                var varChance = rngDefault.Next(1, 3);
                if (varChance == 1)
                {
                    tree.Add(rngCustom.Generate(1, 5, 0).ToString() + "*" + "(" + variable + ")" + "**"  + rngDefault.Next(2, 5).ToString());
                }
                else
                {
                    tree.Add(rngCustom.Generate(1, 5, 0).ToString());
                }
            }

            var eq = tree.InOrder(tree.Head);
            var insertCounter = 0;
            for (int i = 0; i < eq.Count; i++)
            {
                if (eq[i].Equals("*(") || eq[i].Equals("/("))
                {
                    insertCounter++;
                }
            }
            for (int i = 0; i < insertCounter; i++)
            {
                eq.Insert(0, ")");
            }
            string equ = null;
            for (int i = eq.Count - 1; i >= 0; i--)
            {
                equ += eq[i];
            }
            return equ;
        }
    }
}
