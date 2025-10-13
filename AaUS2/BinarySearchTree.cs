namespace AaUS2
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        /**
         * Class to represent BST node.
         */
        public class BSTNode
        {
            public T Data { get; set; } 
            public BSTNode? Parent { get; set; }
            public BSTNode? Left { get; set; }
            public BSTNode? Right { get; set; }

            public BSTNode(T data)
            {
                Data = data;
            }
        }

        public BSTNode? Root { get; set; } = null; 
        public int Size { get; private set; } = 0;

        /**
         * General call for BST insert. 
         */
        public void Insert(T value)
        {
            InsertNode(value);
        }

        /**
         * BST find operation implementation.
         */
        public BSTNode Find(T value)
        {
            var current = Root;
            while (current != null && value.CompareTo(current.Data) != 0)
            {
                int cmp = value.CompareTo(current.Data);
                if (cmp < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return current ?? throw new ArgumentException("BST::find -> No such key!");
        }

        /**
         * BST remove implementation. Subclasses would override it.
         */
        public virtual void Remove(T value)
        {
            var nodeToRemove = Find(value);
            RemoveNode(nodeToRemove);
        }

        /**
         * Performs inOrder traversal.
         */
        public void ProcessInOrder(BSTNode? node, Action<BSTNode> operation)
        {
            if (node != null)
            {
                ProcessInOrder(node.Left, operation);
                operation(node);
                ProcessInOrder(node.Right, operation);
            }
        }

        /**
         * Performs levelOrder traversal.
         */
        public void ProcessLevelOrder(BSTNode? node, Action<BSTNode> operation)
        {
            if (node != null)
            {
                LinkedList<BSTNode> list = new LinkedList<BSTNode>();
                list.AddFirst(node);
                while (list.Count > 0)
                {
                    BSTNode current = list.First!.Value;
                    list.RemoveFirst();
                    operation(current);
                    if (current.Left != null) list.AddLast(current.Left);
                    if (current.Right != null) list.AddLast(current.Right);
                }
            }
        }

        protected virtual BSTNode CreateNode(T value)
        {
            return new BSTNode(value);
        }

        protected virtual BSTNode InsertNode(T value)
        {
            BSTNode node = CreateNode(value);
            if (Root != null)
            {
                // find parent
                BSTNode? current = Root;
                BSTNode? parent = null;
                while (current != null)
                {
                    parent = current;
                    var cmp = value.CompareTo(parent.Data);
                    if (cmp < 0)
                    {
                        current = current.Left;
                    }
                    else if (cmp > 0)
                    {
                        current = current.Right;
                    }
                    else
                    {
                        throw new ArgumentException("BST::insert -> Duplicate key!");
                    }
                }
                // assign relationships
                node.Parent = parent;
                if (value.CompareTo(parent!.Data) < 0)
                {
                    parent.Left = node;
                }
                else
                {
                    parent.Right = node;
                }
            }
            else
            {
                Root = node;
            }
            ++Size;
            return node;
        }

        protected bool IsLeftSon(BSTNode node)
        {
            var parent = node.Parent;
            return parent != null && parent.Left == node;
        }

        private int Degree(BSTNode node)
        {
            int result = 0;
            if (node.Left != null) result++;
            if (node.Right != null) result++;
            return result;
        }

        private void RemoveNode(BSTNode node)
        {
            var parent = node.Parent;
            switch (Degree(node))
            {
                case 0:
                    // if root, set root to null
                    // else remove connections parent-son
                    if (parent == null)
                    {
                        Root = null;
                    }
                    else
                    {
                        if (IsLeftSon(node))
                        {
                            parent.Left = null;
                        }
                        else
                        {
                            parent.Right = null;
                        }
                        node.Parent = null;
                    }
                    break;
                case 1:
                    // if root, son would be new root
                    // else connect node's parent with node's son
                    var son = node.Left ?? node.Right;
                    if (parent == null)
                    {
                        Root = son;
                        son!.Parent = null;
                    }
                    else
                    {
                        if (IsLeftSon(node))
                        {
                            parent.Left = son;
                        }
                        else
                        {
                            parent.Right = son;
                        }
                        son!.Parent = parent;
                        node.Parent = null;
                    }
                    break;
                case 2:
                    // find prevInOrder
                    // swap data
                    // remove normally (degree would be 0 or 1)
                    var prevInOrder = node.Left;
                    while (prevInOrder!.Right != null)
                    {
                        prevInOrder = prevInOrder.Right;
                    }

                    (prevInOrder.Data, node.Data) = (node.Data, prevInOrder.Data);

                    RemoveNode(prevInOrder);
                    break;
            }
        }
    }
}