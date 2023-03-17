using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProjectNet.src
{
    internal class StaticRouter
    {
        private RoutingMap map;

        private List<Node>  openList;

        //This array indicates if a Node has been tested (true), or is still available (false)
        private bool[] closedList;


        public StaticRouter()
        {
            map = RoutingMap.GetMapInstance();
            closedList = new bool[map.getNumNodes()];
            openList = new List<Node>();
        }

        private double CalculateDistance(Node node)
        {
            double distance = node - this.map.getEnd();
            node.SetDistanceWeight(distance);
            return distance;
        }

        private bool OnOpen(Node node)
        {
            for(int i = 0; i< openList.Count(); i++)
            {
                if(node.getId() == openList[i].getId())
                {
                    return true;
                }
            }
            return false;
        }

        private void TracePath()
        {
            Node current = this.map.getEnd();
            while(current != null)
            {
                Debug.WriteLine($"{current.GetCoords().x} {current.GetCoords().y}");
                current.SetIncluded(true);
                current = current.getPrevNode();
            }
        }

        //Returns true if a path is found, false otherwise
        public bool OptimizePath()
        {
            //Might not need a reset, can just create new class instance
            if(map.getStart().GetStatus() == NodeStatus.Obstacle || map.getEnd().GetStatus() == NodeStatus.Obstacle)
            {
                Debug.WriteLine("Either the start or end points are obstacles");
                return false;
            }
            //Starting with the start Node
            openList.Add(map.getStart());

            //Keep searching until the open list is empty or a path is found
            while(openList.Count > 0)
            {
                Node testNode = openList[0];
                int index = 0;

                //Find the next node with the shortest distance to the end and use that as the testNode
                for(int i = 0; i<openList.Count; i++)
                {
                    if (openList[i].GetTotalHeuristic() < 0)
                    {
                        this.CalculateDistance(openList[i]);
                    }
                    if ((openList[i].GetDistanceTravelled() + openList[i].GetTotalHeuristic()) < (testNode.GetDistanceTravelled() + testNode.GetTotalHeuristic()))
                    {
                        testNode = openList[i];
                        index = i;
                    }
                }

                //We have determined the next node that should be tested
                //First check if it is the end node, if so end the function and set the final path
                if(testNode.getId() == this.map.getEnd().getId())
                {
                    TracePath();
                    return true;
                }

                if(testNode == null)
                {
                    Debug.WriteLine("testNode was null");
                    return false;
                }

                //The end has not been reached, remove current node from open list and add to closed list
                openList.RemoveAt(index);
                closedList[testNode.getId()] = true;

                List<Node> neighbours = map.getNeighbours(testNode);

                for(int i = 0; i < neighbours.Count; i++)
                {
                    //skip neighbours that are obstacles or already been visited on the closed list
                    if (neighbours[i].GetStatus() == NodeStatus.Obstacle || closedList[neighbours[i].getId()] == true)
                    {
                        continue;
                    }
                    //Add all new neighbours to the open list and update any already present
                    double newDistance = testNode.GetDistanceTravelled() + neighbours[i].EuclideanDistance(testNode);
                    if (OnOpen(neighbours[i]))
                    {
                        //If the node is already on the open list, but this is a faster path update it to the new distance and new prev
                        if(newDistance < neighbours[i].GetDistanceTravelled())
                        {
                            neighbours[i].SetDistanceTravelled(newDistance);
                            neighbours[i].SetPrevNode(testNode);
                        }
                    }
                    //node is not on the open list, add it with the testNode as the previous node in its path
                    else
                    {
                        neighbours[i].SetDistanceTravelled(newDistance);
                        neighbours[i].SetPrevNode(testNode);
                        openList.Add(neighbours[i]);

                    }
                }
            }


            return false;
        }

    }
}
