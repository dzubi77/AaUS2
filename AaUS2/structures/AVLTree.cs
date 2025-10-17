namespace AaUS2.structures
{
    public class AVLTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        public class AVLNode : BSTNode
        {
            public int Height { get; set; } = 1;
             
            public AVLNode(T data) : base(data) {}
        }

        public override void Remove(T value)
        {
            base.Remove(value);
            // store parent
            // rebalance tree starting from parent
        }

        protected override BSTNode CreateNode(T value)
        {
            return new AVLNode(value);
        }

        protected override BSTNode InsertNode(T value)
        {
            var node = (AVLNode)base.InsertNode(value);
            BalanceTree(node);
            return node;
        }

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

        private void BalanceTree(AVLNode? node)
        {
            var current = node;
            while (current != null)
            {
                var left = (AVLNode?)current.Left;
                var right = (AVLNode?)current.Right;
                int leftHeight = GetHeight(left);
                int rightHeight = GetHeight(right);
                current.Height = 1 + Math.Max(leftHeight, rightHeight);
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
                        RotateLeft(left);
                        RotateRight(current);
                    } 
                }
                else
                {
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
                            RotateRight(right);
                            RotateLeft(current);
                        }
                    }
                }
                current = (AVLNode?)current.Parent;
            }
        }

        private int GetHeight(AVLNode? node)
        {
            return node?.Height ?? 0;
        }

        private void UpdateHeight(AVLNode? node)
        {
            if (node == null) return;
            int leftHeight = GetHeight((AVLNode?)node.Left);
            int rightHeight = GetHeight((AVLNode?)node.Right);
            node.Height = 1 + Math.Max(leftHeight, rightHeight);
        }
    }
}