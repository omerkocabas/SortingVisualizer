using System.ComponentModel;
using System.Windows.Media;

namespace Sorter
{
    public class RectangleModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SolidColorBrush colorBrush;

        public SolidColorBrush ColorBrush
        {
            get { return colorBrush; }
            set
            {
                colorBrush = value;
                OnPropertyChanged(nameof(ColorBrush));
            }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }


        public void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public RectangleModel(int height)
        {
            Height = height;
            ColorBrush = Brushes.Blue;
        }

    }
}
