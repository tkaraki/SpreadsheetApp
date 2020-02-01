using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    public class BST
    {

        public class Node
        {
            public int value;
            public Node Left;
            public Node Right;
        }

        
        Node BSTroot;



        /* Constructor */
        public BST()
        {
            BSTroot = null;
        }


        /* Insert an item into BST */
        public void Add(int val)
        {
            if (BSTroot == null)
            {
                Node NewNode = new Node();
                NewNode.value = val;
                BSTroot = NewNode;
            }
            else
            {
                AddHelper(BSTroot, val);
            }
        }


        /* Print Contents of BST in order from least to greatest */
        public void Print()
        {
            Console.Write("\t");
            printInOrder(BSTroot);
        }


        /* Return the number of nodes in the BST */
        public int NodeCount()
        {
            return NodeCountHelper(BSTroot);
        }



        /* Return the number of levels in the BST*/
        public int BSTLevels()
        {
            return BSTLevelsHelper(BSTroot);
        }



        /* Return the theoretical minimum levels of the BST */
        public double MinLevels()
        {
            double nodeNo = NodeCount();
            return 1 + Math.Floor(Math.Log(nodeNo, 2.0));
        }



        public bool isEmpty()
        {
            return BSTroot == null;
        }

        /* Helper Functions */

        /* Helper function to insert nodes into tree recursively. */
        void AddHelper(Node root, int val)
        {

            if (root.value > val)
            {
                if (root.Left == null)
                {
                    Node NewNode = new Node();
                    NewNode.value = val;
                    root.Left = NewNode;
                }
                else
                {
                    AddHelper(root.Left, val);
                }
            }

            else
            {
                if (root.Right == null)
                {
                    Node NewNode = new Node();
                    NewNode.value = val;
                    root.Right = NewNode;
                }
                else
                {
                    AddHelper(root.Right, val);
                }
            }
        }



        /* Helper function to print items of tree using inorder traversal */
        void printInOrder(Node root)
        {
            if (root == null)
                return;

            printInOrder(root.Left);
            Console.Write(root.value + " ");
            printInOrder(root.Right);
        }



        /* Helper function to return nodes count of tree */
        int NodeCountHelper(Node root)
        {
            if (root == null)
                return 0;
            else
                return (1 + NodeCountHelper(root.Left) + NodeCountHelper(root.Right));
        }



        /* Helper function to return levels of tree */
        int BSTLevelsHelper(Node root)
        {
            if (root == null)
                return 0;
            else
                return 1 + Math.Max(BSTLevelsHelper(root.Left), BSTLevelsHelper(root.Right));

        }



    }
}
