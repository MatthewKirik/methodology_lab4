using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace FileStorage.Tests;

public class FileListTests
{
    private class MockDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    private const string FileName = @"c:\data\mock_dtos.jsonl";

    private static readonly MockDto[] MockObjects =
    {
        new() {Id = 12, Text = "Hello"},
        new() {Id = 13, Text = "Hello1"},
        new() {Id = 14, Text = "Vasyan"},
        new() {Id = 15, Text = "PEtro"},
        new() {Id = 16, Text = "My name is Aloha dance!"},
    };

    private static readonly MockDto MockObject = new() {Id = 12, Text = "Hello"};
    
    private static string GetJsonLine<T>(T obj)
        => JsonConvert.SerializeObject(obj) + Environment.NewLine;
    private static string GetJsonLines<T>(IEnumerable<T> objects) 
        => objects.Aggregate("", (current, obj) => current + GetJsonLine(obj));

    [Fact]
    public void CreatesFileIfNotExists()
    {
        // Arrange
        var fileSystem = new MockFileSystem();
        var _sut = new FileList<MockDto>(
            FileName,
            fileSystem);
        var mockObject = new MockDto {Id = 12, Text = "Hello"};

        // Act
        _sut.Add(mockObject);

        //Assert
        bool fileExists = fileSystem.File.Exists(FileName);
        fileExists.Should().BeTrue();
    }
    
    [Fact]
    public void WritesToFile()
    {
        // Arrange
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {FileName, ""}
        });
        var sut = new FileList<MockDto>(
            FileName,
            fileSystem);
        var lengthBefore = fileSystem.FileInfo
            .FromFileName(FileName).Length;

        // Act
        sut.Add(MockObject);

        //Assert
        var lengthAfter = fileSystem.FileInfo
            .FromFileName(FileName).Length;
        lengthAfter.Should().BeGreaterThan(lengthBefore);
    }
    
    [Fact]
    public void WritesObject()
    {
        // Arrange
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {FileName, ""}
        });
        var _sut = new FileList<MockDto>(
            FileName,
            fileSystem);

        // Act
        _sut.Add(MockObject);

        //Assert
        string json = JsonConvert.SerializeObject(MockObject) + Environment.NewLine;
        string fileContent = fileSystem.File.ReadAllText(FileName);
        fileContent.Should().Be(json);
    }
    
    [Fact]
    public void AllowsToReadObject()
    {
        // Arrange
        string json = GetJsonLine(MockObject);
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {FileName, json}
        });
        var sut = new FileList<MockDto>(
            FileName,
            fileSystem);

        // Act
        var readObject = sut.FirstOrDefault();

        //Assert
        readObject.Should().BeEquivalentTo(MockObject);
    }
    
    [Fact]
    public void AllowsToFilterObjects()
    {
        // Arrange
        string json = GetJsonLines(MockObjects);
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {FileName, json}
        });
        var sut = new FileList<MockDto>(
            FileName,
            fileSystem);
        const int fromId = 13, toId = 15;

        // Act
        var readObjects = sut
            .Where(x => x.Id is >= fromId and <= toId)
            .ToArray();

        //Assert
        var filteredMockObjects = MockObjects
            .Where(x => x.Id is >= fromId and <= toId);
        readObjects.Should().BeEquivalentTo(filteredMockObjects);
    }
    
    [Fact]
    public void RemovesObjects()
    {
        // Arrange
        var json = GetJsonLines(MockObjects);
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {FileName, json}
        });
        var sut = new FileList<MockDto>(
            FileName,
            fileSystem);
        const int fromId = 13, toId = 15;

        // Act
        sut.RemoveAll(x => x.Id is >= fromId and <= toId);

        //Assert
        var filteredMockObjects = MockObjects
            .Where(x => x.Id is < fromId or > toId);
        string jsonAfterRemove = GetJsonLines(filteredMockObjects);
        string fileContent = fileSystem.File.ReadAllText(FileName);
        fileContent.Should().Be(jsonAfterRemove);
    }
    
    [Fact]
    public void RemovesFirstObject()
    {
        // Arrange
        var json = GetJsonLines(MockObjects);
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {FileName, json}
        });
        var sut = new FileList<MockDto>(
            FileName,
            fileSystem);

        // Act
        sut.RemoveFirst(_ => true);

        //Assert
        var filteredMockObjects = MockObjects
            .Skip(1);
        string jsonAfterRemove = GetJsonLines(filteredMockObjects);
        string fileContent = fileSystem.File.ReadAllText(FileName);
        fileContent.Should().Be(jsonAfterRemove);
    }
}