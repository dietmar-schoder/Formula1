namespace Formula1.Contracts.Responses;

public record AliveResponse(
    string Service,
    DateTime UtcNow,
    string Version);
