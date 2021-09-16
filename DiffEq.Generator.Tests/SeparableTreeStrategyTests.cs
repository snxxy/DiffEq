using NUnit.Framework;
using DiffEq.Generator.Strats;
using System;

namespace DiffEq.Generator.Tests;
public class Tests
{
    private SeparableTreeStrategy separableInstance;
    [SetUp]
    public void Setup()
    {
        separableInstance = new SeparableTreeStrategy();
    }

    [Test]
    public void TestEquationGenerationEqualBrackets()
    {
        var result = separableInstance.ExecuteTreeAlgorithm("x", 6);
        var openBracketCount = 0;
        var closeBracketCount = 0;
        foreach (var item in result)
        {
            if (item.Equals('('))
            {
                openBracketCount++;
            }
            else if (item.Equals(')'))
            {
                closeBracketCount++;
            }
        }
        Console.WriteLine(result);
        Assert.AreEqual(openBracketCount, closeBracketCount);
    }

    [Test]
    public void TestEquationGenerationBracketsNotZero()
    {
        var result = separableInstance.ExecuteTreeAlgorithm("x", 6);
        var openBracketCount = 0;
        var closeBracketCount = 0;
        foreach (var item in result)
        {
            if (item.Equals('('))
            {
                openBracketCount++;
            }
            else if (item.Equals(')'))
            {
                closeBracketCount++;
            }
        }
        Console.WriteLine(result);
        Assert.IsTrue(openBracketCount > 0);
        Assert.IsTrue(closeBracketCount > 0);
    }
}