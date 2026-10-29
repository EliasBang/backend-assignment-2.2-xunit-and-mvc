using Xunit;
using SpaceAcademy;

namespace SpaceAcademy.Tests;

public class ExerciseTests
{
    [Theory]
    [InlineData(0, false, 0)]
    [InlineData(1, false, 100)]
    [InlineData(2, false, 200)]
    [InlineData(1, true, 120)]
    [InlineData(2, true, 240)]
    public void Calculate_ReturnsAgreedPrice(int count, bool priority, int expected)
    {
        Assert.Equal(expected, LaunchPrice.Calculate(count, priority));
    }
}
