using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu
{
    public static class SortingAlgorithm
    {
        public static void QuickSort(List<Product> products, string variable, bool ascending)
        {
            if (variable.Equals("name"))
            {
                QuickSort(products, 0, products.Count - 1, ascending, (x, y) => x.Name.CompareTo(y.Name));
            }
            else if (variable.Equals("price"))
            {
                QuickSort(products, 0, products.Count - 1, ascending, (x, y) => x.Price.CompareTo(y.Price));
            }
        }

        public static void QuickSort(List<Product> arr, int left, int right, bool ascending, Func<Product, Product, int> comparer)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right, ascending, comparer);
                QuickSort(arr, left, pivotIndex - 1, ascending, comparer);
                QuickSort(arr, pivotIndex + 1, right, ascending, comparer);
            }
        }

        public static int Partition(List<Product> arr, int left, int right, bool ascending, Func<Product, Product, int> comparer)
        {
            Product pivot = arr[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if ((ascending && comparer(arr[j], pivot) < 0) || (!ascending && comparer(arr[j], pivot) > 0))
                {
                    i++;
                    Product temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            Product temp2 = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = temp2;
            return i + 1;
        }

        public static void PrintProductsList(List<Product> products)
        {
            foreach (Product p in products)
            {
                Console.WriteLine($"{p.Name}: {p.Price} kr, {p.NrInStock}");
            }
        }

    }
}
