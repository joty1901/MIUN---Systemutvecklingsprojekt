using MaterMinds.View;
using MaterMinds.ViewModel;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System;
using System.Windows.Resources;
using System.Runtime.InteropServices.WindowsRuntime;

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private GameViewModel model;

        public GamePage(Player player)
        {
            InitializeComponent();
            model = new GameViewModel(player);
            DataContext = model;
        }
        private void panel_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");
                if (_panel != null && _element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);
                    if (_parent.Name == "GuessController" && _panel.Name != "blackHole")
                    {
                       if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            model.PlaySound();
                            _panel.Children.Clear();
                            _panel.Children.Add(new GameBoardCircle());
                            if (_element is YellowPeg)
                            {
                                _panel.Children.Add(new YellowPeg());
                            }
                            else if (_element is BluePeg)
                            {
                                _panel.Children.Add(new BluePeg());
                            }
                            else if (_element is RedPeg)
                            {
                                _panel.Children.Add(new RedPeg());
                            }
                            else if (_element is GreenPeg)
                            {
                                _panel.Children.Add(new GreenPeg());
                            }
                            else if (_element is PurplePeg)
                            {
                                _panel.Children.Add(new PurplePeg());
                            }
                            else if (_element is OrangePeg)
                            {
                                _panel.Children.Add(new OrangePeg());
                            }
                            var colorId = ((MasterPeg)_element).ColorIndex;
                            int key = int.Parse(_panel.Uid);
                            model.PlacedPegs.AddOrUpdate(key, colorId);

                            e.Effects = DragDropEffects.Move;
                       }
                    }
                    else if (_panel.AllowDrop && _parent.Name != "GuessController")
                    {
                        if (_panel.Name == "blackHole")
                        {
                            _parent.Children.Clear();
                            _parent.Children.Add(new GameBoardCircle());
                            model.PlacedPegs.Remove(int.Parse(_parent.Uid));
                        }
                        else if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            model.PlacedPegs.Remove(int.Parse(_parent.Uid));
                            DropSound();
                            _panel.Children.Clear();
                            _panel.Children.Add(new GameBoardCircle());
                            if (_element is YellowPeg)
                            {
                                _parent.Children.Remove(_element);
                                _panel.Children.Add(new YellowPeg());
                            }
                            else if (_element is BluePeg)
                            {
                                _parent.Children.Remove(_element);
                                _panel.Children.Add(new BluePeg());
                            }
                            else if (_element is RedPeg)
                            {
                                _parent.Children.Remove(_element);
                                _panel.Children.Add(new RedPeg());
                            }
                            else if (_element is GreenPeg)
                            {
                                _parent.Children.Remove(_element);
                                _panel.Children.Add(new GreenPeg());
                            }
                            else if (_element is PurplePeg)
                            {
                                _parent.Children.Remove(_element);
                                _panel.Children.Add(new PurplePeg());
                            }
                            else if (_element is OrangePeg)
                            {
                                _parent.Children.Remove(_element);
                                _panel.Children.Add(new OrangePeg());
                            }
                            var colorId = ((MasterPeg)_element).ColorIndex;
                            int key = int.Parse(_panel.Uid);
                            model.PlacedPegs.AddOrUpdate(key, colorId);
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }
        
        private void DropSound()
        {
            model.Start(model.SoundEffectPlayer, new Uri(@"Resources/Sound/WaterDrop.mp3", UriKind.Relative));
        }
    }
}
