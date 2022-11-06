# Prepare
openssl req -new -x509 -days 1 -keyout ca-key.pem -out ca-crt.pem
# ca_password=qY2EvezssnBeaodsjxU
# ca_cn=ca.localhost

### SERVER ###
# Server key
openssl genrsa -out server-key.pem 4096

# Server cert sign req
openssl req -new -key server-key.pem -out server-csr.pem
# ca_cn=localhost
# ca_password=

# Sign
openssl x509 -req -days 1 -in server-csr.pem -CA ca-crt.pem -CAkey ca-key.pem -CAcreateserial -out server-crt.pem
# ca_password=qY2EvezssnBeaodsjxU

# Verify
openssl verify -CAfile ca-crt.pem server-crt.pem

### CLIENT ###
# Client key
openssl genrsa -out client1-key.pem 4096

# Client cert sign req
openssl req -new -key client1-key.pem -out client1-csr.pem
# cn=client.localhost
# ca_password=qY2EvezssnBeaodsjxU

# Sign
openssl x509 -req -days 1 -in client1-csr.pem -CA ca-crt.pem -CAkey ca-key.pem -CAcreateserial -out client1-crt.pem
# ca_password=qY2EvezssnBeaodsjxU

# Verify
openssl verify -CAfile ca-crt.pem client1-crt.pem
