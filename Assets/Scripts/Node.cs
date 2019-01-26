using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x = 0;
    public int y = 0;
    public int width = 0;
    public int height = 0;
    public int numberOfIterations = 0;
    public bool leafs = false;

    public static List<Node> nodes = new List<Node>();
    public static List<Room> rooms = new List<Room>();


    public Node(int X, int Y, int WIDTH, int HEIGHT, int ITER/*, Tree* TREE*/)
    {
        x = X;
        y = Y;
        width = WIDTH;
        height = HEIGHT;
        numberOfIterations = ITER;

        if (numberOfIterations <= 0 || (width < 10 && height < 10))
        {
            leafs = true;

            /*child_A = nullptr;
            child_B = nullptr;*/

            if ((width < 10) && (height < 10))
                Debug.Log("Width and height are too small! Aborting recursion.");

            rooms.Add(new Room(x, y, width, height));           //adding room

            //        Room_ptr room = std::make_shared<Room>(x, y, width, height);
            //Room* room = new Room(x, y, width, height);
            //tree->pushToRooms(room);
        }

        else if (numberOfIterations > 0)
        {
            leafs = false;
            --numberOfIterations;

            int xA = 0, yA = 0, widA = 0, heiA = 0, xB = 0, yB = 0, widB = 0, heiB = 0;
            int sign = (Random.Range(0, 999) % 2 == 1) ? (-1) : 1;

            // if ((rand()%2 == 0 && height >= 10) || width < 10 || height > 2*width && 2*height > width) {        //divide horizontally
            if (1.6 * width <= height || width < 10 || (Random.Range(0, 999) % 2 == 0 /*&& 2*width > height*/ && width >= 10 && 1.6 * height > width))
            {
                xA = x;
                yA = y;
                widA = width;

                if (height < 14)
                    heiA = (height / 2) + sign * Random.Range(0, 999) % ((height - 2 * 5) / 2 + 1);             //5 - minimum width or size of room
                else
                    heiA = ((height / 2) + sign * (Random.Range(0, 999) % ((height / 8) + 1)) + sign * 3);

                xB = xA;
                yB = yA + heiA;
                widB = widA;
                heiB = height - heiA;

            }
            else
            {                              //divide vertically
                xA = x;
                yA = y;
                heiA = height;

                if (width < 14)
                    widA = (width / 2) + sign * Random.Range(0, 999) % ((width - 2 * 5) / 2 + 1);             //5 - minimum width or size of room
                else
                    widA = (width / 2) + sign * (Random.Range(0, 999) % ((width / 8) + 1)) + sign * 3;

                xB = xA + widA;
                yB = yA;
                heiB = heiA;
                widB = width - widA;




            }

            nodes.Add(new Node(xA, yA, widA, heiA, numberOfIterations));     //adding child node A
            nodes.Add(new Node(xB, yB, widB, heiB, numberOfIterations));     //adding child node B

            rooms.Add(new Room(x, y, width, height, xA, yA, xB, yB));       //adding corridor
        }
    }
}
