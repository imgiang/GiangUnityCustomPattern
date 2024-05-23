using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Helper : MonoBehaviour
{

    public static IEnumerator StartAction(UnityAction action, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        action();
    }

    public static IEnumerator StartAction(UnityAction action, System.Func<bool> condition)
    {
        yield return new WaitUntil(condition);
        action();
    }
    public static void LookAtToDirection(Vector3 diretion, GameObject objLookAt, float speedLockAt = 500)
    {
        float xTarget = diretion.x;
        float yTarget = diretion.y;
        float angle = Mathf.Atan2(yTarget, xTarget) * Mathf.Rad2Deg + 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        objLookAt.transform.rotation = Quaternion.Slerp(objLookAt.transform.rotation, q, Time.deltaTime * speedLockAt);
    }
    public static class IListExtensions
    {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle(List<Transform> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
        public static void Shuffle(List<Vector2> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
    }

    public static byte[] ReadFile(string path)
    {
        byte[] result;
        using (FileStream stream = File.OpenRead(path))
        {
            result = new byte[stream.Length];
            stream.Read(result, 0, (int)stream.Length);
        }
        return result;
    }
    public static Texture2D CreateTextureFromBytes(byte[] bytes)
    {
        //Debug.Log("bytes " + bytes.Length);
        Texture2D tex = new Texture2D(2, 2, TextureFormat.RGB24, false);
        tex.filterMode = FilterMode.Point;
        tex.LoadImage(bytes);
        return tex;
    }
    public static async UniTask<byte[]> ReadFileAsync(string path)
    {
        byte[] result;
        using (FileStream stream = File.OpenRead(path))
        {
            result = new byte[stream.Length];
            await stream.ReadAsync(result, 0, (int)stream.Length);
        }
        return result;
    }

    public static async UniTask WriteBytesAsync(string path, byte[] data)
    {
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            await stream.WriteAsync(data, 0, data.Length);
        }
    }

    public static async void WriteBytesAsync(string path, byte[] data, UnityAction finishedCallback)
    {
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            await stream.WriteAsync(data, 0, data.Length);
        }
        finishedCallback?.Invoke();
    }


    public static void WriteBytes(string path, byte[] data)
    {
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            stream.Write(data, 0, data.Length);
        }
    }


    public static double TimeSpanTotalSeconds(DateTime newDate, DateTime oldDate)
    {
        TimeSpan timeSpan = newDate - oldDate;
        return timeSpan.TotalSeconds;
    }
    public static bool TimeOnDay(DateTime newDate, DateTime oldDate)
    {
        TimeSpan timeSpan = newDate - oldDate;
        return timeSpan.TotalDays > 0;
    }
}