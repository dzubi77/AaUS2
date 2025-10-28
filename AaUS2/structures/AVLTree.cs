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

            public AVLNode(T data) : base(data)
            {
            }
        }

        /**
         * Performs AVL remove operation.
         */
        public override void Remove(T value)
        {
            var nodeToRemove = FindNodeWithValue(value);
            var parent = (AVLNode?)RemoveNode(nodeToRemove);
            --Size;
            BalanceTreeAfterRemove(parent);
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
            BalanceTreeAfterInsert(node);
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

            node.Height = 1 + Math.Max((node.Left as AVLNode)?.Height ?? 0, (node.Right as AVLNode)?.Height ?? 0);
            rightSon.Height = 1 + Math.Max((rightSon.Left as AVLNode)?.Height ?? 0,
                (rightSon.Right as AVLNode)?.Height ?? 0);
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

            node.Height = 1 + Math.Max((node.Left as AVLNode)?.Height ?? 0, (node.Right as AVLNode)?.Height ?? 0);
            leftSon.Height = 1 + Math.Max((leftSon.Left as AVLNode)?.Height ?? 0,
                (leftSon.Right as AVLNode)?.Height ?? 0);
        }

        /**
         * Performs tree balance from given node after insert.
         */
        private void BalanceTreeAfterInsert(AVLNode? node)
        {
            var current = node?.Parent as AVLNode;
            while (current != null) 
            {
                var left = current.Left as AVLNode;
                var right = current.Right as AVLNode;
                int leftHeight = left?.Height ?? 0;
                int rightHeight = right?.Height ?? 0;

                current.Height = 1 + Math.Max(leftHeight, rightHeight);
                int balance = leftHeight - rightHeight;

                if (balance > 1 && left != null)
                {
                    var leftLeft = left.Left as AVLNode;
                    var leftRight = left.Right as AVLNode;
                    int leftBalance = (leftLeft?.Height ?? 0) - (leftRight?.Height ?? 0);

                    // case LL
                    if (leftBalance >= 0)
                    {
                        RotateRight(current);
                    }
                    // case LR
                    else
                    {
                        RotateLeft(left);
                        RotateRight(current);
                    }
                }

                else if (balance < -1 && right != null)
                {
                    var rightLeft = right.Left as AVLNode;
                    var rightRight = right.Right as AVLNode;
                    int rightBalance = (rightLeft?.Height ?? 0) - (rightRight?.Height ?? 0);

                    // case RR
                    if (rightBalance <= 0)
                    {
                        RotateLeft(current);
                    }
                    // case RL
                    else
                    {
                        RotateRight(right);
                        RotateLeft(current);
                    }
                }

                current = current.Parent as AVLNode;
            }
        }

        /**
         * Performs tree balance from given node after remove.
         */
        private void BalanceTreeAfterRemove(AVLNode? node)
        {
            var current = node;
            while (current != null)
            {
                var left = current.Left as AVLNode;
                var right = current.Right as AVLNode;
                int leftHeight = left?.Height ?? 0;
                int rightHeight = right?.Height ?? 0;

                current.Height = 1 + Math.Max(leftHeight, rightHeight);
                int balance = leftHeight - rightHeight;

                if (balance > 1 && left != null)
                {
                    var leftLeft = left.Left as AVLNode;
                    var leftRight = left.Right as AVLNode;
                    int leftBalance = (leftLeft?.Height ?? 0) - (leftRight?.Height ?? 0);

                    // case LL
                    if (leftBalance >= 0) 
                    {
                        RotateRight(current); 
                    }       
                    // case LR
                    else
                    {
                        RotateLeft(left);
                        RotateRight(current);
                    }
                }

                else if (balance < -1 && right != null)
                {
                    var rightLeft = right.Left as AVLNode;
                    var rightRight = right.Right as AVLNode;
                    int rightBalance = (rightLeft?.Height ?? 0) - (rightRight?.Height ?? 0);

                    // case RR
                    if (rightBalance <= 0)  
                    {
                        RotateLeft(current);
                    }
                    // case RL
                    else
                    {
                        RotateRight(right); 
                        RotateLeft(current);
                    }
                }

                current = current.Parent as AVLNode;
            }
        }
    }
}