# Mandatory assignment 2

A example 

## Usage

`docker-compose build && docker-compose up`

### Requirements

- docker
- docker-compose

### Docker teardown and cleanup

- `docker-compose down && docker rmi m2-client m2-server`

## Report

Hash based Commitments over TLS.

### TLS

TLS uses cryptography to create a secure channel that ensures, Authenticity,
Integrity and Confidentiality.

I use a node library to set up a server and a client with two-way tls.

I use openssl to generate certificates for tls

### Hash based Commitments

Alice and Bob, decide on a Cryptographic Hash Function $H$ a number $k$ and a
function $R: n,m \to x$ where $n$ and $m$ is bit strings and $x$ is a number
between 1 and 6.

Alice and Bob agrees on $R: n,m \to 1 + ((n \oplus m) \mod 6)$

Alice generates a random number encoded as a bit string $m$ of length $k$

Alice generates a bit string with random noise $r$

Alice concatenates $r$ and $m$ to get $r|m$

Alice hashes $r|m$ with $H$ to get $H(r|m) = c$

Alice sends $c$ to Bob

Bob generates a random number encoded as a bit string $n$ of length $k$

Bob send $n$ to Alice

Alice sends $r|m$

Bob uses $r|m$ to compute $H(r|m)$ and compares to the previous message $c$

Alice and Bob computes the dice outcome $R(m,n)$
