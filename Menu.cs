using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockMatrixType
{
    public class Menu
    {
        private List<Block> blockMatrixList = new List<Block>();



        public Menu() { }


        public void Run()
        {
            int n;
            do
            {
                GetMenuPoint();
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                }
                catch (System.FormatException) { n = -1; }//WE STILL NEED TO ALLOCATE A VALUE TO N BECAUSE IF WE DON'T N WILL NOT HAVE A VALUE
                switch (n)
                {
                    case 1:
                        GetElement();
                        break;
                    case 2:
                        SetElement();
                        break;
                    case 3:
                        PrintMatrix();
                        break;
                    case 4:
                        AddMatrix();
                        break;
                    case 5:
                        Sum();
                        break;
                    case 6:
                        Mul();
                        break;
                }

            } while (n != 0);//YOU ONLY STOP WHEN N IS 0

        }

        #region Menu operations

        static private void GetMenuPoint()
        {
            Console.WriteLine("\n\n 0. - Quit");
            Console.WriteLine(" 1. - Get an element");
            Console.WriteLine(" 2. - Overwrite an element");
            Console.WriteLine(" 3. - Print a matrix");
            Console.WriteLine(" 4. - Set a matrix");
            Console.WriteLine(" 5. - Add matrices");
            Console.WriteLine(" 6. - Multiply matrices");
            Console.Write(" Choose: ");
        }

        private int GetIndex()
        {
            if (blockMatrixList.Count == 0) return -1;
            int n = 0;
            bool ok;
            do
            {
                Console.Write("Give a matrix index: ");
                ok = false;
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Integer is expected!"); 
                }
                if (n <= 0 || n > blockMatrixList.Count)
                {
                    ok = false;
                    Console.WriteLine("No such matrix!");
                }
            } while (!ok);
            return n - 1; //xMartrixindexlist starts from 0 on computer
            //???It has to return an integer in both cases that is why we are either returning -1 or n-1
        }

        private void GetElement()
        {
            if (blockMatrixList.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex(); //eg the first(0th or the second[1st] matrix )
            do
            {
                try
                {
                    Console.WriteLine("Give the index of the row: ");
                    int i = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the index of the column: ");
                    int j = int.Parse(Console.ReadLine()!);

                    Console.WriteLine($"a[{i},{j}]={blockMatrixList[ind].GetElement(i - 1, j - 1)}");
                    break;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine($"Index must be between 1 and {blockMatrixList[ind].Size}");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Index must be between 1 and {blockMatrixList[ind].Size}");
                }
            } while (true);
        }
        private void SetElement()
        {
            if (blockMatrixList.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            do
            {
                try
                {
                    Console.WriteLine("Give the index of the row: ");
                    int i = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the index of the column: ");
                    int j = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the value: ");
                    int e = int.Parse(Console.ReadLine()!);
                    //xMatrixList[ind][i - 1, j - 1] = e;
                    blockMatrixList[ind].SetElement(i - 1, j - 1, e);
                    break;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine($"Index must be between 1 and {blockMatrixList[ind].Size}");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"Index must be between 1 and {blockMatrixList[ind].Size}");
                }
                catch (Block.OutOfBlockException)
                {
                    Console.WriteLine("Only the elements in the diagonal may be rewritten");
                }
            } while (true);
        }
        private void PrintMatrix()
        {
            if (blockMatrixList.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            Console.Write(blockMatrixList[ind].ToString());
        }
        private void AddMatrix()
        {
            int ind = blockMatrixList.Count;
            bool ok = false;
            int n = -1;
            int b1 = -1;
            int b2 = 0;

            do
            {
                Console.Write("Size: ");
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = n > 0;
                    if (n == 1) // Check if n is 1
                    {
                        throw new ArgumentException("Matrix size cannot be 1.");//thow an exception together with the message. So there is no catching ArgumentException NO WE ARE CATCHING CHECK BELOW
                    }
                    if (n < 1)
                    {
                        throw new ArgumentException("Matrix size cannot be negative.");
                    }

                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Positive integer is expected!");
                }
                catch (ArgumentException ex) // Catch the ArgumentException here
                {
                    Console.WriteLine(ex.Message); // Display the error message(NOW I GET THE USE IF CATCH,,SO THAT WHEN THE EXCEPTION IS THROWN THE PROGRAM WON'T STOP THERE BIT IT WILL GO TO CATCJ WHERE THE EXCEPTION IS HANDLED IF IT HAPPENS TO BE THROWN)
                }
            } while (!ok);


            do
            {
                Console.Write("Size of b1 Matrix: ");
                try
                {
                    b1 = int.Parse(Console.ReadLine()!);
                    ok = b1 > 0;
                    if (b1 < 1)
                    {
                        throw new ArgumentException("Matrix size cannot be negative.");
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Positive integer is expected!");
                }
                catch (ArgumentException ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!ok);
            b2 = n - b1;
            Console.WriteLine($"Size of b2 Matrix will be {b2}");
            
            Block B = new(new List<int>(), new List<int>(), b1, b2);///A temporary list (we initialize it as empty when we want to use it again)we use this to add the first and second matrix
           

            ok = true;
            List<int> elements1 = new List<int>();
            List<int> elements2 = new List<int>();

            /*int val = 0;
            if (n % 2 != 0)
                val++;
            int nbrOfElements = n * 2 - val;*/
            for (int i = 0; i < (b1*b1); i++)
            {
                Console.Write("Element(b1): ");
                try
                {
                    int elem = int.Parse(Console.ReadLine()!);
                    /*if (n % 2 != 0 && i == n + n / 2)
                        elements.Add(0); // if you are in the middle*/
                    elements1.Add(elem);

                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Number is expected!");
                    ok = false;
                    break;
                }
            }

            for (int i = 0; i < (b2 * b2); i++)
            {
                Console.Write("Element(b2): ");
                try
                {
                    int elem = int.Parse(Console.ReadLine()!);
                    /*if (n % 2 != 0 && i == n + n / 2)
                        elements.Add(0); // if you are in the middle*/
                    elements2.Add(elem);

                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Number is expected!");
                    ok = false;
                    break;
                }
            }

            for (int i = 0; i < elements1.Count; i++)
            {
                Console.WriteLine(elements1[i] + " ");
            }
            for (int i = 0; i < elements2.Count; i++)
            {
                Console.WriteLine(elements2[i] + " ");
            }

            // Console.WriteLine("kkkkk");

            if (ok)
            {
                B.Set(elements1,elements2);//size is already set
                
                blockMatrixList.Add(B);
            }

            for (int i = 0; i < blockMatrixList.Count; i++)
            {
                Console.WriteLine(blockMatrixList[i] + " ");
            }
        }

        private void Sum()
        {
            if (blockMatrixList.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write((blockMatrixList[ind1] + blockMatrixList[ind2]).ToString());
            }
            catch (Block.DimensionMismatchException)
            {
                Console.WriteLine("Dimension mismatch!");
            }
        }

        private void Mul()
        {
            if (blockMatrixList.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write((blockMatrixList[ind1] * blockMatrixList[ind2]).ToString());
            }
            catch (Block.DimensionMismatchException)
            {
                Console.WriteLine("Dimension mismatch!");
            }
        }
        #endregion
    }
}
