﻿namespace LearningBasic.Parsing.Code.Statements
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LoadTests : BaseTests
    {
        [TestMethod]
        public void Execute_OfLoad_LoadsLines()
        {
            var programRepository = MakeProgramRepository();
            var rte = MakeRunTimeEnvironment(programRepository);
            var load = new Load("filename");

            load.Execute(rte);
            var expected = programRepository.Lines.ToList();
            var actual = rte.Lines;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Execute_OfLoad_UsesFilename()
        {
            var programRepository = MakeProgramRepository();
            var rte = MakeRunTimeEnvironment(programRepository);
            var load = new Load("filename");

            load.Execute(rte);

            Assert.AreEqual("filename", programRepository.LastFileName);
        }

        [TestMethod]
        public void Execute_OfLoad_StoresFilename()
        {
            var programRepository = MakeProgramRepository();
            var rte = MakeRunTimeEnvironment(programRepository);
            var load = new Load("filename");

            load.Execute(rte);

            Assert.AreEqual("filename", rte.LastUsedName);
        }
    }
}