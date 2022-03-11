namespace ThirteenBase.Tests
{
    [TestClass]
    public sealed class TaskParametersTest
    {
        [TestMethod]
        public void MiddlePartMultiplier()
        {
            Assert.AreEqual(13, TaskParameters.Default.MiddlePartMultiplier);
            Assert.AreEqual(2, new TaskParameters(Base: 2, NumberLength: 3, LeftRightPartLength: 1).MiddlePartMultiplier);
            Assert.AreEqual(9, new TaskParameters(Base: 3, NumberLength: 4, LeftRightPartLength: 1).MiddlePartMultiplier);
        }
    }
}
