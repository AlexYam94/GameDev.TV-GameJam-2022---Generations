using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByteArrayConverter
{
    private static readonly int POS_LENGTH = 8;

    public static byte[] ToByteArray(Vector2 position)
    {
        byte[] arr = new byte[POS_LENGTH];

        Buffer.BlockCopy(BitConverter.GetBytes(position.x), 0, arr, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(position.y), 0, arr, 4, 4);
        // Buffer.BlockCopy(BitConverter.GetBytes(transform.position.z), 0, arr, 8, 4);

        return arr;
    }

    public static byte[] ToByteArray(List<Vector2> positions)
    {
        byte[] arr = new byte[positions.Count * POS_LENGTH];
        for(int i = 0; i < positions.Count; i++){
            Buffer.BlockCopy(ToByteArray(positions[i]), 0, arr, POS_LENGTH * i, 8);
        }
        return arr;
    }

    public static Vector2 ToPosition(byte[] bytes)
    {
        byte[] x = new byte[4],y = new byte[4];
        Array.Copy(bytes, 0, x, 0, 4);
        Array.Copy(bytes, 4, y, 0, 4);
        // Array.Copy(bytes, 8, z, 0, 4);
        Vector2 position = new Vector2();
        position.x = BitConverter.ToSingle(x);
        position.y = BitConverter.ToSingle(y);
        // position.z = BitConverter.ToSingle(z);
        return position;
    }

    public static List<Vector2> ToPositions(byte[] bytes)
    {
        List<Vector2> positions = new List<Vector2>();
        for(int i = 0;i*POS_LENGTH < bytes.Length;i++){
            byte[] arr = new byte[8];
            Array.Copy(bytes, i * POS_LENGTH, arr, 0, POS_LENGTH);
            positions.Add(ToPosition(arr));
        }
        return positions;
    }
}
