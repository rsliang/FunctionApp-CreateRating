namespace FunctionApp_HttpExample
{
    public class User
    {
        string userId;
        string userName;
        string fullName;

        public User ()
        {
            userId = "";
            userName = "";
            fullName = "";
        }

        public User(string uid, string uname, string fullName)
        {
            this.userId = uid;
            this.userName = uname;
            this.fullName = fullName;
        }

        public string GetUserId()
        {
            return userId;
        }

        public string GetUserName()
        {
            return userName;
        }

        public string GetFullName()
        {
            return fullName;
        }

        public string SetUserId(string uid)
        {
            return userId = uid;
        }

        public string SetUserName(string uname)
        {
            return userName = uname;
        }

        public string SetFullName(string fullName)
        {
            return this.fullName = fullName;
        }
    }

    
}