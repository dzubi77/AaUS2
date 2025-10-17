namespace AaUS2.structures
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
        public int Size { get; protected set; } = 0;

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
        public T Find(T value)
        {
            var node = FindNodeWithValue(value);
            return node != null ? node.Data : throw new ArgumentException("BST::find -> No such key!");
        }

        /**
         * Finds element with minimal key value.
         */
        public T FindMin()
        {
            var current = Root;
            if (current == null)
            {
                throw new Exception("BST is empty!");
            }
            else
            {
                while (current.Left != null)
                {
                    current = current.Left;
                }
                return current.Data;
            }
        }

        /**
         * Finds element with maximum key value.
         */
        public T FindMax()
        {
            var current = Root;
            if (current == null)
            {
                throw new Exception("BST is empty!");
            }
            else
            {
                while (current.Right != null)
                {
                    current = current.Right;
                }
                return current.Data;
            }
        }

        /**
         * Performs BST interval find.
         */
        public LinkedList<T> FindAll(T min, T max)
        {
            LinkedList<T> list = new LinkedList<T>();
            LinkedList<BSTNode> stack = new LinkedList<BSTNode>();
            var current = Root;
            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    // if current is within range, check left subtree, else check right subtree
                    if (current.Data.CompareTo(min) >= 0)
                    {
                        stack.AddLast(current);
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                if (stack.Count == 0) break;

                current = stack.Last!.Value;
                stack.RemoveLast();

                // add valid value to final list
                if (current.Data.CompareTo(min) >= 0 && current.Data.CompareTo(max) <= 0)
                {
                    list.AddLast(current.Data);
                }

                // if current value is less than max, check right subtree
                current = current.Data.CompareTo(max) < 0 ? current.Right : null;
            }

            return list;
        }

        /**
         * BST remove implementation. Subclasses would override it.
         */
        public virtual void Remove(T value)
        {
            var nodeToRemove = FindNodeWithValue(value);
            RemoveNode(nodeToRemove);
            Size--;
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

        /**
         * Creates new BST node. Subclass would create its required node type.
         */
        protected virtual BSTNode CreateNode(T value)
        {
            return new BSTNode(value);
        }

        /**
         * Performs standard BST insert. Subclass could override it. 
         */
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
            Size++;
            return node;
        }

        /**
         * Performs BST find, returning the node with searched value.
         */
        protected BSTNode FindNodeWithValue(T value)
        {
            var current = Root;
            while (current != null && value.CompareTo(current.Data) != 0)
            {
                int cmp = value.CompareTo(current.Data);
                current = cmp < 0 ? current.Left : current.Right;
            }

            return current ?? throw new ArgumentException("BST::findNodeWithValue -> No such key!");
        }

        /**
         * Removes node, based on its degree. Returns parent of removed node.
         */
        protected BSTNode? RemoveNode(BSTNode node)
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
            return parent;
        }

        /**
         * Checks if param node is left son.
         */
        protected bool IsLeftSon(BSTNode node)
        {
            var parent = node.Parent;
            return parent != null && parent.Left == node;
        }

        /**
         * Calculates degree of the param node.
         */
        private int Degree(BSTNode node)
        {
            int result = 0;
            if (node.Left != null) result++;
            if (node.Right != null) result++;
            return result;
        }
    }
}