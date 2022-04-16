using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;

namespace AddressbookTestsWhite
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
            Window dialog = OpenGroupsDialog();
            Tree tree = dialog.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode node in root.Nodes)
            {
                groups.Add(new GroupData()
                {
                    Name = node.Text
                });
            }
            
            CloseGroupsDialog(dialog);
            return groups;
        }

        public int GetGroupsCount()
        {
            Window dialog = OpenGroupsDialog();
            int count = dialog.Get<Tree>("uxAddressTreeView").Nodes[0].Nodes.Count();
            CloseGroupsDialog(dialog);
            return count;
        }

        public void CreateGroupIfDoesNotExists()
        {
            if (GetGroupsCount() == 1)
            {
                CreateGroup(new GroupData("newGroupName"));
            }
        }

        public Window SelectGroupByName(GroupData group)
        {
            List<GroupData> groups = new List<GroupData>();
            Window dialog = OpenGroupsDialog();
            Tree tree = dialog.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode node in root.Nodes)
            {
                if (node.Text == group.Name)
                {
                    node.Click();
                    return dialog;
                }
            }
            root.Nodes[0].Click();
            return dialog;
        }

        public void RemoveGroup(GroupData groupToRemove)
        {
            Window dialog = SelectGroupByName(groupToRemove);
            dialog.Get<Button>("uxDeleteAddressButton").Click();

            Window deleteDialog = dialog.ModalWindow("Delete group");
            deleteDialog.Get<Button>("uxOKAddressButton").Click();
            CloseGroupsDialog(dialog);
        }

        public void RemoveLastGroup(GroupData groupToRemove)
        {
            Window dialog = SelectGroupByName(groupToRemove);
            dialog.Get<Button>("uxDeleteAddressButton").Click();

            Window deleteDialog = dialog.MessageBox("Information");
            Button okButton = (Button) deleteDialog.Get(SearchCriteria.ByControlType(ControlType.Button));
            okButton.Click();
            CloseGroupsDialog(dialog);
        }

        public void CreateGroup(GroupData newGroup)
        {
            Window dialog = OpenGroupsDialog();

            dialog.Get<Button>("uxNewAddressButton").Click();
            TextBox textBox = (TextBox) dialog.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.Enter(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseGroupsDialog(dialog);
        }

        public Window OpenGroupsDialog()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GroupEditorWinTitle);
        }

        public void CloseGroupsDialog(Window dialog)
        {
            dialog.Get<Button>("uxCloseAddressButton").Click();
        }
    }
}
