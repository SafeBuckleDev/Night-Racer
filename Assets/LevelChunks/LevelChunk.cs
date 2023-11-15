using UnityEngine;

[CreateAssetMenu(fileName = "New Chunk", menuName = "New Chunk")]
public class LevelChunk : ScriptableObject {
    
    /// <summary>
    /// The prefab containing the level chunk.
    /// </summary>
    public GameObject chunkPrefab;
}
