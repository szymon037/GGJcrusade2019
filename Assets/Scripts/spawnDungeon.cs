using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnDungeon : MonoBehaviour
{
    //private char arr[10, 10];

    public int width = 100;
    public int height = 100;

    

    public GameObject tilePrefab;
    public GameObject sideWallPrefab;
    public GameObject upWallPrefab;
    public GameObject cornerWallPrefab;

    /*public Vector3 rotationOfTilePrefab= new Vector3(90f, 0f, 0f);
    public Vector3 rotationOfWallPrefab = new Vector3(45f, 0f, 0f);*/
    void Start()
    {
        char[,] map = new char[width, height];

        Node root = new Node(0, 0, width, height, 4);

        foreach (Room i in Node.rooms)
        {
            for (int k = 0; k < i.height; ++k)
                for (int j = 0; j < i.width; ++j)
                    if (k == 0)
                        map[i.y + k, i.x + j] = 'w';
                    else if (k == i.height - 1)
                        map[i.y + k, i.x + j] = 's';
                    else if (j == 0)
                        map[i.y + k, i.x + j] = 'a';
                    else if (j == i.width - 1)
                        map[i.y + k, i.x + j] = 'd';

                    else
                        map[i.y + k, i.x + j] = 'X';
        }

        foreach (Room i in Node.rooms)
        {
            if (i.isCorridor)
                for (int k = 0; k < i.height; ++k)
                    for (int j = 0; j < i.width; ++j)
                    {
                        map[i.y + k, i.x + j] = 'X';
                        if (i.isHorizontal)
                        {
                            map[i.y + k, i.x - 1] = 'X';
                            map[i.y + k, i.x + i.width] = 'X';

                            map[i.y + k + 1, i.x + j] = 's';
                            map[i.y + k - 1, i.x + j] = 'w';

                            if (map[i.y + k + 1, i.x - 2] == 'X')
                                map[i.y + k + 1, i.x - 1] = (char)257;
                            else
                                map[i.y + k + 1, i.x - 1] = 's';

                            /*if (map[i.y + k - 1, i.x - 2] == 'X')
                                map[i.y + k - 1, i.x - 1] = (char)259;
                            else*/
                                map[i.y + k - 1, i.x - 1] = 'w';

                            if (map[i.y + k + 1, i.x + i.width + 1] == 'X')
                                map[i.y + k + 1, i.x + i.width] = (char)258;
                            else
                                map[i.y + k + 1, i.x + i.width] = 's';

                           /* if (map[i.y + k - 1, i.x + i.width + 1] == 'X')
                                map[i.y + k - 1, i.x + i.width] = (char)260;
                            else*/
                                map[i.y + k - 1, i.x + i.width] = 'w';
                        }
                        else
                        {
                            map[i.y - 1, i.x + j] = 'X';
                            map[i.y + i.height, i.x + j] = 'X';

                            map[i.y + k, i.x + j + 1] = 'd';
                            map[i.y + k, i.x + j - 1] = 'a';



                            /*if (map[i.y + k + 2, i.x + j + 1] == 'X')
                                map[i.y + k + 1, i.x + j + 1] = (char)259;
                            else*/
                                map[i.y + k + 1, i.x + j + 1] = 'w';    //d


                            /*if (map[i.y + k + 2, i.x + j - 1] == 'X')
                                map[i.y + k + 1, i.x + j - 1] = (char)260;
                            else*/
                                map[i.y + k + 1, i.x + j - 1] = 'w';    //a

                            if (map[i.y - i.height, i.x + 1] == 'X')
                                map[i.y - i.height + 1, i.x + 1] = (char)257;
                            else
                                map[i.y - i.height + 1, i.x + 1] = 'd';

                            if (map[i.y - i.height, i.x - 1] == 'X')
                                map[i.y - i.height + 1, i.x - 1] = (char)258;
                            else
                                map[i.y - i.height + 1, i.x - 1] = 'a';

                        }

                    }


            /*for (int k = 0; k < i.height; ++k)
                for (int j = 0; j < i.width; ++j)
                {
                    map[i.y + k, i.x + j] = 'X';
                    if (i.isHorizontal)
                    {
                        map[i.y + k, i.x - 1] = 'X';
                        map[i.y + k, i.x + i.width] = 'X';
                    }
                    else
                    {
                        map[i.y - 1, i.x + j] = 'X';
                        map[i.y + i.height, i.x + j] = 'X';
                    }

                }*/
        }


            for (int k = 0; k < height; ++k)
                for (int j = 0; j < width; ++j)
                {
                    if (map[k, j] == 'X')
                        Instantiate(tilePrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(90, 0, 0));
                    else if (map[k, j] == 'w')
                        Instantiate(upWallPrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(45, 270, 0));
                    else if (map[k, j] == 's')
                        Instantiate(sideWallPrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(90, 270, 0));
                    else if (map[k, j] == 'a')
                        Instantiate(sideWallPrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(90, 0, 0));
                    else if (map[k, j] == 'd')
                        Instantiate(sideWallPrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(90, 180, 0));

                    else if (map[k, j] == (int) 257)
                        Instantiate(cornerWallPrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(90, 270, 0));
                    else if (map[k, j] == (int) 258)
                        Instantiate(cornerWallPrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(90, 0, 0));
                    else if (map[k, j] == (int) 259)
                        Instantiate(cornerWallPrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(90, 180, 0));
                    else if (map[k, j] == (int) 260)
                        Instantiate(cornerWallPrefab, new Vector3(k, 0f, j) * 0.32f, Quaternion.Euler(90, 90, 0));

            }

            /*foreach (Room room in Node.rooms)
        {
            for (int k = 0; k < room.height; ++k)
                for (int j = 0; j < room.width; ++j)
                {
                    
                    Instantiate(tilePrefab, new Vector3(room.y + k, 0f, room.x + j) * 0.32f, Quaternion.Euler(90, 0, 0));
                    //print("Corridor x: " + room.x + "Corridor y: " + room.y);
                    
                    //Instantiate(tilePrefab, new Vector3(j, 0f, k), Quaternion.identity);
                }
        }*/


            /*for(float i = 0; i < 10; ++i)
                for(float j = 0; j < 10; ++j)
                {
                    if(i%2 == 0 && j%2 == 0)
                        //if(j%2 == 1)
                            Instantiate(tilePrefab, new Vector3(j, i, 0f), Quaternion.identity);

                    else if(i % 2 == 1 && j % 2 == 1)
                        //if (j%2 == 0)
                            Instantiate(tilePrefab, new Vector3(j, i, 0f), Quaternion.identity);
                }*/
        }

        void Update()
        {
            //print(Random.Range(0, 999));
        }
}

