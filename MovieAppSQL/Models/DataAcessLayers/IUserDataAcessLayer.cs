namespace MovieAppSQL.Models
{
    public interface IUserDataAcessLayer
    {
        bool AddUser(User user);
        bool ChangePassword(string email, string pass);
        bool CheckLogin(string email, string pass);
        User GetUserDetails(string emailId);
    }
}