var builder = DistributedApplication.CreateBuilder(args);

var ordering = builder.AddProject<Projects.quick_start_Ordering>("ordering");
var products = builder.AddProject<Projects.quick_start_Products>("products");

builder.Build().Run();