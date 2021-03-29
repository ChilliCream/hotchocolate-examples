using System.Collections.Generic;

namespace MongoDB
{
    public record CreatePersonInput(
        string Name,
        IReadOnlyList<Address> Addresses,
        Address MainAddress);
}