using NUnit.Framework;
using DiffEq.Generator.Strats;
using System;

namespace DiffEq.Generator.Tests;
[TestFixture]
public class SeparableTreeStrategyTests
{
    private SeparableTreeStrategy separableInstance;
    [SetUp]
    public void Setup()
    {
        separableInstance = new SeparableTreeStrategy();
    }

    [Test]
    public void ExecuteTreeAlgorithm_ReturnsEqualBrackets()
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
        Assert.That(openBracketCount, Is.EqualTo(closeBracketCount));
    }

    [Test]
    public void ExecuteTreeAlgorithm_ReturnsPositiveBrackets()
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