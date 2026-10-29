using Xunit;
using SpaceAcademy;

namespace SpaceAcademy.Tests;

public class ExerciseTests
{
    [Fact]
    public void Constructor_StartsEmpty()
    {
        var bay = new CargoBay();
        Assert.Equal(0, bay.Count);
    }
    // TODO: Flere tester. Opprett et nytt CargoBay i hver test.
}
