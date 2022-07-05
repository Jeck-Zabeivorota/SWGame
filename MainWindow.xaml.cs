using System.Windows;

using SWGame.GameObjects.RepublicUnits;
using SWGame.GameObjects.SeparatistsUnits;
using SWGame.GameController;

namespace SWGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UIMap.InitializeMap(11, 11);
            GController.Map = UIMap;

            GController.AddToMap(new CloneSolder { X = 3, Y = 6 }, new CloneSolder { X = 6, Y = 5 }, new Droid { X = 5, Y = 2 });

            GController.StartGame();
        }
    }
}
