using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTestsAutoIt
{
    public class GroupHelper : HelperBase
    {
        public static string GroupEditorWinTitle = "Group editor";

        public GroupHelper(ApplicationManager manager) : base(manager)
        {

        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();
            OpenGroupsDialog();
            string count = aux.ControlTreeView(GroupEditorWinTitle, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GroupEditorWinTitle, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                    "GetText", "#0|#" + i, "");
                groups.Add(new GroupData()
                {
                    Name = item
                });
            }
            CloseGroupsDialog();
            return groups;
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialog();

            aux.ControlClick(GroupEditorWinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");

            CloseGroupsDialog();
        }

        public void OpenGroupsDialog()
        {
            aux.ControlClick(WinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GroupEditorWinTitle);
        }

        public void CloseGroupsDialog()
        {
            aux.ControlClick(GroupEditorWinTitle, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }
    }
}
