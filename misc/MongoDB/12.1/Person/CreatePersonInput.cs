namespace Demo;

public record CreatePersonInput(
    string Name,
    IReadOnlyList<Address> Addresses,
    Address MainAddress);
