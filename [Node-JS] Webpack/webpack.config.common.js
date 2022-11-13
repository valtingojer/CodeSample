const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

// const CoreJsPlugin = require('core-js-webpack-plugin');

//maybe install
//npm install @babel/runtime
//npm install @babel/plugin-transform-runtime

// "dependencies": {
// "@babel/plugin-transform-runtime": "^7.19.6",
// "@babel/runtime": "^7.20.1",
// "core-js-webpack-plugin": "^1.0.3"

//for cleaning up the css, we can use
//npm install --save-dev purgecss-webpack-plugin glob-all
//and
//const { PurgeCSS } = "purgecss-webpack-plugin";
//const glob = require('glob-all');
//at plugins
//new PurgeCSS({
//    paths: glob.sync([
//        path.join(__dirname, './src/*.html'),
//        path.join(__dirname, './src/*.js'),
//    ], { nodir: true })



const PATHS = {
    root: "./",
    dist: path.resolve(__dirname, 'dist'),
    src: path.resolve(__dirname, 'src'),
    output: path.resolve(__dirname, 'dist/output'),
    static: path.resolve(__dirname, 'dist/static'),
    port: 9000, //Math.floor(Math.random() * (9090 - 8080 + 1) + 8080),
}


const config = {
    // entry: './src/js/app.js',
    entry: {
        sample: './src/js/sample.js',
        app: './src/js/app.js',
    },
    output: {
        path: PATHS.output,
        filename: '[name].[contenthash].bundle.js'
    },
    mode: 'production',
    optimization: {
        splitChunks: {
            chunks: 'all',
            minSize: 1, //min size in bytes
        },
    },
    devServer: {
        //allowedHosts?, bonjour?, client?, compress?, devMiddleware?, headers?, historyApiFallback?, host?, hot?, http2?, https?, ipc?, liveReload?, magicHtml?, onAfterSetupMiddleware?, onBeforeSetupMiddleware?, onListening?, open?, port?, proxy?, server?, setupExitSignals?, setupMiddlewares?, static?, watchFiles?, webSocketServer? 
        static: [
            {
                directory: PATHS.output,
                publicPath: '/',
            },
            {
                directory: PATHS.static,
                publicPath: '/',
            },
        ],

        compress: true,
        port: PATHS.port,
        hot: true,
        liveReload: true,
        open: true,
        magicHtml: true,
        watchFiles: {
            paths: [
                `${PATHS.src}/**/*.js`,
            ],
        },

    },
    module: {
        rules: [
            {
                test: /\.js?$/,
                exclude: /node_modules/,
                loader: 'babel-loader',
                options: {
                    presets: [
                        [
                            "@babel/preset-env",
                            {
                                useBuiltIns: "usage",
                                "corejs": "3",
                            },
                        ]
                    ],

                    plugins: [
                        // [
                        //     '@babel/plugin-transform-runtime',
                        //     {
                        //         regenerator: true,
                        //         corejs: { version: 3, proposals: true }
                        //     }
                        // ]
                    ]
                }
            },
            {
                test: /\.s[ac]ss$/i,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                    },
                    // "style-loader",
                    {
                        loader: "css-loader",
                        options: { importLoaders: 2, sourceMap: true },
                    },
                    {
                        loader: "postcss-loader",
                        options: {
                            postcssOptions: {
                                plugins: [
                                    [
                                        "postcss-preset-env",
                                        {
                                            stage: 0,
                                            browsers: "cover 99.5%",
                                        },
                                    ],
                                ],
                            },
                        },
                    },
                    "sass-loader",
                ],
            },
        ]
    },
    plugins: [
        new CleanWebpackPlugin(),
        new HtmlWebpackPlugin({
            template: './src/index.html',
        }),
        new MiniCssExtractPlugin({
            filename: '[name].[contenthash].bundle.css',
        }),

    ],
}

module.exports = config;