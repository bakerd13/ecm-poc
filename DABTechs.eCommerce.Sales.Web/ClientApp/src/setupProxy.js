const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function(app) {
    app.use(
        "/vipsearch/search",
        createProxyMiddleware({target: 'http://localhost:5000', changeOrigin: true,
        })
    );
    app.use(
        "/vipmenu/meganav",
        createProxyMiddleware({target: 'http://localhost:5000', changeOrigin: true,
        })
    );
};