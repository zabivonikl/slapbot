namespace MessengersClients.Types;

public class User
{
    public User(long id, string firstName)
    {
        Id = id;
        FirstName = firstName;
    }

    public long Id { get; private init; }

    public string FirstName { get; private init; }
}