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

I use a library node:tls to set up a server/client session with two-way
authentication.

I use openssl to generate certificates for tls.

### Hash based Commitments

Alice and Bob, decide on a Cryptographic Hash Function $H$ a number $k$ and a
function $R: n,m \to x$ where $n$ and $m$ is numbers in the range 1-6 and $x$ 
is a number between 1 and 6.

- I've chosen $H$ to be System.Security.Cryptography.SHA512::ComputeHash from
dotnet.
- I've chosen $k$ to be 512
- I've chosen $R : n,m \to 1 + (((n-1) + (m-1)) \mod 6)$

Alice generates a random number $m$

Alice generates a salt $s$ of length $k-1$

Alice concatenates $s$ and $m$ to get $s|m$

Alice hashes $s|m$ with $H$ to get $H(s|m) = c$

Alice sends $c$ to Bob

Bob generates a a random number $n$

Bob send $n$ to Alice

Alice sends $s$ to Bob

Bob uses $s$ and the previous message containing $m$ to compute $H(s|m)$ and
compares to the previous message $c$

Alice and Bob computes the dice outcome $R(m,n)$

All messages are sent over TLS
