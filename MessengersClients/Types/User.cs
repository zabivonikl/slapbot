namespace MessengersClients.Types;

public partial class User
{
    public User(long id, string firstName)
    {
        Id = id;
        FirstName = firstName;
    }

    public long Id { get; set; }

    public string FirstName { get; set; }
}