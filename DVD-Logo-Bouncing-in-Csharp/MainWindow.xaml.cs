using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FXP_HELP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creates timer to perform animation
        DispatcherTimer timer = new DispatcherTimer();

        // Initialize image cordination variable
        Point image_cords = new Point();

        // Moove in x\y
        double move_x = 0;
        double move_y = 0;

        // Flags for moving forward or backward
        bool backwards_y = false;
        bool backwards_x = false;

        public MainWindow()
        {
            InitializeComponent();

            // Configuration of the timer
            timer.Interval = System.TimeSpan.FromMilliseconds(15);
            timer.Tick += timer_Tick;

            // Generates random direction
            Random rnd = new Random();
            backwards_x = (rnd.Next(0, 2) == 1) ? true : false;
            backwards_y = (rnd.Next(0, 2) == 1) ? true : false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Get image coordinates
            image_cords = image.TranslatePoint(new Point(0, 0), this);

            
            if (image_cords.X <= 0) // if hit left side
            {
                backwards_x = false;
            }
            else if (image_cords.X + image.Width >= this.Width) // if hit right side
            {
                backwards_x = true;
            }
            else if (image_cords.Y <= 0) // if hit up side
            {
                backwards_y = false;
            }
            else if (image_cords.Y + image.Height >= this.Height) // if hit down side
            {
                backwards_y = true;
            }


            if (backwards_y)
                move_y--; // move down
            else
                move_y++; // move up

            if (backwards_x)
                move_x--; // move left
            else
                move_x++; // move right

            //perform moving
            image.RenderTransform = new TranslateTransform(move_x, move_y);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
