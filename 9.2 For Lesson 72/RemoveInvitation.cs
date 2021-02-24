using System;
using Firebase.Database;

namespace 
{
    public static class RemoveInvitation
    {
        public static void Remove(InvitationClass inpInvi)
        {
            if (AppData_iOS.auth.CurrentUser == null)
                return;

            string invitationTitle = inpInvi.Owner.Uid + "|" + inpInvi.Name;

            DatabaseReference invNode = AppData_iOS.UsersNode
                                               .GetChild(AppData.curUser.Uid)
                                               .GetChild("Invitations")
                                               .GetChild(invitationTitle);
            invNode.RemoveValue();
        }
    }
}
