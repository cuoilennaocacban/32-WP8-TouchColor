using System.ComponentModel;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TouchColorWP8.Utilities;
using TouchColorWP8.ViewModel;
using Point = System.Windows.Point;

namespace TouchColorWP8
{
    public partial class MainPage : PhoneApplicationPage
    {

        private CameraCaptureTask cameraCaptureTask;
        private BitmapImage bmp;
        private WriteableBitmap wb;

        private Color currentColor;
        private bool isAdvanced = false;

        PhotoChooserTask photoChooserTask;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            StaticMethod.GetFavs();
            cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += CameraCaptureTaskOnCompleted;
            photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += new EventHandler<PhotoResult>(photoChooserTask_Completed);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            if (isAdvanced)
            {
                e.Cancel = true;
                ApplicationBar.IsVisible = true;
                AdvancedGrid.Visibility = Visibility.Collapsed;
                BasicGrid.Visibility = Visibility.Visible;
                isAdvanced = false;
            }
        }

        private void CameraCaptureTaskOnCompleted(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //Code to display the photo on the page in an image control named myImage.
                bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                MainImage.Source = bmp;

                Dispatcher.BeginInvoke(BmpOnImageOpened);
            }
        }

        private void BmpOnImageOpened()
        {
            wb = new WriteableBitmap(MainImage, null);
        }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void TakePictureButton_OnTap(object sender, GestureEventArgs e)
        {
            cameraCaptureTask.Show();
        }

        private void MainImage_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = new Point();
            p = e.GetPosition(MainImage);
            GetColor(p);
        }

        private void MainImage_OnTap(object sender, GestureEventArgs e)
        {
            Point p = new Point();
            p = e.GetPosition(MainImage);
            GetColor(p);
        }

        public void GetColor(Point p)
        {
            try
            {
                MouseCoordinateTextBlock.Text = "X: " + p.X + " ; Y: " + p.Y;

                //double width = MainImage.ActualWidth;
                //double height = MainImage.ActualHeight;

                //int arrayNumber = Convert.ToInt32((p.Y - 1)*width + p.X);

                //var temp = wb.Pixels[arrayNumber];

                int X = Convert.ToInt32(p.X);
                int Y = Convert.ToInt32(p.Y);

                currentColor = wb.GetPixel(X, Y);

                ColorRectangle.Background = new SolidColorBrush(currentColor);

                ColorNameTextBlock.Text = ConvertColor(currentColor.R, currentColor.G, currentColor.B);
                AdvancedColorNameTextBlock.Text = ColorNameTextBlock.Text;
                ToAdvanced(currentColor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public string ConvertColor(int red, int green, int blue)
        {
            string name = "";

            if (red > 170) //lot of red
            {
                if (green > 170) //lot of green
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "White";
                    }
                    else if(blue > 85)
                    {
                        name = "Bright Yellow";
                    }
                    else
                    {
                        name = "Yellow";
                    }
                }
                else if (green > 85) //medium green
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "Pink";
                    }
                    else if (blue > 85)
                    {
                        name = "Magenta";
                    }
                    else
                    {
                        name = "Orange";
                    }
                }
                else
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "Purple";
                    }
                    else if (blue > 85)
                    {
                        name = "Dark Pink";
                    }
                    else
                    {
                        name = "Red";
                    }
                }
            }
            else if (red > 85)
            {
                if (green > 170) //lot of green
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "Cyan";
                    }
                    else if (blue > 85)
                    {
                        name = "Green";
                    }
                    else
                    {
                        name = "Bright Green";
                    }
                }
                else if (green > 85) //medium green
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "Dark Blue";
                    }
                    else if (blue > 85)
                    {
                        name = "Dark Grey";
                    }
                    else
                    {
                        name = "Dark Green";
                    }
                }
                else
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "Dark Blue";
                    }
                    else if (blue > 85)
                    {
                        name = "Purple";
                    }
                    else
                    {
                        name = "Dark Red";
                    }
                }
            }
            else
            {
                if (green > 170) //lot of green
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "Cyan";
                    }
                    else if (blue > 85)
                    {
                        name = "Green";
                    }
                    else
                    {
                        name = "Bright Green";
                    }
                }
                else if (green > 85) //medium green
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "Blue";
                    }
                    else if (blue > 85)
                    {
                        name = "Dark Cyan";
                    }
                    else
                    {
                        name = "Dark Green";
                    }
                }
                else
                {
                    if (blue > 170) //lots/medium/little
                    {
                        name = "Bright Blue";
                    }
                    else if (blue > 85)
                    {
                        name = "Dark Blue";
                    }
                    else
                    {
                        name = "Black";
                    }
                }
            }

            return name;
        }

        private void ToAdvanced(Color color)
        {
            int red = color.R;
            int blue = color.B;
            int green = color.G;

            ColorPresenterRectangle.Background = new SolidColorBrush(color);

            HexCodeTextBlock.Text = HexConverter(color);

            RTextBlock.Text = "R: " + red;
            GTextBlock.Text = "G: " + green;
            BTextBlock.Text = "B: " + blue;
        }

        private static String HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private void AdvancedButton_OnClick(object sender, EventArgs e)
        {
            ApplicationBar.IsVisible = false;
            AdvancedGrid.Visibility = Visibility.Visible;
            BasicGrid.Visibility = Visibility.Collapsed;
            isAdvanced = true;
            ToAdvanced(currentColor);
        }

        private void FavsButon_OnClick(object sender, EventArgs e)
        {
            if (StaticData.FavColorCollection.Count > 0)
            {
                NavigationService.Navigate(new Uri("/PageGroups/FavGroup/FavPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("No item in favourite");
            }
        }

        private void AddFavButton_OnClick(object sender, EventArgs e)
        {
            foreach (Color color in StaticData.FavColorCollection)
            {
                if (currentColor == color)
                    return;
            }

            StaticData.FavColorCollection.Add(currentColor);

            //Save
            StaticMethod.SaveFavs();

            MessageBox.Show("New color saved");

        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                //MessageBox.Show(e.ChosenPhoto.Length.ToString());

                //Code to display the photo on the page in an image control named myImage.
                bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                MainImage.Source = bmp;

                Dispatcher.BeginInvoke(BmpOnImageOpened);
            }
        }

        private void SelectPictureButton_OnTap(object sender, GestureEventArgs e)
        {
            photoChooserTask.Show();
        }
    }
}