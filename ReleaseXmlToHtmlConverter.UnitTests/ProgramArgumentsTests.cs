using NUnit.Framework;
using System.Linq;

namespace ReleaseXmlToHtmlConverter.UnitTests
{
    internal class ProgramArgumentsTests
    {
        [Test]
        public void Validate_Not4Or2Parameters_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[] {"abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_Not4Or2Parameters_ErrorMessageHasValue()
        {
            var programArguments = new ProgramArguments(new[] {"abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void Validate_2ArgumentsAndTheyDoNotContainPathArgumentKey_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[] {"abc", "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_2ArgumentsAndTheyDoNotContainPathArgumentKey_ErrorMessageHasValue()
        {
            var programArguments = new ProgramArguments(new[] {"abc", "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void Validate_2ArgumentsAndPathArgumentKeyIsTheLastArgument_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[] {"abc", Constants.PathConsoleArgumentKey});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_2ArgumentsAndPathArgumentKeyIsTheLastArgument_ErrorMessageHasValue()
        {
            var programArguments = new ProgramArguments(new[] {"abc", Constants.PathConsoleArgumentKey});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void Validate_2ArgumentsAndTheNextArgumentAfterPathArgumentKeyIsEmpty_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[] {Constants.PathConsoleArgumentKey, ""});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_2ArgumentsAndTheNextArgumentAfterPathArgumentKeyIsEmpty_ErrorMessageHasValue()
        {
            var programArguments = new ProgramArguments(new[] {Constants.PathConsoleArgumentKey, ""});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void Validate_2ArgumentsAndThePathArgumentKeyArgumentIsTheFirst_AreValidIsTrue()
        {
            var programArguments = new ProgramArguments(new[] {Constants.PathConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsTrue(programArguments.AreValid);
        }

        [Test]
        public void Validate_2ArgumentsAndThePathArgumentKeyArgumentIsTheFirst_ErrorMessageHasNotValue()
        {
            var programArguments = new ProgramArguments(new[] {Constants.PathConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsTrue(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIsMissing_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[] {"abc", "abc", Constants.PathConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIsMissing_ErrorMessageHasValue()
        {
            var programArguments = new ProgramArguments(new[] {"abc", "abc", Constants.PathConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIsTheLast_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[]
                {"abc", "abc", Constants.PathConsoleArgumentKey, Constants.CustomerIdsConsoleArgumentKey});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIsTheLast_ErrorMessageHasValue()
        {
            var programArguments = new ProgramArguments(new[]
                {"abc", "abc", Constants.PathConsoleArgumentKey, Constants.CustomerIdsConsoleArgumentKey});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIs1st_AreValidIsTrue()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.CustomerIdsConsoleArgumentKey, "abc", Constants.PathConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsTrue(programArguments.AreValid);
        }

        [Test]
        public void Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIs1st_ErrorMessageHasNotValue()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.CustomerIdsConsoleArgumentKey, "abc", Constants.PathConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsTrue(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void
            Validate_4ArgumentsAndPathArgumentKeyAndCustomerIdsArgumentKeyAreOnThe1stAnd3rdPositions_AreValidIsTrue()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.PathConsoleArgumentKey, "abc", Constants.CustomerIdsConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsTrue(programArguments.AreValid);
        }

        [Test]
        public void
            Validate_4ArgumentsAndPathArgumentKeyAndCustomerIdsArgumentKeyAreOnThe1stAnd3rdPositions_ErrorMessageHasNotValue()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.PathConsoleArgumentKey, "abc", Constants.CustomerIdsConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsTrue(string.IsNullOrEmpty(programArguments.ErrorMessage));
        }

        [Test]
        public void
            Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIs1stAndPathArgumentValueIsEmpty_SetsPathToEmpty()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.CustomerIdsConsoleArgumentKey, "abc", Constants.PathConsoleArgumentKey, ""});
            Assert.AreEqual("", programArguments.Path);
        }

        [Test]
        public void
            Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIs1stAndCustomerIdsArgumentValueIsEmpty_SetsCustomerIdsToEmpty()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.CustomerIdsConsoleArgumentKey, "", Constants.PathConsoleArgumentKey, "abc"});
            Assert.AreEqual(1, programArguments.CustomerIds?.Count());
            Assert.AreEqual("", programArguments.CustomerIds.First());
        }

        [Test]
        public void
            ProgramArguments_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIs1st_SetsCustomerIdsTo2ndParameter()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.CustomerIdsConsoleArgumentKey, "abc", Constants.PathConsoleArgumentKey, "abcd"});
            Assert.AreEqual(1, programArguments.CustomerIds?.Count());
            Assert.AreEqual("abc", programArguments.CustomerIds.First());
        }

        [Test]
        public void
            ProgramArguments_4ArgumentsAndPathArgumentKeyIs1stAndCustomerIdsArgumentKeyIs3rd_SetsCustomerIdsTo4thParameter()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.PathConsoleArgumentKey, "abc", Constants.CustomerIdsConsoleArgumentKey, "abcd"});
            Assert.AreEqual(1, programArguments.CustomerIds?.Count());
            Assert.AreEqual("abcd", programArguments.CustomerIds.First());
        }

        [Test]
        public void
            ProgramArguments_4ArgumentsAndPathArgumentKeyIs1stAndCustomerIdsArgumentKeyIs3rd_SetsPathTo2ndParameter()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.PathConsoleArgumentKey, "abc", Constants.CustomerIdsConsoleArgumentKey, "abcd"});
            Assert.AreEqual("abc", programArguments.Path);
        }

        [Test]
        public void
            ProgramArguments_4ArgumentsAndCustomerIdsArgumentKeyIs1stAndPathArgumentKeyIs3rd_SetsPathTo4thParameter()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.CustomerIdsConsoleArgumentKey, "abc", Constants.PathConsoleArgumentKey, "abcd"});
            Assert.AreEqual("abcd", programArguments.Path);
        }

        [Test]
        public void
            Validate_4ArgumentsAndPathArgumentKeyIs3rdAndCustomerIdsArgumentKeyIs1stAndItsValuesAreSeparatedWithComma_SetsCustomerIdsToTheListOfParameters()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.CustomerIdsConsoleArgumentKey, "a,b", Constants.PathConsoleArgumentKey, "abc"});
            Assert.AreEqual(2, programArguments.CustomerIds?.Count());
            Assert.AreEqual("a", programArguments.CustomerIds.First());
            Assert.AreEqual("b", programArguments.CustomerIds.Last());
        }
    }
}