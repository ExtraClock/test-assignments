namespace ThirteenBase.Tests;

public abstract class SolutionTestBase<TSolution>
    where TSolution : SolutionBase, new()
{
    protected bool RunActualSolution { get; init; } = true;

    [TestMethod]
    public void SolveZeroLength()
    {
        TSolution solution = new();
        Assert.AreEqual(13, solution.Solve(0, 0));
        Assert.AreEqual(13 * 6, solution.Solve(0, 1));
    }

    [TestMethod]
    public void SolveOneDigit()
    {
        TSolution solution = new() { TaskParameters = new(Base: 13, NumberLength: 3, LeftRightPartLength: 1) };
        Assert.AreEqual(13 * 13, solution.Solve(1, 0));
    }

    [TestMethod]
    public void SolveSimple()
    {
        TSolution solution = new() { TaskParameters = new(Base: 13, NumberLength: 3, LeftRightPartLength: 1) };
        Assert.AreEqual(13, solution.Solve());
    }


    [TestMethod()]
    public void Solve()
    {
        if (!RunActualSolution)
        {
            return;
        }

        TSolution solution = new();
        Assert.AreEqual(661137896133L, solution.Solve());
    }

    [TestMethod]
    public void CountNumbersWithGivenSumOfDigits()
    {
        PlainStupid solution = new();
        Assert.AreEqual(0, solution.CountNumbersWithGivenSumOfDigits(0, 314));
        Assert.AreEqual(1, solution.CountNumbersWithGivenSumOfDigits(314, 0));

        Assert.AreEqual(6, solution.CountNumbersWithGivenSumOfDigits(6, 1));
        Assert.AreEqual(21, solution.CountNumbersWithGivenSumOfDigits(6, 2));

        Assert.AreEqual(0, solution.CountNumbersWithGivenSumOfDigits(1, 13));

        Assert.AreEqual(1, solution.CountNumbersWithGivenSumOfDigits(1, 12));
        Assert.AreEqual(13, solution.CountNumbersWithGivenSumOfDigits(2, 12));

        Assert.AreEqual(6188, solution.CountNumbersWithGivenSumOfDigits(6, 12 * 5));
    }
}
