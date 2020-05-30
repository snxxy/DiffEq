using System;

namespace DiffEq.BinaryTree
{
    class TreeNode<T> : IComparable<T> where T : IComparable<T>
    {
        public TreeNode(T value)
        {
            Value = value;
        }

        public TreeNode<T> Left { get; set; }

        public TreeNode<T> Right { get; set; }

        public T Value { get; }

        public int CompareNode(TreeNode<T> node)
        {
            return Value.CompareTo(node.Value);
        }

        public int CompareTo(T nodeValue)
        {
            return Value.CompareTo(nodeValue);
        }
    }
}