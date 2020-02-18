# Generic services for microservices development as cluster

According to the article: [Logging with ElasticSearch, Kibana, ASP.NET Core and Docker](https://www.humankode.com/asp-net-core/logging-with-elasticsearch-kibana-asp-net-core-and-docker)


## Development

```sh
$ docker-compose up
```

# Production

```sh
$ docker-compose -f docker-compose.yml -f docker-compose.prod.yml up
```

# Standalone without docker-compose

```sh
$ docker run --cap-add=IPC_LOCK -e VAULT_ADDR=http://localhost:8200 -p \ 8200:8200 vault
```