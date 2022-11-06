const tls = require('tls');
const fs = require('fs');
const crypto = require('crypto');

const options = {
    ca: fs.readFileSync('ca-crt.pem'),
    key: fs.readFileSync('client1-key.pem'),
    cert: fs.readFileSync('client1-crt.pem'),
    host: 'localhost',
    port: 8000,
    rejectUnauthorized:true,
    requestCert:true
};

const socket = tls.connect(options, () => {
    console.log('client connected', 
        socket.authorized ? 'authorized' : 'unauthorized');
    process.stdin.pipe(socket);
    process.stdin.resume();
});

socket.setEncoding('utf8');

socket.on('data', (data) => {
    var newdata = ""+data;
    var newdatachunks = newdata.split("\n");
    for (var i = 0 ; i<(newdatachunks.length-1);i++)
        console.log(newdatachunks[i]+"")
});

socket.on('error', (error) => {
    console.log(error);
});

socket.on('end', (data) => {
    console.log('Socket end event');
});