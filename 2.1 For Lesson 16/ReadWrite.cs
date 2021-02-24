using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace
{
    public static class ReadWrite
    {
        static readonly string mainPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        static readonly string dataPath = Path.Combine(mainPath, "data.json");
        static readonly string userPath = Path.Combine(mainPath, "user.json");


        public static void WriteData ()
        {
            AppData.offlineLists = new List<GroceryListClass>();

            if (AppData.currentLists != null)
            foreach (GroceryListClass any in AppData.currentLists)
                if (any.Owner.Uid == AppData.curUser.Uid)
                    AppData.offlineLists.Add(any);

            string dataJson = JsonConvert.SerializeObject(AppData.offlineLists, Formatting.Indented);
            File.WriteAllText(dataPath, dataJson);
        }

        public static void ReadData ()
        {
            AppData.offlineLists = new List<GroceryListClass>();

            if (File.Exists(dataPath))
            {
                using (StreamReader file = File.OpenText(dataPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    AppData.offlineLists = (List<GroceryListClass>)serializer.Deserialize(file,
                                                                                         typeof(List<GroceryListClass>));
                }
            }
        }

        public static void WriteUser ()
        {
            string userJson = JsonConvert.SerializeObject(AppData.curUser, Formatting.Indented);
            File.WriteAllText(userPath, userJson); 
        }


        public static void ReadUser()
        {
            if (File.Exists(userPath))
            {
                using (StreamReader file = File.OpenText(userPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    AppData.curUser = (UserClass)serializer.Deserialize(file,
                                                                        typeof(UserClass));
                }
            }
        }

    }
}
