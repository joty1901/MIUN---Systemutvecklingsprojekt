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

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public int PanelId { get; set; }
        private GameViewModel model;
        private MediaPlayer SoundPlayer = new MediaPlayer();

        public GamePage(Player player)
        {
            InitializeComponent();
            model = new GameViewModel(player);
            DataContext = model;
        }

        //private void panel_DragOver(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent("Object"))
        //    {
        //        e.Effects = DragDropEffects.Move;
        //    }
        //}

        private void panel_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                UIElement _element = (UIElement)e.Data.GetData("Object");
                if (_panel != null && _element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);
                    if (_parent.Name == "GuessController")
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
                    else if (_element.AllowDrop)
                    {
                        model.PlacedPegs.Remove(int.Parse(_parent.Uid));
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
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
        ///// <summary>
        ///// Change cursor to peg depending on color.
        ///// Sorce: https://wpf.2000things.com/tag/cursor/ 
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        //{
          
        //    if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is RedPeg)
        //    {
        //        StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/RedCircle.cur", UriKind.Relative));               
        //        Mouse.SetCursor(new Cursor(sriCurs.Stream));
        //    }
        //    else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is YellowPeg)
        //    {
        //        StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/YellowCircle.cur", UriKind.Relative));
        //        Mouse.SetCursor(new Cursor(sriCurs.Stream));
        //    }
        //    else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is GreenPeg)
        //    {
        //        StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/GreenCircle.cur", UriKind.Relative));
        //        Mouse.SetCursor(new Cursor(sriCurs.Stream));
        //    }
        //    else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is BluePeg)
        //    {
        //        StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/BlueCircle.cur", UriKind.Relative));
        //        Mouse.SetCursor(new Cursor(sriCurs.Stream));
        //    }
        //    else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is PurplePeg)
        //    {
        //        StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/PurpleCircle.cur", UriKind.Relative));
        //        Mouse.SetCursor(new Cursor(sriCurs.Stream));
        //    }
        //    else if (e.Effects.HasFlag(DragDropEffects.Move) && e.OriginalSource is OrangePeg)
        //    {
        //        StreamResourceInfo sriCurs = Application.GetResourceStream(new Uri("Resources/Cursor/OrangeCircle.cur", UriKind.Relative));
        //        Mouse.SetCursor(new Cursor(sriCurs.Stream));
        //    }
        //    else
        //    {
        //        Mouse.SetCursor(Cursors.No);
        //    }
        //    e.Handled = true;
        //}

        private void DropSound()
        {
            SoundPlayer.Open(new Uri(@"Resources/Sound/WaterDrop.mp3", UriKind.Relative));
            SoundPlayer.Play();

            

        }

        
    }
}
