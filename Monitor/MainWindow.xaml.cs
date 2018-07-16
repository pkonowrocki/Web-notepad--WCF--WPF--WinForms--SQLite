using libsqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLite baza = new SQLite();
        protected Dictionary<int, string> UsersData;
        protected Dictionary<int, string> TextsData;
        FileSystemWatcher watcher = new FileSystemWatcher();
        string User = null;
        int? Text = null;
        int? LastCheckedUser=null;
        int? LastCheckedText=null;




        public MainWindow()
        {
            if (!baza.DBis())
            {
                baza.CreateDB();
                baza.CreateUserTable();
                baza.CreateTextTable();
            }
            
            baza.SetWatcher(OnChangedDB);
            InitializeComponent();
            updateUsersTree();
            Users.DataContext = this.DataContext;

        }
        private void OnChangedDB(object source, FileSystemEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)delegate {
                updateUsersTree();
                if (LastCheckedUser != null)
                {
                    updateTextsTree(User);
                    foreach (TreeViewItem it in (Users.Items))
                        if ((int)((it.Header as RadioButton).Tag as int?) == LastCheckedUser)
                            (it.Header as RadioButton).IsChecked = true;
                }
                if (LastCheckedText != null)
                {
                    foreach (TreeViewItem it in (Texts.Items))
                        if ((int)((it.Header as RadioButton).Tag as int?) == LastCheckedText)
                            (it.Header as RadioButton).IsChecked = true;
                }
            });
        }

        void updateUsersTree()
        {
            UsersData = baza.GetUsers();
            Users.Items.Clear();
            foreach (KeyValuePair<int, string> a in UsersData)
            {
                TreeViewItem Node = new TreeViewItem() { Header = new RadioButton() { Content = a.Value } };
                (Node.Header as RadioButton).GroupName = "UsersRadioButtons";
                (Node.Header as RadioButton).Checked += UserRadioButtonChecked;
                (Node.Header as RadioButton).Tag = a.Key;
                Users.Items.Add(Node);
            }
        }

        private void UserRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton a = sender as RadioButton;
            User = a.Content.ToString();
            updateTextsTree(User);
            LastCheckedUser = (int)(a.Tag as int?);
            textsNum.Text = "Liczba dokumentów: " + baza.TextsPerUser(User);
            linesNum.Text = "Liczba wierszy w dokumencie: " ;
            charNum.Text = "Liczba znaków w dokumencie: ";
        }

        private void TextRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton a = sender as RadioButton;
            Text = a.Tag as int?;
            string temp = baza.GetText((int)Text);
            
            
            linesNum.Text = "Liczba wierszy w dokumencie: " + baza.LinesNumAsync((int)Text).Result;//temp.Split('\n').Length;
            charNum.Text = "Liczba znaków w dokumencie: " + baza.TextLenAsync((int)Text).Result;//temp.Length;
            LastCheckedText = (int)(a.Tag as int?);
        }

        void updateTextsTree(string name)
        {
            TextsData = baza.GetTexts(name);
            Texts.Items.Clear();
            foreach (KeyValuePair<int, string> a in TextsData)
            {
                TreeViewItem Node = new TreeViewItem();
                if (a.Value == null)
                    Node.Header = new RadioButton() { Content = "     " } ;
                else
                    Node.Header = new RadioButton() { Content = a.Value };
                (Node.Header as RadioButton).GroupName = "TextsRadioButtons";
                (Node.Header as RadioButton).Checked += TextRadioButtonChecked;
                (Node.Header as RadioButton).Tag = a.Key;
                Texts.Items.Add(Node);
            }
        }
    }
}
