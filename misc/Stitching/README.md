# Schema Stitching Examples

The following examples show different approaches to schema stitching. In general, there are two kinds you can build your gateway. Either you put the stitching logic into the gateway or distribute the stitching logic to the downstream services. As always, we let you also have stitching logic in the gateway and the downstream services.

**Why should I have stitching logic in my gateway?**
There are various reasons to have stitching logic in your gateway like you want to have a general logic that rewrites types based on patterns. The most common reason is you do not have control over the downstream service; this could be due to the downstream service being controlled by another department that uses a completely different toolset that does not support Hot Chocolate remote schema configurations. But there are a lot of other reasons to keep part or all stitching configurations in the gateway.

**Why should I have stitching logic in my downstream services?**
Again there are lots of reasons to organize your schema in a federated configuration. Distributing schema configurations to the downstream services gives you the flexibility to organize the configurations and schema parts to which they belong. Changing your stitching configuration does not force you to redeploy your gateway, which gives you more flexibility with stitched schemas.

## Centralized Configuration

`./centralized`

The folder `centralized` shows you a schema stitching example where we put the schema configuration into the gateway. The downstream services do not have any knowledge that they are being stitched into a larger schema.

## Federated Configuration through GraphQL

`./federated-with-pull`

The simplest way to create a federated schema configuration is to publish the configuration on the downstream service's schema. A downstream service can expose multiple configurations for multiple gateways. 
The gateway will automatically detect if your downstream service exposes a configuration and uses the configuration instead of auto-stitching the schema.

## Federated Configuration through Redis

`./federated-with-hot-reload`

Another more powerful solution is to publish schema configurations to Redis. This will keep the schema of your downstream service clean and allow you to hot-reload the gateway schema whenever there is a change in the downstream services. Moreover, we will keep the schema configuration on Redis. If you have a problem with your downstream service and it becomes offline, the gateway schema definition is no longer affected.

