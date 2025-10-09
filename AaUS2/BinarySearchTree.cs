namespace AaUS2
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        /**
         * BST Node struct.
         */
        public class BSTNode(T data)
        {
            public T Data { get; set; } = data;

            public BSTNode? Parent { get; set; }
            public BSTNode? Left { get; set; }
            public BSTNode? Right { get; set; }
        }

        public BSTNode? Root { get; set; } = null;
        public int Size { get; private set; } = 0;

        /**
         * Public operation to call insert.
         */
        public void Insert(T data)
        {
            InsertNode(data);
        }

        /**
         * BST Find operation implementation.
         * Returns element by key or throws an exception if key does not exist.
         */
        public BSTNode Find(T key) 
        {
            var current = Root;
            while (current != null && key.CompareTo(current.Data) != 0)
            {
                if (key.CompareTo(current.Data) < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return current ?? throw new ArgumentException("BST::Find -> No such key!");
        }

        /**
         * General BST remove implementation.
         * Throws an exception if key does not exist.
         */
        public virtual void Remove(T key)
        { 
            var nodeToRemove = Find(key);
            var parent = nodeToRemove.Parent;
            switch (Degree(nodeToRemove))
            {
                case 0:
                    if (IsRoot(nodeToRemove))
                    {
                        Root = null;
                    }
                    else if (IsLeftSon(nodeToRemove))
                    {
                        parent!.Left = null;
                    }
                    else
                    {
                        parent!.Right = null;
                    }
                    break;
                case 1:
                    var son = nodeToRemove.Left ?? nodeToRemove.Right;
                    if (IsRoot(nodeToRemove))
                    {
                        Root = son;
                        son!.Parent = null;
                        break;
                    }
                    
                    if (IsLeftSon(nodeToRemove))
                    {
                        parent!.Left = son;
                    }
                    else
                    {
                        parent!.Right = son;
                    }

                    son!.Parent = parent;
                    nodeToRemove.Parent = null;
                    nodeToRemove.Left = null;
                    nodeToRemove.Right = null;

                    break;
                case 2:
                    // find prevInOrder
                    // switch data
                    // remove node normally (now either case 0 or case 1)
                    var current = nodeToRemove.Left;
                    BSTNode? nextInOrder = null;
                    while (current != null)
                    {
                        nextInOrder = current;
                        current = current.Right;
                    }

                    var tmp = nextInOrder!.Data;
                    nextInOrder.Data = nodeToRemove.Data;
                    nodeToRemove.Data = tmp;

                    Remove(nextInOrder.Data);
                    break;
            }
        } 

        /**
         * Operation to do InOrder traversal from certain node.
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
         * Operation to do LevelOrder traversal from param node.
         */
        public void ProcessLevelOrder(BSTNode node, Action<BSTNode> operation)
        {
            LinkedList<BSTNode> list = new LinkedList<BSTNode>();
            list.AddFirst(node);
            while (list.Count > 0)
            {
                BSTNode current = list.First.Value;
                operation(current);
                list.RemoveFirst();
                var leftSon = current.Left;
                var rightSon = current.Right;
                if (leftSon != null) list.AddLast(leftSon);
                if (rightSon != null) list.AddLast(rightSon);
            }
        }

        /**
         * General BST insertion. Could be void :D.
         */
        protected virtual BSTNode InsertNode(T data)
        {
            var newNode = CreateNode(data);
            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                var current = Root;
                BSTNode? parent = null;
                while (current != null)
                {
                    parent = current;
                    var cmpResult = data.CompareTo(current.Data);
                    if (cmpResult < 0)
                    {
                        current = current.Left;
                    }
                    else if (cmpResult > 0)
                    {
                        current = current.Right;
                    }
                    else
                    {
                        throw new ArgumentException("BST::Insert -> Table already contains such key!");
                    }
                }
                newNode.Parent = parent;
                if (data.CompareTo(parent!.Data) < 0)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
            }
            return newNode;
        }

        /**
         * Returns newly created node.
         * Virtual to be overriden by subclasses (to return correct node type).
         */
        protected virtual BSTNode CreateNode(T data)
        {
            return new BSTNode(data);
        }

        /**
         * Checks if node is left son.
         */
        private bool IsLeftSon(BSTNode node)
        {
            var parent = node.Parent;
            return parent != null && parent.Left == node;
        }

        /**
         * Checks if node is right son.
         */
        private bool IsRightSon(BSTNode node)
        {
            var parent = node.Parent;
            return parent != null && parent.Right == node;
        }

        /**
         * Checks if node is a root.
         */
        private bool IsRoot(BSTNode node)
        {
            return node == Root;
        }

        /**
         * Returns degree of the param node.
         */
        private int Degree(BSTNode node)
        {
            int result = 0;
            if (node.Left != null) ++result;
            if (node.Right != null) ++result;
            return result;
        }
    }
}
