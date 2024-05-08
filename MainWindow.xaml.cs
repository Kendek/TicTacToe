using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] matrix = new int[3, 3];
        public MainWindow()
        {
            InitializeComponent();
            Player1.IsEnabled = false;
            Player2.IsEnabled = false;
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Array.Clear(matrix);
            ShowField();
        }

        private void Solo_Click(object sender, RoutedEventArgs e)
        {
            Player1.IsEnabled = true;
            Player2.Text = "BOT";
        }
        
        public void ShowField()
        {
            ClearField();

            for (int i = 0; i < 3; i++)
            {
                GameField.RowDefinitions.Add(new());
            }
            for (int j = 0; j < 3; j++)
            {
                GameField.ColumnDefinitions.Add(new());
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button b = new Button();

                    b.Background = new SolidColorBrush(Colors.White);
                    b.Content = GetContent(matrix[i, j], b);
                    b.FontSize = 60;
                    b.Click += Player1_Click;

                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);

                    b.HorizontalAlignment = HorizontalAlignment.Stretch;
                    b.VerticalAlignment = VerticalAlignment.Stretch;

                    GameField.Children.Add(b);

                }
            }

        }

        public void ClearField()
        {
            GameField.Children.Clear();
            GameField.RowDefinitions.Clear();
            GameField.ColumnDefinitions.Clear();
        }

        public string GetContent(int num, Button b)
        {
            switch (num) 
            {
                case 0: return "";
                case 1: b.IsEnabled = false; return "X";
                case 2: b.IsEnabled = false; return "O";
                case 3: b.IsEnabled = false; return "O";
                default: return "";
            }
        }

        public async void Player1_Click(object sender, RoutedEventArgs e)
        {
            Button? b = sender as Button;
            int row = Grid.GetRow(b);
            int column = Grid.GetColumn(b);

            matrix[row, column] = 1;
            
            ShowField();
            if (CheckWin(1) == false && CheckZeros() == 0) { MessageBox.Show("Tie"); ClearField(); }
            else if (CheckWin(1)) { MessageBox.Show($"The winner is {Player1.Text}"); ClearField(); }
            else { await Task.Delay(1000); BotPlayer(); }
        }

        public void BotPlayer()
        {
            Random random = new Random();

            while (true)
            {
                int i = random.Next(0,3);
                int j = random.Next(0,3);

                if (matrix[i, j] == 0)
                {
                    matrix[i, j] = 2;
                    break;
                }
            }
            ShowField();
            if (CheckWin(2) == false && CheckZeros() == 0) { MessageBox.Show("Tie"); ClearField(); }
            else if (CheckWin(2)) { MessageBox.Show("The winner is the BOT"); ClearField(); }
        }

        public bool CheckWin(int num)
        {   
            //Sorok ellenőrzése
            if (matrix[0, 0] == num && matrix[0, 1] == num && matrix[0, 2] == num) { return true; }
            if (matrix[1, 0] == num && matrix[1, 1] == num && matrix[1, 2] == num) { return true; }
            if (matrix[2, 0] == num && matrix[2, 1] == num && matrix[2, 2] == num) { return true; }

            //Oszlopok ellenőrzése
            if (matrix[0, 0] == num && matrix[1, 0] == num && matrix[2, 0] == num) { return true; }
            if (matrix[0, 1] == num && matrix[1, 1] == num && matrix[2, 1] == num) { return true; }
            if (matrix[0, 2] == num && matrix[1, 2] == num && matrix[2, 2] == num) { return true; }

            //Átlók ellenőrzés
            if (matrix[0, 0] == num && matrix[1, 1] == num && matrix[2, 2] == num) { return true; }
            if (matrix[0, 2] == num && matrix[1, 1] == num && matrix[2, 0] == num) { return true; }

            else {  return false; }
        }

        public int CheckZeros()
        {
            int zeros = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i, j] == 0) {  zeros++; }
                }
            }
            return zeros;
        }
    }
}