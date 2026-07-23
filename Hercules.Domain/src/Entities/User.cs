public class UserEntity : IEntityBase
{
    public int Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTimeOffset RegistrationDate { get; private set; }
    public Privilege Privilege { get; private set; }
    public ICollection<TemplateEntity> Templates { get; private set; } = [];
    public ICollection<WorkoutEntity> Workouts { get; private set; } = [];

    private UserEntity() {}
    public UserEntity(string username, string passwordHash, DateTimeOffset registrationDate) 
        => (Username, PasswordHash, RegistrationDate, Privilege) = (username, passwordHash, registrationDate, Privilege.User);
    
    public void SetPasswordHash(string passwordHash) => PasswordHash = passwordHash;
    public void SetUsername(string username) => Username = username;
    public void SetPrivilege(Privilege privilege) => Privilege = privilege;
}