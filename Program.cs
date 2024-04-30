/*namespace XMatrixType
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
*/

using System.Globalization;
using System.Threading;

namespace BlockMatrixType
{
    class Program
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Menu m = new();
            m.Run();
        }
    }

    //class of menu for Xmatrix
}