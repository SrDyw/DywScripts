using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Utilities
{
    public static Vector3 StringToVector(string vectorString) {
        string[] values = vectorString.Split(':');

        return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
    }

    public static string VectorToString(Vector3 vector) {
        return vector.x + ":" + vector.y + ":" + vector.z;
    }

    public static Vector3 RandomNavMeshPoint(Vector3 origin, float radius, int areaMask) {
        // Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * radius;
        NavMeshHit navHit;
        int iterations = 0;
        var offsetOrigin = origin + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));

        while(!NavMesh.SamplePosition(offsetOrigin, out navHit, radius, areaMask)) {
            offsetOrigin += new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
            iterations++;
            if (iterations > 100) {
                Debug.LogWarning("Bucle Error");
                return origin;
            }
        }
        
        return navHit.position;
    }

    public static Vector3 GetRandomVector3(float scaler = 1) {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * scaler;
    }

    public static Vector2 GetRandomVector2(float scaler = 1) {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * scaler;
    }
}
