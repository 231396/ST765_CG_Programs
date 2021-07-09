using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformations : MonoBehaviour
{
    private void Start()
    {
        Test();
    }

    const int cos90 = 0;
    const int sin90 = 1;

    private void Test()
    {
        var origin = new Vector4(0, 10, 10, 1);
        var originFinal = new Vector4(10, 10, 0, 1);
        var inv = InvertV4(origin);

        var p = new Vector4(-2.5f, 20, 7.5f, 1);

        var t1 = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), inv);

        var e1 = new Matrix4x4(new Vector4(2, 0, 0, 0), new Vector4(0, .5f, 0, 0), new Vector4(0, 0, 2, 0), new Vector4(0, 0, 0, 1));
       
        var r1 = Rx(cos90, sin90);

        var r2 = Rz(cos90, sin90);

        //var t2 = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(10, 10, 0, 1));

        var t2 = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), origin);
        print(Diff(origin, originFinal));
        var t3 = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), Diff(origin, originFinal));

        var mt = t3 * t2 * r2 * r1 * e1 * t1;
        print(mt);
        print(mt * p);
    }

    Vector4 InvertV4(Vector4 v) => new Vector4(-v.x, -v.y, -v.z, 1);
    Vector4 Diff(Vector4 a, Vector4 b) => new Vector4(b.x - a.x, b.y - a.y, b.z - a.z, 1);

    Matrix4x4 Rx(float c, float s) => 
        new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, c, s, 0), new Vector4(0, -s, c, 0), new Vector4(0, 0, 0, 1));

    Matrix4x4 Ry(float c, float s) => 
        new Matrix4x4(new Vector4(c, 0, -s, 0), new Vector4(0, 1, 0, 0), new Vector4(s, 0, c, 0), new Vector4(0, 0, 0, 1));

    Matrix4x4 Rz(float c, float s) => 
        new Matrix4x4(new Vector4(c, s, 0, 0), new Vector4(-s, c, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
}
