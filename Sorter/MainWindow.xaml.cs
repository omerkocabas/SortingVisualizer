using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Collections.ObjectModel;

namespace Sorter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int minRectangleHeight = 10;
        int maxRectangleHeight = 100;

        ObservableCollection<RectangleModel> list  = new ObservableCollection<RectangleModel>();
        Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();

            slider.Value = 10;

            createNewArray();

            itemsControl.ItemsSource = list;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            createNewArray();
        }

        private void Create_New_Array_Click(object sender, RoutedEventArgs e)
        {
            createNewArray();
        }

        private void createNewArray()
        {
            maxRectangleHeight = this.ActualHeight == 0 ? (int)this.Height / 2 : (int)this.ActualHeight / 2;
            list.Clear();
            for (int i = 0; i < slider.Value; i++)
            {
                list.Add(new RectangleModel(random.Next(minRectangleHeight, maxRectangleHeight)));
            }

        }

        private async void Bubble_Sort_Button_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => bubbleSort());

        }

        private void bubbleSort()
        {
            int size = list.Count;

            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size - i - 1; j++)
                {
                    var first = list[j];
                    var second = list[j + 1];
                    first.ColorBrush = Brushes.Red;
                    second.ColorBrush = Brushes.Red;

                    Thread.Sleep(20);                    

                    if (first.Height > second.Height)
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {

                            var temp = first;
                            list.RemoveAt(j + 1);
                            list.RemoveAt(j);
                            list.Insert(j, first);
                            list.Insert(j, second);

                        });                        
                    }


                    first.ColorBrush = Brushes.Blue;
                    second.ColorBrush = Brushes.Blue;

                }
            }
            
        }

        private async void Merge_Sort_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(()=> mergeSort(list, 0, list.Count - 1)) ;
        }

        void merge(ObservableCollection<RectangleModel> arr, int l, int m, int r)
        {

            int n1 = m - l + 1;
            int n2 = r - m;

            ObservableCollection<RectangleModel> L = new ObservableCollection<RectangleModel>();
            ObservableCollection<RectangleModel> R = new ObservableCollection<RectangleModel>();
            int i, j;

            for (i = 0; i < n1; ++i)
            {
                L.Add(null);
                L[i] = arr[l + i];
            }
                
            for (j = 0; j < n2; ++j)
            {
                R.Add(null);
                R[j] = arr[m + 1 + j];
            }
                


            i = 0;
            j = 0;

            // Initial index of merged
            // subarray array
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i].Height <= R[j].Height)
                {
                    
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {

                        arr[k] = L[i];
                        
                    });
                    L[i].ColorBrush = Brushes.Red;
                    R[j].ColorBrush = Brushes.Red;
                    Thread.Sleep(100);

                    L[i].ColorBrush = Brushes.Blue;
                    R[j].ColorBrush = Brushes.Blue;
                    ++i;
                    //arr[k] = L[i];
                    //i++;
                }
                else
                {
                    
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {                       
                        arr[k] = R[j];                       
                        
                    });
                    L[i].ColorBrush = Brushes.Red;
                    R[j].ColorBrush = Brushes.Red;
                    Thread.Sleep(100);
                    L[i].ColorBrush = Brushes.Blue;
                    R[j].ColorBrush = Brushes.Blue;
                    ++j;

                }

                ++k;
                
            }

            // Copy remaining elements
            // of L[] if any
            while (i < n1)
            {
                L[i].ColorBrush = Brushes.Red;
                Thread.Sleep(100);
                App.Current.Dispatcher.Invoke((Action)delegate
                {                    
                    arr[k] = L[i];                   
                    
                });
                arr[k].ColorBrush = Brushes.Blue;
                i++;
                k++;
            }

            // Copy remaining elements
            // of R[] if any
            while (j < n2)
            {
                R[j].ColorBrush = Brushes.Red;
                Thread.Sleep(100);
                App.Current.Dispatcher.Invoke((Action)delegate
                {                
                    arr[k] = R[j];                  
                    
                });
                arr[k].ColorBrush = Brushes.Blue;
                j++;
                k++;
            }
        }

        void mergeSort(ObservableCollection<RectangleModel> arr, int l, int r)
        {
            if (l < r)
            {
                // Find the middle
                // point
                int m = l + (r - l) / 2;

                // Sort first and
                // second halves
                mergeSort(arr, l, m);
                mergeSort(arr, m + 1, r);

                // Merge the sorted halves
                merge(arr, l, m, r);
            }
        }

        void swap(ObservableCollection<RectangleModel> arr, int i, int j)
        {
            arr[i].ColorBrush = Brushes.Red;
            arr[j].ColorBrush = Brushes.Red;
            Thread.Sleep(100);
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                RectangleModel temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            });

            arr[i].ColorBrush = Brushes.Blue;
            arr[j].ColorBrush = Brushes.Blue;
        }

        /* This function takes last element as pivot, places
             the pivot element at its correct position in sorted
             array, and places all smaller (smaller than pivot)
             to left of pivot and all greater elements to right
             of pivot */
        int partition(ObservableCollection<RectangleModel> arr, int low, int high)
        {

            // pivot
            int pivot = arr[high].Height;

            arr[high].ColorBrush = Brushes.Green;

            // Index of smaller element and
            // indicates the right position
            // of pivot found so far
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {

                // If current element is smaller
                // than the pivot
                if (arr[j].Height < pivot)
                {

                    // Increment index of
                    // smaller element
                    i++;
                    swap(arr, i, j);
                }
            }
            swap(arr, i + 1, high);

            arr[high].ColorBrush = Brushes.Blue;
            return (i + 1);
        }

        /* The main function that implements QuickSort
                    arr[] --> Array to be sorted,
                    low --> Starting index,
                    high --> Ending index
           */
        void quickSort(ObservableCollection<RectangleModel> arr, int low, int high)
        {
            if (low < high)
            {

                // pi is partitioning index, arr[p]
                // is now at right place
                int pi = partition(arr, low, high);

                // Separately sort elements before
                // partition and after partition
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }


        private async void Quick_Sort_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => quickSort(list, 0, list.Count - 1));
        }

        private async void Heap_Sort_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => heap_sort(list));
        }

        public void heap_sort(ObservableCollection<RectangleModel> arr)
        {
            int N = arr.Count;

            // Build heap (rearrange array)
            for (int i = N / 2 - 1; i >= 0; i--)
                heapify(arr, N, i);

            // One by one extract an element from heap
            for (int i = N - 1; i > 0; i--)
            {
                arr[i].ColorBrush = Brushes.Red;
                arr[0].ColorBrush = Brushes.Red;
                Thread.Sleep(100);
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    // Move current root to end
                    var temp = arr[0];
                    arr[0] = arr[i];
                    arr[i] = temp;
                });

                arr[i].ColorBrush = Brushes.Blue;
                arr[0].ColorBrush = Brushes.Blue;



                // call max heapify on the reduced heap
                heapify(arr, i, 0);
            }
        }

        void heapify(ObservableCollection<RectangleModel> arr, int N, int i)
        {
            int largest = i; // Initialize largest as root
            int l = 2 * i + 1; // left = 2*i + 1
            int r = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (l < N && arr[l].Height > arr[largest].Height)
                largest = l;

            // If right child is larger than largest so far
            if (r < N && arr[r].Height > arr[largest].Height)
                largest = r;

            
            
            if (largest != i)
            {
                arr[i].ColorBrush = Brushes.Red;
                arr[largest].ColorBrush = Brushes.Red;
                Thread.Sleep(100);

                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    // Move current root to end
                    var swap = arr[i];
                    arr[i] = arr[largest];
                    arr[largest] = swap;
                });

                arr[i].ColorBrush = Brushes.Blue;
                arr[largest].ColorBrush = Brushes.Blue;
                // If largest is not root

                // Recursively heapify the affected sub-tree
                heapify(arr, N, largest);
            }
        }
    }
}
