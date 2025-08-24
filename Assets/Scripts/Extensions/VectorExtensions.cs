using UnityEngine;

public static class VectorExtensions
{

    /// <summary>
    /// Takes an input <c>Vector2</c> and embeds it into <c>Vector3</c> space by appending a zero to the z-coordinate.
    /// </summary>
    /// <param name="vector">The <c>Vector2</c> to embed in <c>Vector3</c> space.</param>
    /// <returns>A <c>Vector3</c> with a zero in the z-coordinate.</returns>
    public static Vector3 EmbedToVector3(this Vector2 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }

    /// <summary>
    /// Takes an input <c>Vector3</c> and projects it onto <c>Vector2</c> space by removing the z-coordinate.
    /// </summary>
    /// <param name="vector">The <c>Vector3</c> to project onto <c>Vector2</c> space.</param>
    /// <returns>A <c>Vector2</c> with the original x and y-coordinates of the input.</returns>
    public static Vector2 ProjectToVector2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    /// <summary>
    /// Returns <c>true</c> if the input vector is the zero vector. Returns <c>false</c> otherwise.
    /// </summary>
    public static bool IsZeroVector(this Vector3 vector)
    {
        return vector == Vector3.zero;
    }
    
    /// <summary>
    /// Returns <c>true</c> if the input vector is the zero vector. Returns <c>false</c> otherwise.
    /// </summary>
    public static bool IsZeroVector(this Vector2 vector)
    {
        return vector == Vector2.zero;
    }
    
}
