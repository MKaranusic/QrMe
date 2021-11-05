"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.configureExpress = exports.getHttpsCredentials = exports.serverRun = void 0;
const express = require("express");
const path_1 = require("path");
exports.serverRun = () => {
    const httpPort = process.env.PORT || 50005;
    const http = require('http');
    const app = exports.configureExpress();
    const httpServer = http.createServer(app);
    httpServer.listen(httpPort, () => {
        console.log(`Node Express server listening on http://localhost:${httpPort}`);
    });
    if (process.env.HTTPS !== 'false') {
        const httpsPort = 50006;
        const https = require('https');
        const credentials = exports.getHttpsCredentials();
        if (credentials) {
            const httpsServer = https.createServer(credentials, app);
            httpsServer.listen(httpsPort, () => {
                console.log('\nUse HTTPS server only for local purposes!');
                console.log(`Node Express server listening on https://localhost:${httpsPort}`);
            });
        }
    }
};
exports.getHttpsCredentials = () => {
    const fs = require('fs');
    try {
        const key = fs.readFileSync('server/sslcert/localhost.key', 'utf8');
        const cert = fs.readFileSync('server/sslcert/localhost.crt', 'utf8');
        return { key, cert };
    }
    catch (error) {
        console.log(`Problem with credentials: \n ${error}`);
        return undefined;
    }
};
exports.configureExpress = () => {
    const server = express();
    const distFolder = path_1.join(process.cwd() + '/dist/VirginAdmin');
    const indexHtml = path_1.join('index.html');
    server.use(express.static(distFolder));
    server.get('/*', (_request, response) => {
        response.sendFile(indexHtml, { root: distFolder });
    });
    return server;
};
try {
    exports.serverRun();
}
catch (error) {
    console.log(error);
}
