using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlockMatrixType
{
    public class Block
    {
        #region Exceptions
        public class SizeMismatchException : Exception { }
        public class OutOfBlockException : Exception { }
        public class DimensionMismatchException : Exception { }
        public class InvalidSizeException : Exception { }
        #endregion

        #region Attributes
        private List<int> _vec1;
        private List<int> _vec2;    
        private int _size1;
        private int _size2;

        #endregion
        public int Size // Property for getting the size of the matrix
        {
            get { return (_size1 + _size2); }
        }
        public int Size1 // Property for getting the size of the matrix
        {
            get { return _size1; }
        }
        public int Size2 // Property for getting the size of the matrix
        {
            get { return _size2; }
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2);
        }
        public override bool Equals(Object? obj)
        {
            if (obj == null || !(obj is Block))
                return false;
            else
            {
                Block? m = obj as Block;
                if (m!.Size != this.Size) return false;
                for (int i = 0; i < _vec1.Count; i++)
                {
                    if (_vec1[i] != m._vec1[i]) return false;
                }
                for (int i = 0; i < _vec2.Count; i++)
                {
                    if (_vec2[i] != m._vec2[i]) return false;
                }
                return true;
            }
        }
        #region Constructor
        public Block()//WHERE IS IT USED???????????>> doesn't seem to be used explicitly within the Block class. When you create an instance of the Block class without passing any arguments, the empty constructor will be invoked to initialize the object.
        {
            _vec1 = new List<int>(); //Empty list-->> List<int> _vec = new List<int>();
            _vec2 = new List<int>();
            for (int i = 0; i < (_size1 * _size1); i++)
            {
                _vec1.Add(0);
            }
            for (int i = 0; i < (_size2 * _size2); i++)
            {
                _vec2.Add(0);
            }
        }
        public Block(List<int> vec1, List<int> vec2, int size1, int size2)
        {

            // if (vec.Count != _size * 2)
            //     throw new SizeMismatchException();
            _vec1 = new List<int>(vec1);//new List <int>({})
            _vec2 = new List<int>(vec2);//new List <int>({})
            _size1 = size1;
            _size2 = size2;
        }
        #endregion

        private static int ind1(int i, int j, int size1)
        {

            return (i * size1)+j;
        }
        private static int ind2(int i, int j, int size1,int size2)
        {
            return (((i-size1) * size2) + (j - size1));
        }



        #region Main Methods

        public int GetElement(int i, int j)
        {
            if (i < 0 || j < 0 || i >= (_size1+_size2) || j >= (_size1 + _size2))
            {
                throw new IndexOutOfRangeException();//Already exists in the environment
            }
            else if (i < _size1 && j < _size1)
            {
                return _vec1[ind1(i,j,_size1)];
            }
            else if (i >= _size1 && j>=_size1)
            {
                return _vec2[ind2(i,j,_size1,_size2)];
            }
            else
            {
                return 0;
            }

        }


        public void SetElement(int i, int j, int element)
        {
            if (i < 0 || j < 0 || i >= (_size1 + _size2) || j >= (_size1 + _size2))
            {
                throw new ArgumentOutOfRangeException();//Already exists in the environment
            }
            else if (i < _size1 && j < _size1)
            {
                _vec1[ind1(i, j, _size1)] = element;
            }
            else if (i >= _size1 && j >= _size1)
            {
                _vec2[ind2(i, j, _size1, _size2)] = element;
            }
            else
            {
                throw new OutOfBlockException();
            }


        }
        public override String ToString()
        {
            String str = "";
            for (int i = 0; i < (_size1+_size2); i++)
            {
                for (int j = 0; j < (_size1 + _size2); j++)
                {
                    str = str + "\t" + GetElement(i, j).ToString();
                }
                str = str + "\n";
            }
            return str;
        }
        //new
        public void Set(in List<int> vec1, in List<int> vec2)
        {
            if (vec1.Count != (_size1 * _size1) || vec2.Count != (_size2 * _size2))
                throw new SizeMismatchException();

            this._vec1 = new List<int>(vec1);
            this._vec2 = new List<int>(vec2);
            // List<int> list = new List<int>({1, 3,5,6})
        }
        public static Block operator +(in Block a, in Block b)
        {
            if ((a._size1 + a._size2) != (b._size1 + b._size2))
                throw new DimensionMismatchException();
            if ( (a._size1 != b._size1) || (a._size2 != b._size2) )
                 throw new DimensionMismatchException();
            List<int> l = new List<int>();
            List<int> l1 = new List<int>();
            for (int i = 0; i < a._vec1.Count; i++)
            {
                l.Add(a._vec1[i] + b._vec1[i]);
                
            }
            for (int i = 0; i < a._vec2.Count; i++)
            {
                l1.Add(a._vec2[i] + b._vec2[i]);

            }
            return new Block(l,l1, a._size1,a._size2); ;
        }




        public static Block operator *(in Block a, in Block b)
        {
            if ((a._size1 + a._size2) != (b._size1 + b._size2) || (a._size1 != b._size1) || (a._size2 != b._size2))
            {
                throw new DimensionMismatchException();
            }

            List<int> l = new List<int>();
            List<int> l1 = new List<int>();
            for (int i = 0; i < (a._size1 * a._size1); i++)
            {
                l.Add(0);
            }
            for (int i = 0; i < (a._size2 * a._size2); i++)
            {
                l1.Add(0);
            }
            Block mul = new Block(l, l1, a._size1, a._size2);

            for (int i = 0; i < (a._size1); i++)//if (i < _size1 && j < _size1)
            {
                for (int j = 0; j < (a._size1); j++)
                {
                    int value = 0;

                    for (int k = 0; k < (a._size1); k++)
                    {
                        value += a.GetElement(i, k) * b.GetElement(k, j);
                    }

                    mul.SetElement(i, j, value);
                }
            }

            for (int i = a._size1; i < (a._size1 + a._size2); i++)//if (i >= _size1 && j >= _size1)
            {
                for (int j = a._size1; j < (a._size1 + a._size2); j++)
                {
                    int value = 0;

                    for (int k = a._size1; k < (a._size1 + a._size2); k++)
                    {
                        value += a.GetElement(i, k) * b.GetElement(k, j);
                    }

                    mul.SetElement(i, j, value);
                }
            }

            return mul;
        }


        #endregion
    }
}