const fs = require('fs');

// import * as Prod from './webpack.config.prod.js';
// import * as Dev from './webpack.config.dev.js';

module.exports = (env, argv) => {
    //process.exit(1)
    let mode = argv.mode || 'production';
    let configFile = `./webpack.config.${mode}.js`;

    if (!fs.existsSync(configFile)) {
        console.error(`Config file ${configFile} does not exist`)
        console.log("exiting...")
        console.log("...")
        process.exit(1);
    }

    return require(configFile);
}