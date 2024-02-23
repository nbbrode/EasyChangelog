using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace EasyChangelogTests;

[TestClass]
public class ProgramTests
{
    [TestMethod]
    public void ProgramCs_TaskShouldCompleteSuccessfully()
    {
        // Arrange
        var entryPoint = typeof(Program).Assembly.EntryPoint!;

        // Act
        var task = Task.Run(() => entryPoint.Invoke(null, new object[]
        {
            new[] { "cancel" }
        }));

        task.Wait();

        // Assert
        Assert.IsTrue(task.IsCompletedSuccessfully);
    }
}