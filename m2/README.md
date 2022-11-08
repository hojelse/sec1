# Hash based Commitments over TLS

## Usage

- Update keys using the script `keygen.sh`, if the keys are expired
- Run using `docker-compose build && docker-compose up`

### Requirements

- docker
- docker-compose

### Docker teardown and cleanup

- `docker-compose down && docker rmi m2-client m2-server`

More images might be created that you might want to clean up,
I don't quite know why they appear
