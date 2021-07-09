using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Tester : MonoBehaviour
{
    class Pixel
    {
        public readonly string letter;
        public readonly Vector2 vector;
        public Pixel(char letter, int x, int y)
        {
            this.letter = letter.ToString();
            vector = new Vector2(x, y);
        }
    }

    private void Start()
    {
        var used = new HashSet<Pixel>();
        var stack = new Stack<Pixel>();
        stack.Push(pixels[0]);
        used.Add(pixels[0]);

        string printout = "";
        int loop = 0;
        while(stack.Count != 0)
        {
            for (int i = stack.Count - 1; i >= 0; i--)
                printout += stack.ElementAt(i).letter + ", ";
            printout += '\t';

            var current = stack.Pop();
            printout += current.letter + '\t';

            var neig = GetNeighbors(current.vector);
            foreach (var item in neig)
            {
                printout += PixelName(item, out var pixel) + ", ";
                if (pixel != null && !used.Contains(pixel))
                {
                    stack.Push(pixel);
                    used.Add(pixel);
                }
            }
            printout += '\t';

            printout += current.letter + '\t';

            printout += ++loop + "\r\n";
        }
        print(printout);
    }

    Vector2[] GetNeighbors(Vector2 current) => new Vector2[4]
    {
        new Vector2(current.x, current.y + 1),
        new Vector2(current.x + 1, current.y),
        new Vector2(current.x, current.y - 1),
        new Vector2(current.x - 1, current.y),
    };

    string PixelName(Vector2 vector, out Pixel pixel)
    {
        foreach (var item in pixels)
            if (item.vector.x == vector.x && item.vector.y == vector.y)
            {
                pixel = item;
                return item.letter;
            }
        pixel = null;
        return BorderName(vector);
    }
    string BorderName(Vector2 vector)
    {
        foreach (var item in border)
            if (item.x == vector.x && item.y == vector.y)
                return "CON";
        return "None";
    }

    readonly Pixel[] pixels = new Pixel[6]
    {
        new Pixel('S', 2, 3),
        new Pixel('A', 2, 4),
        new Pixel('B', 3, 3),        
        
        new Pixel('C', 2, 2),
        new Pixel('D', 3, 2),
        new Pixel('E', 4, 2),
    };

    readonly Vector2[] border = new Vector2[15]
    {
        new Vector2(1, 1),
        new Vector2(1, 6),
        new Vector2(6, 1),

        new Vector2(1, 2),
        new Vector2(1, 3),
        new Vector2(1, 4),
        new Vector2(1, 5),

        new Vector2(2, 1),
        new Vector2(3, 1),
        new Vector2(4, 1),
        new Vector2(5, 1),

        new Vector2(2, 5),
        new Vector2(3, 4),
        new Vector2(4, 3),
        new Vector2(5, 2),
    };

}
