using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProjectNet.src
{
    //Enum to save the status of nodes
    internal enum NodeStatus
    {
        Free,
        Obstacle,
        Start,
        End
    }
    //Struct to save the coordinates of a point
    internal struct Point
    {
        public int x, y;

        public Point (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    //Class that stores the information about each Node in the routing map
    internal class Node
    {
        static private int counter = 0;
        private Point coords;
        private Node prevNode = null;
        private double distanceHeuristic = -1;
        private double totalHeuristic = -1;
        private NodeStatus status;
        private int id;
        private double distanceTravelled = 0;
        private bool includedPath = false;


        public Node(int x, int y, NodeStatus status)
        {
            this.coords = new Point(x, y);
            this.status = status;
            this.id = Node.counter;
            Node.counter++;
        }

        public void SetStatus(NodeStatus status)
        {
            this.status = status;
        }

        public static void ResetCounter()
        {
            Node.counter = 0;
        }

        public void SetPrevNode(Node prev)
        {
            this.prevNode = prev;
        }

        public Point GetCoords()
        {
            return coords;
        }

        public NodeStatus GetStatus()
        {
            return status;
        }

        public int getId()
        {
            return id;
        }

        public Node getPrevNode()
        {
            return prevNode;
        }

        public void SetDistanceWeight(double weight)
        {
            this.distanceHeuristic = weight;
            this.totalHeuristic += weight;
        }

        public double GetTotalHeuristic()
        {
            return totalHeuristic;
        }

        public void SetDistanceTravelled(double distance)
        {
            this.distanceTravelled = distance;
        }

        public double GetDistanceTravelled()
        {
            return distanceTravelled;
        }

        public double EuclideanDistance(Node other)
        {
            double xDist = Math.Abs(this.coords.x - other.coords.x);
            double yDist = Math.Abs(this.coords.y - other.coords.y);
            return Math.Sqrt(Math.Pow(xDist, 2) + Math.Pow(yDist, 2));
        }

        public static double operator -(Node left, Node right)
        {
            double xDist = Math.Abs(left.coords.x - right.coords.x);
            double yDist = Math.Abs(left.coords.y - right.coords.y);
            return Math.Sqrt(Math.Pow(xDist, 2) + Math.Pow(yDist, 2));
        }

        public bool GetIncluded()
        {
            return includedPath;
        }
    }
}
