const tls = require('tls');
const fs = require('fs');

const options = { 
    key: fs.readFileSync('server-key.pem'), 
    cert: fs.readFileSync('server-crt.pem'), 
    ca: fs.readFileSync('ca-crt.pem'), 
    // Servers may set requestCert to true to request a client certificate
    requestCert: true,
    // The server will reject any connection which is not authorized
    // with the list of supplied CAs
    rejectUnauthorized: true
}; 

const server = tls.createServer(options, (socket) => {
    console.log('server connected', 
        socket.authorized ? 'authorized' : 'unauthorized');
    
    socket.on('error', (error) => {
        console.log(error);
    });

    socket.on('data', (data) => {
        var newdata = data+""
        var newdatachunks = newdata.split("\n");
        for (var i = 0 ; i<(newdatachunks.length-1);i++)
            console.log(`${newdatachunks[i]+""}`)
    })

    socket.setEncoding('utf8');
    process.stdin.pipe(socket);
    socket.pipe(socket);
});

server.listen(8000, () => {
    console.log('server bound');
});
