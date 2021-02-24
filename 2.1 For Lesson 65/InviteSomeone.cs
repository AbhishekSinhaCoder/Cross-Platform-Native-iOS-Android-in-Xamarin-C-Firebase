using System;
using Firebase.Database;
using Foundation;
using UIKit;

namespace 
{
    public static class InviteSomeone
    {
        public static void Invite(UIViewController thisView,
                                  GroceryListClass toList,
                                  string inviteeEmail)
        {
            UserClass inviteeUser = null;
            String thisListName = toList.Name;

            AppData_iOS.UsersNode.ObserveSingleEvent(DataEventType.Value, (snapshot) =>
            {
                NSEnumerator children = snapshot.Children;
                var childSnapShot = children.NextObject() as DataSnapshot;

                while (childSnapShot != null)
                {
                    NSDictionary childDict = childSnapShot.GetValue<NSDictionary>();

                    if (childDict.ValueForKey((NSString)"Email").ToString() == inviteeEmail)
                    {
                        // user exist
                        inviteeUser = new UserClass
                        {
                            Name = childDict.ValueForKey((NSString)"Name").ToString(),
                            Email = childDict.ValueForKey((NSString)"Email").ToString(),
                            Uid = childDict.ValueForKey((NSString)"Uid").ToString()
                        };
                        break;
                    }
                    childSnapShot = children.NextObject() as DataSnapshot;
                }


                if (inviteeUser == null)
                {
                    AlertShow.Show(thisView,
                                    "No Such User",
                                    "Such user doesn't have an account with us");
                    return;
                }

                String invitationTitle = AppData.curUser.Uid + "|" + thisListName;

                object[] ownerKeys = { "Uid", "Email", "Name" };
                object[] ownerVals = { AppData.curUser.Uid, AppData.curUser.Email, AppData.curUser.Name };
                var ownerDict = NSDictionary.FromObjectsAndKeys(ownerVals, ownerKeys);

                object[] inviteeKeys = { "Name", "Owner" };
                object[] inviteeVals = { thisListName, ownerDict };
                var inviteeDict = NSDictionary.FromObjectsAndKeys(inviteeVals, inviteeKeys);

                DatabaseReference inviteeNode = AppData_iOS.UsersNode
                                                           .GetChild(inviteeUser.Uid)
                                                           .GetChild("Invitations")
                                                           .GetChild(invitationTitle);

                inviteeNode.SetValue<NSDictionary>(inviteeDict);

                AlertShow.Show(thisView,
                                "Invitation Sent",
                                "You have successfully invited " +
                               inviteeUser.Name +
                               " to this list.");
            });

        }
    }
}
