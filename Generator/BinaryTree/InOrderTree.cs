using System;
using System.Collections.Generic;

namespace DiffEq.BinaryTree
{
    class InOrderTree<T> : ITreeTraverse<T> where T : IComparable<T>
    {
        public IEnumerator<T> Traversal(TreeNode<T> node)
        {
            var stack = new Stack<TreeNode<T>>();

            while (stack.Count > 0 || node != null)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }
                else
                {
                    node = stack.Pop();
                    yield return node.Value;
                    node = node.Right;
                }
            }
        }
    }
}