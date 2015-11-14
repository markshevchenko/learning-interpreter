﻿namespace LearningBasic.Parsing.Code.Statements
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ListTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfList_PrintsIncludedLines()
        {
            var inputOutput = MakeInputOutput();
            var rte = MakeRunTimeEnvironment(inputOutput);
            rte.AddOrUpdate(new Line("10", new Nop()));
            rte.AddOrUpdate(new Line("20", new Nop()));
            rte.AddOrUpdate(new Line("30", new Nop()));
            rte.AddOrUpdate(new Line("40", new Nop()));
            rte.AddOrUpdate(new Line("50", new Nop()));
            rte.AddOrUpdate(new Line("60", new Nop()));
            var list = new List(new Range(25, 45));

            list.Execute(rte);

            var expected = new[] { "30 ", "40 ", "" };
            var actual = inputOutput.OutputStrings.ToList();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}