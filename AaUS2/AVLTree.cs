namespace AaUS2
{
    public class AVLTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        /**
         * Struct to represent AVL tree node.
         */
        public class AVLNode(T data) : BSTNode(data)
        {
            public int Height { get; set; }
        }

        /**
         * AVL implementation of remove operation.
         */
        public override void Remove(T key)
        {
            base.Remove(key);
            BalanceTree();
        }

        /**
         * Returns newly created AVL node.
         */
        protected override BSTNode CreateNode(T data)
        {
            return new AVLNode(data); 
        }

        /**
         * AVL implementation of insert operation.
         */
        protected override BSTNode InsertNode(T data)
        {
            var node = (AVLNode)base.InsertNode(data);
            BalanceTree();
            return node;
        }

        /**
         * Operation to balance tree if needed.
         */
        private void BalanceTree()
        {
            //start by parent of inserted/removed node
            //climb up until root
            //if bf > +- 2, perform needed rotation(s)
        }

        /**
         * Performs left rotation.
         */
        private void RotateLeft(AVLNode node)
        {
            var leftSon = node.Left;
            var parent = node.Parent;
            var grandparent = parent!.Parent;
            if (grandparent != null)
            {
                if (grandparent.Left == parent)
                {
                    grandparent.Left = node;
                    node.Parent = grandparent;
                }
                else
                {
                    grandparent.Right = node;
                    node.Parent = grandparent;  
                }
            }
            parent.Right = leftSon;
            if (leftSon != null)
            {
                leftSon.Parent = parent;
            }
            node.Left = parent;
            parent.Parent = node;
        }

        /**
         * Performs right rotation.
         */
        private void RotateRight(AVLNode node)
        {
            var rightSon = node.Right;
            var parent = node.Parent;
            var grandparent = parent!.Parent;
            if (grandparent != null)
            {
                if (grandparent.Left == parent)
                {
                    grandparent.Left = node;
                    node.Parent = grandparent;
                }
                else
                {
                    grandparent.Right = node;
                    node.Parent = grandparent;
                }
            }
            parent.Left = rightSon;
            if (rightSon != null)
            {
                rightSon.Parent = parent;
            }
            node.Right = parent;
            parent.Parent = node;
        }
    }
}
