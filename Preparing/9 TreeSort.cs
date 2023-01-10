using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public static class TreeSort
    {
        // n log n средний
        // n^2 худший
        public class TreeNode
        {
            public int Data { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode(int data)
            {
                Data = data;
            }

            public void Insert(TreeNode node)
            {
                if (node.Data <= Data)
                {
                    if (Left == null) Left = node;
                    else Left.Insert(node);
                }
                else
                {
                    if (Right == null) Right = node;
                    else Right.Insert(node);
                }
            }

            public int[] Transform(List<int> elems = null)
            {
                if (elems == null) elems = new List<int>();

                if (Left != null)
                {
                    Left.Transform(elems);
                }
                elems.Add(Data);

                if (Right != null)
                {
                    Right.Transform(elems);
                }
                return elems.ToArray();
            }
        }

        public static int[] Sort(int[] array)
        {
            TreeNode tree = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                tree.Insert(new TreeNode(array[i]));
            }
            return tree.Transform();
        }
    }
}
