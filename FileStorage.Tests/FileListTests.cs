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

    private const string FilePath = @"c:\data\mock_dtos.jsonl";

    [Fact]
    public void CreatesFileIfNotExists()
    {
        // Arrange
        var fileSystem = new MockFileSystem();
        var _sut = new FileList<MockDto>(
            FilePath,
            fileSystem);
        var mockObject = new MockDto {Id = 12, Text = "Hello"};

        // Act
        _sut.Add(mockObject);
        bool fileExists = fileSystem.File.Exists(FilePath);

        //Assert
        fileExists.Should().BeTrue();
    }
}