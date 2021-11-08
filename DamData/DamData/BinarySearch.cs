using System;

namespace DamData
{
    class Node
    {
        public Dam value;
        public Node left;
        public Node right;
    }

    class Tree
    {
        public Dam _currentBstDam;
        public Node Insert(Node root, Dam damObject)
        {
            if (root == null)
            {
                root = new Node();
                root.value = damObject;
            }
            //add ascii comparison for first letter of damName
            else if (String.Compare(damObject.GetName(), root.value.GetName()) < 0)
            {
                root.left = Insert(root.left, damObject);
            }
            else
            {
                root.right = Insert(root.right, damObject);
            }
            return root;
        }

        public void Traverse(Node root, string damName)
        {
            DataRetriever._opCount++;
            if (root == null)
            {
                return;
            }
            else if (String.Equals(damName, root.value.GetName(), StringComparison.OrdinalIgnoreCase))
            {
                _currentBstDam = root.value;
                return;
            }
            else if (String.Compare(damName, root.value.GetName()) < 0)
                Traverse(root.left, damName);
            else
                Traverse(root.right, damName);
        }
    }
}
