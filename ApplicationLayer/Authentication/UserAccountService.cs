using ApplicationLayer.Authentication;
using ModelClass;

namespace ApplicationLayer.Authentication
{
    public class UserAccountService
    {
        
            public ModelUserProfile? GetByUserName(string email, string pin)
            {
                return DataAccessLayer.DALUserAuth.Authenticate(email, pin);
            }   
       
    }
}
