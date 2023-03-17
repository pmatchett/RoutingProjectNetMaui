
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using RoutingProjectNet.src;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace RoutingProjectNet.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {


        public MainViewModel()
        {
            LabelText = "No actions currently";
            Columns = new ColumnDefinitionCollection();
            Rows = new RowDefinitionCollection();
            RoutingMap map = RoutingMap.GetMapInstance(); 
            for (int i = 0; i < map.GetXDim(); i++)
            {
                Columns.Add(new ColumnDefinition());
            }
            for(int j = 0; j < map.GetYDim(); j++)
            {
                Rows.Add(new RowDefinition());
            }
            MapGrid = new ObservableCollection<Node>();
        }


        [ObservableProperty]
        string _labelText;


        [ObservableProperty]
        ColumnDefinitionCollection _columns;

        [ObservableProperty]
        RowDefinitionCollection _rows;

        [ObservableProperty]
        ObservableCollection<Node> _mapGrid;

        [ObservableProperty]
        Grid _completeMap;




        [RelayCommand]
        void CreateMap()
        {
            LabelText = "Generating map";
            RoutingMap map = RoutingMap.GetMapInstance();
            map.GenerateMap();
            UpdateMap();
            LabelText = "Map generated";
        }

        [RelayCommand]
        void SolveMap()
        {
            LabelText = "Solving map";
            StaticRouter router = new();
            router.OptimizePath();
            UpdateMap();
            LabelText = "Map solved";
        }

        private void UpdateMap()
        {
            RoutingMap map = RoutingMap.GetMapInstance();
            Columns = new ColumnDefinitionCollection();
            Rows = new RowDefinitionCollection();
            for (int i = 0; i < map.GetXDim(); i++)
            {
                Columns.Add(new ColumnDefinition());
            }
            for (int j = 0; j < map.GetYDim(); j++)
            {
                Rows.Add(new RowDefinition());
            }
            Node[,] nodes = map.GetNodes();
            for(int i = 0; i<nodes.GetLength(1); i++)
            {
                for (int j = 0; j < nodes.GetLength(0); j++)
                {
                    if (nodes[j, i] != null)
                    {
                        MapGrid.Add(nodes[j, i]);
                    }
                }
            }
        }
    }
}
