# Adapted from source: https://engineering.circle.com/https-authorized-certs-with-node-js-315e548354a2#.24nmlit7w

# Create a new certificate authority
openssl req -new -x509 -days 99 -keyout ca-key.pem -out ca-crt.pem -subj '/CN=ca.localhost' -passout pass:qY2EvezssnBeaodsjxU

### SERVER ###
# Server private key
openssl genrsa -out server-key.pem 4096

# Server certificate signing request
openssl req -new -key server-key.pem -out server-csr.pem -subj '/CN=localhost'

# Sign the request using the certificate authority
openssl x509 -req -days 99 -in server-csr.pem -CA ca-crt.pem -CAkey ca-key.pem -CAcreateserial -out server-crt.pem -passin pass:qY2EvezssnBeaodsjxU

# Verify
openssl verify -CAfile ca-crt.pem server-crt.pem

mv server-crt.pem ./server/
mv server-csr.pem ./server/
mv server-key.pem ./server/
cp ca-crt.pem ./server/
cp ca-crt.srl ./server/
cp ca-key.pem ./server/

### CLIENT ###
# Client private key
openssl genrsa -out client1-key.pem 4096

# Client certificate signing request
openssl req -new -key client1-key.pem -out client1-csr.pem -subj '/CN=client.localhost' -passout pass:qY2EvezssnBeaodsjxU

# Sign the request using the certificate authority
openssl x509 -req -days 99 -in client1-csr.pem -CA ca-crt.pem -CAkey ca-key.pem -CAcreateserial -out client1-crt.pem -passin pass:qY2EvezssnBeaodsjxU

# Verify
openssl verify -CAfile ca-crt.pem client1-crt.pem

mv client1-crt.pem ./client/
mv client1-csr.pem ./client/
mv client1-key.pem ./client/
mv ca-crt.pem ./client/
mv ca-crt.srl ./client/
mv ca-key.pem ./client/
