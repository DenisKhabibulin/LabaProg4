using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
using Newtonsoft.Json;
using System.IO;

namespace LabaProg4
{
    public partial class MainWindow : Window
    {

        List<Battler> team1 = new List<Battler>();
        List<Battler> team2 = new List<Battler>();
        int team1Score = 0;
        int team2Score = 0;
        const int roundsForWin = 3;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string log;
            log = Battle.Round(team1, team2);
            if (log.Contains("Команда Игрока 1 выиграла раунд"))
                team1Score++;
            else team2Score++;
            Round.Text = $"Score: {team1Score} / {team2Score}";
            if (team1Score == roundsForWin || team2Score == roundsForWin)
            {
                buttonStart.IsEnabled = false;
                if (team1Score > team2Score)
                    log += "Игрок 1 победил.";
                else
                    log += "Игрок 2 победил.";
            }
            BattleLog.Text = log;
        }

        private void Hero_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string textBlockText;
            Button button = sender as Button;
            StackPanel stackPanel = button.Content as StackPanel;
            foreach (UIElement uIElement in stackPanel.Children)
            {
                if (uIElement is Label buttonTextBlock)
                {
                    textBlockText = (string)buttonTextBlock.Content;
                    DragDrop.DoDragDrop(button, textBlockText, DragDropEffects.Copy);
                }
            }

        }


        private void Button_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            Button button = sender as Button;
            StackPanel stackPanel = button.Content as StackPanel;
            foreach (UIElement uIElement1 in stackPanel.Children)
            {
                if (uIElement1 is Label buttonTextBlock)
                {
                    buttonTextBlock.Content = (string)e.Data.GetData(DataFormats.Text);
                    foreach (UIElement uIElement2 in stackPanel.Children)
                    {
                        if (uIElement2 is Image buttonImage)
                        {
                            string path = "Images/" + buttonTextBlock.Content + ".png";
                            buttonImage.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                            ShowHeroStats((string)buttonTextBlock.Content, stackPanel.Children[2] as TextBlock);
                            return;
                        }
                    }
                }
            }
        }
        private void ShowHeroStats(string hero, TextBlock text)
        {
            string className = "LabaProg4." + hero;
            Type type = Type.GetType(className);

            if (type != null)
            {
                object instance = Activator.CreateInstance(type);
                CreateTeam(instance, Convert.ToInt32(text.Tag));
                MethodInfo method = type.GetMethod("ShowStats");
                if (method != null)
                {
                    text.Text = (string)method.Invoke(instance, null);
                }
            }
            else
            {
                Console.WriteLine("Class not found.");
            }
        }
        private void CreateTeam(object type, int n)
        {
            if (n == 1)
                team1.Add((Battler)type);
            else
                team2.Add((Battler)type);
        }
    }
}
