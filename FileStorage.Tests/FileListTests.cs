using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Xunit;

namespace FileStorage.Tests;

public class FileListTests
{
    private class MockDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    private const string filePath = @"c:\data\mock_dtos.jsonl";

    [Fact]
    public void CreatesFileIfNotExists()
    {
        // Arrange
        var fileSystem = new MockFileSystem();
        var _sut = new FileList<MockDto>(
            filePath,
            fileSystem);
        var mockObject = new MockDto {Id = 12, Text = "Hello"};

        // Act
        _sut.Add(mockObject);
        bool fileExists = fileSystem.File.Exists(filePath);

        //Assert
        fileExists.Should().BeTrue();
    }
}