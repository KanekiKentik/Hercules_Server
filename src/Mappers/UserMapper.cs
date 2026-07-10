public static class UserMapper
{
    public static UserDTO ToDTO(this UserEntity user)
    {
        return new (user.Id, user.Username, user.Privilege, user.RegistrationDate);
    }
}