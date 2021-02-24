using System;
using Firebase.Database.Query;

namespace 
{
    public static class RemoveInvitation
    {
        public static void Remove(InvitationClass inpInvite)
        {
            if (AppData_Droid.auth.CurrentUser == null)
                return;
            
            string Name = inpInvite.Name;
            string ownerUid = inpInvite.Owner.Uid;
            String invitationTitle = ownerUid + "|" + Name;

            var inviteNode = AppData_Droid.usersNode
                                    .Child(AppData.curUser.Uid)
                                    .Child("Invitations")
                                    .Child(invitationTitle);

            inviteNode.DeleteAsync();
        }
    }
}
