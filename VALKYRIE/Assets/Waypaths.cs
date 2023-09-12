using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypaths : MonoBehaviour
{
    // Create a dictionary to store paths based on tags
    private static Dictionary<string, List<Transform>> waypointPaths = new Dictionary<string, List<Transform>>();

    // Public function to add a waypoint to a path based on a tag
    public static void AddWaypointToPath(string tag, Transform waypoint)
    {
        if (!waypointPaths.ContainsKey(tag))
        {
            waypointPaths[tag] = new List<Transform>();
        }
        waypointPaths[tag].Add(waypoint);
    }

    // Public function to get the waypoints for a specific path based on a tag
    public static Transform[] GetWaypointsForPath(string tag)
    {
        if (waypointPaths.ContainsKey(tag))
        {
            return waypointPaths[tag].ToArray();
        }
        else
        {
            Debug.LogError("Path with tag " + tag + " not found.");
            return null;
        }
    }

    // Example usage in the Awake method (you can add waypoints to specific paths in the Unity Editor)
    private void Awake()
    {
        // Example: Add waypoints to path with tag "A"
        Transform[] waypointsWithTagA = GetWaypointsWithTag("A");
        foreach (Transform waypoint in waypointsWithTagA)
        {
            AddWaypointToPath("A", waypoint);
        }

        // Example: Add waypoints to path with tag "B"
        Transform[] waypointsWithTagB = GetWaypointsWithTag("B");
        foreach (Transform waypoint in waypointsWithTagB)
        {
            AddWaypointToPath("B", waypoint);
        }
    }

    // Helper function to get waypoints by tag
    private Transform[] GetWaypointsWithTag(string tag)
    {
        List<Transform> waypointsWithTag = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.CompareTag(tag))
            {
                waypointsWithTag.Add(child);
            }
        }
        return waypointsWithTag.ToArray();
    }
}
