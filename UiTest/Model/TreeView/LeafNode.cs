using System;
using System.Windows.Input;
using UiTest.Service.Relay;

namespace UiTest.Model.TreeView
{
    public class LeafNode : TreeNode
    {
        public ICommand OpenCommand { get; }

        public LeafNode(Action<LeafNode> onOpen)
        {
            OpenCommand = new RelayCommand(_ => onOpen(this));
        }
    }
}
