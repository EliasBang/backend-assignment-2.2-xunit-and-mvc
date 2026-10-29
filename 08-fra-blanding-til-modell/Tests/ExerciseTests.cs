using Xunit;
using SpaceAcademy;

namespace SpaceAcademy.Tests;

public class ExerciseTests
{
    [Theory]
    [InlineData(50, true, true)]
    [InlineData(49, true, false)]
    public void CanLaunch_ReturnsAgreedDecision(int fuel, bool approved, bool expected)
    {
        Assert.Equal(expected, LaunchPolicy.CanLaunch(fuel, approved));
    }
}
