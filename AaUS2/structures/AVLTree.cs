namespace AaUS2.structures
{
    public class AVLTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        /**
         * Class to represent AVL node.
         */
        public class AVLNode : BSTNode
        {
            public int Height { get; set; } = 1;
             
            public AVLNode(T data) : base(data) {}
        }

        /**
         * Performs AVL remove operation.
         */
        public override void Remove(T value)
        {
            var nodeToRemove = FindNodeWithValue(value);
            var parent = (AVLNode?)RemoveNode(nodeToRemove);
            --Size;
            BalanceTree(parent);
        }

        /**
         * Returns new AVL node.
         */
        protected override BSTNode CreateNode(T value)
        {
            return new AVLNode(value);
        }

        /**
         * Performs AVL insert operation.
         */
        protected override BSTNode InsertNode(T value)
        {
            var node = (AVLNode)base.InsertNode(value);
            BalanceTree(node);
            return node;
        }

        /**
         * Performs simple left rotation. Customized to AVL with height recalculating.
         */
        private void RotateLeft(AVLNode? node)
        {
            if (node == null) return;

            var rightSon = (AVLNode?)node.Right;
            if (rightSon == null) return;

            var rightLeftSon = (AVLNode?)rightSon.Left;
            var parent = (AVLNode?)node.Parent;

            if (parent == null)
            {
                Root = rightSon;
                rightSon.Parent = null;
            }
            else
            {
                if (IsLeftSon(node))
                {
                    parent.Left = rightSon;
                }
                else
                {
                    parent.Right = rightSon;
                }
                rightSon.Parent = parent;
            }

            rightSon.Left = node;
            node.Parent = rightSon;

            node.Right = rightLeftSon;
            if (rightLeftSon != null)
            {
                rightLeftSon.Parent = node;
            }

            UpdateHeight(node);
            UpdateHeight(rightSon);
        }

        /**
         * Performs simple right rotation. Customized to AVL with height recalculating.
         */
        private void RotateRight(AVLNode? node)
        {
            if (node == null) return;

            var leftSon = (AVLNode?)node.Left;
            if (leftSon == null) return;

            var leftRightSon = (AVLNode?)leftSon.Right;
            var parent = (AVLNode?)node.Parent;

            if (parent == null)
            {
                Root = leftSon;
                leftSon.Parent = null;
            }
            else
            {
                if (IsLeftSon(node))
                {
                    parent.Left = leftSon;
                }
                else
                {
                    parent.Right = leftSon;
                }
                leftSon.Parent = parent;
            }

            leftSon.Right = node;
            node.Parent = leftSon;

            node.Left = leftRightSon;
            if (leftRightSon != null)
            {
                leftRightSon.Parent = node;
            }

            UpdateHeight(node);
            UpdateHeight(leftSon);
        }

        /**
         * Performs tree balance from given node. Used after insert and remove.
         */
        private void BalanceTree(AVLNode? node)
        {
            var current = node;
            while (current != null)
            {
                var left = (AVLNode?)current.Left;
                var right = (AVLNode?)current.Right;
                int leftHeight = GetHeight(left);
                int rightHeight = GetHeight(right); 
                UpdateHeight(current);
                int balance = leftHeight - rightHeight;

                if (left != null)
                {
                    int leftBF = GetHeight((AVLNode)left.Left) - GetHeight((AVLNode)left.Right);
                    
                    //case Left-Left
                    if (balance > 1 && leftBF >= 0)
                    {
                        RotateRight(current);
                    }
                    //case Left-Right
                    else if (balance > 1 && leftBF < 0)
                    {
                        RotateLeft((AVLNode?)current.Left);
                        RotateRight(current);
                    } 
                }
                if (right != null)
                {
                    int rightBF = GetHeight((AVLNode)right.Left) - GetHeight((AVLNode)right.Right);
                        
                    //case Right-Right
                    if (balance < -1 && rightBF <= 0)
                    {
                        RotateLeft(current);
                    }
                    //case Right-Left
                    else if (balance < -1 && rightBF > 0)
                    { 
                        RotateRight((AVLNode?)current.Right);
                        RotateLeft(current);
                    }
                }
                current = (AVLNode?)current.Parent;
            }
        }

        /**
         * Returns height of given node. If node is null, then returns 0.
         */
        private int GetHeight(AVLNode? node)
        {
            return node?.Height ?? 0;
        }

        /**
         * Updates height of given node.
         */
        private void UpdateHeight(AVLNode? node)
        {
            if (node == null) return;
            int leftHeight = GetHeight((AVLNode?)node.Left);
            int rightHeight = GetHeight((AVLNode?)node.Right);
            node.Height = 1 + Math.Max(leftHeight, rightHeight);
        }
    }
}