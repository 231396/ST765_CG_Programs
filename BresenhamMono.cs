using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BresenhamMono : MonoBehaviour
{
    public Vector2Int v0;
    public Vector2Int v1;

    void Start()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        print("Default\r\n" + Bresenham());
        print("Generic\r\n" + BresenhamGeneric());
    }

    string Bresenham()
    {
        var deltaX = Mathf.Abs(v1.x - v0.x);
        var deltaY = Mathf.Abs(v1.y - v0.y);

        var m = Math.Abs(deltaY / (decimal)deltaX);
        decimal e = m - .5m;

        string str = string.Empty;
        if (0 > deltaY && deltaY > deltaX)
        {
            print("Invalid DX e DY");
            return str;
        }


        int x = v0.x; 
        int y = v0.y;
        str += Plot(x, y, x, y, -1, e);
        for (int i = 1; i <= deltaX; i++)
        {
            int px = x;
            int py = y;
            while (e > 0)
            {
                y++;
                e -= 1;
                str += Plot(px, py, x, y, i, e);
            }
            x++;
            e += m;
            str += Plot(px, py, x, y, i, e);
        }
        return str;
    }
    string Plot(int px, int py, int x, int y, int i, decimal e)
    {
        return $"{i}\t({px}, {py})\t{e:0.000}\t{x}\t{y}\r\n";
    }

    string BresenhamGeneric()
    {
        var deltaX = Mathf.Abs(v1.x - v0.x);
        var deltaY = Mathf.Abs(v1.y - v0.y);
        var s1 = Sign(v1.x - v0.x);
        var s2 = Sign(v1.y - v0.y);
        string str = string.Empty;

        bool interchange = false;
        if (deltaY > deltaX)
        {
            var temp = deltaX;
            deltaX = deltaY;
            deltaY = temp;
            interchange = true;
        }
        var e = 2 * deltaY - deltaX;

        int x = v0.x;
        int y = v0.y;
        str += Plot(0, 0, x, y, -1, e);
        for (int i = 1; i <= deltaX; i++)
        {
            int px = x;
            int py = y;
            while (e > 0)
            {
                if (interchange)
                    x += s1;
                else
                    y += s2;
                e -= 2 * deltaX;
                str += Plot(px, py, x, y, i, e);
            }
            if(interchange)
                y += s2;
            else
                x += s1;
            e += 2 * deltaY;
            str += Plot(px, py, x, y, i, e);
        }
        return str;
    }
    int Sign(float value)
    {
        if (value < 0)
            return -1;
        else if (value > 0)
            return 1;
        else
            return 0;
    }

}
