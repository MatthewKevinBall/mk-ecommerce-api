using System.Collections.Generic;
using System.Linq;

public class AuthService{
    private static readonly List<Admin> Admins = new List<Admin>{
        new Admin { Username = "admin", Password = "password"}
    };

    public bool Authenticate(string username, string password){
        return Admins.Any(u => u.Username == username && u.Password == password);
    }
}