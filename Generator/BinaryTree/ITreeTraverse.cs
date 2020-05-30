using System;
using System.Collections.Generic;

namespace DiffEq.BinaryTree
{
    interface ITreeTraverse<T> where T : IComparable<T>
    {
        IEnumerator<T> Traversal(TreeNode<T> node);
    }
}