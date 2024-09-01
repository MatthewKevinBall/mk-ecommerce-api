public static class ConversionHelper
{
    public static  UserResponse UserToUserResponse(User user)
    {
       var userResponse = new UserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            UserType = "User"
        };

        if (user.IsSuperAdmin)
        {
            userResponse.UserType = "Admin";
        }

        return userResponse;
    }
}