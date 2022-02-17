using NUnit.Framework;

namespace ReleaseXmlToHtmlConverter.UnitTests
{
    internal class ProgramTestsExperiments
    {
        [Test]
        public void Validate_MoreThan4Arguments_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.PathConsoleArgumentKey, "abc", Constants.CustomerIdsConsoleArgumentKey, "abc", "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_3Arguments_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[] {Constants.PathConsoleArgumentKey, "abc", "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_3ArgumentsAndCustomerIdsArgumentKeyDoesNotHaveValueParameter_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.PathConsoleArgumentKey, "abc", Constants.CustomerIdsConsoleArgumentKey});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_1Argument_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[] {"abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_OnlyCustomerIdsArgumentsArePresent_AreValidIsFalse()
        {
            var programArguments = new ProgramArguments(new[] {Constants.CustomerIdsConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsFalse(programArguments.AreValid);
        }

        [Test]
        public void Validate_4ArgumentsAndPathArgumentKeyIs1stAndCustomerIdsArgumentKeyIs3rd_AreValidIsTrue()
        {
            var programArguments = new ProgramArguments(new[]
                {Constants.PathConsoleArgumentKey, "abc", Constants.CustomerIdsConsoleArgumentKey, "abc"});
            programArguments.ValidateAndSetParameters();
            Assert.IsTrue(programArguments.AreValid);
        }
    }
}