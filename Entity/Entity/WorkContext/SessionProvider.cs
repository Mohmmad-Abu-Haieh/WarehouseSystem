using System;
using System.Linq.Expressions;

namespace Entity.WorkContext
{
    public class SessionProvider
    {
        public LoggedInUser CurrentUser { get; private set; }
        public SessionProvider()
        {
            CurrentUser = new LoggedInUser();
        }
        public void InitialiseCurrentUser(LoggedInUser user)
        {
            CurrentUser = user;
        }
    }
}
