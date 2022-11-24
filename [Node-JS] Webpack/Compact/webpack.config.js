const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

const PATHS = {
    root: "./",
    dist: path.resolve(__dirname, 'dist'),
    src: path.resolve(__dirname, 'src'),
    output: path.resolve(__dirname, 'dist/bundle')
}


const config = {
    entry: {
        app: `./src/js/site.js`,
    },
    output: {
        path: `${PATHS.output}`,
        filename: `bundle.min.js`
    },
    mode: 'production',
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

                    plugins: []
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
        new MiniCssExtractPlugin({
            filename: `bundle.min.css`,
        }),

    ],
}

module.exports = config;