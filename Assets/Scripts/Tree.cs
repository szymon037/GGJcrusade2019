using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tree 
{

    public Node root;
    /*public List<Node*> nodes;
    public List<Room*> rooms;*/
    public int numberOfIterations;
    public int width;
    public int height;

    public Tree(int noOfIterations, int WID, int HEI)
    {
        numberOfIterations = noOfIterations;
        width = WID;
        height = HEI;
        root = new Node(0, 0, width, height, numberOfIterations/*, this*/);
        //nodes.push_back(root);
    }

    /*void pushToNodes(Node_ptr node)
    {
        nodes.push_back(node);
    }*/

    /*void pushToRooms(Room* room)
    {
        rooms.push_back(room);
    }*/

}
