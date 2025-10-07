namespace AaUS2
{
    public class AVLTree<TKey, TValue> : BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        public class AVLNode(TKey key, TValue value) : BSTNode(key, value)
        {
            public int Height { get; set; } = 1;
        }

        /**
         * Returns new AVL node.
         */
        protected override BSTNode CreateNode(TKey key, TValue value)
        {
            return new AVLNode(key, value);
        }

        /**
         * AVL implementation of Insert - classic BST Insert plus rebalance.
         */
        protected override BSTNode InsertNode(TKey key, TValue value)
        {
            var node = (AVLNode)base.InsertNode(key, value);
            //rebalance
            return node; 
        }

        /**
         * AVL implementation of Remove operation.
         */
        public override TValue Remove(TKey key)
        {
            return base.Remove(key);
        }

        /**
         * Performs left rotation of the node.
         */
        protected void RotateLeft(AVLNode node)
        {
            var parent = node.Parent;
            var grandparent = parent?.Parent;
            var left = node.Left;
            if (grandparent != null) // non-root case
            {
                if (grandparent.Left == parent)
                {
                    grandparent.Left = node;
                }
                else
                {
                    grandparent.Right = node;
                }
            }
            else
            {
                Root = node;
                Root.Parent = null;
            }
            parent.Right = left;
            node.Left = parent;

            left.Parent = parent;
            parent.Parent = node;
            node.Parent = grandparent;
        }

        /**
         * Performs right rotation of the node.
         */
        protected void RotateRight(AVLNode node)
        {

        }
    }
}
