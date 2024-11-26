public class WorldData
{
    Dictionary<string, ChunkData> chunkDictionary = new Dictionary<string, ChunkData>();
    

    public void AddChunkToDictionary(int[] chunkPos, ChunkData newChunk)
    {
        string posToCreate = chunkPos[0] + "," + chunkPos[1];
        chunkDictionary.Add(posToCreate, newChunk);
    }

    public ChunkData GetChunkAtPosition(int[] chunkPos)
    {
        string posToCheck = chunkPos[0] + "," + chunkPos[1];
        return chunkDictionary[posToCheck];
    }
}
