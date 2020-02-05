# Generic services for microservices development as cluster

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