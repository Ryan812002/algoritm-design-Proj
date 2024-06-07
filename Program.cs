using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace AlgoritmProj
{
    class Program
    {
        static void Main(string[] args)
        {
            // root
            Console.WriteLine("please  value vared konid ta save konim root dar tree");
            int value = Convert.ToInt32(Console.ReadLine());
            Node root = new Node(value);
            //


            while (true)
            {
                Console.WriteLine("Please value vared konid ta ezafe konim be derakht (agar 'q' bara khorooj ya (2 bara tabe Find , 3 bara tabe FindNext , 4 bara tabe FindPrev , 5 bara tabe Delete)):");
                string input = Console.ReadLine();


                if (input.ToLower() == "q")
                {
                    break;
                }

                if (input == "2")
                {
                    Console.WriteLine(" enter kon valuee baraye Find kardan");
                    int searchValue = Convert.ToInt32(Console.ReadLine());
                    Node findShode = root.FindNode(searchValue);

                    if (findShode != null)
                    {
                        Console.WriteLine("Found node movafagh");
                    }
                    else
                    {
                        Console.WriteLine("Node vojood nadarad");

                    }
                    Thread.Sleep(1000);
                    break;

                }
                if (input == "3")
                {
                    Console.WriteLine(" enter kon valuee baraye FindBadi ");
                    int searchValue = Convert.ToInt32(Console.ReadLine());
                    Node badiPeida = root.FindNextNode(searchValue);
                    Console.WriteLine(badiPeida.key);
                    if (badiPeida != null)
                    {
                        Console.WriteLine(" node badi movafagh");
                    }
                    else
                    {
                        Console.WriteLine("Node badi vojood nadarad");

                    }
                    Thread.Sleep(1000);
                    break;

                }
                if (input == "4")
                {
                    Console.WriteLine(" enter kon valuee baraye FindGhabli ");
                    int searchValue = Convert.ToInt32(Console.ReadLine());
                    Node ghabliPeida = root.FindPrevNode(searchValue);
                    Console.WriteLine(ghabliPeida.key);
                    if (ghabliPeida != null)
                    {
                        Console.WriteLine(" node ghabli movafagh");
                    }
                    else
                    {
                        Console.WriteLine("Node ghabli vojood nadarad");

                    }
                    Thread.Sleep(1000);
                    break;

                }


                if (input == "5")
                {

                    Console.WriteLine("enter kon valuee baraye delete kardan:");
                    int deleteValue = Convert.ToInt32(Console.ReadLine());
                    root.DeleteNode(deleteValue);
                    Console.WriteLine("\n sakhtar tree :");
                    root.Print();
                    Console.WriteLine();
                    Thread.Sleep(3000);
                    break;


                }

                int newValue = Convert.ToInt32(input);
                root.treeInsert(newValue);

                Console.WriteLine("\n sakhtar tree :");
                root.Print();
                Console.WriteLine();



            }
            //

        }
    }


    public class Node
    {

        public int key;
        public Node right;
        public Node left;

        public Node(int data)
        {
            key = data;
            right = null;
            left = null;
        }


        public void Print(string prefix = "", bool isLeft = true)
        {
            if (right != null)
            {
                right.Print(prefix + (isLeft ? "│   " : "    "), false);
            }

            Console.WriteLine(prefix + (isLeft ? "└── " : "┌── ") + key);

            if (left != null)
            {
                left.Print(prefix + (isLeft ? "    " : "│   "), true);
            }
        }
        //

        public void treeInsert(int newValue)
        {
            if (newValue < key) // moghayese Value jadid ba key
            {
                if (left == null)
                {
                    left = new Node(newValue); // age chap nadasht node jadid besaz to shakhe chap
                }
                else
                {
                    left.treeInsert(newValue); // age chap dasht az left insert kardan ro shoro kon
                }
            }
            if (newValue > key)
            {
                if (right == null)
                {
                    right = new Node(newValue);// age right nadasht node jadid besaz to shakhe right
                }
                else
                {
                    right.treeInsert(newValue);// age right dasht az tight insert kardan ro shoro kon 
                }

            }
        }


        public Node FindNode(int targetValue)
        {
            if (targetValue == key)
            {
                return this; // node peida shode
            }
            else if (targetValue < key && left != null)
            {
                return left.FindNode(targetValue); // Search dar shakhe chap
            }
            else if (targetValue > key && right != null)
            {
                return right.FindNode(targetValue); // Search dar shakhe right
            }
            else
            {
                return null; // Node peida nashode va bargasht null
            }
        }

        //

        public Node DeleteNode(int targetValue)
        {
            if (targetValue < key && left != null)
            {
                left = left.DeleteNode(targetValue); // edame roo shakhe chap
            }
            else if (targetValue > key && right != null)
            {
                right = right.DeleteNode(targetValue); // edame roo shakhe right
            }
            else // targetValue = key
            {
                // agar kolan bache nadasht
                if (left == null && right == null)
                {
                    return null; // remove mikonim
                }

                // age 1 bache dasht
                if (left == null) // chap nadasht 
                {
                    return right; // jabeja ba bache right
                }
                else if (right == null) // right nadasht
                {
                    return left; // jabeja ba bache chap
                }

                // age daraye 2 bache
                // check konim agar shakhe right bache chap dasth oon bayad jaygozin she 
                int jaygozin = FindMinValue(right);
                key = jaygozin;
                right = right.DeleteNode(jaygozin); // hazf node khode jaygozin ta 2 ta value ein ham nabashan
            }

            return this; // Return mikone node jadido ya null
        }
        //

        public int FindMinValue(Node node) // baraye peida kardan kamtarin dar shake chap shake rast node Delete
        {
            while (node.left != null)
            {
                node = node.left;
            }
            return node.key;
        }



        //
        public Node FindPrevNode(int targetValue)
        {
            Node main = this; // root 
            Node ghabli = null;

            while (main != null)
            {
                if (targetValue > main.key)
                {
                    ghabli = main; // Update kardan ghabli 
                    main = main.right; // main beshe child right
                }
                else // targetValue <= main.key 
                {
                    if (main.left != null)
                    {
                        // age shakhe chap dashtesh 
                        main = main.left;
                        while (main.right != null)  // age shakhe right dashtesh 
                        {
                            main = main.right;
                        }
                        return main; // nazdik tarin ozv be TargetValue ma
                    }
                    else
                    {
                        return ghabli; // nazdik tarin mishe hamoon nazdik tarin Node
                    }
                }
            }

            return null; // vaghti main null bashe pas null hastesh
        }



        public Node FindNextNode(int targetValue)
        {
            Node main = this; // root
            Node baddi = null;

            while (main != null)
            {
                if (targetValue < main.key)
                {
                    baddi = main;  // Update kardan baddi 
                    main = main.left;  // main beshe child Chap
                }
                else // targetValue >= main.key
                {
                    if (main.right != null)
                    {
                        // age shakhe right dashtesh 
                        main = main.right;
                        while (main.left != null) // agar baz shakhe chap dasht main beshe oon
                        {
                            main = main.left;
                        }
                        return main; // nazdik tarin ozv be TargetValue ma
                    }
                    else
                    {

                        return baddi; // nazdik tarin mishe hamoon nazdik tarin Node
                    }

                }
            }

            return null;// vaghti main null bashe pas null hastesh
        }




    }







}
