using System.Windows.Media;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Shell;
using TouchColorWP8.ViewModel;

namespace TouchColorWP8.PageGroups.FavGroup
{
    public partial class FavPage : PhoneApplicationPage
    {
        private Color selectedColor;

        public FavPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded; 
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            FavColorListBox.ItemsSource = StaticData.FavColorCollection;
        }

        private void UIElement_OnTap(object sender, GestureEventArgs e)
        {
            //if (selectedColor != (Color) FavColorListBox.SelectedItem)
            //{
                selectedColor = (Color) FavColorListBox.SelectedItem;

                Clipboard.SetText(selectedColor.ToString());

                //MessageBox.Show("Color copied to clipboard: " + selectedColor);

                ToastPrompt toast = new ToastPrompt();
                toast.Title = "Touch Color";
                toast.Message = "Color copied: " + selectedColor;
                toast.TextOrientation = System.Windows.Controls.Orientation.Horizontal;
                toast.Show();
            //}
        }
    }
}