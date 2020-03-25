#!/bin/sh

export VAULT_ADDR='http://0.0.0.0:8200'

vault login 00000000-0000-0000-0000-000000000000

vault secrets enable -version=2 -path=Common kv

vault kv put Common/Elasticsearch Url=http://localhost:9200/

vault kv put Common/Metrics Url=http://localhost:9091/metrics