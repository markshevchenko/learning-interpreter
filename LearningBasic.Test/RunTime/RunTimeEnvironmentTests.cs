﻿namespace LearningBasic.RunTime
{
    using System;
    using LearningBasic.IO;
    using LearningBasic.Parsing;
    using LearningBasic.Parsing.Code.Expressions;
    using LearningBasic.Parsing.Code.Statements;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RunTimeEnvironmentTests : BaseTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunTimeEnvironment_WithNullInputOutput_ThrowsArgumentNullException()
        {
            var inputOutput = (IInputOutput)null;
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunTimeEnvironment_WithNullProgramRepository_ThrowsArgumentNullException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = (IProgramRepository)null;
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
        }

        [TestMethod]
        public void RunTimeEnvironment_AfterConstructing_IsNotClosed()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            Assert.IsFalse(rte.IsClosed);
        }

        [TestMethod]
        public void Close_WhenCalled_SetsIsClosedProperty()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Close();

            Assert.IsTrue(rte.IsClosed);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Double Dispose cause this is the test for double Dispose.")]
        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Close_AfterDispose_ThrowsObjectDisposedException()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Dispose();
            rte.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_WithNullName_ThrowsArgumentNullException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new End()));

            rte.Save(null);
        }

        [TestMethod]
        public void Save_WithNotNullName_PassesNameToProgramRepository()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new End()));

            rte.Save("the name of the file");

            Assert.AreEqual("the name of the file", programRepository.LastFileName);
        }

        [TestMethod]
        public void Save_WithNotNullName_SetsLastUsedName()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new End()));

            rte.Save("the name of the file");

            Assert.AreEqual("the name of the file", rte.LastUsedName);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Save_AfterDispose_ThrowsObjectDisposedException()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new End()));

            rte.Dispose();
            rte.Save("file name");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Load_WithNullName_ThrowsArgumentNullException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Load(null);
        }

        [TestMethod]
        public void Load_WithNotNullName_PassesNameToProgramRepository()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Load("the name of the file");

            Assert.AreEqual("the name of the file", programRepository.LastFileName);
        }

        [TestMethod]
        public void Load_WithNotNullName_SetsLastUsedName()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Load("the name of the file");

            Assert.AreEqual("the name of the file", rte.LastUsedName);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Load_AfterDispose_ThrowsObjectDisposedException()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Dispose();
            rte.Load("file name");
        }

        [TestMethod]
        public void Run_WhenNonEmptyLines_EvaluatesFirstStatementOfLines()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            var wasRunned = false;
            var statement = MakeStatement(() => { wasRunned = true; });
            rte.Lines.Add(new Line("10", statement));

            var result = rte.Run();

            Assert.IsTrue(wasRunned);
        }

        [TestMethod]
        public void Run_WhenEmptyLines_DoesNotThrowException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            try
            {
                var result = rte.Run();
            }
            catch (Exception exception)
            {
                Assert.Fail("Expected no exception, but got: " + exception.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Run_WhenIsRunning_ThrowsInvalidOperationException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new Run()));

            var result = rte.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Run_AfterDispose_ThrowsObjectDisposedException()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new End()));

            rte.Dispose();
            rte.Run();
        }

        [TestMethod]
        public void End_WhenIsRunning_StopsRunning()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            rte.Lines.Add(new Line("10", new End()));
            var shouldNotBeTrue = false;
            rte.Lines.Add(new Line("20", MakeStatement(() => { shouldNotBeTrue = true; })));

            var result = rte.Run();

            Assert.IsFalse(shouldNotBeTrue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void End_WhenIsNotRunning_ThrowsInvalidOperationException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            var end = new End();

            var result = end.Execute(rte);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void End_AfterDispose_ThrowsObjectDisposedException()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new End()));

            rte.Dispose();
            rte.End();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Goto_WhenIsNotRunning_ThrowsInvalidOperationException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);

            var @goto = new Goto(new Constant("100"));

            var result = @goto.Execute(rte);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Goto_WithNonExistentNumber_ThrowsArgumentOutOfRangeException()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new Goto(new Constant("20"))));

            var result = rte.Run();
        }

        [TestMethod]
        public void Goto_WithExistentNumber_GoesToSpecifiedLine()
        {
            var inputOutput = MakeInputOutput();
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new Goto(new Constant("30"))));
            int actual = 0;
            rte.Lines.Add(new Line("20", MakeStatement(() => { actual += 20; })));
            rte.Lines.Add(new Line("30", MakeStatement(() => { actual += 30; })));

            var result = rte.Run();

            Assert.AreEqual(30, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Goto_AfterDispose_ThrowsObjectDisposedException()
        {
            var inputOutput = MakeInputOutput("any string");
            var programRepository = MakeProgramRepository();
            var rte = new RunTimeEnvironment(inputOutput, programRepository);
            rte.Lines.Add(new Line("10", new End()));

            rte.Dispose();
            rte.Goto("10");
        }
    }
}