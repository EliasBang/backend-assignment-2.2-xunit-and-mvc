using Xunit;
using SpaceAcademy;

namespace SpaceAcademy.Tests;

public class ExerciseTests
{
    [Fact]
    public void Calculate_ThreeCrewForFourDays_Returns24()
    {
        // Arrange
        int crewCount = 3;
        int days = 4;
        // Act
        int actual = FuelCalculator.Calculate(crewCount, days);
        // Assert
        Assert.Equal(24, actual);
    }
    // TODO: Skriv to selvstendige Fact-tester.
}
