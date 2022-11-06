# Mandatory Hand-in 2

## 1.

Hash based Commitments over TLS.

- Integrity: HMAC (SHA3)
- Authenticity and Confidentiallity: Diffie-Hellman to AES

### TLS

TLS uses cryptography to create a secure channel that ensures, Authenticity,
Integrity and Confidentiality.

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
