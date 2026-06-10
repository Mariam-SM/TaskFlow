using Qdrant.Client;
using Qdrant.Client.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskFlow.Entities.TaskItems;
using TaskFlow.IServices.AI.Vector;

namespace TaskFlow.Ai.Vector
{
    public class QdrantTaskVectorStore : ITaskVectorStore
    {
        private const string CollectionName = "tasks";
        private readonly QdrantClient _client;

        public QdrantTaskVectorStore(QdrantClient client)
        {
            _client = client;
        }

        public async Task EnsureCollectionExistsAsync()
        {
            var collections = await _client.ListCollectionsAsync();

            var exists = collections.Contains(CollectionName);

            if (!exists)
            {
                await _client.CreateCollectionAsync(
                collectionName: CollectionName,
                vectorsConfig: new VectorParams
                {
                    Size = 768,
                    Distance = Distance.Cosine
                });
            }
        }

        public async Task UpsertAsync(Guid taskId, float[] embedding, string title, string description)
        {
            await EnsureCollectionExistsAsync();

            await _client.UpsertAsync(
                collectionName: CollectionName,
                points: new[]
                {
                    new PointStruct
                    {
                        Id = taskId,

                        Vectors = embedding,

                        Payload =
                        {
                            ["title"] = title,
                            ["description"] = description
                        }
                    }
                });
        }

        public async Task<List<Guid>> SearchAsync(float[] queryEmbedding, int topK = 5)
        {
            await EnsureCollectionExistsAsync();

            var results = await _client.SearchAsync(
                collectionName: CollectionName,
                vector: queryEmbedding,
                limit: (ulong)topK
            );

            Console.WriteLine("SEARCHING QDRANT...");


            return results
                .Select(x => Guid.Parse(x.Id.Uuid)) .ToList();
        }
    }
}
