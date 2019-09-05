using System;
using System.Collections;
using System.Collections.Generic;

namespace Generator
{
    public class BinaryTree<T> : ICollection<T> where T : IComparable<T>
    {
        public ITreeTraverse<T> TraversalStrategy { get; set; } = new InOrderTree<T>();
        public TreeNode<T> Head { get; set; }

        private List<string> treeToString = new List<string>();

        public BinaryTree(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException("Capacity = 0");
            }

            IsFixedSize = true;
            Capacity = capacity;
        }

        public List<string> InOrder(TreeNode<T> root)
        {
            if (root.Left != null)
            {
                InOrder(root.Left);
            }
            treeToString.Add(root.Value.ToString());
            if (root.Right != null)
            {
                InOrder(root.Right);
            }
            return treeToString;
        }

        public int Count { get; private set; }

        public int Capacity { get; }

        public bool IsReadOnly => false;

        public bool IsFixedSize { get; }

        public void Add(T value)
        {
            if (IsFixedSize && Count >= Capacity)
            {
                throw new NotSupportedException($"Can not add more than {Capacity} items");
            }

            if (Head == null)
            {
                Head = new TreeNode<T>(value);
            }
            else
            {
                AddTo(Head, value);
            }
            Count++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            using (IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Add(enumerator.Current);
                }
            }
        }

        public bool Contains(T value)
        {
            return FindWithParent(value, out var _) != null;
        }

        public bool Remove(T value)
        {
            var current = FindWithParent(value, out var parent);

            if (current == null)
            {
                return false;
            }

            Count--;

            if (current.Right == null)
            {
                if (parent == null)
                {
                    Head = current.Left;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    Head = current.Right;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            else
            {
                var leftMost = current.Right.Left;
                var leftMostParent = current.Right;

                while (leftMost.Left != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }

                leftMostParent.Left = leftMost.Right;
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (parent == null)
                {
                    Head = leftMost;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = leftMost;
                    }
                    else if (result < 0)
                    {
                        parent.Right = leftMost;
                    }
                }
            }
            return true;
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException("Non zero lower bound");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException();
            }

            var items = TraversalStrategy.Traversal(Head);
            while (items.MoveNext())
            {
                array[arrayIndex++] = items.Current;
            }
        }
        private static void AddTo(TreeNode<T> node, T value)
        {
            if (value.Equals("tan") || value.Equals("(y)") || value.Equals("x") || value.Equals("y") || value.Equals("(x)") ||
                value.Equals("(y/x)") || value.Equals("+") || value.Equals("-") || value.Equals("/(") || value.Equals("*(") ||
                value.Equals("sin") || value.Equals("cos") || value.Equals("cot") || value.Equals("*x") || value.Equals("*y")
                || value.Equals("y/x") || value.Equals("*y/x") || value.Equals("*(y/x)"))
            {
                if (node.Left == null)
                {
                    node.Left = new TreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new TreeNode<T>(value);
                }
                else
                {
                    AddTo(node.Left, value);
                }
            }
        }

        private TreeNode<T> FindWithParent(T value, out TreeNode<T> parent)
        {
            var current = Head;
            parent = null;

            while (current != null)
            {
                var result = current.CompareTo(value);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return TraversalStrategy.Traversal(Head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}