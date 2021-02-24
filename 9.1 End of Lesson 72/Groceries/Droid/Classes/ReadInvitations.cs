using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace Groceries.Droid
{
    public static class ReadInvitations
    {
        public static async Task Read()
        {            
            AppData.invitationsData = new List<InvitationClass>();

            if (AppData_Droid.auth.CurrentUser == null)
                return;
            
            var allCoordinatesData = await AppData_Droid.usersNode
                .Child(AppData.curUser.Uid)
                .Child("Invitations")
                .OnceAsync<InvitationClass>();

            foreach (FirebaseObject<InvitationClass> anyInvite in allCoordinatesData)
            {
                AppData.invitationsData.Add(anyInvite.Object);
            }
        }
    }
}
