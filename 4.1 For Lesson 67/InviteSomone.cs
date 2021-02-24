using System;
using System.Threading.Tasks;
using Android.App;
using Firebase.Database;
using Firebase.Database.Query;

namespace Groceries.Droid
{
    public static class InviteSomone
    {
        public static async Task Invite(Activity thisActivity,
                                        GroceryListClass toList, 
                                        string inpEmailAddress)
        {
            UserClass inviteeUser = null;
            UserClass ownerUser = toList.Owner;

            string thisName = toList.Name;

            var allUsersData = await AppData_Droid.usersNode.OnceAsync<UserClass>();

            foreach (FirebaseObject<UserClass> any in allUsersData)
            {
                if (inpEmailAddress == any.Object.Email)
                {
                    inviteeUser = any.Object;
                    goto UserExists;
                }
            }

            AlertShow.Show(thisActivity, "No Such User", "We couldn't find this user!");
			return;



        UserExists:
            string invitationTitle = ownerUser.Uid + "|" + thisName;

            InvitationClass thisInvite = new InvitationClass()
            {
                Name = thisName,
                Owner = ownerUser
            };


            await AppData_Droid.usersNode
                         .Child(inviteeUser.Uid)
                         .Child("Invitations")
                         .Child(invitationTitle)
                         .PutAsync(thisInvite);

            AlertShow.Show(thisActivity, 
                           "Invitation Sent",
                           "You have successfully invited " + inviteeUser.Name + " to this list.");
        }
    }
}
