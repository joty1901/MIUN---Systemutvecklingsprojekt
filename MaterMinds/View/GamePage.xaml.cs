using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System;
using MaterMinds.Model;

namespace MaterMinds
{
    public partial class GamePage : Page
    {
        private GameViewModel model;

        public GamePage(Player player)
        {
            InitializeComponent();
            model = new GameViewModel(player);
            DataContext = model;
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
        }
        private void PanelDrop(object sender, DragEventArgs e)
        {
            Panel panel = (Panel)sender;
            UIElement element = (UIElement)e.Data.GetData("Object");
            Panel parent = (Panel)VisualTreeHelper.GetParent(element);
            var newPeg = GetTypeOfPeg(element);
            if (parent.Name == "GuessController")
            {
                if (panel.Name == "blackHole")
                {
                    BlackHoleSound();
                }
                else
                {
                    DropSound();
                    UpdateUI(panel);
                    panel.Children.Add(newPeg);
                    model.PlacedPegs.AddOrUpdate(int.Parse(panel.Uid), newPeg.ColorIndex);
                }
            }
            else if (panel.AllowDrop && parent.Name != "GuessController")
            {
                if (panel.Name == "blackHole")
                {
                    BlackHoleSound();
                    parent.Children.Remove(element);
                    model.PlacedPegs.Remove(int.Parse(parent.Uid));
                }
                else
                {
                    DropSound();
                    UpdateUI(panel);
                    model.PlacedPegs.Remove(int.Parse(parent.Uid));
                    parent.Children.Remove(element);
                    panel.Children.Add(newPeg);
                    model.PlacedPegs.AddOrUpdate(int.Parse(panel.Uid), newPeg.ColorIndex);
                }
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void UpdateUI(Panel panel)
        {
            panel.Children.Clear();
            panel.Children.Add(new GameBoardCircle());
        }

        private MasterPeg GetTypeOfPeg(UIElement element)
        {
            if (element is YellowPeg)
            {
                return new YellowPeg();
            }
            else if (element is BluePeg)
            {
                return new BluePeg();
            }
            else if (element is RedPeg)
            {
                return new RedPeg();
            }
            else if (element is GreenPeg)
            {
                return new GreenPeg();
            }
            else if (element is PurplePeg)
            { 
                return new PurplePeg();
            }
            else if(element is OrangePeg)
            {
                return new OrangePeg();
            }
            return null;
        }

        private void DropSound()
        {
            MediaHelper.PlayMedia(MediaHelper._soundEffectPlayer, new Uri(@"Resources/Sound/WaterDrop.mp3", UriKind.Relative));
        }
        private void BlackHoleSound()
        {
            MediaHelper.PlayMedia(MediaHelper._soundEffectPlayer, new Uri(@"Resources/Sound/BlackholeSound.mp3", UriKind.Relative));
        }
    }
}
