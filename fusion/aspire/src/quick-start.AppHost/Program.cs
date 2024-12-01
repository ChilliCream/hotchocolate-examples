var builder = DistributedApplication.CreateBuilder(args);

var ordering = builder.AddProject<Projects.quick_start_Ordering>("ordering");
var products = builder.AddProject<Projects.quick_start_Products>("products");

builder
    .AddFusionGateway<Projects.quick_start_Gateway>("gateway")
    .WithSubgraph(ordering)
    .WithSubgraph(products);

// Watch out. It's not just `builder.Build().Run();` anymore!
builder.Build().Compose().Run();