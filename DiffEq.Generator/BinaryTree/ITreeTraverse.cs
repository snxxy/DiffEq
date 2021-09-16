using System;
using System.Collections.Generic;

namespace DiffEq.Generator.BinaryTree
{
    interface ITreeTraverse<T> where T : IComparable<T>
    {
        IEnumerator<T> Traversal(TreeNode<T> node);
    }
}