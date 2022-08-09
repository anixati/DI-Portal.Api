using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrmDataExtractor
{

    public static class DataTasks
    {
        private static readonly Store Data = new Store();
        private static CrmContext _cx = null;
        public static void Execute()
        {
            _cx = CrmContext.Create();
            LoadOptions();
            LoadBoards();
            Data.Save();
        }

        private static void LoadBoards()
        {
            Data.Boards = _cx.GetEntity("new_board");
            Data.Roles = _cx.GetEntity("new_role");
            Data.Appointments = _cx.GetEntity("new_appointment");
            Data.Contacts = _cx.GetEntity("contact");
            Data.Users = _cx.GetEntity("systemuser");


        }

        private static void LoadOptions()
        {
            var gops = _cx.GetGlobalOptionSets();
            foreach (var os in Config.gos)
            {
                var md = gops.FirstOrDefault(x => x.Name == os.Value);
                if (md != null)
                {
                    var ls = new Dictionary<string, string>();
                    foreach (var x in md.Options)
                    {
                        var key = x.Label.UserLocalizedLabel.Label;
                        ls[key] = $"{x.Value}";
                    }
                    Data.Options[os.Key] = ls;
                }
                else
                {
                    Console.WriteLine($"  {os.Value} not found");
                }
            }
            foreach (var os in Config.los)
            {
                var vals = os.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var ops = _cx.GetEntityOptionSets(vals[0], vals[1]);
                Data.Options[os.Key] = ops;
            }
        }
    }


}
