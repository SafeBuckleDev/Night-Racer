using UnityEngine;

/// <summary>
/// Spawner spawns track pieces at the start of the game.
/// </summary>
public class Spawner_normal : MonoBehaviour {
    
    [Tooltip("The amount of random chunks of track to place after eachother")]
    [SerializeField] private int numChunks = 100;
    
    [Tooltip("The level chunks that we are able to select from")]
    [SerializeField] private LevelChunk[] availableChunks;
    LevelChunk randomChunk;

    public void Start() {
        GenerateTrack(numChunks);
    }

    /// <summary>
    /// Generates the track by placing randomly selected tracks from the LevelChunk array after each other.
    /// It does this by looking at the endpoint of the track (based on the transform with a EndTag component),
    /// and the start point of the next track piece (based on the transform with a StartTag component), and
    /// aligning these two transforms with each other.
    /// </summary>
    /// <param name="chunkCount"></param>
    private void GenerateTrack(int chunkCount) {
        // Set start rotation and start position based on this generator object
        Vector3 previousEndPointPosition = transform.position;
        Quaternion previousEndPointRotation = transform.rotation;
        
        for (int i = 0; i < chunkCount; ++i) {
            // Select a random chunk
            int randomChunkIndex = Random.Range(0, availableChunks.Length);
            randomChunkIndex =i % availableChunks.Length;
            LevelChunk randomChunk = availableChunks[randomChunkIndex];

            // Spawn chunk
            GameObject spawnedChunk =
                Instantiate(randomChunk.chunkPrefab, previousEndPointPosition, previousEndPointRotation);
        
            // Find end and start points of the spawned chunk
            Transform startPoint = spawnedChunk.GetComponentInChildren<StartTag>()?.transform;
            Transform endPoint = spawnedChunk.GetComponentInChildren<EndTag>()?.transform;
            
            // Move the start chunk such that it aligns with the previous end point
            spawnedChunk.transform.position += previousEndPointRotation * -startPoint.localPosition;
            
            // Save end point as the new starting point
            previousEndPointPosition = endPoint.position;
            previousEndPointRotation = endPoint.rotation;
        }
    }
}
