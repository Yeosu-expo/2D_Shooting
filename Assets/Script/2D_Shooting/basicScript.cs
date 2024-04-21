using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BasicFunc {
    public class BasicScript : MonoBehaviour
    {
        static float MaxX = 3;
        static float MinX = -3;
        static float MaxY = 5;
        static float MinY = -5;
        // Start is called before the first frame update
        static public bool IsOut(GameObject obj)
        {
            Vector2 pos = obj.transform.position;

            if (pos.x > MaxX || pos.x < MinX) { return true; }

            if (pos.y > MaxY || pos.y < MinY) { return true; }

            return false;
        }

        static public bool IsOut(GameObject obj, float MaxX, float MinX, float MaxY, float MinY)
        {
            Vector2 pos = obj.transform.position;

            if (pos.x > MaxX || pos.x < MinX) { return true; }

            if (pos.y > MaxY || pos.y < MinY) { return true; }

            return false;
        }

        static public bool IsOut(GameObject obj, Vector3 vec, float MaxX, float MinX, float MaxY, float MinY)
        {
            Vector2 pos = obj.transform.position + vec;

            if (pos.x > MaxX || pos.x < MinX) { return true; }

            if (pos.y > MaxY || pos.y < MinY) { return true; }

            return false;
        }
    }
}
