using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DiffEq.Generator.Handlers;
using DiffEq.Generator.Models.Solution;
using System.Collections;

namespace DiffEq.Generator.Tests
{
    [TestFixture]
    internal class SolutionHandlerTests
    {
        SolutionHandler solutionHandlerInstance;
        [SetUp]
        public void Setup()
        {
            solutionHandlerInstance = new SolutionHandler();
        }
        [Test]
        public async Task GetSolutionsChecked_NullIEnumerable_ReturnsNull()
        {
            var result = await solutionHandlerInstance.GetSolutionsChecked(null);
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public async Task GetSolutionsChecked_NullItems_ReturnsNull()
        {
            var result = await solutionHandlerInstance.GetSolutionsChecked(new List<Solution>());
            Assert.That(result, Is.Null);

            var testSolution = new Solution();
            testSolution.UserSolution = "something";
            var testInput = new List<Solution>();
            testInput.Add(testSolution);

            result = await solutionHandlerInstance.GetSolutionsChecked(testInput);
            Assert.That(result, Is.Null);
        }

        //NOT ISOLATED!
        //Requires running PySolverApi
        //Consider refactor
        [Test]
        [TestCaseSource(typeof(ExpressionTestTrueSource))]
        public async Task GetSolutionsChecked_CorrectInput_ReturnsTrueEquality(IEnumerable<Solution> testSolutions)
        {
            var result = await solutionHandlerInstance.GetSolutionsChecked(testSolutions);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.All.Not.Null);

            Assert.That(result, Is.All.True);
        }

        public class ExpressionTestTrueSource : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                yield return new List<Solution>() {
                    new Solution(
                        "(-4*x + sin(x))*(13*y + 6)/3", "Eq(y(x), _C - (-4*x + sin(x))*(13*y + 6)/3)"),
                    new Solution(
                        "(x*(-4) + sin(x))*(6+13*y)/3", "Eq(y(x), _C - (-4*x + sin(x))*(13*y + 6)/3)"),
                    new Solution(
                        "4*(4*x - cos(x))/y", "Eq(y(x), _C + 4*(4*x - cos(x))/y)")
                };
            }
        }
    }
}
