using Xunit;
using SpaceAcademy;

namespace SpaceAcademy.Tests;

public class ExerciseTests
{
    [Theory]
    [InlineData(1, 4, true)]
    [InlineData(4, 4, true)]
    [InlineData(5, 4, false)]
    public void CanReserve_ReturnsExpectedDecision(int requested, int available, bool expected)
    {
        bool actual = SeatPolicy.CanReserve(requested, available);
        Assert.Equal(expected, actual);
    }
    // TODO: Legg til null, negativ bestilling og tom shuttle.
}
