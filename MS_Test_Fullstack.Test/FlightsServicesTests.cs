using Microsoft.AspNetCore.Http;
using Moq;
using MS_Test_Fullstack.Domain.IReposotories;
using MS_Test_Fullstack.Domain.Models;
using MS_Test_Fullstack.Services;
using Newtonsoft.Json;
using System.Text;

public class FlightsServicesTests
{
    private readonly Mock<IFlightsRepository> _mockFlightsRepository;
    private readonly FlightsServices _flightsServices;

    public FlightsServicesTests()
    {
        _mockFlightsRepository = new Mock<IFlightsRepository>();
        _flightsServices = new FlightsServices(_mockFlightsRepository.Object);
    }

    [Fact]
    public async Task CreateFlights_Success_ReturnsResultList()
    {
        // Arrange
        var flights = new List<Flights>
        {
            new Flights { Origin = "MZL", Destination = "BOG", Price = 1000 },
            new Flights { Origin = "PEI", Destination = "MZL", Price = 800 }
        };

        var requestBody = JsonConvert.SerializeObject(flights);
        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
        var httpRequest = new DefaultHttpContext().Request;
        httpRequest.Body = memoryStream;

        var expectedResults = new List<ResultFlights>
        {
            new ResultFlights { Id = 1, Route = "MZL-BOG" },
            new ResultFlights { Id = 2, Route = "PEI-MZL" }
        };

        // Mocking repository behavior
        _mockFlightsRepository
            .SetupSequence(repo => repo.Create(It.IsAny<Flights>()))
            .ReturnsAsync(expectedResults[0])
            .ReturnsAsync(expectedResults[1]);

        // Act
        var result = await _flightsServices.CreateFlights(httpRequest);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);



        _mockFlightsRepository.Verify(repo => repo.Create(It.IsAny<Flights>()), Times.Exactly(2));
    }

    [Fact]
    public async Task CreateFlights_Failure_ReturnsErrorMessage()
    {
        // Arrange
        var flights = new List<Flights>
        {
            new Flights { Origin = "MZL", Destination = "BOG", Price = 1000 }
        };

        var requestBody = JsonConvert.SerializeObject(flights);
        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
        var httpRequest = new DefaultHttpContext().Request;
        httpRequest.Body = memoryStream;

        // Mocking repository to throw an exception
        _mockFlightsRepository
            .Setup(repo => repo.Create(It.IsAny<Flights>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _flightsServices.CreateFlights(httpRequest);

        // Assert
        Assert.False(result.IsSuccess);
        //Assert.Equal("Database error", result.Error);

        _mockFlightsRepository.Verify(repo => repo.Create(It.IsAny<Flights>()), Times.Once);
    }

    [Fact]
    public async Task CreateFlights_InvalidJson_ReturnsErrorMessage()
    {
        // Arrange
        var invalidJson = "{ invalid json format }";
        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));
        var httpRequest = new DefaultHttpContext().Request;
        httpRequest.Body = memoryStream;

        // Act
        var result = await _flightsServices.CreateFlights(httpRequest);

        // Assert
        Assert.False(result.IsSuccess);

        Assert.Contains("Invalid character", result.Message); // Example error message for invalid JSON
    }
}
