namespace AaUS2
{
    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey> 
    {
        /**
         * Struct to keep information about the node of binary search tree.
         */
        public class BSTNode(TKey key, TValue value)
        {
            public TKey Key { get; set; } = key;
            public TValue Value { get; set; } = value; 

            public BSTNode? Parent { get; set; }
            public BSTNode? Left { get; set; }
            public BSTNode? Right { get; set; }
        }
        //-------

        public BSTNode? Root { get; set; }

        /**
         * Performs Insert operation, needed for subclasses with different node types.
         */
        public void Insert(TKey key, TValue value) { InsertNode(key, value); }

        /**
         * Performs BST find operation.
         */
        public BSTNode Find(TKey key)
        {
            if (Root == null)
            {
                throw new InvalidOperationException("T? Find -> BST is empty!");
            }
            var current = Root;
            // go until either leaf or find matching key
            while (current != null && key.CompareTo(current.Key) != 0)
            {
                current = key.CompareTo(current.Key) < 0 ? current.Left : current.Right;
            }

            return current ?? throw new ArgumentException("T? find -> No such key!");
        }

        /**
         * Performs BST remove operation (subclasses could override this).
         */
        public virtual TValue Remove(TKey key)
        {
            BSTNode node = Find(key);
            var result = node.Value; 
            var parent = node.Parent; 
            switch (Degree(node))
            {
                case 0:
                    // if node is root, just set to null
                    if (node == Root)
                    {
                        Root = null;
                        break;
                    }
                    if (parent != null)
                    {
                        // check if node was left or right son
                        if (node.Key.CompareTo(parent.Key) < 0)
                        {
                            parent.Left = null;
                        }
                        else
                        {
                            parent.Right = null;
                        }
                    }
                    break;
                case 1:
                    var son = node.Left ?? node.Right;
                    // if node is root, then son would be new root
                    if (node == Root)
                    {
                        Root = son;
                        break;
                    }
                    if (parent != null)
                    {
                        // check if parent would link with left or right son of node
                        if (node.Key.CompareTo(parent.Key) < 0)
                        {
                            node.Left = null;
                            parent.Left = son;
                        }
                        else
                        {
                            node.Right = null;
                            parent.Right = son;
                        }
                    }
                    // link son with parent
                    if (son != null)
                    {
                        son.Parent = parent;
                    }
                    break;
                case 2:
                    // find prevInOrder
                    // copy prevInOrder data to node
                    // remove prevInOrder normally
                    BSTNode prevInOrder = node.Right!;
                    while (prevInOrder.Left != null)
                    {
                        prevInOrder = prevInOrder.Left;
                    }
                    node.Key = prevInOrder.Key;
                    node.Value = prevInOrder.Value;
                    Remove(prevInOrder.Key);
                    break;
            }
            return result;
        }

        /**
         * Virtual operation to create correct node type (used for Insert()). 
         */
        protected virtual BSTNode CreateNode(TKey key, TValue value)
        {
            return new BSTNode(key, value);
        }

        /**
         * Base implementation of BST insert (subclasses could override this). 
         */
        protected virtual BSTNode InsertNode(TKey key, TValue value)
        {
            BSTNode newNode = CreateNode(key, value);
            if (Root != null)
            {
                var parent = FindParent(key);
                if (parent == null) throw new ArgumentException("Tree already contains such key!");
                newNode.Parent = parent;
                if (key.CompareTo(parent.Key) < 0)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
            }
            else
            {
                Root = newNode;
            }
            return newNode;
        }

        /**
         * Operation to find parent of newly added node before insertion.
         */
        public BSTNode? FindParent(TKey key)
        {
            BSTNode? parent = null;
            var current = Root; 
            while (current != null)
            {
                parent = current;
                if (key.CompareTo(current.Key) < 0)
                {
                    current = current.Left;
                }
                else if (key.CompareTo(current.Key) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    // or return null;
                    throw new ArgumentException("BSTNode<K, T>?::FindParent -> BST already contains such key!");
                }
            }
            return parent;
        }

        /**
         * Aside operation to get the degree of the node. 
         */
        protected int Degree(BSTNode node)
        {
            int result = 0;
            if (node.Left != null)
            {
                ++result;
            }
            if (node.Right != null)
            {
                ++result;
            }
            return result;
        }
    }
}