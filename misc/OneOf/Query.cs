namespace OneOf;

public class Query
{
    public IPet CreatePet(PetInput input)
        => input.Cat ?? (IPet)input.Dog!;
}

[OneOf]
public class PetInput
{
    public Cat? Cat { get; set; }

    public Dog? Dog { get; set; }
}

public interface IPet
{
    string Name { get; }
}

public class Cat : IPet
{
    public Cat(string name, int numberOfLives)
    {
        Name = name;
        NumberOfLives = numberOfLives;
    }

    public string Name { get; }

    public int NumberOfLives { get; }
}

public class Dog : IPet
{
    public Dog(string name, bool wagsTail)
    {
        Name = name;
        WagsTail = wagsTail;
    }

    public string Name { get; }

    public bool WagsTail { get; }
}