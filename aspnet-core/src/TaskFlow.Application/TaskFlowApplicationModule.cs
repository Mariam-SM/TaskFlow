using Microsoft.Extensions.DependencyInjection;
using Qdrant.Client;
using System;
using TaskFlow.Ai;
using TaskFlow.Ai.Embeddings;
using TaskFlow.Ai.Indexing;
using TaskFlow.Ai.RAG;
using TaskFlow.Ai.Vector;
using TaskFlow.IServices;
using TaskFlow.IServices.AI;
using TaskFlow.IServices.AI.Embeddings;
using TaskFlow.IServices.AI.Indexing;
using TaskFlow.IServices.AI.RAG;
using TaskFlow.IServices.AI.Vector;
using TaskFlow.Services;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;


namespace TaskFlow;

[DependsOn(
    typeof(TaskFlowDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(TaskFlowApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class TaskFlowApplicationModule : AbpModule
{
    public override void ConfigureServices(
        ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TaskFlowApplicationModule>();
        });

        //context.Services.AddHttpClient<IAiService, AiService>();
        //context.Services.AddHttpClient<IEmbeddingService, OllamaEmbeddingService>();
        context.Services.AddScoped<ITaskRagService, TaskRagService>();

        context.Services.AddScoped< ITaskEmbeddingIndexer, TaskEmbeddingIndexer>();
        //context.Services.AddHttpClient<ITaskVectorStore, QdrantTaskVectorStore>();
        context.Services.AddSingleton(new QdrantClient("localhost", 6334));
        context.Services.AddHttpClient<QdrantTaskVectorStore>();
        context.Services.AddScoped<ITaskVectorStore, QdrantTaskVectorStore>();
        context.Services.AddHttpClient<IOpenAiService, OpenAiService>();

        context.Services.AddScoped<IIntentDetectorService, IntentDetectorService>();

        context.Services.AddHttpClient<IAiService, AiService>(client =>
        {
            client.Timeout = TimeSpan.FromMinutes(10);
        });

        context.Services.AddHttpClient<IEmbeddingService, OllamaEmbeddingService>(client =>
        {
            client.Timeout = TimeSpan.FromMinutes(10);
        });
    }
}
