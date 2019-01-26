using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public int x;
    public int y;
    public int width;
    public int height;
    public bool isCorridor;
    public bool isHorizontal;

    public Room(int nodeX, int nodeY, int nodeWidth, int nodeHeight)
    {
        isCorridor = false;
        isHorizontal = false;

        x = nodeX + 1;
        y = nodeY + 1;
        width = nodeWidth - 2;
        height = nodeHeight - 2;
    }

    public Room(int nodeX, int nodeY, int nodeWidth, int nodeHeight, int nodeX_A, int nodeY_A, int nodeX_B, int nodeY_B)
    {
        isCorridor = true;

        if (nodeX_A == nodeX_B)
        {       //they are divided horizontally
            isHorizontal = false;

            x = nodeX_A + nodeWidth / 2;
            y = (nodeY_B > nodeY_A) ? nodeY_B - 1 : nodeY_A - 1;
            width = 1;
            height = 2;
        }
        else
        {                          //divided vertically
            isHorizontal = true;

            x = (nodeX_B > nodeX_A) ? nodeX_B - 1 : nodeX_A - 1;
            y = nodeY_A + nodeHeight / 2;
            width = 2;
            height = 1;
        }

    }
}
