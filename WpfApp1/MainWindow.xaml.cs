using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string url;
        public MainWindow()
        {
            InitializeComponent();
            
            Webform.Navigate("http://google.com");
            
        }
        //
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            return_url();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Webform.GoBack();
                Link_URL.Text = (Webform.Source).ToString();
            }
            catch(System.Runtime.InteropServices.COMException)
            {
                MessageBox.Show("error");
            }
            
        }

       

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Return)
            {
                return_url();
            }
        }
        public void return_url()
        {
            url = Link_URL.Text;
            char[] url_mass = url.ToCharArray();
            bool[] temp_flag = new bool[7];
            for (int i=0;i<temp_flag.Length;i++)
            {
                temp_flag[i] = false;
            }
            if (url_mass[0] == 'h')
            {
                temp_flag[0] = true;
                if (url_mass[1] == 't')
                {
                    temp_flag[1] = true;
                    if (url_mass[2] == 't')
                    {
                        temp_flag[2] = true;
                        if (url_mass[3] == 'p')
                        {
                            temp_flag[3] = true;
                            if (url_mass[4] == ':')
                            {
                                temp_flag[4] = true;
                                if (url_mass[5] == '/')
                                {
                                    temp_flag[5] = true;
                                    if (url_mass[6] == '/')
                                    {
                                        temp_flag[7] = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                url = "http://" + Link_URL.Text;
                Link_URL.Text = url;
                
            }

            try
            {
                
                Webform.Navigate(url);
                Link_URL.Text = (Webform.Source).ToString();
            }
            catch (System.UriFormatException)
            {
                
                MessageBox.Show("Введён неправильный HTTP запрос");
            }
        }
       

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Webform.Refresh();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Menu options = new Menu();
            options.ShowDialog();
            
        }
        private void Mouse_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton==MouseButtonState.Pressed)
            {
                URL_Update_async();
            }
        }
        public void URL_Update()
        {
            try
            {
                Link_URL.Text = Webform.Source.ToString();
            }
            catch(System.NullReferenceException)
            {

            }
            
        }
        public void URL_Update_async()
        {
            DispatcherOperation op = Dispatcher.BeginInvoke((Action)(() => {
                URL_Update();
            }));
        }

        private void Webform_Navigating(object sender, NavigationEventArgs e)
        {
            URL_Update_async();
        }
    }
}
