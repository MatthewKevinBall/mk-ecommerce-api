using System.Collections.Generic;
using System.Linq;

public class AuthService{
    private static readonly List<User> Users = new List<User>{
        new User { Username = "admin", Password = "password"}
    };

    public bool Authenticate(string username, string password){
        return Users.Any(u => u.Username == username && u.Password == password);
    }
}