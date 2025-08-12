using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using UiTest.Model.TreeView;
using UiTest.Service.Relay;

namespace UiTest.ModelView.Component
{
    public class TreeViewModelView : BaseModelView
    {
        public ObservableCollection<TreeNode> TreeItems { get; private set; }
        public TreeViewModelView()
        {
            TreeItems = new ObservableCollection<TreeNode>();
        }
        public void BuildTree(IEnumerable<Type> types)
        {
            TreeItems.Clear();
            foreach (var type in types)
            {
                if (string.IsNullOrEmpty(type.Namespace)) continue;
                var parts = type.Namespace.Split('.');
                ObservableCollection<TreeNode> currentLevel = TreeItems;
                TreeNode currentNode = null;
                foreach (var part in parts)
                {
                    var existing = currentLevel.OfType<TreeNode>().FirstOrDefault(n => n.Name == part);
                    if (existing == null)
                    {
                        existing = new TreeNode { Name = part};
                        currentLevel.Add(existing);
                    }
                    currentNode = existing;
                    currentLevel = currentNode.Children;
                }
                currentLevel.Add(new LeafNode(OnOpenClass) { Name = type.Name});
            }
        }

        private void OnOpenClass(LeafNode node)
        {
            System.Windows.MessageBox.Show($"Open class: {node.Name}");
        }
    }
}
