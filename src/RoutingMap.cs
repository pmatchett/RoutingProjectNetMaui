using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProjectNet.src
{
    //Singleton class that saves the map used for navigation
    internal class RoutingMap
    {
        private int xDim, yDim, numNodes;

        private Node start, end;

        private Node[,] positions;

        private static RoutingMap map = null;

        private static int xDimSettings = 50, yDimSettings = 50;

        private static double obsPercentSettings = 0.3;

        private RoutingMap(int x, int y)
        {
            xDim = x;
            yDim = y;
            numNodes = x * y;
            positions = new Node[x, y];
        }

        public static RoutingMap GetMapInstance()
        {
            map ??= new RoutingMap(RoutingMap.xDimSettings, RoutingMap.yDimSettings);
            return map;
        }

        public void GenerateMap()
        {
            //Debug.WriteLine("Generating map");
            this.InitNewMap();
            Random rand = new();

            //Determining the start nodes
            int xStart = rand.Next(0, xDim);
            int yStart = rand.Next(0, yDim);
            int xEnd, yEnd;
            do
            {
                xEnd = rand.Next(0, xDim);
                yEnd = rand.Next(0, yDim);

            } while (xEnd == xStart && yEnd == yStart);

            //Debug.WriteLine($"Start position: {xStart} {yStart}");
            //Debug.WriteLine($"End position: {xEnd} {yEnd}");

            for(int i = 0; i < xDim; i++)
            {
                for(int j = 0; j < yDim; j++)
                {
                    if(i == xStart && j == yStart)
                    {
                        positions[i, j] = new Node(i, j, NodeStatus.Start);
                        start = positions[i, j];
                    }
                    else if(i == xEnd && j == yEnd)
                    {
                        positions[i, j] = new Node(i, j, NodeStatus.End);
                        end = positions[i, j];
                    }
                    else
                    {
                        double obstacle = rand.NextDouble();
                        if(obstacle < obsPercentSettings)
                        {
                            positions[i, j] = new Node(i, j, NodeStatus.Obstacle);
                        }
                        else
                        {
                            positions[i, j] = new Node(i, j, NodeStatus.Free);
                        }
                    }
                }
            }
        }

        private void InitNewMap()
        {
            xDim = RoutingMap.xDimSettings;
            yDim = RoutingMap.yDimSettings;
            numNodes = xDim * yDim;
            positions = new Node[xDim, yDim];
        }

        public List<Node> getNeighbours(Node center)
        {
            //Since there can only be max 8 neighbours in a 2D context, any unused are set to null
            List<Node> neighbours = new();
            int x = center.GetCoords().x;
            int y = center.GetCoords().y;
            for(int i = x-1; i <= x+1; i++)
            {
                for (int j = y-1; j <= y+1; j++)
                {
                    //Check if this position is outside the map
                    if(i < 0 || j <0 || i >= xDim || j >= yDim || (i==x && j == y))
                    {
                        continue;
                    }
                    else
                    {
                        neighbours.Add(positions[i, j]);
                    }
                }
            }
            return neighbours;
        }

        public int GetXDim()
        {
            return xDim;
        }

        public static void SetXDimSettings(int x)
        {
            xDimSettings = x;
        }

        public int GetYDim()
        {
            return yDim;
        }

        public static void SetYDimSettings(int y)
        {
            yDimSettings = y;
        }

        public static void SetObsPercentSettings(double obsPercent)
        {
            obsPercentSettings = obsPercent;
        }

        public Node getStart()
        {
            return start;
        }

        public Node getEnd()
        {
            return end;
        }

        public int getNumNodes()
        {
            return numNodes;
        }

        public NodeStatus? GetNodeStatus(int x, int y)
        {
            if (x < 0 || x > (xDim-1) || y < 0 || y > (yDim - 1))
            {
                return null;
            }
            return positions[x, y].GetStatus();
        }

        public bool? GetNodeIncluded(int x, int y)
        {
            if (x < 0 || x > (xDim - 1) || y < 0 || y > (yDim - 1))
            {
                return null;
            }
            return positions[x, y].GetIncluded();
        }
    }
}
