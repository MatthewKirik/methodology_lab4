using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace FileStorage.Tests;

public class FileListTests
{
    private class MockDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    private const string fileDirectory = @"c:\data\";
    private const string fileName = "mock_dtos.jsonl";
    private const string filePath = @"c:\data\mock_dtos.jsonl";

    [Fact]
    public void CreatesFileIfNotExists()
    {
        // Arrange
        var fileSystem = new MockFileSystem();
        
        // Act
        var _sut = new FileList<MockDto>(
            fileDirectory,
            fileName,
            fileSystem);
        
        //Assert
        bool fileExists = fileSystem.File.Exists(filePath);
        Assert.True(fileExists);
    }
}