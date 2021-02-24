using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Foundation;

namespace Groceries.iOS
{
    public static class ReadInvitations
    {
		public static async Task Read()
		{
            AppData.invitationsData = new List<InvitationClass>();


            if (AppData_iOS.auth.CurrentUser == null)
                return;

            bool done = false;


            AppData_iOS.UsersNode
                   .GetChild(AppData.curUser.Uid)
                   .GetChild("Invitations")
                   .ObserveSingleEvent(DataEventType.Value, (snapshot) =>
			{
                if (!snapshot.HasChildren)
                    goto ThisIsDone;
                
                var allCoordinatesData = snapshot.GetValue<NSDictionary>();


                foreach (NSDictionary eachCoordAllVals in allCoordinatesData.Values)
                {
					NSString fetchedListName = (NSString)eachCoordAllVals.ValueForKey((NSString)"Name");

					var invitationOwner = (NSDictionary)eachCoordAllVals.ValueForKey((NSString)"Owner");

                    InvitationClass newInvitation = new InvitationClass
                    {
                        Name = fetchedListName,
                        Owner = new UserClass
                        {
                            Name = (NSString)invitationOwner.ValueForKey((NSString)"Name"),
                            Uid = (NSString)invitationOwner.ValueForKey((NSString)"Uid"),
                            Email = (NSString)invitationOwner.ValueForKey((NSString)"Email")
                        }
                    };

                    AppData.invitationsData.Add(newInvitation);
				}

                ThisIsDone:
				done = true;
			});

       

            while (!done)
			{
				await Task.Delay(50);
			}
		}
	}
}
