using Ong.Domain;

namespace Ong.Tests;

public class OutboxMessageTests
{
    [Fact]
    public void OutboxMessage_DeveSerCriadoComSucesso()
    {
        // Arrange
        var id = Guid.NewGuid();
        var type = "DonationCreated";
        var payload = "{\"donationId\": 1}";
        var createdOn = DateTime.UtcNow;

        // Act
        var message = new OutboxMessage(id, type, payload, createdOn);

        // Assert
        Assert.Equal(id, message.Id);
        Assert.Equal(type, message.Type);
        Assert.Equal(payload, message.Payload);
        Assert.Equal(createdOn, message.CreatedOn);
        Assert.Null(message.ProcessedOn);
        Assert.Null(message.Error);
    }

    [Fact]
    public void OutboxMessage_DeveMarcarComoProcessado()
    {
        // Arrange
        var message = new OutboxMessage(
            Guid.NewGuid(),
            "DonationCreated",
            "{}",
            DateTime.UtcNow
        );

        // Act
        var processedOn = DateTime.UtcNow;
        message.ProcessedOn = processedOn;

        // Assert
        Assert.NotNull(message.ProcessedOn);
        Assert.Equal(processedOn, message.ProcessedOn);
    }

    [Fact]
    public void OutboxMessage_DeveCriarComErro()
    {
        // Arrange & Act
        var message = new OutboxMessage(
            Guid.NewGuid(),
            "DonationCreated",
            "{}",
            DateTime.UtcNow,
            processedOn: DateTime.UtcNow,
            error: "Falha ao processar"
        );

        // Assert
        Assert.NotNull(message.ProcessedOn);
        Assert.Equal("Falha ao processar", message.Error);
    }
}

