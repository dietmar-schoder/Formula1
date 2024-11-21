using Formula1.Domain.Entities;

namespace Formula1.Contracts.Dtos;

public record CircuitDto(
    int Id,
    string Name)
{
    public static CircuitDto FromCircuit(Circuit circuit)
        => new(
            Id: circuit.Id,
            Name: circuit.Name);
}
