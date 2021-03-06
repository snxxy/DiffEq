﻿using System;
using System.Collections.Generic;
using DiffEq.BinaryTree;

namespace DiffEq.Strats
{
    class SeparableTreeStrategy:IGenerationStrategy
    {
        private RandomNumberGenerator rng = new RandomNumberGenerator();
        public string ExecuteTreeAlgorithm(string variable, int difficulty)
        {
            List<string> operators;

            Random random = new Random();
            operators = new List<string>() { "+", "-", "/", "*" };
            List<string> trigo = new List<string>() { "sin", "cos", "tan", "cot" };
            var tree = new BinaryTree<string>(difficulty);
            if (difficulty % 2 == 0)
            {
                for (int i = 0; i < difficulty / 2 - 1; i++)
                {
                    string toAdd = operators[random.Next(0, operators.Count - 1)];
                    if (toAdd.Equals("*") || toAdd.Equals("/"))
                    {
                        toAdd = toAdd + "(";
                    }
                    tree.Add(toAdd);
                }
                tree.Add(trigo[random.Next(0, trigo.Count - 1)]);
                tree.Add("(" + variable + ")");
                for (int i = 0; i < difficulty / 2 - 1; i++)
                {
                    var varChance = random.Next(1, 3);
                    if (varChance == 1)
                    {
                        var powChance = random.Next(1, 3);
                        if (powChance == 1)
                        {
                            tree.Add(rng.Generate(1, 5, 0).ToString() + "*" + variable + "**" + "2");
                        }
                        else
                        {
                            tree.Add(rng.Generate(1, 5, 0).ToString() + "*" + variable);
                        }
                    }
                    else
                    {
                        tree.Add(rng.Generate(1, 5, 0).ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < difficulty / 2 - 1; i++)
                {
                    string toAdd = operators[random.Next(0, operators.Count - 1)];
                    if (toAdd.Equals("*") || toAdd.Equals("/"))
                    {
                        toAdd = toAdd + "(";
                    }
                    tree.Add(toAdd);
                }
                tree.Add("*" + variable);
                for (int i = 0; i < difficulty / 2; i++)
                {
                    var varChance = random.Next(1, 3);
                    if (varChance == 1)
                    {
                        var powChance = random.Next(1, 3);
                        if (powChance == 1)
                        {
                            tree.Add(rng.Generate(1, 5, 0).ToString() + "*" + variable + "**" + "2");
                        }
                        else
                        {
                            tree.Add(rng.Generate(1, 5, 0).ToString() + "*" + variable);
                        }
                    }
                    else
                    {
                        tree.Add(rng.Generate(1, 5, 0).ToString());
                    }
                }
            }
            List<string> eq = tree.InOrder(tree.Head);
            int insertCounter = 0;
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
