namespace AaUS2
{
    public class AVLTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        public class AVLNode : BSTNode
        {
            public int Height { get; set; }

            public AVLNode(T data) : base(data) {}
        }

        public override void Remove(T value)
        {
            base.Remove(value);
            //rebalance tree
        }

        protected override BSTNode CreateNode(T value)
        {
            return new AVLNode(value);
        }

        protected override BSTNode InsertNode(T value)
        {
            var node = (AVLNode)base.InsertNode(value);
            //rebalance tree
            return node;
        }

        private void RotateLeft(BSTNode node)
        {
            var leftSon = node.Left;
            var parent = node.Parent;
            var grandparent = parent!.Parent;
            
            if (grandparent != null)
            {
                node.Parent = grandparent;
                if (IsLeftSon(parent))
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
                node.Parent = null;
            }

            parent.Right = leftSon;
            if (leftSon != null)
            {
                leftSon.Parent = parent;
            }

            node.Left = parent;
            parent.Parent = node;
        }

        private void RotateRight(BSTNode node)
        {
            var rightSon = node.Right;
            var parent = node.Parent;
            var grandparent = parent!.Parent;

            if (grandparent != null)
            {
                node.Parent = grandparent;
                if (IsLeftSon(parent))
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
                node.Parent = null;
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