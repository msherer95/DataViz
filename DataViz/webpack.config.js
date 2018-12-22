
const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const { CheckerPlugin } = require('awesome-typescript-loader');

module.exports = {
    mode: 'development',
    entry: {
        main: './wwwroot/src/index.tsx'
    },
    devtool: 'inline-source-map',
    output: {
        path: path.resolve(__dirname, './wwwroot/dist'),
        filename: 'bundle.js',
        publicPath: 'dist/'
    },
    devServer: {
        hot: true, // enable HMR on the server
    },
    plugins: [
        new CleanWebpackPlugin(['./wwwroot/dist']),
        new webpack.HotModuleReplacementPlugin(),
        new CheckerPlugin()
    ],
    externals: {
        'react': 'React',
        'react-dom': 'ReactDOM'
    },
    resolve: {
        extensions: ['.ts', '.tsx', '.js', '.jsx', '.json']
    },
    module: {
        rules: [
			{
                test: /\.css$/,
                use: [
                    'style-loader',
                    'css-loader'
                ]
            },
            {
                test: /\.scss$/,
                use: [
                    'style-loader',
					'css-loader',
					'postcss-loader',
                    'sass-loader'
                ]
            },
            {
                test: /\.(png|svg|jpg|gif)$/,
                use: [
                    'file-loader'
                ]
            },
            {
                test: /\.(js|ts|jsx|tsx)$/,
                exclude: /(node_modules|bower_components)/,
                use: ['babel-loader', 'awesome-typescript-loader'],
            },
            { enforce: 'pre', test: /\.js$/, loader: 'source-map-loader' }
        ]
    },
};
