const common = require('./webpack.config.common.js');

const config = {
    ...common,
    mode: 'production',
}


module.exports = config;