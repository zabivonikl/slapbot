namespace Database.Entities;

public class User
{
    private readonly MessengersClients.Types.User user;

    public User() => user = new MessengersClients.Types.User(0, "");

    public User(MessengersClients.Types.User user) => this.user = user;

    public long Id
    {
        get => user.Id;
        private set => user.Id = value;
    }

    public string FirstName
    {
        get => user.FirstName;
        private set => user.FirstName = value;
    }

    public List<Game> Games { get; set; } = new();
}