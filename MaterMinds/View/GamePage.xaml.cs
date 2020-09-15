using MaterMinds.View;
using MaterMinds.ViewModel;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;

namespace MaterMinds
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private GameViewModel model;

        public GamePage()
        {
            InitializeComponent();
            model = new GameViewModel();
            DataContext = model; 
        }

        public GamePage(Player player)
        {
            InitializeComponent();
            model = new GameViewModel();
            DataContext = model;
        }

        private void panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {

                e.Effects = DragDropEffects.Move;

            }
        }

        private void panel_Drop(object sender, DragEventArgs e)
        {
            // If an element in the panel has already handled the drop,
            // the panel should not also handle it.
            if (e.Handled == false)
            {
                Panel _panel = (Panel)sender;
                
                UIElement _element = (UIElement)e.Data.GetData("Object");

                if (_panel != null && _element != null)
                {
                    // Get the panel that the element currently belongs to,
                    // then remove it from that panel and add it the Children of
                    // the panel that its been dropped on.
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);

                    if (_parent != null)
                    {
                       if (e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            
                            if (_element is YellowPeg)
                            {
                                YellowPeg yellowCopy = new YellowPeg();
                                yellowCopy.IsMoveble = false;
                                _panel.Children.Add(yellowCopy);
                            }
                            else if (_element is BluePeg)
                            {
                                BluePeg blueCopy = new BluePeg();
                                blueCopy.IsMoveble = false;
                                _panel.Children.Add(blueCopy);
                                
                            }
                            else if (_element is RedPeg)
                            {
                                RedPeg redCopy = new RedPeg();
                                redCopy.IsMoveble = false;
                                _panel.Children.Add(redCopy);
                            }
                            else if (_element is GreenPeg)
                            {
                                GreenPeg greenCopy = new GreenPeg();
                                greenCopy.IsMoveble = false;
                                _panel.Children.Add(greenCopy);
                            }
                            else if (_element is PurplePeg)
                            {
                                PurplePeg purpleCopy = new PurplePeg();
                                purpleCopy.IsMoveble = false;
                                _panel.Children.Add(purpleCopy);
                            }
                            else if (_element is OrangePeg)
                            {
                                OrangePeg OrangeCopy = new OrangePeg();
                                OrangeCopy.IsMoveble = false;
                                _panel.Children.Add(OrangeCopy);
                            }
                            var colorId = ((MasterPeg)_element).ColorIndex;
                            int key = int.Parse(_panel.Uid);
                            model.PlacedPegs.AddOrUpdate(key, colorId);

                            // set the value to return to the DoDragDrop call
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }

            }
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);


            if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(Cursors.Hand);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }
    }
}
